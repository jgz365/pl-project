namespace POSCashierSystem
{
    partial class PaymentConfigurationForm3
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
            Guna.UI2.WinForms.Suite.CustomizableEdges ce1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

            pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            lblPageTitle = new System.Windows.Forms.Label();
            lblPageSubtitle = new System.Windows.Forms.Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();

            // ── Settings card (top) ──────────────────────────────────────────
            pnlSettingsCard = new Guna.UI2.WinForms.Guna2Panel();
            lblSettingsTitle = new System.Windows.Forms.Label();
            lblMonthsLabel = new System.Windows.Forms.Label();
            btnMinus = new Guna.UI2.WinForms.Guna2Button();
            lblMonthCount = new System.Windows.Forms.Label();
            btnPlus = new Guna.UI2.WinForms.Guna2Button();

            // ── Summary card (bottom) ────────────────────────────────────────
            pnlSummaryCard = new Guna.UI2.WinForms.Guna2Panel();
            lblTransIcon = new System.Windows.Forms.Label();
            lblTransTitle = new System.Windows.Forms.Label();
            sepCardInner = new Guna.UI2.WinForms.Guna2Separator();
            pnlAmortRow = new Guna.UI2.WinForms.Guna2Panel();
            lblAmortKey = new System.Windows.Forms.Label();
            lblAmortValue = new System.Windows.Forms.Label();
            sepTotalDue = new Guna.UI2.WinForms.Guna2Separator();
            lblTotalDueKey = new System.Windows.Forms.Label();
            lblTotalDueValue = new System.Windows.Forms.Label();

            btnProceed = new Guna.UI2.WinForms.Guna2Button();

            pnlBackground.SuspendLayout();
            pnlSettingsCard.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            pnlAmortRow.SuspendLayout();
            SuspendLayout();

            // ─── pnlBackground ───────────────────────────────────────────────
            pnlBackground.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                 | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlBackground.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            pnlBackground.Controls.Add(lblPageTitle);
            pnlBackground.Controls.Add(lblPageSubtitle);
            pnlBackground.Controls.Add(btnBack);
            pnlBackground.Controls.Add(pnlSettingsCard);
            pnlBackground.Controls.Add(pnlSummaryCard);
            pnlBackground.Controls.Add(btnProceed);
            pnlBackground.CustomizableEdges = ce1;
            pnlBackground.Location = new System.Drawing.Point(0, 0);
            pnlBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = ce2;
            pnlBackground.Size = new System.Drawing.Size(1347, 750);
            pnlBackground.TabIndex = 0;

            // ─── lblPageTitle ─────────────────────────────────────────────────
            lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new System.Drawing.Point(28, 18);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new System.Drawing.Size(340, 35);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Payment Configuration";

            // ─── lblPageSubtitle ──────────────────────────────────────────────
            lblPageSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new System.Drawing.Point(30, 55);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new System.Drawing.Size(280, 22);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Review charges and discounts";

            // ─── btnBack ─────────────────────────────────────────────────────
            btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnBack.BorderColor = System.Drawing.Color.Transparent;
            btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            btnBack.CustomizableEdges = ce3;
            btnBack.FillColor = System.Drawing.Color.Transparent;
            btnBack.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            btnBack.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            btnBack.HoverState.BorderColor = System.Drawing.Color.Transparent;
            btnBack.HoverState.FillColor = System.Drawing.Color.Transparent;
            btnBack.HoverState.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            btnBack.Location = new System.Drawing.Point(1243, 22);
            btnBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.PressedColor = System.Drawing.Color.Transparent;
            btnBack.ShadowDecoration.CustomizableEdges = ce4;
            btnBack.Size = new System.Drawing.Size(80, 40);
            btnBack.TabIndex = 0;
            btnBack.Text = "← Back";
            btnBack.Click += BtnBack_Click;

            // ─── pnlSettingsCard ─────────────────────────────────────────────
            pnlSettingsCard.BackColor = System.Drawing.Color.Transparent;
            pnlSettingsCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSettingsCard.BorderRadius = 20;
            pnlSettingsCard.BorderThickness = 1;
            pnlSettingsCard.Controls.Add(lblSettingsTitle);
            pnlSettingsCard.Controls.Add(lblMonthsLabel);
            pnlSettingsCard.Controls.Add(btnMinus);
            pnlSettingsCard.Controls.Add(lblMonthCount);
            pnlSettingsCard.Controls.Add(btnPlus);
            pnlSettingsCard.CustomizableEdges = ce5;
            pnlSettingsCard.FillColor = System.Drawing.Color.White;
            pnlSettingsCard.Location = new System.Drawing.Point(412, 100);
            pnlSettingsCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pnlSettingsCard.Name = "pnlSettingsCard";
            pnlSettingsCard.ShadowDecoration.BorderRadius = 20;
            pnlSettingsCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(30, 148, 163, 184);
            pnlSettingsCard.ShadowDecoration.CustomizableEdges = ce6;
            pnlSettingsCard.ShadowDecoration.Depth = 10;
            pnlSettingsCard.ShadowDecoration.Enabled = true;
            pnlSettingsCard.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 4, 12, 12);
            pnlSettingsCard.Size = new System.Drawing.Size(716, 100);
            pnlSettingsCard.TabIndex = 1;

            // ─── lblSettingsTitle ─────────────────────────────────────────────
            lblSettingsTitle.BackColor = System.Drawing.Color.Transparent;
            lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblSettingsTitle.Location = new System.Drawing.Point(22, 22);
            lblSettingsTitle.Name = "lblSettingsTitle";
            lblSettingsTitle.Size = new System.Drawing.Size(220, 28);
            lblSettingsTitle.TabIndex = 0;
            lblSettingsTitle.Text = "Advance Payment Settings";

            // ─── lblMonthsLabel ───────────────────────────────────────────────
            lblMonthsLabel.BackColor = System.Drawing.Color.Transparent;
            lblMonthsLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblMonthsLabel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblMonthsLabel.Location = new System.Drawing.Point(22, 58);
            lblMonthsLabel.Name = "lblMonthsLabel";
            lblMonthsLabel.Size = new System.Drawing.Size(140, 26);
            lblMonthsLabel.TabIndex = 1;
            lblMonthsLabel.Text = "Number of Months:";

            // ─── btnMinus ─────────────────────────────────────────────────────
            btnMinus.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnMinus.BorderRadius = 8;
            btnMinus.BorderThickness = 1;
            btnMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            btnMinus.CustomizableEdges = ce7;
            btnMinus.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnMinus.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            btnMinus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnMinus.HoverState.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnMinus.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnMinus.HoverState.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            btnMinus.Location = new System.Drawing.Point(180, 50);
            btnMinus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnMinus.Name = "btnMinus";
            btnMinus.PressedColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnMinus.ShadowDecoration.CustomizableEdges = ce8;
            btnMinus.Size = new System.Drawing.Size(40, 36);
            btnMinus.TabIndex = 2;
            btnMinus.Text = "−";
            btnMinus.Click += BtnMinus_Click;

            // ─── lblMonthCount ────────────────────────────────────────────────
            lblMonthCount.BackColor = System.Drawing.Color.Transparent;
            lblMonthCount.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblMonthCount.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblMonthCount.Location = new System.Drawing.Point(228, 52);
            lblMonthCount.Name = "lblMonthCount";
            lblMonthCount.Size = new System.Drawing.Size(44, 32);
            lblMonthCount.TabIndex = 3;
            lblMonthCount.Text = "1";
            lblMonthCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ─── btnPlus ──────────────────────────────────────────────────────
            btnPlus.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnPlus.BorderRadius = 8;
            btnPlus.BorderThickness = 1;
            btnPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            btnPlus.CustomizableEdges = ce9;
            btnPlus.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnPlus.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            btnPlus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnPlus.HoverState.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnPlus.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnPlus.HoverState.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            btnPlus.Location = new System.Drawing.Point(280, 50);
            btnPlus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnPlus.Name = "btnPlus";
            btnPlus.PressedColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnPlus.ShadowDecoration.CustomizableEdges = ce10;
            btnPlus.Size = new System.Drawing.Size(40, 36);
            btnPlus.TabIndex = 4;
            btnPlus.Text = "+";
            btnPlus.Click += BtnPlus_Click;

            // ─── pnlSummaryCard ───────────────────────────────────────────────
            pnlSummaryCard.BackColor = System.Drawing.Color.Transparent;
            pnlSummaryCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pnlSummaryCard.BorderRadius = 20;
            pnlSummaryCard.BorderThickness = 1;
            pnlSummaryCard.Controls.Add(lblTransIcon);
            pnlSummaryCard.Controls.Add(lblTransTitle);
            pnlSummaryCard.Controls.Add(sepCardInner);
            pnlSummaryCard.Controls.Add(pnlAmortRow);
            pnlSummaryCard.Controls.Add(sepTotalDue);
            pnlSummaryCard.Controls.Add(lblTotalDueKey);
            pnlSummaryCard.Controls.Add(lblTotalDueValue);
            pnlSummaryCard.CustomizableEdges = ce11;
            pnlSummaryCard.FillColor = System.Drawing.Color.White;
            pnlSummaryCard.Location = new System.Drawing.Point(412, 218);
            pnlSummaryCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.ShadowDecoration.BorderRadius = 20;
            pnlSummaryCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(35, 148, 163, 184);
            pnlSummaryCard.ShadowDecoration.CustomizableEdges = ce12;
            pnlSummaryCard.ShadowDecoration.Depth = 12;
            pnlSummaryCard.ShadowDecoration.Enabled = true;
            pnlSummaryCard.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 4, 14, 14);
            pnlSummaryCard.Size = new System.Drawing.Size(716, 277);
            pnlSummaryCard.TabIndex = 2;
            pnlSummaryCard.Paint += pnlSummaryCard_Paint;

            // ─── lblTransIcon ─────────────────────────────────────────────────
            lblTransIcon.BackColor = System.Drawing.Color.Transparent;
            lblTransIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            lblTransIcon.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            lblTransIcon.Location = new System.Drawing.Point(22, 22);
            lblTransIcon.Name = "lblTransIcon";
            lblTransIcon.Size = new System.Drawing.Size(30, 35);
            lblTransIcon.TabIndex = 0;
            lblTransIcon.Text = "\U0001f9fe";

            // ─── lblTransTitle ────────────────────────────────────────────────
            lblTransTitle.BackColor = System.Drawing.Color.Transparent;
            lblTransTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTransTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTransTitle.Location = new System.Drawing.Point(58, 25);
            lblTransTitle.Name = "lblTransTitle";
            lblTransTitle.Size = new System.Drawing.Size(250, 30);
            lblTransTitle.TabIndex = 1;
            lblTransTitle.Text = "Transaction Summary";

            // ─── sepCardInner ─────────────────────────────────────────────────
            sepCardInner.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sepCardInner.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            sepCardInner.Location = new System.Drawing.Point(0, 70);
            sepCardInner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            sepCardInner.Name = "sepCardInner";
            sepCardInner.Size = new System.Drawing.Size(716, 1);
            sepCardInner.TabIndex = 2;

            // ─── pnlAmortRow ──────────────────────────────────────────────────
            pnlAmortRow.BorderColor = System.Drawing.Color.Transparent;
            pnlAmortRow.BorderRadius = 10;
            pnlAmortRow.Controls.Add(lblAmortKey);
            pnlAmortRow.Controls.Add(lblAmortValue);
            pnlAmortRow.CustomizableEdges = ce13;
            pnlAmortRow.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            pnlAmortRow.Location = new System.Drawing.Point(89, 79);
            pnlAmortRow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pnlAmortRow.Name = "pnlAmortRow";
            pnlAmortRow.Size = new System.Drawing.Size(554, 55);
            pnlAmortRow.TabIndex = 3;

            // ─── lblAmortKey ──────────────────────────────────────────────────
            lblAmortKey.BackColor = System.Drawing.Color.Transparent;
            lblAmortKey.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblAmortKey.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblAmortKey.Location = new System.Drawing.Point(14, 14);
            lblAmortKey.Name = "lblAmortKey";
            lblAmortKey.Size = new System.Drawing.Size(240, 28);
            lblAmortKey.TabIndex = 0;
            lblAmortKey.Text = "Monthly Amortization (x1)";

            // ─── lblAmortValue ────────────────────────────────────────────────
            lblAmortValue.BackColor = System.Drawing.Color.Transparent;
            lblAmortValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblAmortValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblAmortValue.Location = new System.Drawing.Point(356, 11);
            lblAmortValue.Name = "lblAmortValue";
            lblAmortValue.Size = new System.Drawing.Size(178, 28);
            lblAmortValue.TabIndex = 1;
            lblAmortValue.Text = "₱0.00";
            lblAmortValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ─── sepTotalDue ──────────────────────────────────────────────────
            sepTotalDue.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sepTotalDue.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            sepTotalDue.Location = new System.Drawing.Point(0, 152);
            sepTotalDue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            sepTotalDue.Name = "sepTotalDue";
            sepTotalDue.Size = new System.Drawing.Size(716, 1);
            sepTotalDue.TabIndex = 4;

            // ─── lblTotalDueKey ───────────────────────────────────────────────
            lblTotalDueKey.BackColor = System.Drawing.Color.Transparent;
            lblTotalDueKey.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTotalDueKey.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblTotalDueKey.Location = new System.Drawing.Point(89, 196);
            lblTotalDueKey.Name = "lblTotalDueKey";
            lblTotalDueKey.Size = new System.Drawing.Size(181, 43);
            lblTotalDueKey.TabIndex = 5;
            lblTotalDueKey.Text = "Total Due";

            // ─── lblTotalDueValue ─────────────────────────────────────────────
            lblTotalDueValue.BackColor = System.Drawing.Color.Transparent;
            lblTotalDueValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTotalDueValue.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);    // teal for Advance
            lblTotalDueValue.Location = new System.Drawing.Point(365, 196);
            lblTotalDueValue.Name = "lblTotalDueValue";
            lblTotalDueValue.Size = new System.Drawing.Size(278, 45);
            lblTotalDueValue.TabIndex = 6;
            lblTotalDueValue.Text = "₱0.00";
            lblTotalDueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ─── btnProceed ───────────────────────────────────────────────────
            btnProceed.BorderColor = System.Drawing.Color.Transparent;
            btnProceed.BorderRadius = 14;
            btnProceed.Cursor = System.Windows.Forms.Cursors.Hand;
            btnProceed.CustomizableEdges = ce14;
            btnProceed.FillColor = System.Drawing.Color.FromArgb(5, 150, 105);           // teal
            btnProceed.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            btnProceed.ForeColor = System.Drawing.Color.White;
            btnProceed.HoverState.BorderColor = System.Drawing.Color.Transparent;
            btnProceed.HoverState.FillColor = System.Drawing.Color.FromArgb(4, 120, 87);
            btnProceed.HoverState.ForeColor = System.Drawing.Color.White;
            btnProceed.Location = new System.Drawing.Point(412, 514);
            btnProceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnProceed.Name = "btnProceed";
            btnProceed.PressedColor = System.Drawing.Color.FromArgb(3, 100, 75);
            btnProceed.Size = new System.Drawing.Size(716, 80);
            btnProceed.TabIndex = 3;
            btnProceed.Text = "Proceed to Collection  →";
            btnProceed.Click += BtnProceed_Click;

            // ─── PaymentConfigurationForm3 ────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            Controls.Add(pnlBackground);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "PaymentConfigurationForm3";
            Size = new System.Drawing.Size(1347, 750);
            Load += PaymentConfigurationForm3_Load;

            pnlBackground.ResumeLayout(false);
            pnlSettingsCard.ResumeLayout(false);
            pnlSummaryCard.ResumeLayout(false);
            pnlAmortRow.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ── Field declarations ────────────────────────────────────────────────
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;

        private Guna.UI2.WinForms.Guna2Panel pnlSettingsCard;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Label lblMonthsLabel;
        private Guna.UI2.WinForms.Guna2Button btnMinus;
        private System.Windows.Forms.Label lblMonthCount;
        private Guna.UI2.WinForms.Guna2Button btnPlus;

        private Guna.UI2.WinForms.Guna2Panel pnlSummaryCard;
        private System.Windows.Forms.Label lblTransIcon;
        private System.Windows.Forms.Label lblTransTitle;
        private Guna.UI2.WinForms.Guna2Separator sepCardInner;
        private Guna.UI2.WinForms.Guna2Panel pnlAmortRow;
        private System.Windows.Forms.Label lblAmortKey;
        private System.Windows.Forms.Label lblAmortValue;
        private Guna.UI2.WinForms.Guna2Separator sepTotalDue;
        private System.Windows.Forms.Label lblTotalDueKey;
        private System.Windows.Forms.Label lblTotalDueValue;

        private Guna.UI2.WinForms.Guna2Button btnProceed;
    }
}