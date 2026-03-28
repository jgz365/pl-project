using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    partial class UC_UserCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblFullName = new Label();
            lblUsername = new Label();
            lblRoleIcon = new Label();
            lblRoleText = new Label();
            pnlStatusPill = new Panel();
            pnlStatusDot = new Panel();
            lblStatusText = new Label();
            lblLastLogin = new Label();
            btnEdit = new Guna2Button();
            btnArchive = new Guna2Button();
            pnlBottomBorder = new Panel();
            pnlStatusPill.SuspendLayout();
            SuspendLayout();
            // 
            // lblFullName
            // 
            lblFullName.BackColor = Color.Transparent;
            lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(15, 23, 42);
            lblFullName.Location = new Point(17, 19);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(274, 24);
            lblFullName.TabIndex = 0;
            lblFullName.Text = "System Admin";
            // 
            // lblUsername
            // 
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI", 8F);
            lblUsername.ForeColor = Color.FromArgb(148, 163, 184);
            lblUsername.Location = new Point(17, 44);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(274, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "@admin";
            // 
            // lblRoleIcon
            // 
            lblRoleIcon.BackColor = Color.Transparent;
            lblRoleIcon.Font = new Font("Segoe UI", 11F);
            lblRoleIcon.ForeColor = Color.FromArgb(245, 158, 11);
            lblRoleIcon.Location = new Point(331, 27);
            lblRoleIcon.Name = "lblRoleIcon";
            lblRoleIcon.Size = new Size(25, 29);
            lblRoleIcon.TabIndex = 2;
            lblRoleIcon.Text = "👑";
            lblRoleIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRoleText
            // 
            lblRoleText.BackColor = Color.Transparent;
            lblRoleText.Font = new Font("Segoe UI", 9F);
            lblRoleText.ForeColor = Color.FromArgb(71, 85, 105);
            lblRoleText.Location = new Point(361, 28);
            lblRoleText.Name = "lblRoleText";
            lblRoleText.Size = new Size(160, 24);
            lblRoleText.TabIndex = 3;
            lblRoleText.Text = "Super Admin";
            // 
            // pnlStatusPill
            // 
            pnlStatusPill.BackColor = Color.FromArgb(220, 252, 231);
            pnlStatusPill.Controls.Add(pnlStatusDot);
            pnlStatusPill.Controls.Add(lblStatusText);
            pnlStatusPill.Location = new Point(549, 24);
            pnlStatusPill.Margin = new Padding(3, 4, 3, 4);
            pnlStatusPill.Name = "pnlStatusPill";
            pnlStatusPill.Size = new Size(96, 35);
            pnlStatusPill.TabIndex = 4;
            pnlStatusPill.Paint += pnlStatusPill_Paint;
            // 
            // pnlStatusDot
            // 
            pnlStatusDot.BackColor = Color.FromArgb(34, 197, 94);
            pnlStatusDot.Location = new Point(10, 12);
            pnlStatusDot.Margin = new Padding(3, 4, 3, 4);
            pnlStatusDot.Name = "pnlStatusDot";
            pnlStatusDot.Size = new Size(9, 11);
            pnlStatusDot.TabIndex = 0;
            pnlStatusDot.Paint += pnlStatusDot_Paint;
            // 
            // lblStatusText
            // 
            lblStatusText.BackColor = Color.Transparent;
            lblStatusText.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblStatusText.ForeColor = Color.FromArgb(22, 163, 74);
            lblStatusText.Location = new Point(23, 7);
            lblStatusText.Name = "lblStatusText";
            lblStatusText.Size = new Size(66, 21);
            lblStatusText.TabIndex = 1;
            lblStatusText.Text = "Active";
            // 
            // lblLastLogin
            // 
            lblLastLogin.BackColor = Color.Transparent;
            lblLastLogin.Font = new Font("Segoe UI", 9F);
            lblLastLogin.ForeColor = Color.FromArgb(100, 116, 139);
            lblLastLogin.Location = new Point(709, 28);
            lblLastLogin.Name = "lblLastLogin";
            lblLastLogin.Size = new Size(137, 24);
            lblLastLogin.TabIndex = 5;
            lblLastLogin.Text = "Just now";
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEdit.BorderColor = Color.FromArgb(219, 234, 254);
            btnEdit.BorderRadius = 8;
            btnEdit.BorderThickness = 1;
            btnEdit.CustomizableEdges = customizableEdges1;
            btnEdit.FillColor = Color.FromArgb(239, 246, 255);
            btnEdit.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnEdit.ForeColor = Color.FromArgb(37, 99, 235);
            btnEdit.HoverState.FillColor = Color.FromArgb(214, 226, 255);
            btnEdit.HoverState.ForeColor = Color.FromArgb(29, 78, 216);
            btnEdit.Location = new Point(852, 21);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEdit.Size = new Size(106, 37);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "✎  Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnArchive
            // 
            btnArchive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnArchive.BorderColor = Color.FromArgb(254, 215, 170);
            btnArchive.BorderRadius = 8;
            btnArchive.BorderThickness = 1;
            btnArchive.CustomizableEdges = customizableEdges3;
            btnArchive.FillColor = Color.FromArgb(255, 247, 237);
            btnArchive.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnArchive.ForeColor = Color.FromArgb(194, 65, 12);
            btnArchive.HoverState.FillColor = Color.FromArgb(254, 200, 130);
            btnArchive.HoverState.ForeColor = Color.FromArgb(154, 52, 18);
            btnArchive.Location = new Point(969, 21);
            btnArchive.Margin = new Padding(3, 4, 3, 4);
            btnArchive.Name = "btnArchive";
            btnArchive.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnArchive.Size = new Size(108, 37);
            btnArchive.TabIndex = 7;
            btnArchive.Text = "🗄  Archive";
            btnArchive.Click += btnArchive_Click;
            // 
            // pnlBottomBorder
            // 
            pnlBottomBorder.BackColor = Color.FromArgb(241, 245, 249);
            pnlBottomBorder.Dock = DockStyle.Bottom;
            pnlBottomBorder.Location = new Point(17, 82);
            pnlBottomBorder.Margin = new Padding(3, 4, 3, 4);
            pnlBottomBorder.Name = "pnlBottomBorder";
            pnlBottomBorder.Size = new Size(1063, 1);
            pnlBottomBorder.TabIndex = 8;
            // 
            // UC_UserCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblFullName);
            Controls.Add(lblUsername);
            Controls.Add(lblRoleIcon);
            Controls.Add(lblRoleText);
            Controls.Add(pnlStatusPill);
            Controls.Add(lblLastLogin);
            Controls.Add(btnEdit);
            Controls.Add(btnArchive);
            Controls.Add(pnlBottomBorder);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UC_UserCard";
            Padding = new Padding(17, 0, 17, 0);
            Size = new Size(1097, 83);
            MouseEnter += cardPanel_MouseEnter;
            MouseLeave += cardPanel_MouseLeave;
            pnlStatusPill.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ── Field declarations ────────────────────────────────────────────────
        private Label lblFullName;
        private Label lblUsername;
        private Label lblRoleIcon;
        private Label lblRoleText;
        private Panel pnlStatusPill;
        private Panel pnlStatusDot;
        private Label lblStatusText;
        private Label lblLastLogin;
        private Guna2Button btnEdit;
        private Guna2Button btnArchive;
        private Panel pnlBottomBorder;
    }
}