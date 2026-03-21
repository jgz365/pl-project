using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    partial class FormAddUser
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlCard = new Guna2Panel();
            pnlTitleBar = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            btnClose = new Guna2Button();
            pnlDivider = new System.Windows.Forms.Panel();
            lblFullName = new System.Windows.Forms.Label();
            txtFullName = new Guna2TextBox();
            lblUsername = new System.Windows.Forms.Label();
            pnlUsernameRow = new System.Windows.Forms.Panel();
            txtUsername = new Guna2TextBox();
            lblUsernameCheck = new System.Windows.Forms.Label();
            pnlRoleStatus = new System.Windows.Forms.Panel();
            lblRole = new System.Windows.Forms.Label();
            cmbRole = new Guna2ComboBox();
            lblStatus = new System.Windows.Forms.Label();
            cmbStatus = new Guna2ComboBox();
            lblPassword = new System.Windows.Forms.Label();
            txtPassword = new Guna2TextBox();
            lblConfirmPassword = new System.Windows.Forms.Label();
            txtConfirmPassword = new Guna2TextBox();
            pnlShowPassword = new System.Windows.Forms.Panel();
            togShowPassword = new Guna2ToggleSwitch();
            lblShowPassword = new System.Windows.Forms.Label();
            pnlButtons = new System.Windows.Forms.Panel();
            btnCancel = new Guna2Button();
            btnAdd = new Guna2Button();

            pnlCard.SuspendLayout();
            pnlTitleBar.SuspendLayout();
            pnlUsernameRow.SuspendLayout();
            pnlRoleStatus.SuspendLayout();
            pnlShowPassword.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            ClientSize = new System.Drawing.Size(460, 660);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FormAddUser";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add New User";
            Controls.Add(pnlCard);

            // ── pnlCard ───────────────────────────────────────────────────────
            pnlCard.BackColor = System.Drawing.Color.Transparent;
            pnlCard.FillColor = System.Drawing.Color.White;
            pnlCard.BorderRadius = 20;
            pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Depth = 18;
            pnlCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(50, 0, 0, 0);
            pnlCard.Controls.Add(pnlTitleBar);
            pnlCard.Controls.Add(pnlDivider);
            pnlCard.Controls.Add(lblFullName);
            pnlCard.Controls.Add(txtFullName);
            pnlCard.Controls.Add(lblUsername);
            pnlCard.Controls.Add(pnlUsernameRow);
            pnlCard.Controls.Add(pnlRoleStatus);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(lblConfirmPassword);
            pnlCard.Controls.Add(txtConfirmPassword);
            pnlCard.Controls.Add(pnlShowPassword);
            pnlCard.Controls.Add(pnlButtons);

            // ── Title bar ─────────────────────────────────────────────────────
            pnlTitleBar.BackColor = System.Drawing.Color.Transparent;
            pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            pnlTitleBar.Name = "pnlTitleBar";
            pnlTitleBar.Size = new System.Drawing.Size(460, 60);
            pnlTitleBar.Controls.Add(lblTitle);
            pnlTitleBar.Controls.Add(btnClose);
            pnlTitleBar.MouseDown += pnlTitleBar_MouseDown;
            pnlTitleBar.MouseMove += pnlTitleBar_MouseMove;
            pnlTitleBar.MouseUp += pnlTitleBar_MouseUp;

            lblTitle.BackColor = System.Drawing.Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTitle.Location = new System.Drawing.Point(28, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.AutoSize = true;
            lblTitle.Text = "Add New User";

            btnClose.BorderRadius = 50;
            btnClose.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnClose.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnClose.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnClose.Location = new System.Drawing.Point(408, 14);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(34, 34);
            btnClose.Text = "✕";
            btnClose.Click += btnClose_Click;

            pnlDivider.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            pnlDivider.Location = new System.Drawing.Point(28, 60);
            pnlDivider.Name = "pnlDivider";
            pnlDivider.Size = new System.Drawing.Size(404, 1);

            // ── Full Name ─────────────────────────────────────────────────────
            lblFullName.BackColor = System.Drawing.Color.Transparent;
            lblFullName.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblFullName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblFullName.Location = new System.Drawing.Point(28, 76);
            lblFullName.Name = "lblFullName";
            lblFullName.AutoSize = true;
            lblFullName.Text = "Full Name";

            txtFullName.BorderRadius = 8;
            txtFullName.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtFullName.BorderThickness = 1;
            txtFullName.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtFullName.Font = new System.Drawing.Font("Segoe UI", 9f);
            txtFullName.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            txtFullName.PlaceholderText = "Full Name";
            txtFullName.Location = new System.Drawing.Point(28, 98);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new System.Drawing.Size(404, 40);
            txtFullName.TabIndex = 0;

            // ── Username ──────────────────────────────────────────────────────
            lblUsername.BackColor = System.Drawing.Color.Transparent;
            lblUsername.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblUsername.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblUsername.Location = new System.Drawing.Point(28, 152);
            lblUsername.Name = "lblUsername";
            lblUsername.AutoSize = true;
            lblUsername.Text = "Username";

            pnlUsernameRow.BackColor = System.Drawing.Color.Transparent;
            pnlUsernameRow.Location = new System.Drawing.Point(28, 174);
            pnlUsernameRow.Name = "pnlUsernameRow";
            pnlUsernameRow.Size = new System.Drawing.Size(404, 40);
            pnlUsernameRow.Controls.Add(txtUsername);
            pnlUsernameRow.Controls.Add(lblUsernameCheck);

            txtUsername.BorderRadius = 8;
            txtUsername.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtUsername.BorderThickness = 1;
            txtUsername.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtUsername.Font = new System.Drawing.Font("Segoe UI", 9f);
            txtUsername.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            txtUsername.PlaceholderText = "Username";
            txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            txtUsername.Name = "txtUsername";
            txtUsername.TabIndex = 1;
            txtUsername.TextChanged += txtUsername_TextChanged;

            lblUsernameCheck.BackColor = System.Drawing.Color.Transparent;
            lblUsernameCheck.Font = new System.Drawing.Font("Segoe UI", 11f);
            lblUsernameCheck.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            lblUsernameCheck.Location = new System.Drawing.Point(372, 8);
            lblUsernameCheck.Name = "lblUsernameCheck";
            lblUsernameCheck.Size = new System.Drawing.Size(26, 24);
            lblUsernameCheck.Text = "✔";
            lblUsernameCheck.Visible = false;

            // ── Role + Status ─────────────────────────────────────────────────
            pnlRoleStatus.BackColor = System.Drawing.Color.Transparent;
            pnlRoleStatus.Location = new System.Drawing.Point(28, 228);
            pnlRoleStatus.Name = "pnlRoleStatus";
            pnlRoleStatus.Size = new System.Drawing.Size(404, 80);
            pnlRoleStatus.Controls.Add(lblRole);
            pnlRoleStatus.Controls.Add(cmbRole);
            pnlRoleStatus.Controls.Add(lblStatus);
            pnlRoleStatus.Controls.Add(cmbStatus);

            lblRole.BackColor = System.Drawing.Color.Transparent;
            lblRole.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblRole.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblRole.Location = new System.Drawing.Point(0, 0);
            lblRole.Name = "lblRole";
            lblRole.AutoSize = true;
            lblRole.Text = "Role";

            cmbRole.BorderRadius = 8;
            cmbRole.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            cmbRole.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            cmbRole.Font = new System.Drawing.Font("Segoe UI", 9f);
            cmbRole.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            cmbRole.Location = new System.Drawing.Point(0, 22);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new System.Drawing.Size(192, 40);
            cmbRole.TabIndex = 2;
            cmbRole.Items.Add("Super Admin");
            cmbRole.Items.Add("Assessor");
            cmbRole.Items.Add("POS Cashier");
            cmbRole.Items.Add("Inventory");
            cmbRole.SelectedIndex = 0;

            lblStatus.BackColor = System.Drawing.Color.Transparent;
            lblStatus.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblStatus.Location = new System.Drawing.Point(212, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.AutoSize = true;
            lblStatus.Text = "Status";

            cmbStatus.BorderRadius = 8;
            cmbStatus.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            cmbStatus.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9f);
            cmbStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            cmbStatus.Location = new System.Drawing.Point(212, 22);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new System.Drawing.Size(192, 40);
            cmbStatus.TabIndex = 3;
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = 0;

            // ── Password (always visible) ─────────────────────────────────────
            lblPassword.BackColor = System.Drawing.Color.Transparent;
            lblPassword.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblPassword.Location = new System.Drawing.Point(28, 322);
            lblPassword.Name = "lblPassword";
            lblPassword.AutoSize = true;
            lblPassword.Text = "Password";

            txtPassword.BorderRadius = 8;
            txtPassword.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtPassword.BorderThickness = 1;
            txtPassword.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtPassword.Font = new System.Drawing.Font("Segoe UI", 9f);
            txtPassword.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            txtPassword.PlaceholderText = "Enter password";
            txtPassword.PasswordChar = '*';
            txtPassword.Location = new System.Drawing.Point(28, 344);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(404, 40);
            txtPassword.TabIndex = 4;

            lblConfirmPassword.BackColor = System.Drawing.Color.Transparent;
            lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            lblConfirmPassword.Location = new System.Drawing.Point(28, 396);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Text = "Confirm Password";

            txtConfirmPassword.BorderRadius = 8;
            txtConfirmPassword.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            txtConfirmPassword.BorderThickness = 1;
            txtConfirmPassword.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9f);
            txtConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            txtConfirmPassword.PlaceholderText = "Confirm password";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Location = new System.Drawing.Point(28, 418);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new System.Drawing.Size(404, 40);
            txtConfirmPassword.TabIndex = 5;

            // ── Show Password toggle ──────────────────────────────────────────
            pnlShowPassword.BackColor = System.Drawing.Color.Transparent;
            pnlShowPassword.Location = new System.Drawing.Point(28, 470);
            pnlShowPassword.Name = "pnlShowPassword";
            pnlShowPassword.Size = new System.Drawing.Size(200, 36);
            pnlShowPassword.Controls.Add(togShowPassword);
            pnlShowPassword.Controls.Add(lblShowPassword);

            togShowPassword.Location = new System.Drawing.Point(0, 6);
            togShowPassword.Name = "togShowPassword";
            togShowPassword.Size = new System.Drawing.Size(46, 24);
            togShowPassword.CheckedState.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            togShowPassword.UncheckedState.FillColor = System.Drawing.Color.FromArgb(203, 213, 225);
            togShowPassword.CheckedChanged += togShowPassword_CheckedChanged;

            lblShowPassword.BackColor = System.Drawing.Color.Transparent;
            lblShowPassword.Font = new System.Drawing.Font("Segoe UI", 9f);
            lblShowPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblShowPassword.Location = new System.Drawing.Point(54, 8);
            lblShowPassword.Name = "lblShowPassword";
            lblShowPassword.AutoSize = true;
            lblShowPassword.Text = "Show Password";

            // ── Buttons ───────────────────────────────────────────────────────
            pnlButtons.BackColor = System.Drawing.Color.Transparent;
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlButtons.Height = 70;
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnAdd);

            btnCancel.BorderRadius = 10;
            btnCancel.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnCancel.BorderThickness = 1;
            btnCancel.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnCancel.Location = new System.Drawing.Point(156, 14);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(110, 42);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            btnAdd.BorderRadius = 10;
            btnAdd.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
            btnAdd.Location = new System.Drawing.Point(278, 14);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(140, 42);
            btnAdd.Text = "Add User";
            btnAdd.Click += btnAdd_Click;

            // ── Resume ────────────────────────────────────────────────────────
            pnlShowPassword.ResumeLayout(false);
            pnlShowPassword.PerformLayout();
            pnlRoleStatus.ResumeLayout(false);
            pnlRoleStatus.PerformLayout();
            pnlUsernameRow.ResumeLayout(false);
            pnlTitleBar.ResumeLayout(false);
            pnlTitleBar.PerformLayout();
            pnlButtons.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna2Panel pnlCard;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private Guna2Button btnClose;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblFullName;
        private Guna2TextBox txtFullName;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlUsernameRow;
        private Guna2TextBox txtUsername;
        private System.Windows.Forms.Label lblUsernameCheck;
        private System.Windows.Forms.Panel pnlRoleStatus;
        private System.Windows.Forms.Label lblRole;
        private Guna2ComboBox cmbRole;
        private System.Windows.Forms.Label lblStatus;
        private Guna2ComboBox cmbStatus;
        private System.Windows.Forms.Label lblPassword;
        private Guna2TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private Guna2TextBox txtConfirmPassword;
        private System.Windows.Forms.Panel pnlShowPassword;
        private Guna2ToggleSwitch togShowPassword;
        private System.Windows.Forms.Label lblShowPassword;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna2Button btnCancel;
        private Guna2Button btnAdd;
    }
}