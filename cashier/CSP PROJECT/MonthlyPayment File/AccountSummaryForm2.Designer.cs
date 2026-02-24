using System.Xml.Linq;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace POSCashierSystem
{
    partial class AccountSummaryForm2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            lblPageTitle = new Label();
            lblPageSubtitle = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            pnlAvatar = new Guna.UI2.WinForms.Guna2Panel();
            lblInitials = new Label();
            lblCustomerName = new Label();
            lblLocation = new Label();
            chipTicket = new Guna.UI2.WinForms.Guna2Button();
            chipStatus = new Guna.UI2.WinForms.Guna2Button();
            sep1 = new Guna.UI2.WinForms.Guna2Separator();
            lblUnitTitle = new Label();
            lblModelKey = new Label();
            lblModelValue = new Label();
            lblColorKey = new Label();
            lblColorValue = new Label();
            lblEngineKey = new Label();
            lblEngineValue = new Label();
            lblFinTitle = new Label();
            lblBalanceKey = new Label();
            lblBalanceValue = new Label();
            lblAmortKey = new Label();
            lblAmortValue = new Label();
            lblDueDateKey = new Label();
            lblDueDateValue = new Label();
            sep2 = new Guna.UI2.WinForms.Guna2Separator();
            btnContinue = new Guna.UI2.WinForms.Guna2Button();
            pnlBackground.SuspendLayout();
            pnlCard.SuspendLayout();
            pnlAvatar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackground
            // 
            pnlBackground.BackColor = Color.FromArgb(242, 245, 250);
            pnlBackground.Controls.Add(lblPageTitle);
            pnlBackground.Controls.Add(lblPageSubtitle);
            pnlBackground.Controls.Add(btnBack);
            pnlBackground.Controls.Add(pnlCard);
            pnlBackground.CustomizableEdges = customizableEdges13;
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnlBackground.Size = new Size(1221, 750);
            pnlBackground.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = Color.Transparent;
            lblPageTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblPageTitle.Location = new Point(28, 18);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(280, 32);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Account Summary";
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.BackColor = Color.Transparent;
            lblPageSubtitle.Font = new Font("Segoe UI", 9F);
            lblPageSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new Point(30, 52);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(280, 22);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Verify details before proceeding";
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBack.BorderColor = Color.Transparent;
            btnBack.Cursor = Cursors.Hand;
            btnBack.CustomizableEdges = customizableEdges1;
            btnBack.FillColor = Color.Transparent;
            btnBack.Font = new Font("Segoe UI", 9.5F);
            btnBack.ForeColor = Color.FromArgb(100, 116, 139);
            btnBack.HoverState.BorderColor = Color.Transparent;
            btnBack.HoverState.FillColor = Color.Transparent;
            btnBack.HoverState.ForeColor = Color.FromArgb(30, 41, 59);
            btnBack.Location = new Point(1117, 22);
            btnBack.Name = "btnBack";
            btnBack.PressedColor = Color.Transparent;
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new Size(80, 40);
            btnBack.TabIndex = 0;
            btnBack.Text = "← Back";
            btnBack.Click += BtnBack_Click;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.Transparent;
            pnlCard.BorderColor = Color.FromArgb(226, 232, 240);
            pnlCard.BorderRadius = 20;
            pnlCard.BorderThickness = 1;
            pnlCard.Controls.Add(pnlAvatar);
            pnlCard.Controls.Add(lblCustomerName);
            pnlCard.Controls.Add(lblLocation);
            pnlCard.Controls.Add(chipTicket);
            pnlCard.Controls.Add(chipStatus);
            pnlCard.Controls.Add(sep1);
            pnlCard.Controls.Add(lblUnitTitle);
            pnlCard.Controls.Add(lblModelKey);
            pnlCard.Controls.Add(lblModelValue);
            pnlCard.Controls.Add(lblColorKey);
            pnlCard.Controls.Add(lblColorValue);
            pnlCard.Controls.Add(lblEngineKey);
            pnlCard.Controls.Add(lblEngineValue);
            pnlCard.Controls.Add(lblFinTitle);
            pnlCard.Controls.Add(lblBalanceKey);
            pnlCard.Controls.Add(lblBalanceValue);
            pnlCard.Controls.Add(lblAmortKey);
            pnlCard.Controls.Add(lblAmortValue);
            pnlCard.Controls.Add(lblDueDateKey);
            pnlCard.Controls.Add(lblDueDateValue);
            pnlCard.Controls.Add(sep2);
            pnlCard.Controls.Add(btnContinue);
            pnlCard.CustomizableEdges = customizableEdges11;
            pnlCard.FillColor = Color.White;
            pnlCard.Location = new Point(58, 104);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.BorderRadius = 20;
            pnlCard.ShadowDecoration.Color = Color.FromArgb(40, 148, 163, 184);
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlCard.ShadowDecoration.Depth = 15;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Shadow = new Padding(0, 4, 16, 16);
            pnlCard.Size = new Size(1101, 580);
            pnlCard.TabIndex = 1;
            // 
            // pnlAvatar
            // 
            pnlAvatar.BorderRadius = 36;
            pnlAvatar.Controls.Add(lblInitials);
            pnlAvatar.CustomizableEdges = customizableEdges3;
            pnlAvatar.FillColor = Color.FromArgb(219, 234, 254);
            pnlAvatar.Location = new Point(24, 25);
            pnlAvatar.Name = "pnlAvatar";
            pnlAvatar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlAvatar.Size = new Size(72, 90);
            pnlAvatar.TabIndex = 0;
            // 
            // lblInitials
            // 
            lblInitials.BackColor = Color.Transparent;
            lblInitials.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblInitials.ForeColor = Color.FromArgb(37, 99, 235);
            lblInitials.Location = new Point(0, 0);
            lblInitials.Name = "lblInitials";
            lblInitials.Size = new Size(72, 90);
            lblInitials.TabIndex = 0;
            lblInitials.Text = "JD";
            lblInitials.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCustomerName
            // 
            lblCustomerName.BackColor = Color.Transparent;
            lblCustomerName.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblCustomerName.ForeColor = Color.FromArgb(30, 41, 59);
            lblCustomerName.Location = new Point(112, 20);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(600, 38);
            lblCustomerName.TabIndex = 22;
            lblCustomerName.Text = "Juan Dela Cruz";
            // 
            // lblLocation
            // 
            lblLocation.BackColor = Color.Transparent;
            lblLocation.Font = new Font("Segoe UI", 9.5F);
            lblLocation.ForeColor = Color.FromArgb(100, 116, 139);
            lblLocation.Location = new Point(112, 56);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(300, 22);
            lblLocation.TabIndex = 23;
            lblLocation.Text = "Quezon City";
            // 
            // chipTicket
            // 
            chipTicket.BorderColor = Color.Transparent;
            chipTicket.BorderRadius = 20;
            chipTicket.CustomizableEdges = customizableEdges5;
            chipTicket.FillColor = Color.FromArgb(219, 234, 254);
            chipTicket.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            chipTicket.ForeColor = Color.FromArgb(37, 99, 235);
            chipTicket.HoverState.BorderColor = Color.Transparent;
            chipTicket.HoverState.FillColor = Color.FromArgb(191, 219, 254);
            chipTicket.HoverState.ForeColor = Color.FromArgb(29, 78, 216);
            chipTicket.Location = new Point(112, 84);
            chipTicket.Name = "chipTicket";
            chipTicket.ShadowDecoration.CustomizableEdges = customizableEdges6;
            chipTicket.Size = new Size(122, 32);
            chipTicket.TabIndex = 1;
            chipTicket.Text = "LA-2026-0001";
            // 
            // chipStatus
            // 
            chipStatus.BorderColor = Color.FromArgb(203, 213, 225);
            chipStatus.BorderRadius = 20;
            chipStatus.BorderThickness = 1;
            chipStatus.CustomizableEdges = customizableEdges7;
            chipStatus.FillColor = Color.FromArgb(241, 245, 249);
            chipStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            chipStatus.ForeColor = Color.FromArgb(100, 116, 139);
            chipStatus.HoverState.BorderColor = Color.FromArgb(148, 163, 184);
            chipStatus.HoverState.FillColor = Color.FromArgb(226, 232, 240);
            chipStatus.HoverState.ForeColor = Color.FromArgb(51, 65, 85);
            chipStatus.Location = new Point(242, 84);
            chipStatus.Name = "chipStatus";
            chipStatus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            chipStatus.Size = new Size(127, 32);
            chipStatus.TabIndex = 2;
            chipStatus.Text = "MONTHLY";
            // 
            // sep1
            // 
            sep1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sep1.FillColor = Color.FromArgb(226, 232, 240);
            sep1.Location = new Point(0, 138);
            sep1.Name = "sep1";
            sep1.Size = new Size(1101, 1);
            sep1.TabIndex = 3;
            // 
            // lblUnitTitle
            // 
            lblUnitTitle.BackColor = Color.Transparent;
            lblUnitTitle.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblUnitTitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblUnitTitle.Location = new Point(28, 160);
            lblUnitTitle.Name = "lblUnitTitle";
            lblUnitTitle.Size = new Size(140, 20);
            lblUnitTitle.TabIndex = 21;
            lblUnitTitle.Text = "UNIT DETAILS";
            // 
            // lblModelKey
            // 
            lblModelKey.BackColor = Color.Transparent;
            lblModelKey.Font = new Font("Segoe UI", 9.5F);
            lblModelKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblModelKey.Location = new Point(28, 196);
            lblModelKey.Name = "lblModelKey";
            lblModelKey.Size = new Size(120, 28);
            lblModelKey.TabIndex = 20;
            lblModelKey.Text = "Model";
            // 
            // lblModelValue
            // 
            lblModelValue.BackColor = Color.Transparent;
            lblModelValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblModelValue.ForeColor = Color.FromArgb(30, 41, 59);
            lblModelValue.Location = new Point(178, 196);
            lblModelValue.Name = "lblModelValue";
            lblModelValue.Size = new Size(220, 28);
            lblModelValue.TabIndex = 19;
            lblModelValue.Text = "Honda ADV 160";
            lblModelValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblColorKey
            // 
            lblColorKey.BackColor = Color.Transparent;
            lblColorKey.Font = new Font("Segoe UI", 9.5F);
            lblColorKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblColorKey.Location = new Point(28, 251);
            lblColorKey.Name = "lblColorKey";
            lblColorKey.Size = new Size(120, 28);
            lblColorKey.TabIndex = 18;
            lblColorKey.Text = "Color";
            // 
            // lblColorValue
            // 
            lblColorValue.BackColor = Color.Transparent;
            lblColorValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblColorValue.ForeColor = Color.FromArgb(30, 41, 59);
            lblColorValue.Location = new Point(178, 251);
            lblColorValue.Name = "lblColorValue";
            lblColorValue.Size = new Size(220, 28);
            lblColorValue.TabIndex = 17;
            lblColorValue.Text = "Matte Black";
            lblColorValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEngineKey
            // 
            lblEngineKey.BackColor = Color.Transparent;
            lblEngineKey.Font = new Font("Segoe UI", 9.5F);
            lblEngineKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblEngineKey.Location = new Point(28, 306);
            lblEngineKey.Name = "lblEngineKey";
            lblEngineKey.Size = new Size(120, 28);
            lblEngineKey.TabIndex = 16;
            lblEngineKey.Text = "Engine No.";
            // 
            // lblEngineValue
            // 
            lblEngineValue.BackColor = Color.Transparent;
            lblEngineValue.Font = new Font("Segoe UI", 9.5F);
            lblEngineValue.ForeColor = Color.FromArgb(71, 85, 105);
            lblEngineValue.Location = new Point(178, 306);
            lblEngineValue.Name = "lblEngineValue";
            lblEngineValue.Size = new Size(220, 28);
            lblEngineValue.TabIndex = 15;
            lblEngineValue.Text = "K1Z-998877";
            lblEngineValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblFinTitle
            // 
            lblFinTitle.BackColor = Color.Transparent;
            lblFinTitle.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblFinTitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblFinTitle.Location = new Point(661, 160);
            lblFinTitle.Name = "lblFinTitle";
            lblFinTitle.Size = new Size(160, 20);
            lblFinTitle.TabIndex = 14;
            lblFinTitle.Text = "FINANCIAL STATUS";
            // 
            // lblBalanceKey
            // 
            lblBalanceKey.BackColor = Color.Transparent;
            lblBalanceKey.Font = new Font("Segoe UI", 9.5F);
            lblBalanceKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblBalanceKey.Location = new Point(661, 196);
            lblBalanceKey.Name = "lblBalanceKey";
            lblBalanceKey.Size = new Size(160, 28);
            lblBalanceKey.TabIndex = 30;
            lblBalanceKey.Text = "Current Balance";
            // 
            // lblBalanceValue
            // 
            lblBalanceValue.BackColor = Color.Transparent;
            lblBalanceValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBalanceValue.ForeColor = Color.FromArgb(220, 38, 38);
            lblBalanceValue.Location = new Point(831, 193);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.Size = new Size(200, 32);
            lblBalanceValue.TabIndex = 31;
            lblBalanceValue.Text = "₱134,118.08";
            lblBalanceValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAmortKey
            // 
            lblAmortKey.BackColor = Color.Transparent;
            lblAmortKey.Font = new Font("Segoe UI", 9.5F);
            lblAmortKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblAmortKey.Location = new Point(661, 251);
            lblAmortKey.Name = "lblAmortKey";
            lblAmortKey.Size = new Size(160, 28);
            lblAmortKey.TabIndex = 13;
            lblAmortKey.Text = "Monthly Amortization";
            // 
            // lblAmortValue
            // 
            lblAmortValue.BackColor = Color.Transparent;
            lblAmortValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAmortValue.ForeColor = Color.FromArgb(30, 41, 59);
            lblAmortValue.Location = new Point(831, 248);
            lblAmortValue.Name = "lblAmortValue";
            lblAmortValue.Size = new Size(200, 32);
            lblAmortValue.TabIndex = 12;
            lblAmortValue.Text = "₱5,364.72";
            lblAmortValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDueDateKey
            // 
            lblDueDateKey.BackColor = Color.Transparent;
            lblDueDateKey.Font = new Font("Segoe UI", 9.5F);
            lblDueDateKey.ForeColor = Color.FromArgb(100, 116, 139);
            lblDueDateKey.Location = new Point(661, 306);
            lblDueDateKey.Name = "lblDueDateKey";
            lblDueDateKey.Size = new Size(160, 28);
            lblDueDateKey.TabIndex = 32;
            lblDueDateKey.Text = "Next Due Date";
            // 
            // lblDueDateValue
            // 
            lblDueDateValue.BackColor = Color.Transparent;
            lblDueDateValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDueDateValue.ForeColor = Color.FromArgb(37, 99, 235);
            lblDueDateValue.Location = new Point(831, 303);
            lblDueDateValue.Name = "lblDueDateValue";
            lblDueDateValue.Size = new Size(200, 32);
            lblDueDateValue.TabIndex = 33;
            lblDueDateValue.Text = "Feb 1, 2026";
            lblDueDateValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // sep2
            // 
            sep2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sep2.FillColor = Color.FromArgb(226, 232, 240);
            sep2.Location = new Point(0, 474);
            sep2.Name = "sep2";
            sep2.Size = new Size(1101, 1);
            sep2.TabIndex = 10;
            // 
            // btnContinue
            // 
            btnContinue.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnContinue.BorderColor = Color.Transparent;
            btnContinue.BorderRadius = 10;
            btnContinue.Cursor = Cursors.Hand;
            btnContinue.CustomizableEdges = customizableEdges9;
            btnContinue.FillColor = Color.FromArgb(37, 99, 235);
            btnContinue.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnContinue.ForeColor = Color.White;
            btnContinue.HoverState.BorderColor = Color.Transparent;
            btnContinue.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            btnContinue.HoverState.ForeColor = Color.White;
            btnContinue.Location = new Point(807, 494);
            btnContinue.Name = "btnContinue";
            btnContinue.PressedColor = Color.FromArgb(30, 64, 175);
            btnContinue.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnContinue.Size = new Size(270, 55);
            btnContinue.TabIndex = 11;
            btnContinue.Text = "Continue to Configuration  →";
            btnContinue.Click += BtnContinue_Click;
            // 
            // AccountSummaryForm2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 245, 250);
            Controls.Add(pnlBackground);
            Name = "AccountSummaryForm2";
            Size = new Size(1221, 750);
            Load += AccountSummaryForm2_Load;
            pnlBackground.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            pnlAvatar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ── Field declarations ────────────────────────────────────────────────
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2Panel pnlAvatar;
        private System.Windows.Forms.Label lblInitials;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblLocation;
        private Guna.UI2.WinForms.Guna2Button chipTicket;
        private Guna.UI2.WinForms.Guna2Button chipStatus;
        private Guna.UI2.WinForms.Guna2Separator sep1;
        private System.Windows.Forms.Label lblUnitTitle;
        private System.Windows.Forms.Label lblModelKey;
        private System.Windows.Forms.Label lblModelValue;
        private System.Windows.Forms.Label lblColorKey;
        private System.Windows.Forms.Label lblColorValue;
        private System.Windows.Forms.Label lblEngineKey;
        private System.Windows.Forms.Label lblEngineValue;
        private System.Windows.Forms.Label lblFinTitle;
        private System.Windows.Forms.Label lblBalanceKey;
        private System.Windows.Forms.Label lblBalanceValue;
        private System.Windows.Forms.Label lblAmortKey;
        private System.Windows.Forms.Label lblAmortValue;
        private System.Windows.Forms.Label lblDueDateKey;
        private System.Windows.Forms.Label lblDueDateValue;
        private Guna.UI2.WinForms.Guna2Separator sep2;
        private Guna.UI2.WinForms.Guna2Button btnContinue;
    }
}