using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSCashierSystem
{
    /// <summary>
    /// Step 2 — Account Summary.
    /// All UI layout lives in AccountSummaryForm.Designer.cs.
    /// This file contains ONLY data-binding logic and navigation events.
    /// </summary>
    public partial class AccountSummaryForm : UserControl
    {
        // ── Injected data ─────────────────────────────────────────────────────
        private readonly CustomerSummary _customer;

        // ── Navigation events ─────────────────────────────────────────────────
        public event EventHandler BackRequested;
        public event EventHandler<CustomerSummary> ContinueRequested;

        // ─────────────────────────────────────────────────────────────────────
        // Constructor
        // ─────────────────────────────────────────────────────────────────────
        public AccountSummaryForm(CustomerSummary customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Load
        // ─────────────────────────────────────────────────────────────────────
        private void AccountSummaryForm_Load(object sender, EventArgs e)
        {
            PopulateLabels();
            CenterCard();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Fill every Designer label from _customer
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateLabels()
        {
            // Avatar
            lblInitials.Text = GetInitials(_customer.Name);
            pnlAvatar.FillColor = GetAvatarBg(_customer.Name);
            lblInitials.ForeColor = GetAvatarFg(_customer.Name);

            // Header
            lblCustomerName.Text = _customer.Name;
            chipTicket.Text = _customer.QueueTicket;
            chipStatus.Text = _customer.TransactionType ?? "DOWN PAYMENT";

            // Unit Details
            lblModelValue.Text = _customer.UnitDetails?.Model ?? "—";
            lblColorValue.Text = _customer.UnitDetails?.Color ?? "—";
            lblEngineValue.Text = _customer.UnitDetails?.EngineNo ?? "—";

            // Financial Status
            decimal amort = _customer.FinancialStatus?.MonthlyAmortization ?? 0m;
            lblAmortValue.Text = $"₱{amort:N2}";
        }

        // ─────────────────────────────────────────────────────────────────────
        // Center card + stretch background
        // ─────────────────────────────────────────────────────────────────────
        private void CenterCard()
        {
            pnlCard.Location = new Point(
                Math.Max(0, (Width - pnlCard.Width) / 2),
                Math.Max(0, (Height - pnlCard.Height) / 2 + 14)
            );
            pnlBackground.Size = this.Size;
            btnBack.Location = new Point(Width - btnBack.Width - 28, 18);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) CenterCard();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Button events
        // ─────────────────────────────────────────────────────────────────────
        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            ContinueRequested?.Invoke(this, _customer);

            var host = this.Parent;
            if (host == null) return;

            decimal dpAmount = _customer.FinancialStatus?.DownPaymentDue ?? 0m;

            var payScreen = new PaymentConfigurationForm
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            // ── FIX: pass BOTH the amount AND the customer ────────────────────
            payScreen.SetPaymentData(dpAmount, _customer);

            payScreen.BackRequested += (s, args) =>
            {
                host.Controls.Remove(payScreen);
                host.Controls.Add(this);
                this.BringToFront();
            };

            payScreen.ProceedRequested += (s, amount) =>
            {
                // handled inside PaymentConfigurationForm.BtnProceed_Click
            };

            host.Controls.Add(payScreen);
            payScreen.BringToFront();
            host.Controls.Remove(this);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Avatar palette helpers
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
        {
            if (string.IsNullOrWhiteSpace(name)) return _bgPalette[0];
            return _bgPalette[char.ToUpper(name[0]) % _bgPalette.Length];
        }

        private static Color GetAvatarFg(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return _fgPalette[0];
            return _fgPalette[char.ToUpper(name[0]) % _fgPalette.Length];
        }

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