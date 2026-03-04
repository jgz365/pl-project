using System.Xml.Linq;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace POSCashierSystem
{
    partial class AccountSummaryForm4
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
            lblPageTitle = new System.Windows.Forms.Label();
            lblPageSubtitle = new System.Windows.Forms.Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            pnlAvatar = new Guna.UI2.WinForms.Guna2Panel();
            lblInitials = new System.Windows.Forms.Label();
            lblCustomerName = new System.Windows.Forms.Label();
            lblLocation = new System.Windows.Forms.Label();
            chipTicket = new Guna.UI2.WinForms.Guna2Button();
            chipStatus = new Guna.UI2.WinForms.Guna2Button();
            sep1 = new Guna.UI2.WinForms.Guna2Separator();
            lblUnitTitle = new System.Windows.Forms.Label();
            lblModelKey = new System.Windows.Forms.Label();
            lblModelValue = new System.Windows.Forms.Label();
            lblColorKey = new System.Windows.Forms.Label();
            lblColorValue = new System.Windows.Forms.Label();
            lblEngineKey = new System.Windows.Forms.Label();
            lblEngineValue = new System.Windows.Forms.Label();
            lblFinTitle = new System.Windows.Forms.Label();
            lblBalanceKey = new System.Windows.Forms.Label();
            lblBalanceValue = new System.Windows.Forms.Label();
            lblAmortKey = new System.Windows.Forms.Label();
            lblAmortValue = new System.Windows.Forms.Label();
            lblDueDateKey = new System.Windows.Forms.Label();
            lblDueDateValue = new System.Windows.Forms.Label();
            sep2 = new Guna.UI2.WinForms.Guna2Separator();
            btnContinue = new Guna.UI2.WinForms.Guna2Button();
            pnlBackground.SuspendLayout();
            pnlCard.SuspendLayout();
            pnlAvatar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackground
            // 
            pnlBackground.BackColor = System.Drawing.Color.FromArgb(242, 245, 250);
            pnlBackground.Controls.Add(lblPageTitle);
            pnlBackground.Controls.Add(lblPageSubtitle);
            pnlBackground.Controls.Add(btnBack);
            pnlBackground.Controls.Add(pnlCard);
            pnlBackground.CustomizableEdges = customizableEdges13;
            pnlBackground.Location = new System.Drawing.Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnlBackground.Size = new System.Drawing.Size(1221, 750);
            pnlBackground.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblPageTitle.Location = new System.Drawing.Point(28, 18);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(300, 32);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Account Summary";
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new System.Drawing.Point(30, 52);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new System.Drawing.Size(300, 22);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Verify details before proceeding";
            // 
            // btnBack
            // 
            btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnBack.BorderColor = System.Drawing.Color.Transparent;
            btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            btnBack.CustomizableEdges = customizableEdges1;
            btnBack.FillColor = System.Drawing.Color.Transparent;
            btnBack.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            btnBack.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnBack.HoverState.BorderColor = System.Drawing.Color.Transparent;
            btnBack.HoverState.FillColor = System.Drawing.Color.Transparent;
            btnBack.HoverState.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnBack.Location = new System.Drawing.Point(1117, 22);
            btnBack.Name = "btnBack";
            btnBack.PressedColor = System.Drawing.Color.Transparent;
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new System.Drawing.Size(80, 40);
            btnBack.TabIndex = 0;
            btnBack.Text = "← Back";
            btnBack.Click += new System.EventHandler(BtnBack_Click);
            // 
            // pnlCard
            // 
            pnlCard.BackColor = System.Drawing.Color.Transparent;
            pnlCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
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
            pnlCard.FillColor = System.Drawing.Color.White;
            pnlCard.Location = new System.Drawing.Point(58, 104);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.BorderRadius = 20;
            pnlCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(40, 148, 163, 184);
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlCard.ShadowDecoration.Depth = 15;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 4, 16, 16);
            pnlCard.Size = new System.Drawing.Size(1101, 580);
            pnlCard.TabIndex = 1;
            // 
            // pnlAvatar
            // 
            pnlAvatar.BorderRadius = 36;
            pnlAvatar.Controls.Add(lblInitials);
            pnlAvatar.CustomizableEdges = customizableEdges3;
            pnlAvatar.FillColor = System.Drawing.Color.FromArgb(255, 228, 230);   // red tint for Settlement
            pnlAvatar.Location = new System.Drawing.Point(24, 25);
            pnlAvatar.Name = "pnlAvatar";
            pnlAvatar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlAvatar.Size = new System.Drawing.Size(72, 90);
            pnlAvatar.TabIndex = 0;
            // 
            // lblInitials
            // 
            lblInitials.BackColor = System.Drawing.Color.Transparent;
            lblInitials.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblInitials.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);   // red
            lblInitials.Location = new System.Drawing.Point(0, 0);
            lblInitials.Name = "lblInitials";
            lblInitials.Size = new System.Drawing.Size(72, 90);
            lblInitials.TabIndex = 0;
            lblInitials.Text = "JD";
            lblInitials.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCustomerName
            // 
            lblCustomerName.BackColor = System.Drawing.Color.Transparent;
            lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblCustomerName.Location = new System.Drawing.Point(112, 20);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new System.Drawing.Size(600, 38);
            lblCustomerName.TabIndex = 22;
            lblCustomerName.Text = "Juan Dela Cruz";
            // 
            // lblLocation
            // 
            lblLocation.BackColor = System.Drawing.Color.Transparent;
            lblLocation.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblLocation.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblLocation.Location = new System.Drawing.Point(112, 56);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new System.Drawing.Size(300, 22);
            lblLocation.TabIndex = 23;
            lblLocation.Text = "Quezon City";
            // 
            // chipTicket
            // 
            chipTicket.BorderColor = System.Drawing.Color.Transparent;
            chipTicket.BorderRadius = 20;
            chipTicket.CustomizableEdges = customizableEdges5;
            chipTicket.FillColor = System.Drawing.Color.FromArgb(255, 228, 230);   // red tint
            chipTicket.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            chipTicket.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            chipTicket.HoverState.BorderColor = System.Drawing.Color.Transparent;
            chipTicket.HoverState.FillColor = System.Drawing.Color.FromArgb(254, 202, 202);
            chipTicket.HoverState.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            chipTicket.Location = new System.Drawing.Point(112, 84);
            chipTicket.Name = "chipTicket";
            chipTicket.ShadowDecoration.CustomizableEdges = customizableEdges6;
            chipTicket.Size = new System.Drawing.Size(122, 32);
            chipTicket.TabIndex = 1;
            chipTicket.Text = "LA-2026-0001";
            // 
            // chipStatus
            // 
            chipStatus.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            chipStatus.BorderRadius = 20;
            chipStatus.BorderThickness = 1;
            chipStatus.CustomizableEdges = customizableEdges7;
            chipStatus.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            chipStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            chipStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            chipStatus.HoverState.BorderColor = System.Drawing.Color.FromArgb(148, 163, 184);
            chipStatus.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            chipStatus.HoverState.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            chipStatus.Location = new System.Drawing.Point(242, 84);
            chipStatus.Name = "chipStatus";
            chipStatus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            chipStatus.Size = new System.Drawing.Size(127, 32);
            chipStatus.TabIndex = 2;
            chipStatus.Text = "SETTLEMENT";
            // 
            // sep1
            // 
            sep1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sep1.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            sep1.Location = new System.Drawing.Point(0, 138);
            sep1.Name = "sep1";
            sep1.Size = new System.Drawing.Size(1101, 1);
            sep1.TabIndex = 3;
            // 
            // lblUnitTitle
            // 
            lblUnitTitle.BackColor = System.Drawing.Color.Transparent;
            lblUnitTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            lblUnitTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblUnitTitle.Location = new System.Drawing.Point(28, 160);
            lblUnitTitle.Name = "lblUnitTitle";
            lblUnitTitle.Size = new System.Drawing.Size(140, 20);
            lblUnitTitle.TabIndex = 21;
            lblUnitTitle.Text = "UNIT DETAILS";
            // 
            // lblModelKey
            // 
            lblModelKey.BackColor = System.Drawing.Color.Transparent;
            lblModelKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblModelKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblModelKey.Location = new System.Drawing.Point(28, 196);
            lblModelKey.Name = "lblModelKey";
            lblModelKey.Size = new System.Drawing.Size(120, 28);
            lblModelKey.TabIndex = 20;
            lblModelKey.Text = "Model";
            // 
            // lblModelValue
            // 
            lblModelValue.BackColor = System.Drawing.Color.Transparent;
            lblModelValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblModelValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblModelValue.Location = new System.Drawing.Point(178, 196);
            lblModelValue.Name = "lblModelValue";
            lblModelValue.Size = new System.Drawing.Size(220, 28);
            lblModelValue.TabIndex = 19;
            lblModelValue.Text = "Honda ADV 160";
            lblModelValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblColorKey
            // 
            lblColorKey.BackColor = System.Drawing.Color.Transparent;
            lblColorKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblColorKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblColorKey.Location = new System.Drawing.Point(28, 251);
            lblColorKey.Name = "lblColorKey";
            lblColorKey.Size = new System.Drawing.Size(120, 28);
            lblColorKey.TabIndex = 18;
            lblColorKey.Text = "Color";
            // 
            // lblColorValue
            // 
            lblColorValue.BackColor = System.Drawing.Color.Transparent;
            lblColorValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblColorValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblColorValue.Location = new System.Drawing.Point(178, 251);
            lblColorValue.Name = "lblColorValue";
            lblColorValue.Size = new System.Drawing.Size(220, 28);
            lblColorValue.TabIndex = 17;
            lblColorValue.Text = "Matte Black";
            lblColorValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEngineKey
            // 
            lblEngineKey.BackColor = System.Drawing.Color.Transparent;
            lblEngineKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblEngineKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblEngineKey.Location = new System.Drawing.Point(28, 306);
            lblEngineKey.Name = "lblEngineKey";
            lblEngineKey.Size = new System.Drawing.Size(120, 28);
            lblEngineKey.TabIndex = 16;
            lblEngineKey.Text = "Engine No.";
            // 
            // lblEngineValue
            // 
            lblEngineValue.BackColor = System.Drawing.Color.Transparent;
            lblEngineValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblEngineValue.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblEngineValue.Location = new System.Drawing.Point(178, 306);
            lblEngineValue.Name = "lblEngineValue";
            lblEngineValue.Size = new System.Drawing.Size(220, 28);
            lblEngineValue.TabIndex = 15;
            lblEngineValue.Text = "K1Z-998877";
            lblEngineValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinTitle
            // 
            lblFinTitle.BackColor = System.Drawing.Color.Transparent;
            lblFinTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            lblFinTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblFinTitle.Location = new System.Drawing.Point(661, 160);
            lblFinTitle.Name = "lblFinTitle";
            lblFinTitle.Size = new System.Drawing.Size(160, 20);
            lblFinTitle.TabIndex = 14;
            lblFinTitle.Text = "FINANCIAL STATUS";
            // 
            // lblBalanceKey
            // 
            lblBalanceKey.BackColor = System.Drawing.Color.Transparent;
            lblBalanceKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblBalanceKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblBalanceKey.Location = new System.Drawing.Point(661, 196);
            lblBalanceKey.Name = "lblBalanceKey";
            lblBalanceKey.Size = new System.Drawing.Size(160, 28);
            lblBalanceKey.TabIndex = 30;
            lblBalanceKey.Text = "Current Balance";
            // 
            // lblBalanceValue  (larger / more prominent for Full Settlement)
            // 
            lblBalanceValue.BackColor = System.Drawing.Color.Transparent;
            lblBalanceValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblBalanceValue.Location = new System.Drawing.Point(811, 190);
            lblBalanceValue.Name = "lblBalanceValue";
            lblBalanceValue.Size = new System.Drawing.Size(220, 36);
            lblBalanceValue.TabIndex = 31;
            lblBalanceValue.Text = "₱134,118.08";
            lblBalanceValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmortKey
            // 
            lblAmortKey.BackColor = System.Drawing.Color.Transparent;
            lblAmortKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblAmortKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblAmortKey.Location = new System.Drawing.Point(661, 251);
            lblAmortKey.Name = "lblAmortKey";
            lblAmortKey.Size = new System.Drawing.Size(160, 28);
            lblAmortKey.TabIndex = 13;
            lblAmortKey.Text = "Monthly Amortization";
            // 
            // lblAmortValue
            // 
            lblAmortValue.BackColor = System.Drawing.Color.Transparent;
            lblAmortValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblAmortValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblAmortValue.Location = new System.Drawing.Point(831, 248);
            lblAmortValue.Name = "lblAmortValue";
            lblAmortValue.Size = new System.Drawing.Size(200, 32);
            lblAmortValue.TabIndex = 12;
            lblAmortValue.Text = "₱5,364.72";
            lblAmortValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDueDateKey
            // 
            lblDueDateKey.BackColor = System.Drawing.Color.Transparent;
            lblDueDateKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblDueDateKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDueDateKey.Location = new System.Drawing.Point(661, 306);
            lblDueDateKey.Name = "lblDueDateKey";
            lblDueDateKey.Size = new System.Drawing.Size(160, 28);
            lblDueDateKey.TabIndex = 32;
            lblDueDateKey.Text = "Next Due Date";
            // 
            // lblDueDateValue
            // 
            lblDueDateValue.BackColor = System.Drawing.Color.Transparent;
            lblDueDateValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblDueDateValue.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);   // red for settlement urgency
            lblDueDateValue.Location = new System.Drawing.Point(831, 303);
            lblDueDateValue.Name = "lblDueDateValue";
            lblDueDateValue.Size = new System.Drawing.Size(200, 32);
            lblDueDateValue.TabIndex = 33;
            lblDueDateValue.Text = "Feb 1, 2026";
            lblDueDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sep2
            // 
            sep2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sep2.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            sep2.Location = new System.Drawing.Point(0, 474);
            sep2.Name = "sep2";
            sep2.Size = new System.Drawing.Size(1101, 1);
            sep2.TabIndex = 10;
            // 
            // btnContinue  (red accent for Full Settlement)
            // 
            btnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnContinue.BorderColor = System.Drawing.Color.Transparent;
            btnContinue.BorderRadius = 10;
            btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            btnContinue.CustomizableEdges = customizableEdges9;
            btnContinue.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);        // red
            btnContinue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnContinue.ForeColor = System.Drawing.Color.White;
            btnContinue.HoverState.BorderColor = System.Drawing.Color.Transparent;
            btnContinue.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnContinue.HoverState.ForeColor = System.Drawing.Color.White;
            btnContinue.Location = new System.Drawing.Point(807, 494);
            btnContinue.Name = "btnContinue";
            btnContinue.PressedColor = System.Drawing.Color.FromArgb(153, 27, 27);
            btnContinue.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnContinue.Size = new System.Drawing.Size(270, 55);
            btnContinue.TabIndex = 11;
            btnContinue.Text = "Continue to Configuration  →";
            btnContinue.Click += new System.EventHandler(BtnContinue_Click);
            // 
            // AccountSummaryForm4
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(242, 245, 250);
            Controls.Add(pnlBackground);
            Name = "AccountSummaryForm4";
            Size = new System.Drawing.Size(1221, 750);
            Load += new System.EventHandler(AccountSummaryForm4_Load);
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