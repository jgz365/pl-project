using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    public partial class UC_Users : UserControl
    {
        private string _filterRole = "All";
        private string _filterStatus = "All";
        private bool _showingArchive = false;

        private class UserRecord
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public string Username { get; set; } = "";
            public string Role { get; set; } = "";
            public string Status { get; set; } = "";
            public string Login { get; set; } = "";
        }

        private List<UserRecord> _activeUsers = new List<UserRecord>
        {
            new UserRecord { Id="001", Name="System Admin",  Username="@admin",   Role="Super Admin", Status="Active",   Login="Just now"   },
            new UserRecord { Id="002", Name="Maria Santos",  Username="@maria.s", Role="Assessor",    Status="Active",   Login="2 hrs ago"  },
            new UserRecord { Id="003", Name="Juan Cruz",     Username="@juan.c",  Role="POS Cashier", Status="Active",   Login="1 hr ago"   },
            new UserRecord { Id="004", Name="Pedro Mendoza", Username="@pedro.m", Role="Inventory",   Status="Inactive", Login="5 days ago" },
        };

        private List<UserRecord> _archivedUsers = new List<UserRecord>();

        public UC_Users()
        {
            InitializeComponent();
            UpdateSummaryCards();
            PopulateUserCards();
        }

        // ════════════════════════════════════════════════════════════════════
        //  POPULATE
        // ════════════════════════════════════════════════════════════════════
        private void PopulateUserCards()
        {
            flpUserList.SuspendLayout();
            flpUserList.Controls.Clear();

            var source = _showingArchive ? _archivedUsers : _activeUsers;
            string search = txt_Search?.Text.Trim().ToLower() ?? "";
            int shown = 0;

            foreach (var u in source)
            {
                if (!_showingArchive)
                {
                    bool roleOk = _filterRole == "All" || u.Role == _filterRole;
                    bool statusOk = _filterStatus == "All" || u.Status == _filterStatus;
                    bool searchOk = string.IsNullOrEmpty(search)
                        || u.Name.ToLower().Contains(search)
                        || u.Username.ToLower().Contains(search)
                        || u.Role.ToLower().Contains(search);
                    if (!roleOk || !statusOk || !searchOk) continue;
                }

                var card = new UC_UserCard
                {
                    Width = flpUserList.ClientSize.Width - 4,
                    Height = 83,
                    Margin = new Padding(0)
                };
                card.SetUserData(u.Id, u.Name, u.Username, u.Role, u.Status, u.Login);

                if (_showingArchive)
                {
                    card.SetArchiveMode(true);
                    card.OnEditClick += (s, _) => HandleRestore(s as UC_UserCard);
                    card.OnArchiveClick += (s, _) => HandlePermanentDelete(s as UC_UserCard);
                }
                else
                {
                    card.OnEditClick += (s, _) => HandleEdit(s as UC_UserCard);
                    card.OnArchiveClick += (s, _) => HandleArchiveClick(s as UC_UserCard);
                }

                flpUserList.Controls.Add(card);
                shown++;
            }

            lblNoResults.Text = _showingArchive ? "No archived users found." : "No users found.";
            lblNoResults.Visible = (shown == 0);
            flpUserList.ResumeLayout(true);
        }

        // ════════════════════════════════════════════════════════════════════
        //  EDIT — opens FormEditUser with blur overlay
        // ════════════════════════════════════════════════════════════════════
        private void HandleEdit(UC_UserCard? card)
        {
            if (card == null) return;

            Form? mainForm = this.FindForm();

            var overlay = new FormBlurOverlay(mainForm ?? new Form());
            overlay.Show(mainForm);

            using var dlg = new FormEditUser();
            dlg.SetUserData(card.UserID, card.FullName, card.Username, card.Role, card.UserStatus);
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowDialog(mainForm);

            overlay.Close();
            overlay.Dispose();

            if (dlg.IsUpdated)
            {
                // Update the record in the active list
                var record = _activeUsers.Find(u => u.Id == card.UserID);
                if (record != null)
                {
                    record.Name = dlg.EditedFullName;
                    record.Username = dlg.EditedUsername;
                    record.Role = dlg.EditedRole;
                    record.Status = dlg.EditedStatus;
                }
                UpdateSummaryCards();
                PopulateUserCards();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  ADD USER — opens FormAddUser with blur overlay
        // ════════════════════════════════════════════════════════════════════
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Form? mainForm = this.FindForm();

            var overlay = new FormBlurOverlay(mainForm ?? new Form());
            overlay.Show(mainForm);

            using var dlg = new FormAddUser();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowDialog(mainForm);

            overlay.Close();
            overlay.Dispose();

            if (dlg.IsAdded)
            {
                // Generate a new ID
                string newId = (_activeUsers.Count + _archivedUsers.Count + 1).ToString("D3");

                _activeUsers.Add(new UserRecord
                {
                    Id = newId,
                    Name = dlg.NewFullName,
                    Username = "@" + dlg.NewUsername.TrimStart('@'),
                    Role = dlg.NewRole,
                    Status = dlg.NewStatus,
                    Login = "Just now"
                });

                UpdateSummaryCards();
                PopulateUserCards();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  ARCHIVE — overlay + amber modal → move to archived list
        // ════════════════════════════════════════════════════════════════════
        private void HandleArchiveClick(UC_UserCard? card)
        {
            if (card == null) return;
            Form? mainForm = this.FindForm();

            var overlay = new FormBlurOverlay(mainForm ?? new Form());
            overlay.Show(mainForm);

            using var modal = new UC_ConfirmArchive(card.Username.TrimStart('@'));
            modal.StartPosition = FormStartPosition.CenterParent;
            modal.ShowDialog(mainForm);

            overlay.Close();
            overlay.Dispose();

            if (modal.IsConfirmed)
            {
                var record = _activeUsers.Find(u => u.Id == card.UserID);
                if (record != null)
                {
                    _activeUsers.Remove(record);
                    _archivedUsers.Add(record);
                    UpdateSummaryCards();
                    PopulateUserCards();
                }
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  PERMANENT DELETE — overlay + red modal → remove forever
        // ════════════════════════════════════════════════════════════════════
        private void HandlePermanentDelete(UC_UserCard? card)
        {
            if (card == null) return;
            Form? mainForm = this.FindForm();

            var overlay = new FormBlurOverlay(mainForm ?? new Form());
            overlay.Show(mainForm);

            using var modal = new UC_ConfirmDelete(card.Username.TrimStart('@'));
            modal.StartPosition = FormStartPosition.CenterParent;
            modal.ShowDialog(mainForm);

            overlay.Close();
            overlay.Dispose();

            if (modal.IsConfirmed)
            {
                var record = _archivedUsers.Find(u => u.Id == card.UserID);
                if (record != null)
                {
                    _archivedUsers.Remove(record);
                    PopulateUserCards();
                }
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  RESTORE
        // ════════════════════════════════════════════════════════════════════
        private void HandleRestore(UC_UserCard? card)
        {
            if (card == null) return;
            var record = _archivedUsers.Find(u => u.Id == card.UserID);
            if (record == null) return;

            if (MessageBox.Show($"Restore {record.Name}?", "Restore User",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _archivedUsers.Remove(record);
                _activeUsers.Add(record);
                UpdateSummaryCards();
                PopulateUserCards();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  SUMMARY CARDS
        // ════════════════════════════════════════════════════════════════════
        private void UpdateSummaryCards()
        {
            int total = _activeUsers.Count, admin = 0, staff = 0, cashier = 0;
            foreach (var u in _activeUsers)
            {
                if (u.Role is "Super Admin" or "Admin") admin++;
                if (u.Role is "Assessor" or "Inventory" or "POS Cashier") staff++;
                if (u.Role == "POS Cashier") cashier++;
            }
            lblTotalUsersVal.Text = total.ToString();
            lblAdminVal.Text = admin.ToString();
            lblStaffVal.Text = staff.ToString();
            lblCashierVal.Text = cashier.ToString();
        }

        // ════════════════════════════════════════════════════════════════════
        //  ARCHIVE LIST TOGGLE
        // ════════════════════════════════════════════════════════════════════
        private void btnArchiveList_Click(object sender, EventArgs e)
        {
            _showingArchive = !_showingArchive;

            if (_showingArchive)
            {
                btnArchiveList.Text = "← Active Users";
                btnArchiveList.FillColor = Color.FromArgb(217, 119, 6);
                btnArchiveList.ForeColor = Color.White;
                lblSystemUsers.Text = "Archived Users";
            }
            else
            {
                btnArchiveList.Text = "Archive List";
                btnArchiveList.FillColor = Color.White;
                btnArchiveList.ForeColor = Color.FromArgb(71, 85, 105);
                lblSystemUsers.Text = "System Users";
            }

            PopulateUserCards();
        }

        // ════════════════════════════════════════════════════════════════════
        //  LIVE SEARCH
        // ════════════════════════════════════════════════════════════════════
        private void txt_Search_TextChanged(object sender, EventArgs e)
            => PopulateUserCards();

        // ════════════════════════════════════════════════════════════════════
        //  FILTER DROPDOWN
        // ════════════════════════════════════════════════════════════════════
        private void btnFilter_Click(object sender, EventArgs e)
        {
            var cms = new ContextMenuStrip
            {
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Renderer = new ToolStripProfessionalRenderer(new CleanMenuColors())
            };

            cms.Items.Add(new ToolStripLabel("  BY ROLE")
            { Font = new Font("Segoe UI", 7.5f, FontStyle.Bold), ForeColor = Color.FromArgb(148, 163, 184) });
            foreach (var role in new[] { "All", "Super Admin", "Assessor", "POS Cashier", "Inventory" })
            {
                string r = role; bool marked = (r == _filterRole);
                var item = new ToolStripMenuItem(r == "All" ? "  All Roles" : "  " + r)
                {
                    Font = marked ? new Font("Segoe UI", 9f, FontStyle.Bold) : new Font("Segoe UI", 9f),
                    BackColor = marked ? Color.FromArgb(239, 246, 255) : Color.White
                };
                item.Click += (_, __) => { _filterRole = r; PopulateUserCards(); };
                cms.Items.Add(item);
            }

            cms.Items.Add(new ToolStripSeparator());

            cms.Items.Add(new ToolStripLabel("  BY STATUS")
            { Font = new Font("Segoe UI", 7.5f, FontStyle.Bold), ForeColor = Color.FromArgb(148, 163, 184) });
            foreach (var status in new[] { "All", "Active", "Inactive" })
            {
                string st = status; bool marked = (st == _filterStatus);
                var item = new ToolStripMenuItem(st == "All" ? "  All Statuses" : "  " + st)
                {
                    Font = marked ? new Font("Segoe UI", 9f, FontStyle.Bold) : new Font("Segoe UI", 9f),
                    BackColor = marked ? Color.FromArgb(239, 246, 255) : Color.White
                };
                item.Click += (_, __) => { _filterStatus = st; PopulateUserCards(); };
                cms.Items.Add(item);
            }

            cms.Items.Add(new ToolStripSeparator());
            var clear = new ToolStripMenuItem("  Clear All Filters")
            { ForeColor = Color.FromArgb(220, 38, 38), Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            clear.Click += (_, __) => { _filterRole = "All"; _filterStatus = "All"; txt_Search.Text = ""; PopulateUserCards(); };
            cms.Items.Add(clear);
            cms.Show(btnFilter, new Point(0, btnFilter.Height + 4));
        }

        // ════════════════════════════════════════════════════════════════════
        //  RESIZE
        // ════════════════════════════════════════════════════════════════════
        private void flpUserList_Resize(object sender, EventArgs e)
        {
            foreach (Control c in flpUserList.Controls)
                if (c is UC_UserCard uc)
                    uc.Width = flpUserList.ClientSize.Width - 4;
        }

        private void UC_Users_Load(object sender, EventArgs e) { }
    }
}