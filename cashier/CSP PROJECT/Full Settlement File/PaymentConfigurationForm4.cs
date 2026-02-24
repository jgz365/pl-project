using System;
using System.Drawing;
using System.Windows.Forms;
using POSCashierSystem;

namespace CSP_PROJECT.POSCashier.FullSettlement_File
{
    /// <summary>
    /// Step 2 — Full Settlement Payment Configuration.
    /// Displays Outstanding Principal Balance, applies an Early Settlement Rebate
    /// (percentage-based discount for settling the full loan early), and shows
    /// the resulting Total Due. Then routes to CollectionForm4.
    ///
    /// REBATE RULES (Philippine motorcycle financing norms):
    ///   • Rebate applies only when the customer is settling EARLY — i.e., there
    ///     are remaining months on the loan and the settlement date is before the
    ///     scheduled last due date.
    ///   • The rebate percentage (e.g., 5%) is applied against the Outstanding
    ///     Principal Balance (not the original contract price).
    ///   • Rebate = CurrentBalance × RebatePercent / 100
    ///   • Total Due = CurrentBalance − Rebate  (floor: ₱1.00, cannot go ≤ 0)
    ///   • If the balance is already at the last payment (≤ monthly amort),
    ///     no rebate is applied — customer just pays the remaining balance.
    ///   • RebatePercent is clamped between 0% and 25% (business rules).
    /// </summary>
    public partial class PaymentConfigurationForm4 : UserControl
    {
        private CustomerSummary? _customer;
        private decimal _outstandingBalance = 0m;
        private decimal _rebatePercent = 5m;      // default: 5%
        private decimal _rebateAmount = 0m;
        private decimal _totalDue = 0m;

        private const decimal MIN_REBATE_PCT = 0m;
        private const decimal MAX_REBATE_PCT = 25m;

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<decimal> ProceedRequested = delegate { };

        public PaymentConfigurationForm4()
        {
            InitializeComponent();
        }

        // ── Public API ─────────────────────────────────────────────────────────
        /// <summary>
        /// Called by AccountSummaryForm4 before pushing this screen onto the host.
        /// </summary>
        public void SetPaymentData(CustomerSummary customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _outstandingBalance = customer.FinancialStatus?.CurrentBalance ?? 0m;

            // Determine rebate eligibility:
            // No rebate if the balance is at or below one monthly amortization
            // (customer is on their last month — nothing to rebate).
            decimal amort = customer.FinancialStatus?.MonthlyAmortization ?? 0m;
            bool eligibleForRebate = _outstandingBalance > amort && _outstandingBalance > 0m;
            _rebatePercent = eligibleForRebate ? 5m : 0m;

            if (IsHandleCreated)
            {
                RefreshSummary();
                CenterControls();
            }
        }

        // ── Load ───────────────────────────────────────────────────────────────
        private void PaymentConfigurationForm4_Load(object sender, EventArgs e)
        {
            RefreshSummary();
            CenterControls();
        }

        // ── Compute & refresh ──────────────────────────────────────────────────
        private void RefreshSummary()
        {
            // Clamp rebate percent
            _rebatePercent = Math.Max(MIN_REBATE_PCT, Math.Min(MAX_REBATE_PCT, _rebatePercent));

            // Rebate calculation
            _rebateAmount = Math.Round(_outstandingBalance * _rebatePercent / 100m, 3);

            // Total due — floor at ₱1.00 to avoid zero/negative billing
            _totalDue = Math.Max(1m, _outstandingBalance - _rebateAmount);

            // Principal row
            lblPrincipalValue.Text = $"₱{_outstandingBalance:N2}";

            // Rebate row — show percentage in label
            lblRebateKey.Text = $"Early Settlement Rebate ({_rebatePercent:0.##}%)";
            lblRebateValue.Text = $"-₱{_rebateAmount:N3}";

            // Show/hide rebate row based on eligibility
            pnlRebateRow.Visible = _rebatePercent > 0m;

            // Total
            lblTotalDueValue.Text = $"₱{_totalDue:N3}";
        }

        // ── Layout ─────────────────────────────────────────────────────────────
        private void CenterControls()
        {
            pnlBackground.Size = this.Size;

            int centerX = Math.Max(0, (Width - pnlSummaryCard.Width) / 2);
            int topY = Math.Max(80, (Height - (pnlSummaryCard.Height + 18 + 80)) / 2);

            pnlSummaryCard.Location = new Point(centerX, topY);
            btnProceed.Location = new Point(centerX, pnlSummaryCard.Bottom + 18);
            btnProceed.Width = pnlSummaryCard.Width;

            btnBack.Location = new Point(Width - btnBack.Width - 28, 18);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) CenterControls();
        }

        // ── Button events ──────────────────────────────────────────────────────
        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BtnProceed_Click(object sender, EventArgs e)
        {
            if (_customer == null) return;

            ProceedRequested?.Invoke(this, _totalDue);

            var host = this.Parent;
            if (host == null) return;

            var collectionScreen = new CollectionForm4
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            collectionScreen.SetData(_customer, _outstandingBalance, _rebatePercent, _rebateAmount, _totalDue);

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

        private void pnlSummaryCard_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
    }
}