// ══════════════════════════════════════════════════════════════════════════
//  Form_TransactionHistory.Designer.cs  — FIXED VERSION
//
//  KEY FIXES vs. previous version:
//  1. Form uses TransparencyKey = Color.Magenta so rounded corners are
//     clipped correctly (no square white background showing).
//  2. pnlCard is the sole visual surface — BackColor = Magenta (transparent
//     key), BorderRadius = 20, so Guna clips the corners properly.
//  3. No hardcoded transactions — data comes from TransactionStore (shared
//     static list) or starts empty; sub-forms push rows into the store.
//  4. All layout positions recalculated to match reference screenshot 1:1.
// ══════════════════════════════════════════════════════════════════════════

namespace POSCashierSystem
{
    partial class Form_TransactionHistory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            pnlTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            picIcon = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            pnlDivider1 = new System.Windows.Forms.Panel();
            txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            lblTypeLabel = new System.Windows.Forms.Label();
            pnlTypePills = new System.Windows.Forms.Panel();
            lblStatusLabel = new System.Windows.Forms.Label();
            pnlStatusPills = new System.Windows.Forms.Panel();
            pnlDivider2 = new System.Windows.Forms.Panel();
            pnlColHeader = new System.Windows.Forms.Panel();
            lblColId = new System.Windows.Forms.Label();
            lblColTime = new System.Windows.Forms.Label();
            lblColType = new System.Windows.Forms.Label();
            lblColCustomer = new System.Windows.Forms.Label();
            lblColAmount = new System.Windows.Forms.Label();
            lblColStatus = new System.Windows.Forms.Label();
            pnlDivider3 = new System.Windows.Forms.Panel();
            flowRows = new System.Windows.Forms.FlowLayoutPanel();
            pnlFooter = new System.Windows.Forms.Panel();
            lblTxCount = new System.Windows.Forms.Label();
            lblTotalLabel = new System.Windows.Forms.Label();
            lblTotalAmt = new System.Windows.Forms.Label();

            pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(picIcon)).BeginInit();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════════
            //  FORM  — transparent key lets Guna round the corners cleanly
            // ══════════════════════════════════════════════════════════════
            this.Text = "Transaction History";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.Magenta;   // transparency key
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.KeyPreview = true;
            this.ShowInTaskbar = false;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_TransactionHistory_KeyDown);

            // ══════════════════════════════════════════════════════════════
            //  pnlCard — the white rounded card that IS the visible window
            // ══════════════════════════════════════════════════════════════
            pnlCard.Location = new System.Drawing.Point(0, 0);
            pnlCard.Size = new System.Drawing.Size(900, 640);
            pnlCard.BorderRadius = 20;
            pnlCard.FillColor = System.Drawing.Color.White;
            pnlCard.BorderColor = System.Drawing.Color.FromArgb(222, 226, 238);
            pnlCard.BorderThickness = 1;
            // drop-shadow so the card lifts off the dimmed background
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(60, 0, 0, 0);
            pnlCard.ShadowDecoration.Depth = 18;
            pnlCard.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 6, 28, 28);

            // ── Title bar (drag region) ───────────────────────────────────
            pnlTitleBar.FillColor = System.Drawing.Color.Transparent;
            pnlTitleBar.BorderColor = System.Drawing.Color.Transparent;
            pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            pnlTitleBar.Size = new System.Drawing.Size(800, 74);
            pnlTitleBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
            pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlTitleBar_MouseDown);

            // ── Icon circle ───────────────────────────────────────────────
            picIcon.FillColor = System.Drawing.Color.FromArgb(232, 246, 255);
            picIcon.Size = new System.Drawing.Size(42, 42);
            picIcon.Location = new System.Drawing.Point(24, 16);
            picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            picIcon.BackColor = System.Drawing.Color.Transparent;

            // ── Title ─────────────────────────────────────────────────────
            lblTitle.Text = "Transaction History";
            lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(22, 28, 45);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(76, 14);
            lblTitle.BackColor = System.Drawing.Color.Transparent;

            // ── Subtitle ──────────────────────────────────────────────────
            lblSubtitle.Text = "All recent transactions for this session";
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9f);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(140, 148, 170);
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new System.Drawing.Point(77, 38);
            lblSubtitle.BackColor = System.Drawing.Color.Transparent;

            // ── Close button ──────────────────────────────────────────────
            btnClose.Size = new System.Drawing.Size(34, 34);
            btnClose.Location = new System.Drawing.Point(852, 20);
            btnClose.Text = "✕";
            btnClose.Font = new System.Drawing.Font("Segoe UI", 11f);
            btnClose.ForeColor = System.Drawing.Color.FromArgb(130, 140, 160);
            btnClose.FillColor = System.Drawing.Color.FromArgb(242, 244, 248);
            btnClose.BorderRadius = 17;
            btnClose.BorderColor = System.Drawing.Color.Transparent;
            btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            btnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            btnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);

            // ── Divider 1 (below header) ──────────────────────────────────
            pnlDivider1.BackColor = System.Drawing.Color.FromArgb(232, 235, 245);
            pnlDivider1.Location = new System.Drawing.Point(0, 74);
            pnlDivider1.Size = new System.Drawing.Size(900, 1);

            // ── Search box ────────────────────────────────────────────────
            txtSearch.PlaceholderText = "  🔍  Search by ID, customer or unit model…";
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 10f);
            txtSearch.ForeColor = System.Drawing.Color.FromArgb(40, 50, 70);
            txtSearch.FillColor = System.Drawing.Color.FromArgb(246, 248, 252);
            txtSearch.BorderColor = System.Drawing.Color.FromArgb(220, 224, 236);
            txtSearch.BorderRadius = 10;
            txtSearch.BorderThickness = 1;
            txtSearch.Location = new System.Drawing.Point(24, 90);
            txtSearch.Size = new System.Drawing.Size(852, 40);
            txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);

            // ── TYPE label ────────────────────────────────────────────────
            lblTypeLabel.Text = "TYPE";
            lblTypeLabel.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            lblTypeLabel.ForeColor = System.Drawing.Color.FromArgb(160, 168, 190);
            lblTypeLabel.AutoSize = true;
            lblTypeLabel.Location = new System.Drawing.Point(24, 146);
            lblTypeLabel.BackColor = System.Drawing.Color.Transparent;

            // ── Type pill strip ───────────────────────────────────────────
            pnlTypePills.Location = new System.Drawing.Point(24, 163);
            pnlTypePills.Size = new System.Drawing.Size(852, 34);
            pnlTypePills.BackColor = System.Drawing.Color.Transparent;
            pnlTypePills.AutoScroll = false;

            // ── STATUS label ──────────────────────────────────────────────
            lblStatusLabel.Text = "STATUS";
            lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            lblStatusLabel.ForeColor = System.Drawing.Color.FromArgb(160, 168, 190);
            lblStatusLabel.AutoSize = true;
            lblStatusLabel.Location = new System.Drawing.Point(24, 208);
            lblStatusLabel.BackColor = System.Drawing.Color.Transparent;

            // ── Status pill strip ─────────────────────────────────────────
            pnlStatusPills.Location = new System.Drawing.Point(24, 225);
            pnlStatusPills.Size = new System.Drawing.Size(852, 34);
            pnlStatusPills.BackColor = System.Drawing.Color.Transparent;
            pnlStatusPills.AutoScroll = false;

            // ── Divider 2 (above column headers) ─────────────────────────
            pnlDivider2.BackColor = System.Drawing.Color.FromArgb(232, 235, 245);
            pnlDivider2.Location = new System.Drawing.Point(0, 271);
            pnlDivider2.Size = new System.Drawing.Size(900, 1);

            // ── Column header bar ─────────────────────────────────────────
            pnlColHeader.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            pnlColHeader.Location = new System.Drawing.Point(0, 272);
            pnlColHeader.Size = new System.Drawing.Size(900, 30);

            var colFont = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            var colColor = System.Drawing.Color.FromArgb(150, 158, 180);

            lblColId.Text = "ID"; lblColId.Font = colFont; lblColId.ForeColor = colColor; lblColId.AutoSize = true; lblColId.Location = new System.Drawing.Point(24, 8); lblColId.BackColor = System.Drawing.Color.Transparent;
            lblColTime.Text = "TIME"; lblColTime.Font = colFont; lblColTime.ForeColor = colColor; lblColTime.AutoSize = true; lblColTime.Location = new System.Drawing.Point(118, 8); lblColTime.BackColor = System.Drawing.Color.Transparent;
            lblColType.Text = "TYPE"; lblColType.Font = colFont; lblColType.ForeColor = colColor; lblColType.AutoSize = true; lblColType.Location = new System.Drawing.Point(192, 8); lblColType.BackColor = System.Drawing.Color.Transparent;
            lblColCustomer.Text = "CUSTOMER"; lblColCustomer.Font = colFont; lblColCustomer.ForeColor = colColor; lblColCustomer.AutoSize = true; lblColCustomer.Location = new System.Drawing.Point(390, 8); lblColCustomer.BackColor = System.Drawing.Color.Transparent;
            lblColAmount.Text = "AMOUNT"; lblColAmount.Font = colFont; lblColAmount.ForeColor = colColor; lblColAmount.AutoSize = true; lblColAmount.Location = new System.Drawing.Point(660, 8); lblColAmount.BackColor = System.Drawing.Color.Transparent;
            lblColStatus.Text = "STATUS"; lblColStatus.Font = colFont; lblColStatus.ForeColor = colColor; lblColStatus.AutoSize = true; lblColStatus.Location = new System.Drawing.Point(806, 8); lblColStatus.BackColor = System.Drawing.Color.Transparent;

            pnlColHeader.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblColId, lblColTime, lblColType, lblColCustomer, lblColAmount, lblColStatus
            });

            // ── Divider 3 (below column headers) ─────────────────────────
            pnlDivider3.BackColor = System.Drawing.Color.FromArgb(232, 235, 245);
            pnlDivider3.Location = new System.Drawing.Point(0, 302);
            pnlDivider3.Size = new System.Drawing.Size(900, 1);

            // ── Row flow panel ────────────────────────────────────────────
            flowRows.Location = new System.Drawing.Point(0, 303);
            flowRows.Size = new System.Drawing.Size(900, 278);
            flowRows.BackColor = System.Drawing.Color.White;
            flowRows.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowRows.WrapContents = false;
            flowRows.AutoScroll = true;
            flowRows.Padding = new System.Windows.Forms.Padding(0);

            // ── Footer ────────────────────────────────────────────────────
            pnlFooter.BackColor = System.Drawing.Color.White;
            pnlFooter.Location = new System.Drawing.Point(0, 592);
            pnlFooter.Size = new System.Drawing.Size(900, 48);
            pnlFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlFooter_Paint);

            lblTxCount.Text = "Showing 0 transactions";
            lblTxCount.Font = new System.Drawing.Font("Segoe UI", 9f);
            lblTxCount.ForeColor = System.Drawing.Color.FromArgb(150, 158, 180);
            lblTxCount.AutoSize = true;
            lblTxCount.Location = new System.Drawing.Point(24, 15);
            lblTxCount.BackColor = System.Drawing.Color.Transparent;

            lblTotalLabel.Text = "TOTAL COLLECTED";
            lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(140, 148, 170);
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Location = new System.Drawing.Point(676, 6);
            lblTotalLabel.BackColor = System.Drawing.Color.Transparent;

            lblTotalAmt.Text = "₱0.00";
            lblTotalAmt.Font = new System.Drawing.Font("Segoe UI Semibold", 14f, System.Drawing.FontStyle.Bold);
            lblTotalAmt.ForeColor = System.Drawing.Color.FromArgb(34, 197, 162);
            lblTotalAmt.AutoSize = true;
            lblTotalAmt.Location = new System.Drawing.Point(674, 22);
            lblTotalAmt.BackColor = System.Drawing.Color.Transparent;

            pnlFooter.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblTxCount, lblTotalLabel, lblTotalAmt
            });

            // ── Compose pnlCard ───────────────────────────────────────────
            pnlCard.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                pnlTitleBar,
                picIcon,
                lblTitle,
                lblSubtitle,
                btnClose,
                pnlDivider1,
                txtSearch,
                lblTypeLabel,
                pnlTypePills,
                lblStatusLabel,
                pnlStatusPills,
                pnlDivider2,
                pnlColHeader,
                pnlDivider3,
                flowRows,
                pnlFooter
            });

            this.Controls.Add(pnlCard);

            ((System.ComponentModel.ISupportInitialize)(picIcon)).EndInit();
            pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // ── Field declarations ───────────────────────────────────────────
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2Panel pnlTitleBar;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Panel pnlDivider1;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.Label lblTypeLabel;
        private System.Windows.Forms.Panel pnlTypePills;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Panel pnlStatusPills;
        private System.Windows.Forms.Panel pnlDivider2;
        private System.Windows.Forms.Panel pnlColHeader;
        private System.Windows.Forms.Label lblColId;
        private System.Windows.Forms.Label lblColTime;
        private System.Windows.Forms.Label lblColType;
        private System.Windows.Forms.Label lblColCustomer;
        private System.Windows.Forms.Label lblColAmount;
        private System.Windows.Forms.Label lblColStatus;
        private System.Windows.Forms.Panel pnlDivider3;
        private System.Windows.Forms.FlowLayoutPanel flowRows;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblTxCount;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalAmt;
    }
}