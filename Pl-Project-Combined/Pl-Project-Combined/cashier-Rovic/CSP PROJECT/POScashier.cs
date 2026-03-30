using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace POSCashierSystem
{
    public partial class POSCashier : Form
    {
        // ── Win32 idle-time helper ────────────────────────────────────
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        private double GetSystemIdleSeconds()
        {
            var lii = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };
            GetLastInputInfo(ref lii);
            return (Environment.TickCount - (int)lii.dwTime) / 1000.0;
        }

        // ── Accent colors ─────────────────────────────────────────────
        private readonly Color cardDefaultBorderColor = Color.FromArgb(224, 224, 224);
        private readonly Color cardDefaultBackColor = Color.White;
        private readonly Color cardHoverBackColor = Color.FromArgb(250, 252, 253);

        private readonly Color downPaymentAccent = Color.FromArgb(102, 217, 189);
        private readonly Color monthlyPaymentAccent = Color.FromArgb(138, 171, 255);
        private readonly Color fullCashAccent = Color.FromArgb(188, 140, 255);
        private readonly Color advancePaymentAccent = Color.FromArgb(102, 217, 189);
        private readonly Color fullSettlementAccent = Color.FromArgb(255, 138, 138);

        // ── Session & idle constants ──────────────────────────────────
        private const int SESSION_MINUTES = 120;
        private const int IDLE_WARN_SECS = 270;
        private const int IDLE_LOCK_SECS = 300;

        // ── State ─────────────────────────────────────────────────────
        private DateTime _sessionExpiry;
        private bool _idleWarningShown;
        private System.Windows.Forms.Timer _masterTimer;

        // ── Reference design geometry (1920×1080 @ 125 % DPI) ────────
        private const float REF_W = 1867f;
        private const float REF_H = 1102f;
        private const float REF_TH = 154f;

        // ─────────────────────────────────────────────────────────────
        public POSCashier()
        {
            InitializeComponent();
            InitializeCardHoverEffects();
        }

        // ═════════════════════════════════════════════════════════════
        //  LOAD
        // ═════════════════════════════════════════════════════════════
        private void POSCashier_Load(object sender, EventArgs e)
        {
            _sessionExpiry = DateTime.Now.AddMinutes(SESSION_MINUTES);
            _idleWarningShown = false;

            _masterTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _masterTimer.Tick += MasterTimer_Tick;
            _masterTimer.Start();

            ApplyResponsiveLayout();
            RefreshExpiresLabel(TimeSpan.FromMinutes(SESSION_MINUTES));
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _masterTimer?.Stop();
            _masterTimer?.Dispose();
            base.OnFormClosed(e);
        }

        // ═════════════════════════════════════════════════════════════
        //  MASTER TIMER
        // ═════════════════════════════════════════════════════════════
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan remaining = _sessionExpiry - DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                _masterTimer.Stop();
                MessageBox.Show(
                    "Your session has expired. The register will now close.",
                    "Session Expired",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            RefreshExpiresLabel(remaining);

            double idleSecs = GetSystemIdleSeconds();

            if (idleSecs >= IDLE_LOCK_SECS)
            {
                _masterTimer.Stop();
                MessageBox.Show(
                    "The register has been locked due to inactivity.",
                    "Auto-Locked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            if (idleSecs >= IDLE_WARN_SECS && !_idleWarningShown)
            {
                _idleWarningShown = true;
                int secsLeft = IDLE_LOCK_SECS - (int)idleSecs;
                lblSystemOnline.Text = $"⚠ IDLE — locks in {secsLeft}s";
                lblSystemOnline.ForeColor = Color.FromArgb(255, 186, 115);
            }
            else if (idleSecs < IDLE_WARN_SECS)
            {
                if (_idleWarningShown)
                {
                    _idleWarningShown = false;
                    lblSystemOnline.Text = "SYSTEM ONLINE";
                    lblSystemOnline.ForeColor = Color.FromArgb(102, 217, 189);
                }

                if (idleSecs >= IDLE_WARN_SECS)
                {
                    int secsLeft = IDLE_LOCK_SECS - (int)idleSecs;
                    lblSystemOnline.Text = $"⚠ IDLE — locks in {secsLeft}s";
                }
            }
        }

        private void RefreshExpiresLabel(TimeSpan remaining)
        {
            string display;
            Color textColor;

            if (remaining.TotalSeconds <= 0)
            {
                display = "Expires: --:--";
                textColor = Color.FromArgb(255, 138, 138);
            }
            else if (remaining.TotalMinutes < 10)
            {
                display = $"Expires: {(int)remaining.TotalMinutes:D2}:{remaining.Seconds:D2}";
                textColor = remaining.TotalMinutes < 5
                            ? Color.FromArgb(255, 100, 100)
                            : Color.FromArgb(255, 186, 115);
            }
            else
            {
                display = $"Expires: {_sessionExpiry:HH:mm}";
                textColor = Color.FromArgb(120, 120, 120);
            }

            lblExpires.Text = display;
            lblExpires.ForeColor = textColor;
        }

        // ═════════════════════════════════════════════════════════════
        //  RESPONSIVE LAYOUT
        // ═════════════════════════════════════════════════════════════
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (ClientSize.Width < 100 || ClientSize.Height < 100) return;

            float w = ClientSize.Width;
            float h = ClientSize.Height;
            float sx = w / REF_W;
            float sy = h / REF_H;
            float s = Math.Min(sx, sy);

            // ── TOP BAR ──────────────────────────────────────────────
            int topH = S(REF_TH, sy);
            pnlTop.SetBounds(0, 0, (int)w, topH);

            int avSz = S(53, s);
            int avX = S(44, sx);
            int avY = (topH - avSz) / 2;
            guna2CirclePictureBox2.SetBounds(avX, avY, avSz, avSz);

            int txX = avX + avSz + S(15, sx);
            lblStationTitle.Location = new Point(txX, S(31, sy));
            lblStationStatus.Location = new Point(txX, S(57, sy));
            lblStaffName.Location = new Point(txX, S(83, sy));

            int sysBaseX = (int)(w * 0.62f);
            int dotSz = S(14, s);
            int dotY = S(57, sy);
            guna2CirclePictureBox1.SetBounds(sysBaseX, dotY, dotSz, dotSz);
            int sysLblX = sysBaseX + dotSz + S(6, sx);
            lblSystemOnline.Location = new Point(sysLblX, S(51, sy));
            lblExpires.Location = new Point(sysLblX, S(78, sy));

            int totX = (int)(w * 0.75f);
            lblTotalAmount.Location = new Point(totX, S(42, sy));
            lblTotalCollected.Location = new Point(totX, S(88, sy));

            int btnW = S(187, sx);
            int btnH = S(65, sy);
            int btnX = (int)(w - btnW - w * 0.02f);
            int btnY = (topH - btnH) / 2;
            btnCloseRegister.SetBounds(btnX, btnY, btnW, btnH);

            // ── BACKGROUND PANEL ─────────────────────────────────────
            pnlBackground.SetBounds(0, topH, (int)w, (int)h - topH);

            float bgW = w;
            float bgH = h - topH;
            float bsx = bgW / REF_W;
            float bsy = bgH / (REF_H - REF_TH);
            float bs = Math.Min(bsx, bsy);

            int ntDotSz = S(27, bs);
            int ntDotH = S(31, bs);
            int ntDotX = S(64, bsx);
            int ntY = topH + S(41, bsy);
            guna2CirclePictureBox3.SetBounds(ntDotX, ntY, ntDotSz, ntDotH);
            lblNewTransaction.Location = new Point(ntDotX + ntDotSz + S(10, bsx), ntY + 3);

            // ── CARD GRID ─────────────────────────────────────────────
            //
            //  Row 1 │ Down Payment    │ Monthly Payment  │
            //  Row 2 │    Full Cash Purchase (centred)    │
            //  Row 3 │ Advance Payment │ Full Settlement  │
            //
            int margin = S(80, bsx);
            int colGap = S(46, bsx);
            int rowGap = S(27, bsy);
            int rtW = S(467, bsx);

            int gridW = (int)(bgW - margin * 2 - colGap - rtW - S(46, bsx));
            int cardW = (gridW - colGap) / 2;
            int cardH = S(262, bsy);
            int cardTopY = S(92, bsy);

            int col1X = margin;
            int col2X = margin + cardW + colGap;

            int row1Y = cardTopY;
            int row2Y = cardTopY + cardH + rowGap;
            int row3Y = cardTopY + (cardH + rowGap) * 2;

            // Row 1
            SetCardBounds(pnlDownPayment, col1X, row1Y, cardW, cardH);
            SetCardBounds(pnlMonthlyPayment, col2X, row1Y, cardW, cardH);

            // Row 2 — Full Cash centred
            int centredCardX = margin + (gridW - cardW) / 2;
            SetCardBounds(pnlFullCash, centredCardX, row2Y, cardW, cardH);

            // Row 3
            SetCardBounds(pnlAdvancePayment, col1X, row3Y, cardW, cardH);
            SetCardBounds(pnlFullSettlement, col2X, row3Y, cardW, cardH);

            ScaleCardInterior(pnlDownPaymentIcon, lblDownPayment, lblDownPaymentDesc, bs, cardH);
            ScaleCardInterior(pnlMonthlyPaymentIcon, lblMonthlyPayment, lblMonthlyPaymentDesc, bs, cardH);
            ScaleCardInterior(pnlFullCashIcon, lblFullCash, lblFullCashDesc, bs, cardH);
            ScaleCardInterior(pnlAdvancePaymentIcon, lblAdvancePayment, lblAdvancePaymentDesc, bs, cardH);
            ScaleCardInterior(pnlFullSettlementIcon, lblFullSettlement, lblFullSettlementDesc, bs, cardH);

            // ── RECENT TRANSACTIONS panel ─────────────────────────────
            int rtX = (int)(bgW - rtW - S(46, bsx));
            int rtY = cardTopY;
            int rtH = row3Y + cardH - cardTopY;
            pnlNoTransactions.SetBounds(rtX, rtY, rtW, rtH);

            lblNoTransactions.SetBounds(0, (rtH - lblNoTransactions.Height) / 2,
                                        rtW, lblNoTransactions.Height);

            int hdrY = S(50, bsy);
            lblRecentTransactions.Location = new Point(rtX, hdrY);
            lblViewAll.Location = new Point(rtX + rtW - lblViewAll.Width - S(4, bsx), hdrY);
        }

        private static int S(float value, float factor) => (int)Math.Round(value * factor);

        private static void SetCardBounds(Guna2Panel card, int x, int y, int w, int h)
            => card.SetBounds(x, y, w, h);

        private static void ScaleCardInterior(
            Guna2Panel iconPanel, Label titleLbl, Label descLbl,
            float s, int cardH)
        {
            int pad = S(27, s);
            int icoSz = S(53, s);
            int icoH = S(62, s);
            int icoTop = S(31, s);

            iconPanel.SetBounds(pad, icoTop, icoSz, icoH);

            if (iconPanel.Controls.Count > 0 && iconPanel.Controls[0] is Label icoLbl)
                icoLbl.SetBounds(0, 0, icoSz, icoH);

            int titleY = icoTop + icoH + S(28, s);
            titleLbl.Location = new Point(pad, titleY);

            int descY = titleY + titleLbl.PreferredHeight + S(6, s);
            descLbl.Location = new Point(pad, Math.Min(descY, cardH - S(30, s)));
        }

        // ═════════════════════════════════════════════════════════════
        //  HOVER EFFECTS
        // ═════════════════════════════════════════════════════════════
        private void InitializeCardHoverEffects()
        {
            AttachHoverEffect(pnlDownPayment, downPaymentAccent);
            AttachHoverEffect(pnlMonthlyPayment, monthlyPaymentAccent);
            AttachHoverEffect(pnlFullCash, fullCashAccent);
            AttachHoverEffect(pnlAdvancePayment, advancePaymentAccent);
            AttachHoverEffect(pnlFullSettlement, fullSettlementAccent);
        }

        private void AttachHoverEffect(Guna2Panel panel, Color accent)
        {
            panel.MouseEnter += (s, e) => CardHover(panel, accent, true);
            panel.MouseLeave += (s, e) =>
            {
                if (!panel.ClientRectangle.Contains(panel.PointToClient(Cursor.Position)))
                    CardHover(panel, accent, false);
            };

            foreach (Control ctrl in panel.Controls)
            {
                ctrl.MouseEnter += (s, e) => CardHover(panel, accent, true);
                ctrl.MouseLeave += (s, e) =>
                {
                    if (!panel.ClientRectangle.Contains(panel.PointToClient(Cursor.Position)))
                        CardHover(panel, accent, false);
                };
            }
        }

        private void CardHover(Guna2Panel panel, Color accent, bool entering)
        {
            if (entering)
            {
                panel.BorderColor = accent;
                panel.FillColor = cardHoverBackColor;
                panel.ShadowDecoration.Enabled = true;
                panel.ShadowDecoration.Shadow = new Padding(0, 2, 8, 8);
                panel.ShadowDecoration.Color = Color.FromArgb(50, accent);
                panel.Cursor = Cursors.Hand;
            }
            else
            {
                panel.BorderColor = cardDefaultBorderColor;
                panel.FillColor = cardDefaultBackColor;
                panel.ShadowDecoration.Enabled = false;
                panel.Cursor = Cursors.Default;
            }
        }

        // ═════════════════════════════════════════════════════════════
        //  NAVIGATION — card clicks
        // ═════════════════════════════════════════════════════════════
        private void PnlDownPayment_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);
            RemoveSubForms<DownPaymentForm>();
            var ctrl = new DownPaymentForm { Dock = DockStyle.Fill, Margin = Padding.Empty };
            ctrl.CloseRequested += (s, _) => RestoreMainView();
            PushSubForm(ctrl);
        }

        private void PnlMonthlyPayment_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);
            RemoveSubForms<MonthlyPaymentForm>();
            var ctrl = new MonthlyPaymentForm { Dock = DockStyle.Fill, Margin = Padding.Empty };
            ctrl.CloseRequested += (s, _) => RestoreMainView();
            PushSubForm(ctrl);
        }

        // ── Full Cash Purchase now opens UC_main_menu (the kiosk catalog) ──
        private void PnlFullCash_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);
            RemoveSubForms<customer_kiosk.UC_main_menu>();
            var ctrl = new customer_kiosk.UC_main_menu { Dock = DockStyle.Fill, Margin = Padding.Empty };
            ctrl.CloseRequested += (s, _) => RestoreMainView();
            PushSubForm(ctrl);
        }

        private void PnlAdvancePayment_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);
            RemoveSubForms<AdvancePaymentForm>();
            var ctrl = new AdvancePaymentForm { Dock = DockStyle.Fill, Margin = Padding.Empty };
            ctrl.CloseRequested += (s, _) => RestoreMainView();
            PushSubForm(ctrl);
        }

        private void PnlFullSettlement_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);
            RemoveSubForms<FullSettlementForm>();
            var ctrl = new FullSettlementForm { Dock = DockStyle.Fill, Margin = Padding.Empty };
            ctrl.CloseRequested += (s, _) => RestoreMainView();
            PushSubForm(ctrl);
        }

        private void PushSubForm(UserControl ctrl)
        {
            pnlBackground.Controls.Add(ctrl);
            ctrl.BringToFront();
            ctrl.Focus();
        }

        private void RemoveSubForms<T>() where T : Control
        {
            for (int i = pnlBackground.Controls.Count - 1; i >= 0; i--)
                if (pnlBackground.Controls[i] is T)
                    pnlBackground.Controls.RemoveAt(i);
        }

        // ─────────────────────────────────────────────────────────────
        // View All
        // ─────────────────────────────────────────────────────────────
        private void LblViewAll_Click(object sender, EventArgs e)
        {
            var overlay = Form_BlurOverlay.ShowOver(this);
            using (var histForm = new Form_TransactionHistory())
                histForm.ShowDialog(overlay);
            overlay.FadeOut();
        }

        // ─────────────────────────────────────────────────────────────
        // Restore main dashboard
        // ─────────────────────────────────────────────────────────────
        public void RestoreMainView()
        {
            var toRemove = new System.Collections.Generic.List<Control>();
            foreach (Control ctrl in pnlBackground.Controls)
                if (ctrl is UserControl) toRemove.Add(ctrl);
            foreach (var ctrl in toRemove)
            {
                pnlBackground.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            SetMainViewVisible(true);
        }

        private void SetMainViewVisible(bool visible)
        {
            pnlDownPayment.Visible = visible;
            pnlMonthlyPayment.Visible = visible;
            pnlFullCash.Visible = visible;
            pnlAdvancePayment.Visible = visible;
            pnlFullSettlement.Visible = visible;
            pnlNoTransactions.Visible = visible;
            lblNewTransaction.Visible = visible;
            guna2CirclePictureBox3.Visible = visible;
            lblRecentTransactions.Visible = visible;
            lblViewAll.Visible = visible;
        }

        // ═════════════════════════════════════════════════════════════
        //  STUBS
        // ═════════════════════════════════════════════════════════════
        private void BtnCloseRegister_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Are you sure you want to close the register?",
                    "Close Register",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}