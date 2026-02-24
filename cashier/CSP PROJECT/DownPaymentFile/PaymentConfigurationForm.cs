using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace POSCashierSystem
{
    /// <summary>
    /// Step 3 — Payment Configuration.
    /// Shows Transaction Summary card then opens CollectionForm on proceed.
    /// </summary>
    public partial class PaymentConfigurationForm : UserControl
    {
        private decimal _downPaymentAmount = 0m;
        private CustomerSummary _customer;          // kept for handoff to CollectionForm

        public event EventHandler BackRequested;
        public event EventHandler<decimal> ProceedRequested;

        // ─────────────────────────────────────────────────────────────────────
        // Constructor — parameterless so Designer works
        // ─────────────────────────────────────────────────────────────────────
        public PaymentConfigurationForm()
        {
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Load
        // ─────────────────────────────────────────────────────────────────────
        private void PaymentConfigurationForm_Load(object sender, EventArgs e)
        {
            RefreshLabels();
            CenterControls();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Public API — called by AccountSummaryForm before adding to host
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Primary entry point.  AccountSummaryForm calls this with the
        /// customer's FinancialStatus.DownPaymentDue value.
        /// </summary>
        public void SetPaymentData(decimal amount, CustomerSummary customer = null)
        {
            _downPaymentAmount = amount;
            _customer = customer;

            if (IsHandleCreated)
            {
                RefreshLabels();
                CenterControls();
            }
        }

        /// <summary>
        /// Optional: read amount from payment.json.
        /// Shape: { "DownPaymentAmount": 53400.00 }
        /// </summary>
        public void LoadFromJson(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("payment.json not found.", jsonPath);

            var data = JsonConvert.DeserializeObject<PaymentData>(File.ReadAllText(jsonPath));
            _downPaymentAmount = data?.DownPaymentAmount ?? 0m;

            if (IsHandleCreated)
            {
                RefreshLabels();
                CenterControls();
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Update the two value labels
        // ─────────────────────────────────────────────────────────────────────
        private void RefreshLabels()
        {
            lblDownPaymentValue.Text = $"₱{_downPaymentAmount:N2}";
            lblTotalDueValue.Text = $"₱{_downPaymentAmount:N2}";
        }

        // ─────────────────────────────────────────────────────────────────────
        // Center card + button based on actual control width
        // ─────────────────────────────────────────────────────────────────────
        private void CenterControls()
        {
            pnlBackground.Size = this.Size;

            int cardX = Math.Max(0, (Width - pnlCard.Width) / 2);
            int cardY = Math.Max(0, (Height - pnlCard.Height) / 2 - 50);
            pnlCard.Location = new Point(cardX, cardY);

            btnProceed.Location = new Point(cardX, pnlCard.Bottom + 18);
            btnProceed.Width = pnlCard.Width;

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
            ProceedRequested?.Invoke(this, _downPaymentAmount);

            var host = this.Parent;
            if (host == null) return;

            // Build CollectionForm — pass the customer object and total due
            var collectionScreen = new CollectionForm(_customer, _downPaymentAmount)
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            // "← Back to Config" → return here
            collectionScreen.BackRequested += (s, args) =>
            {
                host.Controls.Remove(collectionScreen);
                host.Controls.Add(this);
                this.BringToFront();
            };

            // Transaction complete → wire your ReceiptForm here
            collectionScreen.TransactionComplete += (s, result) =>
            {
                // TODO: show ReceiptForm(result) or reset to DownPaymentForm
                // For now the CollectionForm shows a MessageBox confirmation.
            };

            host.Controls.Add(collectionScreen);
            collectionScreen.BringToFront();
            host.Controls.Remove(this);
        }
    }

    // =========================================================================
    // JSON model
    // =========================================================================
    public class PaymentData
    {
        public decimal DownPaymentAmount { get; set; }
    }
}