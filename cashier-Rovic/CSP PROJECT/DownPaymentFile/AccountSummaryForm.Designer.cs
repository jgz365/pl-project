namespace POSCashierSystem
{
    partial class AccountSummaryForm
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
            // ── Declare every control ─────────────────────────────────────────
            this.pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();

            this.pnlCard = new Guna.UI2.WinForms.Guna2Panel();

            // Header row inside card
            this.pnlAvatar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblInitials = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.chipTicket = new Guna.UI2.WinForms.Guna2Button();
            this.chipStatus = new Guna.UI2.WinForms.Guna2Button();

            // Separator 1
            this.sep1 = new Guna.UI2.WinForms.Guna2Separator();

            // Left column – UNIT DETAILS
            this.lblUnitTitle = new System.Windows.Forms.Label();
            this.lblModelKey = new System.Windows.Forms.Label();
            this.lblModelValue = new System.Windows.Forms.Label();
            this.lblColorKey = new System.Windows.Forms.Label();
            this.lblColorValue = new System.Windows.Forms.Label();
            this.lblEngineKey = new System.Windows.Forms.Label();
            this.lblEngineValue = new System.Windows.Forms.Label();

            // Right column – FINANCIAL STATUS
            this.lblFinTitle = new System.Windows.Forms.Label();
            this.lblAmortKey = new System.Windows.Forms.Label();
            this.lblAmortValue = new System.Windows.Forms.Label();

            // Separator 2
            this.sep2 = new Guna.UI2.WinForms.Guna2Separator();

            // Footer
            this.btnContinue = new Guna.UI2.WinForms.Guna2Button();

            // ── Suspend ───────────────────────────────────────────────────────
            this.pnlBackground.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlAvatar.SuspendLayout();
            this.SuspendLayout();

            // =================================================================
            // UserControl root
            // Size = 900 × 600 so the Designer shows a realistic preview.
            // At runtime Dock = Fill makes it expand to the host panel.
            // =================================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(242, 245, 250);
            this.Name = "AccountSummaryForm";
            this.Size = new System.Drawing.Size(900, 600);
            this.Load += new System.EventHandler(this.AccountSummaryForm_Load);
            this.Controls.Add(this.pnlBackground);

            // =================================================================
            // pnlBackground  –  fills the whole UserControl
            // =================================================================
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(242, 245, 250);
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Size = new System.Drawing.Size(900, 600);
            this.pnlBackground.Anchor = System.Windows.Forms.AnchorStyles.Top
                                         | System.Windows.Forms.AnchorStyles.Bottom
                                         | System.Windows.Forms.AnchorStyles.Left
                                         | System.Windows.Forms.AnchorStyles.Right;
            this.pnlBackground.TabIndex = 0;
            this.pnlBackground.Controls.Add(this.lblPageTitle);
            this.pnlBackground.Controls.Add(this.lblPageSubtitle);
            this.pnlBackground.Controls.Add(this.btnBack);
            this.pnlBackground.Controls.Add(this.pnlCard);

            // =================================================================
            // Top-bar labels + Back button
            // =================================================================

            // lblPageTitle
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Text = "Account Summary";
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Location = new System.Drawing.Point(28, 14);
            this.lblPageTitle.Size = new System.Drawing.Size(260, 26);
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left;

            // lblPageSubtitle
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Text = "Verify details before proceeding";
            this.lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPageSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageSubtitle.Location = new System.Drawing.Point(30, 42);
            this.lblPageSubtitle.Size = new System.Drawing.Size(260, 18);
            this.lblPageSubtitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                           | System.Windows.Forms.AnchorStyles.Left;

            // btnBack  –  anchored top-right
            this.btnBack.Name = "btnBack";
            this.btnBack.Text = "← Back";
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnBack.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.BorderColor = System.Drawing.Color.Transparent;
            this.btnBack.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.HoverState.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnBack.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.btnBack.PressedColor = System.Drawing.Color.Transparent;
            this.btnBack.Location = new System.Drawing.Point(796, 18);
            this.btnBack.Size = new System.Drawing.Size(80, 32);
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                | System.Windows.Forms.AnchorStyles.Right;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.TabIndex = 0;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);

            // =================================================================
            // pnlCard  –  white rounded card, 760 × 420, anchored top-left
            // CenterCard() repositions it at runtime; fixed here for Designer.
            // =================================================================
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.FillColor = System.Drawing.Color.White;
            this.pnlCard.BorderRadius = 20;
            this.pnlCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlCard.BorderThickness = 1;
            this.pnlCard.ShadowDecoration.Enabled = true;
            this.pnlCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(40, 148, 163, 184);
            this.pnlCard.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 4, 16, 16);
            this.pnlCard.ShadowDecoration.Depth = 15;
            this.pnlCard.ShadowDecoration.BorderRadius = 20;
            this.pnlCard.Location = new System.Drawing.Point(70, 82);  // CenterCard() overrides this
            this.pnlCard.Size = new System.Drawing.Size(760, 420);
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                       | System.Windows.Forms.AnchorStyles.Left;
            this.pnlCard.TabIndex = 1;
            // Add children in Z-order (bottom first)
            this.pnlCard.Controls.Add(this.btnContinue);
            this.pnlCard.Controls.Add(this.sep2);
            this.pnlCard.Controls.Add(this.lblAmortValue);
            this.pnlCard.Controls.Add(this.lblAmortKey);
            this.pnlCard.Controls.Add(this.lblFinTitle);
            this.pnlCard.Controls.Add(this.lblEngineValue);
            this.pnlCard.Controls.Add(this.lblEngineKey);
            this.pnlCard.Controls.Add(this.lblColorValue);
            this.pnlCard.Controls.Add(this.lblColorKey);
            this.pnlCard.Controls.Add(this.lblModelValue);
            this.pnlCard.Controls.Add(this.lblModelKey);
            this.pnlCard.Controls.Add(this.lblUnitTitle);
            this.pnlCard.Controls.Add(this.sep1);
            this.pnlCard.Controls.Add(this.chipStatus);
            this.pnlCard.Controls.Add(this.chipTicket);
            this.pnlCard.Controls.Add(this.lblCustomerName);
            this.pnlCard.Controls.Add(this.pnlAvatar);

            // =================================================================
            // CARD HEADER  (all coords relative to pnlCard)
            // =================================================================

            // pnlAvatar  –  72×72 circle at (24, 20)
            this.pnlAvatar.Name = "pnlAvatar";
            this.pnlAvatar.FillColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.pnlAvatar.BorderRadius = 36;
            this.pnlAvatar.Location = new System.Drawing.Point(24, 20);
            this.pnlAvatar.Size = new System.Drawing.Size(72, 72);
            this.pnlAvatar.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left;
            this.pnlAvatar.TabIndex = 0;
            this.pnlAvatar.Controls.Add(this.lblInitials);

            // lblInitials  –  fills pnlAvatar
            this.lblInitials.Name = "lblInitials";
            this.lblInitials.Text = "JD";
            this.lblInitials.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblInitials.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblInitials.BackColor = System.Drawing.Color.Transparent;
            this.lblInitials.Location = new System.Drawing.Point(0, 0);
            this.lblInitials.Size = new System.Drawing.Size(72, 72);
            this.lblInitials.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblCustomerName  –  beside avatar, top line
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Text = "Juan Dela Cruz";
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblCustomerName.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerName.Location = new System.Drawing.Point(112, 22);
            this.lblCustomerName.Size = new System.Drawing.Size(600, 30);
            this.lblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.Top
                                           | System.Windows.Forms.AnchorStyles.Left;

            // chipTicket  –  "KD-2025-001"  below name
            this.chipTicket.Name = "chipTicket";
            this.chipTicket.Text = "KD-2025-001";
            this.chipTicket.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chipTicket.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.chipTicket.FillColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.chipTicket.HoverState.FillColor = System.Drawing.Color.FromArgb(191, 219, 254);
            this.chipTicket.HoverState.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.chipTicket.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.chipTicket.BorderColor = System.Drawing.Color.Transparent;
            this.chipTicket.BorderRadius = 20;
            this.chipTicket.Location = new System.Drawing.Point(112, 60);
            this.chipTicket.Size = new System.Drawing.Size(112, 26);
            this.chipTicket.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                   | System.Windows.Forms.AnchorStyles.Left;
            this.chipTicket.Cursor = System.Windows.Forms.Cursors.Default;
            this.chipTicket.TabIndex = 1;

            // chipStatus  –  "DOWN PAYMENT"  beside chipTicket
            this.chipStatus.Name = "chipStatus";
            this.chipStatus.Text = "DOWN PAYMENT";
            this.chipStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chipStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.chipStatus.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.chipStatus.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.chipStatus.HoverState.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.chipStatus.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.chipStatus.HoverState.BorderColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.chipStatus.BorderThickness = 1;
            this.chipStatus.BorderRadius = 20;
            this.chipStatus.Location = new System.Drawing.Point(232, 60);
            this.chipStatus.Size = new System.Drawing.Size(130, 26);
            this.chipStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                   | System.Windows.Forms.AnchorStyles.Left;
            this.chipStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.chipStatus.TabIndex = 2;

            // =================================================================
            // sep1  –  full-width 1px divider at y=106
            // =================================================================
            this.sep1.Name = "sep1";
            this.sep1.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.sep1.Location = new System.Drawing.Point(0, 106);
            this.sep1.Size = new System.Drawing.Size(760, 1);
            this.sep1.Anchor = System.Windows.Forms.AnchorStyles.Top
                                | System.Windows.Forms.AnchorStyles.Left
                                | System.Windows.Forms.AnchorStyles.Right;
            this.sep1.TabIndex = 3;

            // =================================================================
            // CARD BODY  –  two columns starting at y=108
            //
            //  LEFT  column:  x=28  (keys) | x=178 (values)
            //  RIGHT column:  x=400 (keys) | x=570 (values, right-aligned to x=740)
            //  Row heights: title=28, row1=44 apart
            // =================================================================

            // ── Left column title ─────────────────────────────────────────────
            this.lblUnitTitle.Name = "lblUnitTitle";
            this.lblUnitTitle.Text = "UNIT DETAILS";
            this.lblUnitTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblUnitTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblUnitTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblUnitTitle.Location = new System.Drawing.Point(28, 122);
            this.lblUnitTitle.Size = new System.Drawing.Size(140, 16);
            this.lblUnitTitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left;

            // ── Row 1: Model ──────────────────────────────────────────────────
            this.lblModelKey.Name = "lblModelKey";
            this.lblModelKey.Text = "Model";
            this.lblModelKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblModelKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblModelKey.BackColor = System.Drawing.Color.Transparent;
            this.lblModelKey.Location = new System.Drawing.Point(28, 152);
            this.lblModelKey.Size = new System.Drawing.Size(120, 22);
            this.lblModelKey.Anchor = System.Windows.Forms.AnchorStyles.Top
                                       | System.Windows.Forms.AnchorStyles.Left;

            this.lblModelValue.Name = "lblModelValue";
            this.lblModelValue.Text = "Honda ADV 160";
            this.lblModelValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblModelValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblModelValue.BackColor = System.Drawing.Color.Transparent;
            this.lblModelValue.Location = new System.Drawing.Point(178, 152);
            this.lblModelValue.Size = new System.Drawing.Size(190, 22);
            this.lblModelValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblModelValue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                         | System.Windows.Forms.AnchorStyles.Left;

            // ── Row 2: Color ──────────────────────────────────────────────────
            this.lblColorKey.Name = "lblColorKey";
            this.lblColorKey.Text = "Color";
            this.lblColorKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblColorKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblColorKey.BackColor = System.Drawing.Color.Transparent;
            this.lblColorKey.Location = new System.Drawing.Point(28, 196);
            this.lblColorKey.Size = new System.Drawing.Size(120, 22);
            this.lblColorKey.Anchor = System.Windows.Forms.AnchorStyles.Top
                                       | System.Windows.Forms.AnchorStyles.Left;

            this.lblColorValue.Name = "lblColorValue";
            this.lblColorValue.Text = "Matte Black";
            this.lblColorValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblColorValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblColorValue.BackColor = System.Drawing.Color.Transparent;
            this.lblColorValue.Location = new System.Drawing.Point(178, 196);
            this.lblColorValue.Size = new System.Drawing.Size(190, 22);
            this.lblColorValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblColorValue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                         | System.Windows.Forms.AnchorStyles.Left;

            // ── Row 3: Engine No. ─────────────────────────────────────────────
            this.lblEngineKey.Name = "lblEngineKey";
            this.lblEngineKey.Text = "Engine No.";
            this.lblEngineKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblEngineKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblEngineKey.BackColor = System.Drawing.Color.Transparent;
            this.lblEngineKey.Location = new System.Drawing.Point(28, 240);
            this.lblEngineKey.Size = new System.Drawing.Size(120, 22);
            this.lblEngineKey.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left;

            this.lblEngineValue.Name = "lblEngineValue";
            this.lblEngineValue.Text = "K1Z-998877";
            this.lblEngineValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblEngineValue.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblEngineValue.BackColor = System.Drawing.Color.Transparent;
            this.lblEngineValue.Location = new System.Drawing.Point(178, 240);
            this.lblEngineValue.Size = new System.Drawing.Size(190, 22);
            this.lblEngineValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEngineValue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                          | System.Windows.Forms.AnchorStyles.Left;

            // ── Right column title ────────────────────────────────────────────
            this.lblFinTitle.Name = "lblFinTitle";
            this.lblFinTitle.Text = "FINANCIAL STATUS";
            this.lblFinTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblFinTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblFinTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFinTitle.Location = new System.Drawing.Point(400, 122);
            this.lblFinTitle.Size = new System.Drawing.Size(160, 16);
            this.lblFinTitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                       | System.Windows.Forms.AnchorStyles.Left;

            // ── Row 1: Monthly Amortization ───────────────────────────────────
            this.lblAmortKey.Name = "lblAmortKey";
            this.lblAmortKey.Text = "Monthly Amortization";
            this.lblAmortKey.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAmortKey.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAmortKey.BackColor = System.Drawing.Color.Transparent;
            this.lblAmortKey.Location = new System.Drawing.Point(400, 152);
            this.lblAmortKey.Size = new System.Drawing.Size(160, 22);
            this.lblAmortKey.Anchor = System.Windows.Forms.AnchorStyles.Top
                                       | System.Windows.Forms.AnchorStyles.Left;

            // Value – right-aligned, ends at x=736
            this.lblAmortValue.Name = "lblAmortValue";
            this.lblAmortValue.Text = "₱5,364.72";
            this.lblAmortValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAmortValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblAmortValue.BackColor = System.Drawing.Color.Transparent;
            this.lblAmortValue.Location = new System.Drawing.Point(570, 150);
            this.lblAmortValue.Size = new System.Drawing.Size(166, 26);
            this.lblAmortValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAmortValue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                         | System.Windows.Forms.AnchorStyles.Left;

            // =================================================================
            // sep2  –  full-width 1px divider at y=338
            // =================================================================
            this.sep2.Name = "sep2";
            this.sep2.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.sep2.Location = new System.Drawing.Point(0, 338);
            this.sep2.Size = new System.Drawing.Size(760, 1);
            this.sep2.Anchor = System.Windows.Forms.AnchorStyles.Top
                                | System.Windows.Forms.AnchorStyles.Left
                                | System.Windows.Forms.AnchorStyles.Right;
            this.sep2.TabIndex = 10;

            // =================================================================
            // btnContinue  –  anchored bottom-right inside card
            // =================================================================
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Text = "Continue to Configuration  →";
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnContinue.HoverState.FillColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnContinue.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnContinue.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.btnContinue.PressedColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnContinue.BorderColor = System.Drawing.Color.Transparent;
            this.btnContinue.BorderRadius = 10;
            this.btnContinue.Location = new System.Drawing.Point(490, 356);
            this.btnContinue.Size = new System.Drawing.Size(252, 44);
            this.btnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                                                    | System.Windows.Forms.AnchorStyles.Right;
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinue.TabIndex = 11;
            this.btnContinue.Click += new System.EventHandler(this.BtnContinue_Click);

            // ── Resume ────────────────────────────────────────────────────────
            this.pnlAvatar.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlBackground.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // ── Designer-visible field declarations ───────────────────────────────
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2Panel pnlAvatar;
        private System.Windows.Forms.Label lblInitials;
        private System.Windows.Forms.Label lblCustomerName;
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
        private System.Windows.Forms.Label lblAmortKey;
        private System.Windows.Forms.Label lblAmortValue;
        private Guna.UI2.WinForms.Guna2Separator sep2;
        private Guna.UI2.WinForms.Guna2Button btnContinue;
    }
}