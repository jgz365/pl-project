// ═══════════════════════════════════════
// FILE: FormAddUser.cs
// Fix: cmbRole shows display names ("Super Admin") but saves DB values ("SuperAdmin")
// ═══════════════════════════════════════
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    public partial class FormAddUser : Form
    {
        // ── Maps display label → DB ENUM value ───────────────────────────────
        private static readonly Dictionary<string, string> RoleMap = new()
        {
            { "Super Admin",  "SuperAdmin"  },
            { "Admin",        "Admin"       },
            { "Assessor",     "Assessor"    },
            { "POS Cashier",  "POSCashier"  },
            { "Inventory",    "Inventory"   },
        };

        // ── Output properties ─────────────────────────────────────────────────
        public bool IsAdded { get; private set; }
        public string NewFullName { get; private set; } = "";
        public string NewUsername { get; private set; } = "";
        public string NewRole { get; private set; } = "";   // DB value
        public string NewStatus { get; private set; } = "";
        public string NewPassword { get; private set; } = "";

        // ── Drag support ──────────────────────────────────────────────────────
        private Point _dragOffset;
        private bool _isDragging;

        public FormAddUser()
        {
            InitializeComponent();
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
        private bool Validate_Fields()
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
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ════════════════════════════════════════════════════════════════════
        //  BUTTONS
        // ════════════════════════════════════════════════════════════════════
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Validate_Fields()) return;

            string displayRole = cmbRole.SelectedItem?.ToString() ?? "";

            NewFullName = txtFullName.Text.Trim();
            NewUsername = txtUsername.Text.Trim().TrimStart('@');
            // Convert display label → DB ENUM value
            NewRole = RoleMap.TryGetValue(displayRole, out string? dbRole) ? dbRole : displayRole;
            NewStatus = cmbStatus.SelectedItem?.ToString() ?? "Active";
            NewPassword = txtPassword.Text;
            IsAdded = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsAdded = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsAdded = false;
            this.DialogResult = DialogResult.Cancel;
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