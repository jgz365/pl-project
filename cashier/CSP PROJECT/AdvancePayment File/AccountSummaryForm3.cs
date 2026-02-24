using CSP_PROJECT.POSCashier.FullSettlement_File;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSCashierSystem
{
    public partial class AccountSummaryForm3 : UserControl
    {
        private readonly CustomerSummary _customer;

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<CustomerSummary> ContinueRequested = delegate { };

        public AccountSummaryForm3(CustomerSummary customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            InitializeComponent();
        }

        private void AccountSummaryForm3_Load(object sender, EventArgs e)
        {
            PopulateLabels();
            CenterCard();
        }

        private void PopulateLabels()
        {
            lblInitials.Text = GetInitials(_customer.Name);
            pnlAvatar.FillColor = GetAvatarBg(_customer.Name);
            lblInitials.ForeColor = GetAvatarFg(_customer.Name);

            lblCustomerName.Text = _customer.Name;
            lblLocation.Text = _customer.Location ?? "Quezon City";
            chipTicket.Text = _customer.QueueTicket;
            chipStatus.Text = _customer.TransactionType ?? "ADVANCE";

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

        private void CenterCard()
        {
            pnlCard.Location = new Point(
                Math.Max(0, (Width - pnlCard.Width) / 2),
                Math.Max(0, (Height - pnlCard.Height) / 2 + 14));
            pnlBackground.Size = this.Size;
            btnBack.Location = new Point(Width - btnBack.Width - 28, 18);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) CenterCard();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            ContinueRequested?.Invoke(this, _customer);

            var host = this.Parent;
            if (host == null) return;

            // ── Step 2: Route to PaymentConfigurationForm3 (Advance Payment) ──
            var paymentConfig = new PaymentConfigurationForm3
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            paymentConfig.SetPaymentData(_customer);

            paymentConfig.BackRequested += (s, args) =>
            {
                host.Controls.Remove(paymentConfig);
                host.Controls.Add(this);
                this.BringToFront();
            };

            host.Controls.Add(paymentConfig);
            paymentConfig.BringToFront();
            host.Controls.Remove(this);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Avatar helpers
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
        private static Color GetAvatarBg(string n)
        {
            if (string.IsNullOrWhiteSpace(n)) return _bgPalette[0];
            return _bgPalette[char.ToUpper(n[0]) % _bgPalette.Length];
        }
        private static Color GetAvatarFg(string n)
        {
            if (string.IsNullOrWhiteSpace(n)) return _fgPalette[0];
            return _fgPalette[char.ToUpper(n[0]) % _fgPalette.Length];
        }
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