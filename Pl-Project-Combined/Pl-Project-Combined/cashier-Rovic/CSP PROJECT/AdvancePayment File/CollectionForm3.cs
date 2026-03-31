using POSCashierSystem;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSP_PROJECT.POSCashier.AdvancePayment_File
{
    public partial class CollectionForm3 : UserControl
    {
        private CustomerSummary _customer;
        private int _numberOfMonths = 1;
        private decimal _monthlyAmortization = 0m;
        private decimal _totalDue = 0m;
        private decimal _amountReceived = 0m;

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<CollectionResult3> TransactionComplete = delegate { };

        // ── Designer reference dimensions (96 DPI) ────────────────────────────
        private const float REF_W = 1021f;
        private const float REF_H = 750f;
        private const float REF_SIDEBAR_W = 366f;

        private const float SB_REF_W = 366f;
        private const float SB_REF_H = 750f;

        private const float RT_REF_W = 683f;
        private const float RT_REF_H = 750f;
        private const float RT_CONTENT_W_REF = 480f;

        public CollectionForm3()
        {
            InitializeComponent();
        }

        // ── Called by PaymentConfigurationForm3 ──────────────────────────────
        public void SetData(CustomerSummary customer, int numberOfMonths, decimal monthlyAmortization)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _numberOfMonths = numberOfMonths < 1 ? 1 : numberOfMonths;
            _monthlyAmortization = monthlyAmortization;
            _totalDue = _monthlyAmortization * _numberOfMonths;
        }

        private void CollectionForm3_Load(object sender, EventArgs e)
        {
            if (_customer == null) return;
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
        private static int S(float refVal, float scale) => (int)Math.Round(refVal * scale);

        // ─────────────────────────────────────────────────────────────────────
        private void ApplyResponsiveLayout()
        {
            if (ClientSize.Width < 400 || ClientSize.Height < 200) return;

            float formW = ClientSize.Width;
            float formH = ClientSize.Height;
            float sc = Math.Min(formW / REF_W, formH / REF_H);

            pnlRoot.SetBounds(0, 0, (int)formW, (int)formH);

            int sidebarW = S(REF_SIDEBAR_W, sc);
            int sidebarH = (int)formH;
            pnlSidebar.SetBounds(0, 0, sidebarW, sidebarH);

            int rightW = (int)formW - sidebarW;
            int rightH = (int)formH;
            pnlRight.SetBounds(sidebarW, 0, rightW, rightH);

            ScaleSidebarInterior(sidebarW, sidebarH);
            ScaleRightInterior(rightW, rightH);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Sidebar interior  (ref: 366 × 750)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleSidebarInterior(int panelW, int panelH)
        {
            float sc = Math.Min(panelW / SB_REF_W, panelH / SB_REF_H);

            btnBackToConfig.SetBounds(S(14f, sc), S(20f, sc), S(150f, sc), S(38f, sc));

            int avatarSize = Math.Max(48, S(90f, sc));
            btnAvatar.SetBounds((panelW - avatarSize) / 2, S(76f, sc), avatarSize, avatarSize);
            btnAvatar.BorderRadius = avatarSize / 2;

            int labelPadX = S(14f, sc);
            int labelW = panelW - labelPadX * 2;
            lblCustomerName.SetBounds(labelPadX, S(178f, sc), labelW, S(30f, sc));
            lblQueueTicket.SetBounds(labelPadX, S(210f, sc), labelW, S(24f, sc));

            int cardPadX = S(18f, sc);
            int cardY = S(248f, sc);
            int cardW = panelW - cardPadX * 2;
            int cardH = Math.Max(100, panelH - cardY - S(12f, sc));
            pnlBreakdownCard.SetBounds(cardPadX, cardY, cardW, cardH);

            ScaleBreakdownCardInterior(cardW, cardH);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Breakdown card interior  (ref: 345 × 220)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleBreakdownCardInterior(int cardW, int cardH)
        {
            const float refCW = 345f;
            const float refCH = 220f;
            float sc = Math.Min(cardW / refCW, cardH / refCH);

            int padX = S(18f, sc);
            lblBreakdownTitle.SetBounds(padX, S(16f, sc), cardW - padX * 2, S(22f, sc));
            sep1.SetBounds(0, S(46f, sc), cardW, 1);

            int rowY = S(60f, sc);
            int rowH = Math.Max(20, S(28f, sc));
            int valX = S(180f, sc);
            int valW = cardW - valX - padX;

            lblBreakdownKey.SetBounds(padX, rowY, valX - padX - 4, rowH);
            lblBreakdownValue.SetBounds(valX, rowY, valW, rowH);

            int sep2Y = rowY + rowH + S(4f, sc);
            sep2.SetBounds(0, sep2Y, cardW, 1);

            int totKeyY = sep2Y + S(14f, sc);
            lblTotalDueKey.SetBounds(padX, totKeyY, S(190f, sc), S(24f, sc));

            int totValY = totKeyY + S(28f, sc);
            lblTotalDueValue.SetBounds(padX, totValY, cardW - padX * 2, S(30f, sc));
        }

        // ─────────────────────────────────────────────────────────────────────
        // Right panel interior  (ref content block w=480 inside 683)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleRightInterior(int panelW, int panelH)
        {
            float sc = Math.Min(panelW / RT_REF_W, panelH / RT_REF_H);

            int contentW = S(RT_CONTENT_W_REF, sc);
            int contentX = Math.Max(0, (panelW - contentW) / 2);

            lblAmountReceivedTitle.SetBounds(contentX, S(60f, sc), contentW, S(22f, sc));

            int inputH = S(90f, sc);
            pnlAmountInput.SetBounds(contentX, S(88f, sc), contentW, inputH);

            float inpSc = Math.Min(contentW / 480f, inputH / 90f);
            int pesoH = S(48f, inpSc);
            lblPesoSign.SetBounds(S(14f, inpSc), (inputH - pesoH) / 2, S(30f, inpSc), pesoH);
            int txtX = S(48f, inpSc);
            int txtH = S(70f, inpSc);
            txtAmountReceived.SetBounds(txtX, (inputH - txtH) / 2, contentW - txtX - S(4f, inpSc), txtH);

            int btnY = S(200f, sc);
            int btnH = S(65f, sc);
            int btn3W = S(108f, sc);
            int btnExW = S(120f, sc);
            int btnGap = S(12f, sc);

            btnAdd100.SetBounds(contentX, btnY, btn3W, btnH);
            btnAdd500.SetBounds(contentX + btn3W + btnGap, btnY, btn3W, btnH);
            btnAdd1000.SetBounds(contentX + (btn3W + btnGap) * 2, btnY, btn3W, btnH);
            btnExact.SetBounds(contentX + (btn3W + btnGap) * 3, btnY, btnExW, btnH);

            int changePnlH = S(90f, sc);
            pnlChangeDue.SetBounds(contentX, S(295f, sc), contentW, changePnlH);

            float chgSc = Math.Min(contentW / 480f, changePnlH / 90f);
            int chgValW = S(240f, chgSc);
            lblChangeDueTitle.SetBounds(S(24f, chgSc), S(28f, chgSc), S(160f, chgSc), S(28f, chgSc));
            lblChangeDueValue.SetBounds(contentW - chgValW - S(20f, chgSc), S(14f, chgSc), chgValW, S(62f, chgSc));

            btnProcess.SetBounds(contentX, S(415f, sc), contentW, S(75f, sc));
        }

        // ─────────────────────────────────────────────────────────────────────
        private void PopulateSidebar()
        {
            btnAvatar.Text = GetInitials(_customer.Name);
            btnAvatar.FillColor = GetAvatarBg(_customer.Name);
            btnAvatar.ForeColor = GetAvatarFg(_customer.Name);
            btnAvatar.HoverState.FillColor = btnAvatar.FillColor;
            btnAvatar.HoverState.ForeColor = btnAvatar.ForeColor;
            btnAvatar.PressedColor = btnAvatar.FillColor;

            lblCustomerName.Text = _customer.Name;
            lblQueueTicket.Text = _customer.QueueTicket;

            lblBreakdownKey.Text = $"Monthly Amortization (x{_numberOfMonths})";
            lblBreakdownValue.Text = $"₱{_monthlyAmortization:N2}";
            lblTotalDueValue.Text = $"₱{_totalDue:N0}";
        }

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
                lblChangeDueValue.ForeColor = Color.FromArgb(52, 211, 153);
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

        private void TxtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            _amountReceived = decimal.TryParse(txtAmountReceived.Text, out decimal val) ? val : 0m;
            RefreshCalculation();
        }

        private void BtnAdd100_Click(object sender, EventArgs e) => AddAmount(100m);
        private void BtnAdd500_Click(object sender, EventArgs e) => AddAmount(500m);
        private void BtnAdd1000_Click(object sender, EventArgs e) => AddAmount(1000m);

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

        // ─── Process → ReceiptForm3 ───────────────────────────────────────────
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (_amountReceived < _totalDue) return;

            var result = new CollectionResult3
            {
                Customer = _customer,
                NumberOfMonths = _numberOfMonths,
                MonthlyAmort = _monthlyAmortization,
                TotalDue = _totalDue,
                AmountReceived = _amountReceived,
                ChangeDue = _amountReceived - _totalDue,
                ProcessedAt = DateTime.Now
            };

            TransactionStore.Add(new Transaction
            {
                TransactionId = $"TX-{result.ProcessedAt:yyyyMMddHHmmssfff}",
                QueueNumber = result.Customer?.QueueTicket ?? string.Empty,
                DateTime = result.ProcessedAt,
                PaymentType = "Advance Payment",
                CustomerName = result.Customer?.Name ?? "Unknown Customer",
                UnitModel = result.Customer?.UnitDetails?.Model ?? "-",
                Amount = result.TotalDue,
                Status = "Paid"
            });

            TransactionComplete?.Invoke(this, result);

            var host = this.Parent;
            if (host == null) return;

            var receiptScreen = new ReceiptForm3(result)
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

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
            var p = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return p.Length >= 2
                ? $"{p[0][0]}{p[1][0]}".ToUpper()
                : name.Substring(0, Math.Min(2, name.Length)).ToUpper();
        }
    }

    public class CollectionResult3
    {
        public required CustomerSummary Customer { get; set; }
        public int NumberOfMonths { get; set; }
        public decimal MonthlyAmort { get; set; }
        public decimal TotalDue { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ChangeDue { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}