// ═══════════════════════════════════════
// FILE: UC_users.cs
// Users are now loaded from the database via DatabaseManager.
// No hardcoded user data anywhere.
// ═══════════════════════════════════════
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class UC_Users : UserControl
    {
        private string _filterRole = "All";
        private string _filterStatus = "All";
        private bool _showingArchive = false;

        // ── In-memory cache loaded from DB ───────────────────────────────────
        private List<UserModel> _activeUsers = new();
        private List<UserModel> _archivedUsers = new();

        public UC_Users()
        {
            InitializeComponent();
        }

        // ════════════════════════════════════════════════════════════════════
        //  LOAD — fires once when the control appears on screen
        // ════════════════════════════════════════════════════════════════════
        private void UC_Users_Load(object sender, EventArgs e)
        {
            RefreshFromDatabase();
        }

        // ════════════════════════════════════════════════════════════════════
        //  REFRESH FROM DATABASE
        // ════════════════════════════════════════════════════════════════════
        private void RefreshFromDatabase()
        {
            try
            {
                var all = DatabaseManager.GetAllUsers();

                _activeUsers.Clear();
                _archivedUsers.Clear();

                foreach (var u in all)
                {
                    // Treat Inactive/Suspended as archived for display purposes;
                    // adjust this logic if you add a real is_archived column later.
                    if (u.Status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                        _activeUsers.Add(u);
                    else
                        _archivedUsers.Add(u);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load users from database:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateSummaryCards();
            PopulateUserCards();
        }

        // ════════════════════════════════════════════════════════════════════
        //  FORMAT last_login for display
        // ════════════════════════════════════════════════════════════════════
        private static string FormatLastLogin(DateTime? dt)
        {
            if (dt == null) return "Never";
            var diff = DateTime.Now - dt.Value;
            if (diff.TotalMinutes < 2) return "Just now";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} min ago";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} hr{((int)diff.TotalHours == 1 ? "" : "s")} ago";
            if (diff.TotalDays < 7) return $"{(int)diff.TotalDays} day{((int)diff.TotalDays == 1 ? "" : "s")} ago";
            return dt.Value.ToString("MMM dd, yyyy");
        }

        // ════════════════════════════════════════════════════════════════════
        //  POPULATE CARDS
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
                        || u.FullName.ToLower().Contains(search)
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

                card.SetUserData(
                    u.Id.ToString(),
                    u.FullName,
                    u.Username,
                    u.Role,
                    u.Status,
                    FormatLastLogin(u.LastLogin));

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
        //  EDIT — opens FormEditUser, saves to DB on confirm
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
                if (!int.TryParse(card.UserID, out int uid)) return;

                var updated = new UserModel
                {
                    Id = uid,
                    FullName = dlg.EditedFullName,
                    Username = dlg.EditedUsername.TrimStart('@'),
                    Role = dlg.EditedRole,
                    Status = dlg.EditedStatus
                };

                bool ok = DatabaseManager.UpdateUser(
                    updated,
                    string.IsNullOrWhiteSpace(dlg.EditedPassword) ? null : dlg.EditedPassword);

                if (!ok)
                    MessageBox.Show("Failed to update user in database.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                RefreshFromDatabase();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  ADD USER — saves to DB
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
                // Check for duplicate username
                if (DatabaseManager.UserExists(dlg.NewUsername.TrimStart('@')))
                {
                    MessageBox.Show("That username already exists. Please choose a different one.",
                        "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newUser = new UserModel
                {
                    FullName = dlg.NewFullName,
                    Username = dlg.NewUsername.TrimStart('@'),
                    Role = dlg.NewRole,
                    Status = dlg.NewStatus
                };

                bool ok = DatabaseManager.AddUser(newUser, dlg.NewPassword);
                if (!ok)
                    MessageBox.Show("Failed to add user to database.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                RefreshFromDatabase();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  ARCHIVE — sets status to Inactive in DB
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
                if (!int.TryParse(card.UserID, out int uid)) return;

                var user = _activeUsers.Find(u => u.Id == uid);
                if (user != null)
                {
                    var updated = new UserModel
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Username = user.Username,
                        Role = user.Role,
                        Status = "Inactive"   // Archive = set Inactive
                    };
                    DatabaseManager.UpdateUser(updated, null);
                }

                RefreshFromDatabase();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  PERMANENT DELETE — removes from DB entirely
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
                if (int.TryParse(card.UserID, out int uid))
                    DatabaseManager.DeleteUser(uid);

                RefreshFromDatabase();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  RESTORE — sets status back to Active in DB
        // ════════════════════════════════════════════════════════════════════
        private void HandleRestore(UC_UserCard? card)
        {
            if (card == null) return;
            if (!int.TryParse(card.UserID, out int uid)) return;

            if (MessageBox.Show($"Restore {card.FullName}?", "Restore User",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var user = _archivedUsers.Find(u => u.Id == uid);
                if (user != null)
                {
                    var updated = new UserModel
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Username = user.Username,
                        Role = user.Role,
                        Status = "Active"
                    };
                    DatabaseManager.UpdateUser(updated, null);
                }

                RefreshFromDatabase();
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
                if (u.Role is "SuperAdmin" or "Admin") admin++;
                if (u.Role is "Assessor" or "Inventory" or "POSCashier") staff++;
                if (u.Role == "POSCashier") cashier++;
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

            // Role names match EXACTLY what's stored in the DB ENUM
            foreach (var role in new[] { "All", "SuperAdmin", "Admin", "Assessor", "POSCashier", "Inventory" })
            {
                string r = role; bool marked = (r == _filterRole);
                string label = r == "All" ? "  All Roles" : "  " + r;
                var item = new ToolStripMenuItem(label)
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

            foreach (var status in new[] { "All", "Active", "Inactive", "Suspended" })
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
            clear.Click += (_, __) =>
            {
                _filterRole = "All";
                _filterStatus = "All";
                txt_Search.Text = "";
                PopulateUserCards();
            };
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
    }
}