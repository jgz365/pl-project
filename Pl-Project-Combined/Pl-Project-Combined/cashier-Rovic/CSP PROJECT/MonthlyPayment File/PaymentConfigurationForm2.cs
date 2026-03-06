using CSP_PROJECT.POSCashier.MonthlyPayment_File;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace POSCashierSystem
{
    /// <summary>
    /// Step 3 — Payment Configuration.
    /// Shows Transaction Summary card then opens CollectionForm2 on proceed.
    /// </summary>
    public partial class PaymentConfigurationForm2 : UserControl
    {
        private decimal _downPaymentAmount = 0m;
        private CustomerSummary? _customer;

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<decimal> ProceedRequested = delegate { };

        public PaymentConfigurationForm2()
        {
            InitializeComponent();
        }

        private void PaymentConfigurationForm_Load(object sender, EventArgs e)
        {
            RefreshLabels();
            CenterControls();
        }

        public void SetPaymentData(decimal amount, CustomerSummary? customer = null)
        {
            _downPaymentAmount = amount;
            _customer = customer;

            if (IsHandleCreated)
            {
                RefreshLabels();
                CenterControls();
            }
        }

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

        private void RefreshLabels()
        {
            lblDownPaymentValue.Text = $"₱{_downPaymentAmount:N2}";
            lblTotalDueValue.Text = $"₱{_downPaymentAmount:N2}";
        }

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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        // ─── Proceed → CollectionForm2 ────────────────────────────────────────
        private void BtnProceed_Click(object sender, EventArgs e)
        {
            ProceedRequested?.Invoke(this, _downPaymentAmount);

            var host = this.Parent;
            if (host == null) return;

            // ── FIX: use CollectionForm2, not the old CollectionForm ──────────
            var collectionScreen = new CollectionForm2
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            collectionScreen.SetData(_customer!);

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

        private void pnlCard_Paint(object sender, PaintEventArgs e) { }
    }
}