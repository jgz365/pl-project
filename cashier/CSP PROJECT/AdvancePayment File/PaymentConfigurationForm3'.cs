using System;
using System.Drawing;
using System.Windows.Forms;
using CSP_PROJECT.POSCashier.AdvancePayment_File;

namespace POSCashierSystem
{
    /// <summary>
    /// Step 2 — Advance Payment Configuration.
    /// Lets the cashier choose how many months to pay in advance,
    /// shows a live Transaction Summary card, then routes to CollectionForm3.
    /// </summary>
    public partial class PaymentConfigurationForm3 : UserControl
    {
        private CustomerSummary? _customer;
        private int _numberOfMonths = 1;          // min = 1
        private decimal _monthlyAmortization = 0m;

        // ── Validation constants ──────────────────────────────────────────────
        private const int MIN_MONTHS = 1;
        private const int MAX_MONTHS = 24;         // reasonable ceiling

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<decimal> ProceedRequested = delegate { };

        public PaymentConfigurationForm3()
        {
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Public API — called by AccountSummaryForm3
        // ─────────────────────────────────────────────────────────────────────
        public void SetPaymentData(CustomerSummary customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _monthlyAmortization = customer.FinancialStatus?.MonthlyAmortization ?? 0m;
            _numberOfMonths = 1;

            if (IsHandleCreated)
            {
                RefreshMonthDisplay();
                RefreshSummary();
                CenterControls();
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Load
        // ─────────────────────────────────────────────────────────────────────
        private void PaymentConfigurationForm3_Load(object sender, EventArgs e)
        {
            RefreshMonthDisplay();
            RefreshSummary();
            CenterControls();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Month stepper logic
        // ─────────────────────────────────────────────────────────────────────
        private void BtnMinus_Click(object sender, EventArgs e)
        {
            if (_numberOfMonths <= MIN_MONTHS) return;
            _numberOfMonths--;
            RefreshMonthDisplay();
            RefreshSummary();
        }

        private void BtnPlus_Click(object sender, EventArgs e)
        {
            if (_numberOfMonths >= MAX_MONTHS) return;
            _numberOfMonths++;
            RefreshMonthDisplay();
            RefreshSummary();
        }

        private void RefreshMonthDisplay()
        {
            lblMonthCount.Text = _numberOfMonths.ToString();
            // Dim the minus button at the floor
            btnMinus.Enabled = _numberOfMonths > MIN_MONTHS;
            btnPlus.Enabled = _numberOfMonths < MAX_MONTHS;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Recompute and refresh transaction summary
        // ─────────────────────────────────────────────────────────────────────
        private void RefreshSummary()
        {
            decimal total = _monthlyAmortization * _numberOfMonths;
            lblAmortKey.Text = $"Monthly Amortization (x{_numberOfMonths})";
            lblAmortValue.Text = $"₱{total:N2}";
            lblTotalDueValue.Text = $"₱{total:N2}";
        }

        private decimal TotalDue => _monthlyAmortization * _numberOfMonths;

        // ─────────────────────────────────────────────────────────────────────
        // Layout / resize
        // ─────────────────────────────────────────────────────────────────────
        private void CenterControls()
        {
            pnlBackground.Size = this.Size;

            int centerX = Math.Max(0, (Width - pnlSettingsCard.Width) / 2);
            int topY = Math.Max(0, (Height - (pnlSummaryCard.Height + pnlSettingsCard.Height + 18 + 80 + 18)) / 2);

            pnlSettingsCard.Location = new Point(centerX, topY);
            pnlSummaryCard.Location = new Point(centerX, pnlSettingsCard.Bottom + 18);
            btnProceed.Location = new Point(centerX, pnlSummaryCard.Bottom + 18);
            btnProceed.Width = pnlSummaryCard.Width;

            btnBack.Location = new Point(Width - btnBack.Width - 28, 18);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) CenterControls();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Button events
        // ─────────────────────────────────────────────────────────────────────
        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BtnProceed_Click(object sender, EventArgs e)
        {
            decimal total = TotalDue;
            ProceedRequested?.Invoke(this, total);

            var host = this.Parent;
            if (host == null) return;

            var collectionScreen = new CollectionForm3
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            collectionScreen.SetData(_customer!, _numberOfMonths, _monthlyAmortization);

            collectionScreen.BackRequested += (s, args) =>
            {
                host.Controls.Remove(collectionScreen);
                host.Controls.Add(this);
                this.BringToFront();
            };

            host.Controls.Add(collectionScreen);
            collectionScreen.BringToFront();
            host.Controls.Remove(this);
        }

        private void pnlSummaryCard_Paint(object sender, PaintEventArgs e) { }
    }
}