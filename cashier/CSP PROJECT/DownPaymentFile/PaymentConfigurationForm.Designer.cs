namespace POSCashierSystem
{
    partial class PaymentConfigurationForm
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
            // ── Declare all controls ──────────────────────────────────────────
            this.pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();

            // White summary card
            this.pnlCard = new Guna.UI2.WinForms.Guna2Panel();

            // Card header row  (icon + "Transaction Summary")
            this.lblTransIcon = new System.Windows.Forms.Label();
            this.lblTransTitle = new System.Windows.Forms.Label();

            // Divider inside card
            this.sepCardInner = new Guna.UI2.WinForms.Guna2Separator();

            // Down Payment row  (light grey pill)
            this.pnlDownPaymentRow = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDownPaymentKey = new System.Windows.Forms.Label();
            this.lblDownPaymentValue = new System.Windows.Forms.Label();

            // Divider before Total Due
            this.sepTotalDue = new Guna.UI2.WinForms.Guna2Separator();

            // Total Due row
            this.lblTotalDueKey = new System.Windows.Forms.Label();
            this.lblTotalDueValue = new System.Windows.Forms.Label();

            // Purple "Proceed" button — sits BELOW the card, same width
            this.btnProceed = new Guna.UI2.WinForms.Guna2Button();

            // ── Suspend ───────────────────────────────────────────────────────
            this.pnlBackground.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlDownPaymentRow.SuspendLayout();
            this.SuspendLayout();

            // =================================================================
            // UserControl root  –  900 × 600 for a clear Designer preview
            // =================================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); // #F8FAFC
            this.Name = "PaymentConfigurationForm";
            this.Size = new System.Drawing.Size(900, 600);
            this.Load += new System.EventHandler(this.PaymentConfigurationForm_Load);
            this.Controls.Add(this.pnlBackground);

            // =================================================================
            // pnlBackground  –  matches UserControl size, expands with anchors
            // =================================================================
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
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
            this.pnlBackground.Controls.Add(this.btnProceed);

            // =================================================================
            // Top-bar  –  title, subtitle, back button
            // =================================================================

            // lblPageTitle
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Text = "Payment Configuration";
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);   // near-black
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Location = new System.Drawing.Point(28, 14);
            this.lblPageTitle.Size = new System.Drawing.Size(320, 28);
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left;

            // lblPageSubtitle
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Text = "Review charges and discounts";
            this.lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // #64748B
            this.lblPageSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageSubtitle.Location = new System.Drawing.Point(30, 44);
            this.lblPageSubtitle.Size = new System.Drawing.Size(260, 18);
            this.lblPageSubtitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                           | System.Windows.Forms.AnchorStyles.Left;

            // btnBack  –  top-right ghost button
            this.btnBack.Name = "btnBack";
            this.btnBack.Text = "← Back";
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnBack.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.BorderColor = System.Drawing.Color.Transparent;
            this.btnBack.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.HoverState.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
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
            // pnlCard  –  white rounded card, 530 × 210
            // CenterCard() positions it at runtime; (185, 90) is Designer preview
            // =================================================================
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.FillColor = System.Drawing.Color.White;
            this.pnlCard.BorderRadius = 20;
            this.pnlCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlCard.BorderThickness = 1;
            this.pnlCard.ShadowDecoration.Enabled = true;
            this.pnlCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(35, 148, 163, 184);
            this.pnlCard.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 4, 14, 14);
            this.pnlCard.ShadowDecoration.Depth = 12;
            this.pnlCard.ShadowDecoration.BorderRadius = 20;
            this.pnlCard.Location = new System.Drawing.Point(185, 90);
            this.pnlCard.Size = new System.Drawing.Size(530, 210);
            this.pnlCard.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                       | System.Windows.Forms.AnchorStyles.Left;
            this.pnlCard.TabIndex = 1;
            this.pnlCard.Controls.Add(this.lblTransIcon);
            this.pnlCard.Controls.Add(this.lblTransTitle);
            this.pnlCard.Controls.Add(this.sepCardInner);
            this.pnlCard.Controls.Add(this.pnlDownPaymentRow);
            this.pnlCard.Controls.Add(this.sepTotalDue);
            this.pnlCard.Controls.Add(this.lblTotalDueKey);
            this.pnlCard.Controls.Add(this.lblTotalDueValue);

            // =================================================================
            // CARD HEADER  –  icon  +  "Transaction Summary"
            // =================================================================

            // lblTransIcon  –  Unicode receipt emoji stands in for the SVG icon
            this.lblTransIcon.Name = "lblTransIcon";
            this.lblTransIcon.Text = "🧾";
            this.lblTransIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.lblTransIcon.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246); // #8B5CF6 purple
            this.lblTransIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblTransIcon.Location = new System.Drawing.Point(22, 18);
            this.lblTransIcon.Size = new System.Drawing.Size(30, 28);
            this.lblTransIcon.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left;

            // lblTransTitle
            this.lblTransTitle.Name = "lblTransTitle";
            this.lblTransTitle.Text = "Transaction Summary";
            this.lblTransTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTransTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTransTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTransTitle.Location = new System.Drawing.Point(58, 20);
            this.lblTransTitle.Size = new System.Drawing.Size(250, 24);
            this.lblTransTitle.Anchor = System.Windows.Forms.AnchorStyles.Top
                                         | System.Windows.Forms.AnchorStyles.Left;

            // sepCardInner  –  thin divider under header
            this.sepCardInner.Name = "sepCardInner";
            this.sepCardInner.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.sepCardInner.Location = new System.Drawing.Point(0, 56);
            this.sepCardInner.Size = new System.Drawing.Size(530, 1);
            this.sepCardInner.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right;
            this.sepCardInner.TabIndex = 2;

            // =================================================================
            // pnlDownPaymentRow  –  light grey pill row  (y=68, 8px padding each side)
            // =================================================================
            this.pnlDownPaymentRow.Name = "pnlDownPaymentRow";
            this.pnlDownPaymentRow.FillColor = System.Drawing.Color.FromArgb(248, 250, 252); // #F8FAFC
            this.pnlDownPaymentRow.BorderRadius = 10;
            this.pnlDownPaymentRow.BorderColor = System.Drawing.Color.Transparent;
            this.pnlDownPaymentRow.Location = new System.Drawing.Point(14, 66);
            this.pnlDownPaymentRow.Size = new System.Drawing.Size(502, 44);
            this.pnlDownPaymentRow.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                | System.Windows.Forms.AnchorStyles.Left;
            this.pnlDownPaymentRow.TabIndex = 3;
            this.pnlDownPaymentRow.Controls.Add(this.lblDownPaymentKey);
            this.pnlDownPaymentRow.Controls.Add(this.lblDownPaymentValue);

            // lblDownPaymentKey  –  left side of the row
            this.lblDownPaymentKey.Name = "lblDownPaymentKey";
            this.lblDownPaymentKey.Text = "Down Payment";
            this.lblDownPaymentKey.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDownPaymentKey.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105); // #475569
            this.lblDownPaymentKey.BackColor = System.Drawing.Color.Transparent;
            this.lblDownPaymentKey.Location = new System.Drawing.Point(14, 11);
            this.lblDownPaymentKey.Size = new System.Drawing.Size(160, 22);
            this.lblDownPaymentKey.Anchor = System.Windows.Forms.AnchorStyles.Top
                                             | System.Windows.Forms.AnchorStyles.Left;

            // lblDownPaymentValue  –  right side, right-aligned
            this.lblDownPaymentValue.Name = "lblDownPaymentValue";
            this.lblDownPaymentValue.Text = "₱53,400.00";
            this.lblDownPaymentValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDownPaymentValue.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); // #1E293B
            this.lblDownPaymentValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDownPaymentValue.Location = new System.Drawing.Point(310, 11);
            this.lblDownPaymentValue.Size = new System.Drawing.Size(178, 22);
            this.lblDownPaymentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDownPaymentValue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                               | System.Windows.Forms.AnchorStyles.Left;

            // =================================================================
            // sepTotalDue  –  thin divider between row and Total Due
            // =================================================================
            this.sepTotalDue.Name = "sepTotalDue";
            this.sepTotalDue.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.sepTotalDue.Location = new System.Drawing.Point(0, 122);
            this.sepTotalDue.Size = new System.Drawing.Size(530, 1);
            this.sepTotalDue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                       | System.Windows.Forms.AnchorStyles.Left
                                       | System.Windows.Forms.AnchorStyles.Right;
            this.sepTotalDue.TabIndex = 4;

            // =================================================================
            // TOTAL DUE row  –  y=134 inside pnlCard
            // =================================================================

            // lblTotalDueKey  –  bold, left
            this.lblTotalDueKey.Name = "lblTotalDueKey";
            this.lblTotalDueKey.Text = "Total Due";
            this.lblTotalDueKey.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalDueKey.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTotalDueKey.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDueKey.Location = new System.Drawing.Point(28, 158);
            this.lblTotalDueKey.Size = new System.Drawing.Size(160, 28);
            this.lblTotalDueKey.Anchor = System.Windows.Forms.AnchorStyles.Top
                                          | System.Windows.Forms.AnchorStyles.Left;

            // lblTotalDueValue  –  large purple bold, right
            this.lblTotalDueValue.Name = "lblTotalDueValue";
            this.lblTotalDueValue.Text = "₱53,400.00";
            this.lblTotalDueValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalDueValue.ForeColor = System.Drawing.Color.FromArgb(139, 92, 246); // #8B5CF6
            this.lblTotalDueValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDueValue.Location = new System.Drawing.Point(238, 152);
            this.lblTotalDueValue.Size = new System.Drawing.Size(278, 36);
            this.lblTotalDueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalDueValue.Anchor = System.Windows.Forms.AnchorStyles.Top
                                            | System.Windows.Forms.AnchorStyles.Left;

            // =================================================================
            // btnProceed  –  full-width purple button, sits below pnlCard
            // Same X and width as pnlCard.  y = card.Bottom + 18
            // =================================================================
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Text = "Proceed to Collection  →";
            this.btnProceed.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnProceed.ForeColor = System.Drawing.Color.White;
            this.btnProceed.FillColor = System.Drawing.Color.FromArgb(139, 92, 246);  // #8B5CF6
            this.btnProceed.HoverState.FillColor = System.Drawing.Color.FromArgb(124, 58, 237);  // #7C3AED
            this.btnProceed.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnProceed.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.btnProceed.PressedColor = System.Drawing.Color.FromArgb(109, 40, 217);  // #6D28D9
            this.btnProceed.BorderColor = System.Drawing.Color.Transparent;
            this.btnProceed.BorderRadius = 14;
            this.btnProceed.Location = new System.Drawing.Point(185, 318);
            this.btnProceed.Size = new System.Drawing.Size(530, 52);
            this.btnProceed.Anchor = System.Windows.Forms.AnchorStyles.Top
                                                   | System.Windows.Forms.AnchorStyles.Left;
            this.btnProceed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProceed.TabIndex = 2;
            this.btnProceed.Click += new System.EventHandler(this.BtnProceed_Click);

            // ── Resume ────────────────────────────────────────────────────────
            this.pnlDownPaymentRow.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlBackground.ResumeLayout(false);
            this.ResumeLayout(false);
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