namespace POSCashierSystem
{
    partial class PaymentConfigurationForm2
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            lblPageTitle = new Label();
            lblPageSubtitle = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            lblTransIcon = new Label();
            lblTransTitle = new Label();
            sepCardInner = new Guna.UI2.WinForms.Guna2Separator();
            pnlDownPaymentRow = new Guna.UI2.WinForms.Guna2Panel();
            lblDownPaymentKey = new Label();
            lblDownPaymentValue = new Label();
            sepTotalDue = new Guna.UI2.WinForms.Guna2Separator();
            lblTotalDueKey = new Label();
            lblTotalDueValue = new Label();
            btnProceed = new Guna.UI2.WinForms.Guna2Button();
            pnlBackground.SuspendLayout();
            pnlCard.SuspendLayout();
            pnlDownPaymentRow.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackground
            // 
            pnlBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlBackground.BackColor = Color.FromArgb(248, 250, 252);
            pnlBackground.Controls.Add(lblPageTitle);
            pnlBackground.Controls.Add(lblPageSubtitle);
            pnlBackground.Controls.Add(btnBack);
            pnlBackground.Controls.Add(pnlCard);
            pnlBackground.Controls.Add(btnProceed);
            pnlBackground.CustomizableEdges = customizableEdges9;
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Margin = new Padding(3, 4, 3, 4);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlBackground.Size = new Size(1347, 750);
            pnlBackground.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            lblPageTitle.BackColor = Color.Transparent;
            lblPageTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblPageTitle.Location = new Point(28, 18);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(320, 35);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Payment Configuration";
            // 
            // lblPageSubtitle
            // 
            lblPageSubtitle.BackColor = Color.Transparent;
            lblPageSubtitle.Font = new Font("Segoe UI", 9F);
            lblPageSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblPageSubtitle.Location = new Point(30, 55);
            lblPageSubtitle.Name = "lblPageSubtitle";
            lblPageSubtitle.Size = new Size(260, 22);
            lblPageSubtitle.TabIndex = 1;
            lblPageSubtitle.Text = "Review charges and discounts";
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
            btnBack.HoverState.ForeColor = Color.FromArgb(15, 23, 42);
            btnBack.Location = new Point(1243, 22);
            btnBack.Margin = new Padding(3, 4, 3, 4);
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
            pnlCard.Controls.Add(lblTransIcon);
            pnlCard.Controls.Add(lblTransTitle);
            pnlCard.Controls.Add(sepCardInner);
            pnlCard.Controls.Add(pnlDownPaymentRow);
            pnlCard.Controls.Add(sepTotalDue);
            pnlCard.Controls.Add(lblTotalDueKey);
            pnlCard.Controls.Add(lblTotalDueValue);
            pnlCard.CustomizableEdges = customizableEdges5;
            pnlCard.FillColor = Color.White;
            pnlCard.Location = new Point(412, 134);
            pnlCard.Margin = new Padding(3, 4, 3, 4);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.BorderRadius = 20;
            pnlCard.ShadowDecoration.Color = Color.FromArgb(35, 148, 163, 184);
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlCard.ShadowDecoration.Depth = 12;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Shadow = new Padding(0, 4, 14, 14);
            pnlCard.Size = new Size(716, 277);
            pnlCard.TabIndex = 1;
            pnlCard.Paint += pnlCard_Paint;
            // 
            // lblTransIcon
            // 
            lblTransIcon.BackColor = Color.Transparent;
            lblTransIcon.Font = new Font("Segoe UI Emoji", 14F);
            lblTransIcon.ForeColor = Color.FromArgb(139, 92, 246);
            lblTransIcon.Location = new Point(22, 22);
            lblTransIcon.Name = "lblTransIcon";
            lblTransIcon.Size = new Size(30, 35);
            lblTransIcon.TabIndex = 0;
            lblTransIcon.Text = "\U0001f9fe";
            // 
            // lblTransTitle
            // 
            lblTransTitle.BackColor = Color.Transparent;
            lblTransTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTransTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTransTitle.Location = new Point(58, 25);
            lblTransTitle.Name = "lblTransTitle";
            lblTransTitle.Size = new Size(250, 30);
            lblTransTitle.TabIndex = 1;
            lblTransTitle.Text = "Transaction Summary";
            // 
            // sepCardInner
            // 
            sepCardInner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sepCardInner.FillColor = Color.FromArgb(226, 232, 240);
            sepCardInner.Location = new Point(0, 70);
            sepCardInner.Margin = new Padding(3, 4, 3, 4);
            sepCardInner.Name = "sepCardInner";
            sepCardInner.Size = new Size(716, 1);
            sepCardInner.TabIndex = 2;
            // 
            // pnlDownPaymentRow
            // 
            pnlDownPaymentRow.BorderColor = Color.Transparent;
            pnlDownPaymentRow.BorderRadius = 10;
            pnlDownPaymentRow.Controls.Add(lblDownPaymentKey);
            pnlDownPaymentRow.Controls.Add(lblDownPaymentValue);
            pnlDownPaymentRow.CustomizableEdges = customizableEdges3;
            pnlDownPaymentRow.FillColor = Color.FromArgb(248, 250, 252);
            pnlDownPaymentRow.Location = new Point(89, 79);
            pnlDownPaymentRow.Margin = new Padding(3, 4, 3, 4);
            pnlDownPaymentRow.Name = "pnlDownPaymentRow";
            pnlDownPaymentRow.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlDownPaymentRow.Size = new Size(554, 55);
            pnlDownPaymentRow.TabIndex = 3;
            // 
            // lblDownPaymentKey
            // 
            lblDownPaymentKey.BackColor = Color.Transparent;
            lblDownPaymentKey.Font = new Font("Segoe UI", 10F);
            lblDownPaymentKey.ForeColor = Color.FromArgb(71, 85, 105);
            lblDownPaymentKey.Location = new Point(14, 14);
            lblDownPaymentKey.Name = "lblDownPaymentKey";
            lblDownPaymentKey.Size = new Size(205, 28);
            lblDownPaymentKey.TabIndex = 0;
            lblDownPaymentKey.Text = "Monthly Amorization";
            // 
            // lblDownPaymentValue
            // 
            lblDownPaymentValue.BackColor = Color.Transparent;
            lblDownPaymentValue.Font = new Font("Segoe UI", 10F);
            lblDownPaymentValue.ForeColor = Color.FromArgb(30, 41, 59);
            lblDownPaymentValue.Location = new Point(356, 11);
            lblDownPaymentValue.Name = "lblDownPaymentValue";
            lblDownPaymentValue.Size = new Size(178, 28);
            lblDownPaymentValue.TabIndex = 1;
            lblDownPaymentValue.Text = "₱53,400.00";
            lblDownPaymentValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // sepTotalDue
            // 
            sepTotalDue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sepTotalDue.FillColor = Color.FromArgb(241, 245, 249);
            sepTotalDue.Location = new Point(0, 152);
            sepTotalDue.Margin = new Padding(3, 4, 3, 4);
            sepTotalDue.Name = "sepTotalDue";
            sepTotalDue.Size = new Size(716, 1);
            sepTotalDue.TabIndex = 4;
            // 
            // lblTotalDueKey
            // 
            lblTotalDueKey.BackColor = Color.Transparent;
            lblTotalDueKey.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalDueKey.ForeColor = Color.FromArgb(15, 23, 42);
            lblTotalDueKey.Location = new Point(89, 196);
            lblTotalDueKey.Name = "lblTotalDueKey";
            lblTotalDueKey.Size = new Size(181, 43);
            lblTotalDueKey.TabIndex = 5;
            lblTotalDueKey.Text = "Total Due";
            // 
            // lblTotalDueValue
            // 
            lblTotalDueValue.BackColor = Color.Transparent;
            lblTotalDueValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalDueValue.ForeColor = Color.FromArgb(139, 92, 246);
            lblTotalDueValue.Location = new Point(365, 196);
            lblTotalDueValue.Name = "lblTotalDueValue";
            lblTotalDueValue.Size = new Size(278, 45);
            lblTotalDueValue.TabIndex = 6;
            lblTotalDueValue.Text = "₱53,400.00";
            lblTotalDueValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnProceed
            // 
            btnProceed.BorderColor = Color.Transparent;
            btnProceed.BorderRadius = 14;
            btnProceed.Cursor = Cursors.Hand;
            btnProceed.CustomizableEdges = customizableEdges7;
            btnProceed.FillColor = Color.FromArgb(139, 92, 246);
            btnProceed.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            btnProceed.ForeColor = Color.White;
            btnProceed.HoverState.BorderColor = Color.Transparent;
            btnProceed.HoverState.FillColor = Color.FromArgb(124, 58, 237);
            btnProceed.HoverState.ForeColor = Color.White;
            btnProceed.Location = new Point(412, 420);
            btnProceed.Margin = new Padding(3, 4, 3, 4);
            btnProceed.Name = "btnProceed";
            btnProceed.PressedColor = Color.FromArgb(109, 40, 217);
            btnProceed.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnProceed.Size = new Size(716, 80);
            btnProceed.TabIndex = 2;
            btnProceed.Text = "Proceed to Collection  →";
            btnProceed.Click += BtnProceed_Click;
            // 
            // PaymentConfigurationForm2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(pnlBackground);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PaymentConfigurationForm2";
            Size = new Size(1347, 750);
            Load += PaymentConfigurationForm_Load;
            pnlBackground.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            pnlDownPaymentRow.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ── Designer field declarations ───────────────────────────────────────
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private System.Windows.Forms.Label lblTransIcon;
        private System.Windows.Forms.Label lblTransTitle;
        private Guna.UI2.WinForms.Guna2Separator sepCardInner;
        private Guna.UI2.WinForms.Guna2Panel pnlDownPaymentRow;
        private System.Windows.Forms.Label lblDownPaymentKey;
        private System.Windows.Forms.Label lblDownPaymentValue;
        private Guna.UI2.WinForms.Guna2Separator sepTotalDue;
        private System.Windows.Forms.Label lblTotalDueKey;
        private System.Windows.Forms.Label lblTotalDueValue;
        private Guna.UI2.WinForms.Guna2Button btnProceed;
    }
}