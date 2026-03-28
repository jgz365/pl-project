using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSCashierSystem
{
    public partial class AccountSummaryForm2 : UserControl
    {
        private readonly CustomerSummary _customer;

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<CustomerSummary> ContinueRequested = delegate { };

        // ── Reference card dimensions (from designer) ─────────────────
        // pnlBackground: 1221 × 750
        // pnlCard:       1101 × 560  at (58, 104)
        private const float REF_UC_W = 1221f;
        private const float REF_UC_H = 750f;
        private const float REF_CARD_W = 1101f;
        private const float REF_CARD_H = 560f;

        public AccountSummaryForm2(CustomerSummary customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            InitializeComponent();
        }

        private void AccountSummaryForm2_Load(object sender, EventArgs e)
        {
            PopulateLabels();
            ApplyResponsiveLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) ApplyResponsiveLayout();
        }

        // ═════════════════════════════════════════════════════════════
        //  RESPONSIVE LAYOUT
        // ═════════════════════════════════════════════════════════════
        private void ApplyResponsiveLayout()
        {
            int w = Math.Max(400, Width);
            int h = Math.Max(300, Height);

            // ── pnlBackground fills everything ────────────────────────
            pnlBackground.SetBounds(0, 0, w, h);

            // ── Scale the card proportionally, capped at reference size ─
            float sx = (float)w / REF_UC_W;
            float sy = (float)h / REF_UC_H;
            float s = Math.Min(sx, sy);          // uniform scale
            s = Math.Min(s, 1.0f);               // never bigger than reference

            int cardW = (int)(REF_CARD_W * s);
            int cardH = (int)(REF_CARD_H * s);

            // Vertical nudge: keep the header labels visible (≈104px in reference)
            int headerH = (int)(104f * sy);
            int availH = h - headerH;
            if (cardH > availH - 20) cardH = Math.Max(300, availH - 20);

            // Centre horizontally; sit below header area
            int cardX = Math.Max(0, (w - cardW) / 2);
            int cardY = headerH;
            pnlCard.SetBounds(cardX, cardY, cardW, cardH);

            // Adjust the internal separator and Continue button to card height
            sep2.SetBounds(0, cardH - 86, cardW, 1);
            btnContinue.Location = new Point(cardW - btnContinue.Width - 24, cardH - 72);

            // ── Header labels + back button ────────────────────────────
            lblPageTitle.Location = new Point(28, 18);
            lblPageSubtitle.Location = new Point(30, 52);
            btnBack.Location = new Point(w - btnBack.Width - 28, 18);
        }

        // ═════════════════════════════════════════════════════════════
        //  DATA
        // ═════════════════════════════════════════════════════════════
        private void PopulateLabels()
        {
            lblInitials.Text = GetInitials(_customer.Name);
            pnlAvatar.FillColor = GetAvatarBg(_customer.Name);
            lblInitials.ForeColor = GetAvatarFg(_customer.Name);

            lblCustomerName.Text = _customer.Name;
            lblLocation.Text = _customer.Location ?? "Quezon City";
            chipTicket.Text = _customer.QueueTicket;
            chipStatus.Text = _customer.TransactionType ?? "MONTHLY";

            lblModelValue.Text = _customer.UnitDetails?.Model ?? "—";
            lblColorValue.Text = _customer.UnitDetails?.Color ?? "—";
            lblEngineValue.Text = _customer.UnitDetails?.EngineNo ?? "—";

            decimal balance = _customer.FinancialStatus?.CurrentBalance ?? 0m;
            decimal amort = _customer.FinancialStatus?.MonthlyAmortization ?? 0m;
            string dueDate = _customer.FinancialStatus?.NextDueDate ?? "—";

            lblBalanceValue.Text = $"₱{balance:N2}";
            lblAmortValue.Text = $"₱{amort:N2}";
            lblDueDateValue.Text = dueDate;
        }

        // ═════════════════════════════════════════════════════════════
        //  BUTTON HANDLERS
        // ═════════════════════════════════════════════════════════════
        private void BtnBack_Click(object sender, EventArgs e)
            => BackRequested?.Invoke(this, EventArgs.Empty);

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            ContinueRequested?.Invoke(this, _customer);

            var host = this.Parent;
            if (host == null) return;

            var paymentConfig = new PaymentConfigurationForm2
            {
                Dock = DockStyle.Fill,
                Margin = Padding.Empty
            };

            paymentConfig.SetPaymentData(
                _customer.FinancialStatus?.MonthlyAmortization ?? 0m,
                _customer);

            paymentConfig.BackRequested += (s, args) =>
            {
                host.Controls.Remove(paymentConfig);
                paymentConfig.Dispose();
                host.Controls.Add(this);
                this.BringToFront();
            };

            host.Controls.Add(paymentConfig);
            paymentConfig.BringToFront();
            host.Controls.Remove(this);
        }

        // ═════════════════════════════════════════════════════════════
        //  AVATAR HELPERS
        // ═════════════════════════════════════════════════════════════
        private static readonly Color[] _bgPalette =
        {
            Color.FromArgb(219, 234, 254), Color.FromArgb(209, 250, 229),
            Color.FromArgb(254, 243, 199), Color.FromArgb(237, 233, 254),
            Color.FromArgb(255, 228, 230),
        };
        private static readonly Color[] _fgPalette =
        {
            Color.FromArgb(37,  99, 235), Color.FromArgb(5,  150, 105),
            Color.FromArgb(217, 119,   6), Color.FromArgb(109, 40, 217),
            Color.FromArgb(225,  29,  72),
        };

        private static Color GetAvatarBg(string n)
            => string.IsNullOrWhiteSpace(n) ? _bgPalette[0]
               : _bgPalette[char.ToUpper(n[0]) % _bgPalette.Length];

        private static Color GetAvatarFg(string n)
            => string.IsNullOrWhiteSpace(n) ? _fgPalette[0]
               : _fgPalette[char.ToUpper(n[0]) % _fgPalette.Length];

        private static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";
            var p = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return p.Length >= 2
                ? $"{p[0][0]}{p[1][0]}".ToUpper()
                : name.Substring(0, Math.Min(2, name.Length)).ToUpper();
        }
    }
}