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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlCard = new Guna2Panel();
            pnlTitleBar = new Panel();
            lblTitle = new Label();
            btnClose = new Guna2Button();
            pnlDivider = new Panel();
            lblFullName = new Label();
            txtFullName = new Guna2TextBox();
            lblUsername = new Label();
            pnlUsernameRow = new Panel();
            txtUsername = new Guna2TextBox();
            lblUsernameCheck = new Label();
            pnlRoleStatus = new Panel();
            lblRole = new Label();
            cmbRole = new Guna2ComboBox();
            lblStatus = new Label();
            cmbStatus = new Guna2ComboBox();
            lblPassword = new Label();
            txtPassword = new Guna2TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new Guna2TextBox();
            pnlShowPassword = new Panel();
            togShowPassword = new Guna2ToggleSwitch();
            lblShowPassword = new Label();
            pnlButtons = new Panel();
            btnCancel = new Guna2Button();
            btnAdd = new Guna2Button();
            pnlCard.SuspendLayout();
            pnlTitleBar.SuspendLayout();
            pnlUsernameRow.SuspendLayout();
            pnlRoleStatus.SuspendLayout();
            pnlShowPassword.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.Transparent;
            pnlCard.BorderRadius = 20;
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
            pnlCard.CustomizableEdges = customizableEdges21;
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.FillColor = Color.White;
            pnlCard.Location = new Point(0, 0);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.Color = Color.FromArgb(50, 0, 0, 0);
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges22;
            pnlCard.ShadowDecoration.Depth = 18;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.Size = new Size(460, 660);
            pnlCard.TabIndex = 0;
            // 
            // pnlTitleBar
            // 
            pnlTitleBar.BackColor = Color.Transparent;
            pnlTitleBar.Controls.Add(lblTitle);
            pnlTitleBar.Controls.Add(btnClose);
            pnlTitleBar.Location = new Point(0, 0);
            pnlTitleBar.Name = "pnlTitleBar";
            pnlTitleBar.Size = new Size(460, 60);
            pnlTitleBar.TabIndex = 0;
            pnlTitleBar.MouseDown += pnlTitleBar_MouseDown;
            pnlTitleBar.MouseMove += pnlTitleBar_MouseMove;
            pnlTitleBar.MouseUp += pnlTitleBar_MouseUp;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(28, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(177, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add New User";
            // 
            // btnClose
            // 
            btnClose.BorderRadius = 50;
            btnClose.CustomizableEdges = customizableEdges1;
            btnClose.FillColor = Color.FromArgb(241, 245, 249);
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.HoverState.FillColor = Color.FromArgb(226, 232, 240);
            btnClose.Location = new Point(408, 14);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClose.Size = new Size(34, 34);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            btnClose.Click += btnClose_Click;
            // 
            // pnlDivider
            // 
            pnlDivider.BackColor = Color.FromArgb(241, 245, 249);
            pnlDivider.Location = new Point(28, 60);
            pnlDivider.Name = "pnlDivider";
            pnlDivider.Size = new Size(404, 1);
            pnlDivider.TabIndex = 1;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.BackColor = Color.Transparent;
            lblFullName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(55, 65, 81);
            lblFullName.Location = new Point(28, 76);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(80, 20);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Full Name";
            // 
            // txtFullName
            // 
            txtFullName.BorderColor = Color.FromArgb(226, 232, 240);
            txtFullName.BorderRadius = 8;
            txtFullName.CustomizableEdges = customizableEdges3;
            txtFullName.DefaultText = "";
            txtFullName.FillColor = Color.FromArgb(248, 250, 252);
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.ForeColor = Color.FromArgb(30, 41, 59);
            txtFullName.Location = new Point(28, 98);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Full Name";
            txtFullName.SelectedText = "";
            txtFullName.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtFullName.Size = new Size(404, 40);
            txtFullName.TabIndex = 0;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(55, 65, 81);
            lblUsername.Location = new Point(28, 152);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(80, 20);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username";
            // 
            // pnlUsernameRow
            // 
            pnlUsernameRow.BackColor = Color.Transparent;
            pnlUsernameRow.Controls.Add(txtUsername);
            pnlUsernameRow.Controls.Add(lblUsernameCheck);
            pnlUsernameRow.Location = new Point(28, 174);
            pnlUsernameRow.Name = "pnlUsernameRow";
            pnlUsernameRow.Size = new Size(404, 40);
            pnlUsernameRow.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.BorderColor = Color.FromArgb(226, 232, 240);
            txtUsername.BorderRadius = 8;
            txtUsername.CustomizableEdges = customizableEdges5;
            txtUsername.DefaultText = "";
            txtUsername.Dock = DockStyle.Fill;
            txtUsername.FillColor = Color.FromArgb(248, 250, 252);
            txtUsername.Font = new Font("Segoe UI", 9F);
            txtUsername.ForeColor = Color.FromArgb(30, 41, 59);
            txtUsername.Location = new Point(0, 0);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtUsername.Size = new Size(404, 40);
            txtUsername.TabIndex = 1;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblUsernameCheck
            // 
            lblUsernameCheck.BackColor = Color.Transparent;
            lblUsernameCheck.Font = new Font("Segoe UI", 11F);
            lblUsernameCheck.ForeColor = Color.FromArgb(22, 163, 74);
            lblUsernameCheck.Location = new Point(372, 8);
            lblUsernameCheck.Name = "lblUsernameCheck";
            lblUsernameCheck.Size = new Size(26, 24);
            lblUsernameCheck.TabIndex = 2;
            lblUsernameCheck.Text = "✔";
            lblUsernameCheck.Visible = false;
            // 
            // pnlRoleStatus
            // 
            pnlRoleStatus.BackColor = Color.Transparent;
            pnlRoleStatus.Controls.Add(lblRole);
            pnlRoleStatus.Controls.Add(cmbRole);
            pnlRoleStatus.Controls.Add(lblStatus);
            pnlRoleStatus.Controls.Add(cmbStatus);
            pnlRoleStatus.Location = new Point(28, 228);
            pnlRoleStatus.Name = "pnlRoleStatus";
            pnlRoleStatus.Size = new Size(404, 80);
            pnlRoleStatus.TabIndex = 5;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.BackColor = Color.Transparent;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(55, 65, 81);
            lblRole.Location = new Point(0, 0);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(40, 20);
            lblRole.TabIndex = 0;
            lblRole.Text = "Role";
            // 
            // cmbRole
            // 
            cmbRole.BackColor = Color.Transparent;
            cmbRole.BorderColor = Color.FromArgb(226, 232, 240);
            cmbRole.BorderRadius = 8;
            cmbRole.CustomizableEdges = customizableEdges7;
            cmbRole.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FillColor = Color.FromArgb(248, 250, 252);
            cmbRole.FocusedColor = Color.Empty;
            cmbRole.Font = new Font("Segoe UI", 9F);
            cmbRole.ForeColor = Color.FromArgb(30, 41, 59);
            cmbRole.ItemHeight = 30;
            cmbRole.Items.AddRange(new object[] { "Super Admin", "Assessor", "POS Cashier", "Inventory" });
            cmbRole.Location = new Point(0, 22);
            cmbRole.Name = "cmbRole";
            cmbRole.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cmbRole.Size = new Size(192, 36);
            cmbRole.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(55, 65, 81);
            lblStatus.Location = new Point(212, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(53, 20);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            cmbStatus.BackColor = Color.Transparent;
            cmbStatus.BorderColor = Color.FromArgb(226, 232, 240);
            cmbStatus.BorderRadius = 8;
            cmbStatus.CustomizableEdges = customizableEdges9;
            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FillColor = Color.FromArgb(248, 250, 252);
            cmbStatus.FocusedColor = Color.Empty;
            cmbStatus.Font = new Font("Segoe UI", 9F);
            cmbStatus.ForeColor = Color.FromArgb(30, 41, 59);
            cmbStatus.ItemHeight = 30;
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cmbStatus.Location = new Point(212, 22);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cmbStatus.Size = new Size(192, 36);
            cmbStatus.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(55, 65, 81);
            lblPassword.Location = new Point(28, 322);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(76, 20);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.BorderColor = Color.FromArgb(226, 232, 240);
            txtPassword.BorderRadius = 8;
            txtPassword.CustomizableEdges = customizableEdges11;
            txtPassword.DefaultText = "";
            txtPassword.FillColor = Color.FromArgb(248, 250, 252);
            txtPassword.Font = new Font("Segoe UI", 9F);
            txtPassword.ForeColor = Color.FromArgb(30, 41, 59);
            txtPassword.Location = new Point(28, 344);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Enter password";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtPassword.Size = new Size(404, 40);
            txtPassword.TabIndex = 4;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.BackColor = Color.Transparent;
            lblConfirmPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = Color.FromArgb(55, 65, 81);
            lblConfirmPassword.Location = new Point(28, 396);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(137, 20);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Confirm Password";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderColor = Color.FromArgb(226, 232, 240);
            txtConfirmPassword.BorderRadius = 8;
            txtConfirmPassword.CustomizableEdges = customizableEdges13;
            txtConfirmPassword.DefaultText = "";
            txtConfirmPassword.FillColor = Color.FromArgb(248, 250, 252);
            txtConfirmPassword.Font = new Font("Segoe UI", 9F);
            txtConfirmPassword.ForeColor = Color.FromArgb(30, 41, 59);
            txtConfirmPassword.Location = new Point(28, 418);
            txtConfirmPassword.Margin = new Padding(3, 4, 3, 4);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.PlaceholderText = "Confirm password";
            txtConfirmPassword.SelectedText = "";
            txtConfirmPassword.ShadowDecoration.CustomizableEdges = customizableEdges14;
            txtConfirmPassword.Size = new Size(404, 40);
            txtConfirmPassword.TabIndex = 5;
            // 
            // pnlShowPassword
            // 
            pnlShowPassword.BackColor = Color.Transparent;
            pnlShowPassword.Controls.Add(togShowPassword);
            pnlShowPassword.Controls.Add(lblShowPassword);
            pnlShowPassword.Location = new Point(28, 470);
            pnlShowPassword.Name = "pnlShowPassword";
            pnlShowPassword.Size = new Size(200, 36);
            pnlShowPassword.TabIndex = 8;
            // 
            // togShowPassword
            // 
            togShowPassword.CheckedState.FillColor = Color.FromArgb(37, 99, 235);
            togShowPassword.CustomizableEdges = customizableEdges15;
            togShowPassword.Location = new Point(0, 6);
            togShowPassword.Name = "togShowPassword";
            togShowPassword.ShadowDecoration.CustomizableEdges = customizableEdges16;
            togShowPassword.Size = new Size(46, 24);
            togShowPassword.TabIndex = 0;
            togShowPassword.UncheckedState.FillColor = Color.FromArgb(203, 213, 225);
            togShowPassword.CheckedChanged += togShowPassword_CheckedChanged;
            // 
            // lblShowPassword
            // 
            lblShowPassword.AutoSize = true;
            lblShowPassword.BackColor = Color.Transparent;
            lblShowPassword.Font = new Font("Segoe UI", 9F);
            lblShowPassword.ForeColor = Color.FromArgb(71, 85, 105);
            lblShowPassword.Location = new Point(54, 8);
            lblShowPassword.Name = "lblShowPassword";
            lblShowPassword.Size = new Size(110, 20);
            lblShowPassword.TabIndex = 1;
            lblShowPassword.Text = "Show Password";
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.Transparent;
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnAdd);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Location = new Point(0, 590);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(460, 70);
            pnlButtons.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.BorderColor = Color.FromArgb(226, 232, 240);
            btnCancel.BorderRadius = 10;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges17;
            btnCancel.FillColor = Color.FromArgb(241, 245, 249);
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(55, 65, 81);
            btnCancel.HoverState.FillColor = Color.FromArgb(226, 232, 240);
            btnCancel.Location = new Point(156, 14);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnCancel.Size = new Size(110, 42);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.BorderRadius = 10;
            btnAdd.CustomizableEdges = customizableEdges19;
            btnAdd.FillColor = Color.FromArgb(37, 99, 235);
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            btnAdd.Location = new Point(278, 14);
            btnAdd.Name = "btnAdd";
            btnAdd.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnAdd.Size = new Size(140, 42);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add User";
            btnAdd.Click += btnAdd_Click;
            // 
            // FormAddUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(460, 660);
            Controls.Add(pnlCard);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormAddUser";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New User";
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            pnlTitleBar.ResumeLayout(false);
            pnlTitleBar.PerformLayout();
            pnlUsernameRow.ResumeLayout(false);
            pnlRoleStatus.ResumeLayout(false);
            pnlRoleStatus.PerformLayout();
            pnlShowPassword.ResumeLayout(false);
            pnlShowPassword.PerformLayout();
            pnlButtons.ResumeLayout(false);
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