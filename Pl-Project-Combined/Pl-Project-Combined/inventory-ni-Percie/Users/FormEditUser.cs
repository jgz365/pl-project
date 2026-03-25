// ═══════════════════════════════════════
// FILE: FormEditUser.cs
// Fix: cmbRole shows display names ("Super Admin") but saves DB values ("SuperAdmin")
//      SetUserData maps DB value → display label so the dropdown pre-selects correctly
// ═══════════════════════════════════════
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    public partial class FormEditUser : Form
    {
        // ── Display label ↔ DB ENUM value ────────────────────────────────────
        private static readonly Dictionary<string, string> RoleToDb = new()
        {
            { "Super Admin",  "SuperAdmin"  },
            { "Admin",        "Admin"       },
            { "Assessor",     "Assessor"    },
            { "POS Cashier",  "POSCashier"  },
            { "Inventory",    "Inventory"   },
        };

        private static readonly Dictionary<string, string> DbToRole = new()
        {
            { "SuperAdmin",  "Super Admin"  },
            { "Admin",       "Admin"        },
            { "Assessor",    "Assessor"     },
            { "POSCashier",  "POS Cashier"  },
            { "Inventory",   "Inventory"    },
        };

        // ── Heights ───────────────────────────────────────────────────────────
        private const int COLLAPSED_HEIGHT = 505;
        private const int EXPANDED_HEIGHT = 705;
        private const int BUTTONS_Y_COLLAPSED = 430;
        private const int BUTTONS_Y_EXPANDED = 630;

        public bool IsUpdated { get; private set; }
        public string EditedFullName { get; private set; } = "";
        public string EditedUsername { get; private set; } = "";
        public string EditedRole { get; private set; } = "";   // DB value
        public string EditedStatus { get; private set; } = "";
        public string EditedPassword { get; private set; } = "";

        private Point _dragOffset;
        private bool _isDragging;

        public FormEditUser()
        {
            InitializeComponent();
            pnlPasswordSection.Visible = false;
            pnlButtons.Location = new Point(0, BUTTONS_Y_COLLAPSED);
            this.ClientSize = new Size(460, COLLAPSED_HEIGHT);
        }

        // ════════════════════════════════════════════════════════════════════
        //  SET DATA — called by UC_Users before ShowDialog
        //  role parameter is the DB value (e.g. "POSCashier")
        // ════════════════════════════════════════════════════════════════════
        public void SetUserData(string id, string fullName, string username,
                                string role, string status)
        {
            txtFullName.Text = fullName;
            txtUsername.Text = username.TrimStart('@');

            // Convert DB value → display label for the combo
            string displayRole = DbToRole.TryGetValue(role, out string? dr) ? dr : role;
            cmbRole.SelectedItem = displayRole;
            if (cmbRole.SelectedIndex < 0) cmbRole.SelectedIndex = 0;

            cmbStatus.SelectedItem = status;
            if (cmbStatus.SelectedIndex < 0) cmbStatus.SelectedIndex = 0;
        }

        // ════════════════════════════════════════════════════════════════════
        //  CHANGE PASSWORD TOGGLE
        // ════════════════════════════════════════════════════════════════════
        private void chkChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            bool expanding = chkChangePassword.Checked;
            pnlPasswordSection.Visible = expanding;
            pnlButtons.Location = new Point(0, expanding ? BUTTONS_Y_EXPANDED : BUTTONS_Y_COLLAPSED);
            this.ClientSize = new Size(460, expanding ? EXPANDED_HEIGHT : COLLAPSED_HEIGHT);

            int x = (Screen.PrimaryScreen!.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen!.WorkingArea.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        // ════════════════════════════════════════════════════════════════════
        //  SHOW PASSWORD TOGGLE
        // ════════════════════════════════════════════════════════════════════
        private void togShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            char pc = togShowPassword.Checked ? '\0' : '*';
            txtPassword.PasswordChar = pc;
            txtConfirmPassword.PasswordChar = pc;
        }

        // ════════════════════════════════════════════════════════════════════
        //  USERNAME INDICATOR
        // ════════════════════════════════════════════════════════════════════
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblUsernameCheck.Visible = !string.IsNullOrWhiteSpace(txtUsername.Text);
        }

        // ════════════════════════════════════════════════════════════════════
        //  VALIDATE
        // ════════════════════════════════════════════════════════════════════
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbRole.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a role.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (chkChangePassword.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        // ════════════════════════════════════════════════════════════════════
        //  BUTTONS
        // ════════════════════════════════════════════════════════════════════
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            string displayRole = cmbRole.SelectedItem?.ToString() ?? "";

            EditedFullName = txtFullName.Text.Trim();
            EditedUsername = txtUsername.Text.Trim().TrimStart('@');
            // Convert display label → DB ENUM value
            EditedRole = RoleToDb.TryGetValue(displayRole, out string? dbRole) ? dbRole : displayRole;
            EditedStatus = cmbStatus.SelectedItem?.ToString() ?? "";
            EditedPassword = chkChangePassword.Checked ? txtPassword.Text : "";
            IsUpdated = true;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsUpdated = false;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsUpdated = false;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ════════════════════════════════════════════════════════════════════
        //  DRAG
        // ════════════════════════════════════════════════════════════════════
        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            _dragOffset = new Point(e.X, e.Y);
        }

        private void pnlTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
                this.Location = new Point(
                    this.Left + e.X - _dragOffset.X,
                    this.Top + e.Y - _dragOffset.Y);
        }

        private void pnlTitleBar_MouseUp(object sender, MouseEventArgs e)
            => _isDragging = false;
    }
}