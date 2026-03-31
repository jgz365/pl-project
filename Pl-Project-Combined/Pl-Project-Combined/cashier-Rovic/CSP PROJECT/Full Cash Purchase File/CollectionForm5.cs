using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using POSCashierSystem;

namespace CSP_PROJECT
{
    /// <summary>
    /// CollectionForm5 — Cashier payment collection screen for the kiosk full-cash flow.
    /// Receives customer name, charges breakdown, and total from ck_confirm_payment.
    /// </summary>
    public partial class CollectionForm5 : UserControl
    {
        // ── Data passed in from ck_confirm_payment ────────────────────────────
        private string _customerName = string.Empty;
        private string _mobileNumber = string.Empty;
        private List<(string Label, decimal Amount)> _charges = new();
        private decimal _totalDue = 0m;
        private decimal _amountReceived = 0m;

        public event EventHandler BackRequested = delegate { };

        // ── Designer reference dimensions (96 DPI) ────────────────────────────
        // Form:         1021 × 750
        // pnlSidebar:   x=0,   y=0, w=366, h=750
        // pnlRight:     x=366, y=0, w=655, h=750   (1021-366 = 655)
        //   Content block inside pnlRight: x=90, w=480 (ref 683 assumed, but actual is 655)
        //   We derive content centre from the ref proportions instead.
        private const float REF_W = 1021f;
        private const float REF_H = 750f;
        private const float REF_SIDEBAR_W = 366f;
        private const float REF_RIGHT_W = 655f;   // 1021 - 366

        // Sidebar interior reference (relative to sidebar 366 × 750)
        private const float SB_REF_W = 366f;
        private const float SB_REF_H = 750f;

        // Right-panel content block reference (relative to pnlRight 655 × 750)
        // In the designer the block sits at x=90 inside a 683-wide panel.
        // We keep the same proportional margin: 90/683 ≈ 13.2 %
        private const float RT_REF_W = 683f;   // original designer width used for proportions
        private const float RT_REF_H = 750f;
        private const float RT_CONTENT_X_REF = 90f;    // left edge of content block
        private const float RT_CONTENT_W_REF = 480f;   // content block width

        // ─────────────────────────────────────────────────────────────────────
        public CollectionForm5()
        {
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Called by ck_confirm_payment before adding to parent
        // ─────────────────────────────────────────────────────────────────────
        public void SetData(string customerName, string mobileNumber,
                            List<(string Label, decimal Amount)> charges,
                            decimal totalDue)
        {
            _customerName = customerName ?? string.Empty;
            _mobileNumber = mobileNumber ?? string.Empty;
            _charges = charges ?? new List<(string, decimal)>();
            _totalDue = totalDue;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Lifecycle
        // ─────────────────────────────────────────────────────────────────────
        private void CollectionForm5_Load(object sender, EventArgs e)
        {
            PopulateSidebar();
            ApplyResponsiveLayout();
            ResetPaymentState();
            txtAmountReceived.Focus();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) ApplyResponsiveLayout();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Round-scale helper
        // ─────────────────────────────────────────────────────────────────────
        private static int S(float refVal, float scale) => (int)Math.Round(refVal * scale);

        // ─────────────────────────────────────────────────────────────────────
        // Master responsive layout
        // ─────────────────────────────────────────────────────────────────────
        private void ApplyResponsiveLayout()
        {
            if (ClientSize.Width < 400 || ClientSize.Height < 200) return;

            float formW = ClientSize.Width;
            float formH = ClientSize.Height;

            // Uniform scale factor — preserves aspect ratio of the full form
            float sc = Math.Min(formW / REF_W, formH / REF_H);

            // ── Root panel fills the whole UserControl ────────────────────────
            pnlRoot.SetBounds(0, 0, (int)formW, (int)formH);

            // ── Sidebar ───────────────────────────────────────────────────────
            int sidebarW = S(REF_SIDEBAR_W, sc);
            int sidebarH = (int)formH;
            pnlSidebar.SetBounds(0, 0, sidebarW, sidebarH);

            // ── Right panel ───────────────────────────────────────────────────
            int rightX = sidebarW;
            int rightW = (int)formW - sidebarW;
            int rightH = (int)formH;
            pnlRight.SetBounds(rightX, 0, rightW, rightH);

            // ── Scale each section independently ──────────────────────────────
            ScaleSidebarInterior(sidebarW, sidebarH);
            ScaleRightInterior(rightW, rightH);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Sidebar interior   (designer reference: 366 × 750)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleSidebarInterior(int panelW, int panelH)
        {
            float sc = Math.Min(panelW / SB_REF_W, panelH / SB_REF_H);

            // ── Back button — ref (14, 20, 150, 38) ───────────────────────────
            btnBack.SetBounds(S(14f, sc), S(20f, sc), S(150f, sc), S(38f, sc));

            // ── Avatar — ref (119, 76, 90, 90); keep it square and centred ────
            int avatarSize = Math.Max(48, S(90f, sc));
            int avatarX = (panelW - avatarSize) / 2;
            int avatarY = S(76f, sc);
            btnAvatar.SetBounds(avatarX, avatarY, avatarSize, avatarSize);
            btnAvatar.BorderRadius = avatarSize / 2;

            // ── Customer name — ref (14, 178, 310, 30), text centered ──────────
            int labelPadX = S(14f, sc);
            int labelW = panelW - labelPadX * 2;
            lblCustomerName.SetBounds(labelPadX, S(178f, sc), labelW, S(30f, sc));

            // ── Customer subtitle — ref (14, 210, 310, 24) ────────────────────
            lblCustomerSubtitle.SetBounds(labelPadX, S(210f, sc), labelW, S(24f, sc));

            // ── Breakdown card — ref (18, 248, 345, 400) ─────────────────────
            int cardPadX = S(18f, sc);
            int cardY = S(248f, sc);
            int cardW = panelW - cardPadX * 2;
            // Height: fills remaining sidebar space with a small bottom margin
            int cardH = Math.Max(100, panelH - cardY - S(12f, sc));
            pnlBreakdownCard.SetBounds(cardPadX, cardY, cardW, cardH);

            // Scale interior of breakdown card
            ScaleBreakdownCardInterior(cardW, cardH);
        }

        // ─────────────────────────────────────────────────────────────────────
        // pnlBreakdownCard interior   (designer reference: 345 × 400)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleBreakdownCardInterior(int cardW, int cardH)
        {
            const float refCW = 345f;
            const float refCH = 400f;
            float sc = Math.Min(cardW / refCW, cardH / refCH);

            // ── "CHARGES BREAKDOWN" title — ref (18, 16, 266, 22) ─────────────
            int padX = S(18f, sc);
            lblBreakdownTitle.SetBounds(padX, S(16f, sc), cardW - padX * 2, S(22f, sc));

            // ── sep1 — ref (0, 46, 345, 1) ────────────────────────────────────
            sep1.SetBounds(0, S(46f, sc), cardW, 1);

            // Dynamic charge rows + total are rebuilt inside BuildChargesRows
            // We pass the scale factor so it can position them correctly
            RebuildChargesRows(cardW, sc);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Dynamic charges rows — rebuilds and positions all rows
        // ─────────────────────────────────────────────────────────────────────
        private void RebuildChargesRows(int cardW, float sc)
        {
            // Remove all dynamic controls (keep the static ones: title, sep1)
            var toRemove = new List<Control>();
            foreach (Control c in pnlBreakdownCard.Controls)
            {
                if (c != lblBreakdownTitle && c != sep1)
                    toRemove.Add(c);
            }
            foreach (Control c in toRemove)
                pnlBreakdownCard.Controls.Remove(c);

            // Row geometry — ref: first row at y=56, rowH=28
            int padX = S(18f, sc);
            int y = S(56f, sc);
            int rowH = Math.Max(20, S(28f, sc));
            int valX = S(200f, sc);
            int valW = cardW - valX - padX;

            foreach (var (label, amount) in _charges)
            {
                var key = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    Location = new Point(padX, y),
                    Size = new Size(valX - padX - 4, rowH),
                    Text = label
                };
                var val = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Location = new Point(valX, y),
                    Size = new Size(valW, rowH),
                    Text = $"₱{amount:N2}",
                    TextAlign = ContentAlignment.TopRight
                };
                pnlBreakdownCard.Controls.Add(key);
                pnlBreakdownCard.Controls.Add(val);
                y += rowH;
            }

            // ── sep2 — ref sits 4px below last row ────────────────────────────
            int sep2Y = y + S(4f, sc);
            sep2.SetBounds(0, sep2Y, cardW, 1);
            pnlBreakdownCard.Controls.Add(sep2);

            // ── "Total Due" key — ref 14px below sep ──────────────────────────
            int totKeyY = sep2Y + S(14f, sc);
            lblTotalDueKey.SetBounds(padX, totKeyY, S(190f, sc), S(24f, sc));
            pnlBreakdownCard.Controls.Add(lblTotalDueKey);

            // ── Total Due value — ref 38px below sep ──────────────────────────
            int totValY = sep2Y + S(38f, sc);
            lblTotalDueValue.SetBounds(padX, totValY, cardW - padX * 2, S(30f, sc));
            pnlBreakdownCard.Controls.Add(lblTotalDueValue);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Right panel interior   (designer reference content block: x=90, w=480 inside 683)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleRightInterior(int panelW, int panelH)
        {
            // Scale from designer's right-panel reference (683 × 750)
            float sc = Math.Min(panelW / RT_REF_W, panelH / RT_REF_H);

            // Content block: scale width and centre horizontally inside pnlRight
            int contentW = S(RT_CONTENT_W_REF, sc);
            int contentX = Math.Max(0, (panelW - contentW) / 2);

            // ── "AMOUNT RECEIVED" label — ref (90, 60, 480, 22) ───────────────
            lblAmountReceivedTitle.SetBounds(contentX, S(60f, sc), contentW, S(22f, sc));

            // ── Amount input panel — ref (90, 88, 480, 90) ────────────────────
            int inputH = S(90f, sc);
            pnlAmountInput.SetBounds(contentX, S(88f, sc), contentW, inputH);

            // Controls inside pnlAmountInput — ref: 480 × 90
            float inpSc = Math.Min(contentW / 480f, inputH / 90f);
            int pesoW = S(30f, inpSc);
            int pesoH = S(48f, inpSc);
            int pesoY = (inputH - pesoH) / 2;
            lblPesoSign.SetBounds(S(14f, inpSc), pesoY, pesoW, pesoH);
            int txtX = S(48f, inpSc);
            int txtH = S(70f, inpSc);
            int txtY = (inputH - txtH) / 2;
            txtAmountReceived.SetBounds(txtX, txtY, contentW - txtX - S(4f, inpSc), txtH);

            // ── Quick-add buttons — ref row at y=200, each 65 tall ────────────
            // 100(108) gap(12) 500(108) gap(12) 1000(108) gap(12) Exact(120) = 480
            int btnY = S(200f, sc);
            int btnH = S(65f, sc);
            int btn3W = S(108f, sc);
            int btnExactW = S(120f, sc);
            int btnGap = S(12f, sc);

            btnAdd100.SetBounds(contentX, btnY, btn3W, btnH);
            btnAdd500.SetBounds(contentX + btn3W + btnGap, btnY, btn3W, btnH);
            btnAdd1000.SetBounds(contentX + (btn3W + btnGap) * 2, btnY, btn3W, btnH);
            btnExact.SetBounds(contentX + (btn3W + btnGap) * 3, btnY, btnExactW, btnH);

            // ── Change Due panel — ref (90, 295, 480, 90) ─────────────────────
            int changePnlH = S(90f, sc);
            pnlChangeDue.SetBounds(contentX, S(295f, sc), contentW, changePnlH);

            // Controls inside pnlChangeDue — ref: 480 × 90
            float chgSc = Math.Min(contentW / 480f, changePnlH / 90f);
            lblChangeDueTitle.SetBounds(S(24f, chgSc), S(28f, chgSc), S(160f, chgSc), S(28f, chgSc));
            // Value label right-aligned: ref right margin = 480-(220+240)=20px
            int chgValW = S(240f, chgSc);
            int chgValX = contentW - chgValW - S(20f, chgSc);
            lblChangeDueValue.SetBounds(chgValX, S(14f, chgSc), chgValW, S(62f, chgSc));

            // ── Process button — ref (90, 415, 480, 75) ───────────────────────
            btnProcess.SetBounds(contentX, S(415f, sc), contentW, S(75f, sc));
        }

        // ─────────────────────────────────────────────────────────────────────
        // Populate sidebar with customer info + initial charges
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateSidebar()
        {
            // Avatar
            btnAvatar.Text = GetInitials(_customerName);
            btnAvatar.FillColor = GetAvatarBg(_customerName);
            btnAvatar.ForeColor = GetAvatarFg(_customerName);
            btnAvatar.HoverState.FillColor = btnAvatar.FillColor;
            btnAvatar.HoverState.ForeColor = btnAvatar.ForeColor;
            btnAvatar.PressedColor = btnAvatar.FillColor;

            lblCustomerName.Text = _customerName;
            lblCustomerSubtitle.Text = string.IsNullOrWhiteSpace(_mobileNumber)
                ? "Full Cash Purchase"
                : _mobileNumber;

            // Total amount text — layout will position it after resize
            lblTotalDueValue.Text = $"₱{_totalDue:N0}";
        }

        // ─────────────────────────────────────────────────────────────────────
        // Payment state
        // ─────────────────────────────────────────────────────────────────────
        private void ResetPaymentState()
        {
            _amountReceived = 0m;
            txtAmountReceived.Text = string.Empty;
            RefreshCalculation();
        }

        private void RefreshCalculation()
        {
            decimal change = _amountReceived - _totalDue;
            bool paid = _amountReceived >= _totalDue;

            if (_amountReceived <= 0m)
            {
                lblChangeDueValue.Text = "₱0";
                lblChangeDueValue.ForeColor = Color.FromArgb(71, 85, 105);
            }
            else if (paid)
            {
                lblChangeDueValue.Text = $"₱{change:N0}";
                lblChangeDueValue.ForeColor = Color.FromArgb(5, 150, 105);
            }
            else
            {
                decimal shortage = _totalDue - _amountReceived;
                lblChangeDueValue.Text = $"-₱{shortage:N0}";
                lblChangeDueValue.ForeColor = Color.FromArgb(248, 113, 113);
            }

            btnProcess.Enabled = paid;
            btnProcess.FillColor = paid
                ? Color.FromArgb(5, 150, 105)
                : Color.FromArgb(167, 243, 208);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Amount button handlers
        // ─────────────────────────────────────────────────────────────────────
        private void TxtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            _amountReceived = decimal.TryParse(txtAmountReceived.Text, out decimal v) ? v : 0m;
            RefreshCalculation();
        }

        private void BtnAdd100_Click(object sender, EventArgs e) => AddAmount(100m);
        private void BtnAdd500_Click(object sender, EventArgs e) => AddAmount(500m);
        private void BtnAdd1000_Click(object sender, EventArgs e) => AddAmount(1_000m);

        private void AddAmount(decimal amount)
        {
            _amountReceived += amount;
            txtAmountReceived.Text = _amountReceived % 1 == 0
                ? ((long)_amountReceived).ToString()
                : _amountReceived.ToString("N2");
            txtAmountReceived.SelectionStart = txtAmountReceived.Text.Length;
            RefreshCalculation();
        }

        private void BtnExact_Click(object sender, EventArgs e)
        {
            _amountReceived = _totalDue;
            txtAmountReceived.Text = _totalDue % 1 == 0
                ? ((long)_totalDue).ToString()
                : _totalDue.ToString("N2");
            txtAmountReceived.SelectionStart = txtAmountReceived.Text.Length;
            RefreshCalculation();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Process transaction
        // ─────────────────────────────────────────────────────────────────────
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (_amountReceived < _totalDue) return;

            var result = new CollectionResult5
            {
                CustomerName = _customerName,
                MobileNumber = _mobileNumber,
                Charges = _charges,
                TotalDue = _totalDue,
                AmountReceived = _amountReceived,
                ChangeDue = _amountReceived - _totalDue,
                ProcessedAt = DateTime.Now
            };

            TransactionStore.Add(new Transaction
            {
                TransactionId = $"TX-{result.ProcessedAt:yyyyMMddHHmmssfff}",
                DateTime = result.ProcessedAt,
                PaymentType = "Full Cash",
                CustomerName = string.IsNullOrWhiteSpace(result.CustomerName) ? "Unknown Customer" : result.CustomerName,
                UnitModel = "Cash Purchase",
                Amount = result.TotalDue,
                Status = "Paid"
            });

            var host = this.Parent;
            if (host == null) return;

            var receiptScreen = new ReceiptForm5(result)
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            receiptScreen.ResetRequested += (s, args) =>
            {
                host.Controls.Remove(receiptScreen);
                receiptScreen.Dispose();
            };

            host.Controls.Add(receiptScreen);
            receiptScreen.BringToFront();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Back button
        // ─────────────────────────────────────────────────────────────────────
        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Avatar colour helpers
        // ─────────────────────────────────────────────────────────────────────
        private static readonly Color[] _bgPalette =
        {
            Color.FromArgb(219, 234, 254),
            Color.FromArgb(209, 250, 229),
            Color.FromArgb(254, 243, 199),
            Color.FromArgb(237, 233, 254),
            Color.FromArgb(255, 228, 230),
        };
        private static readonly Color[] _fgPalette =
        {
            Color.FromArgb(37,  99, 235),
            Color.FromArgb(5,  150, 105),
            Color.FromArgb(217, 119,   6),
            Color.FromArgb(109,  40, 217),
            Color.FromArgb(225,  29,  72),
        };

        private static Color GetAvatarBg(string name)
            => string.IsNullOrWhiteSpace(name)
               ? _bgPalette[0]
               : _bgPalette[char.ToUpper(name[0]) % _bgPalette.Length];

        private static Color GetAvatarFg(string name)
            => string.IsNullOrWhiteSpace(name)
               ? _fgPalette[0]
               : _fgPalette[char.ToUpper(name[0]) % _fgPalette.Length];

        private static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";
            var parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length >= 2
                ? $"{parts[0][0]}{parts[1][0]}".ToUpper()
                : name.Substring(0, Math.Min(2, name.Length)).ToUpper();
        }
    }
}