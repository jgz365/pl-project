using POSCashierSystem;
using System;
using System.Drawing;
using System.Windows.Forms;

// ── NO reference to MonthlyPayment_File — that namespace belongs to CollectionForm2/ReceiptForm2 ──

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
            ResizePanels();
            ResetPaymentState();
            txtAmountReceived.Focus();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) ResizePanels();
        }

        private void ResizePanels()
        {
            pnlRoot.Size = this.Size;
            pnlSidebar.Height = this.Height;

            pnlRight.Location = new Point(pnlSidebar.Right, 0);
            pnlRight.Size = new Size(Math.Max(0, Width - pnlSidebar.Width), Height);

            int contentW = Math.Min(560, pnlRight.Width - 80);
            int contentX = Math.Max(0, (pnlRight.Width - contentW) / 2);

            lblAmountReceivedTitle.Location = new Point(contentX, 60);
            lblAmountReceivedTitle.Width = contentW;

            pnlAmountInput.Location = new Point(contentX, 88);
            pnlAmountInput.Width = contentW;
            txtAmountReceived.Width = pnlAmountInput.Width - 60;

            int btnW = (contentW - 3 * 12) / 4;
            btnAdd100.Location = new Point(contentX, 192);
            btnAdd500.Location = new Point(contentX + btnW + 12, 192);
            btnAdd1000.Location = new Point(contentX + (btnW + 12) * 2, 192);
            btnExact.Location = new Point(contentX + (btnW + 12) * 3, 192);
            btnAdd100.Width = btnAdd500.Width = btnAdd1000.Width = btnExact.Width = btnW;

            pnlChangeDue.Location = new Point(contentX, 264);
            pnlChangeDue.Width = contentW;
            lblChangeDueValue.Location = new Point(pnlChangeDue.Width - 240, 14);
            lblChangeDueValue.Width = 216;

            btnProcess.Location = new Point(contentX, 354);
            btnProcess.Width = contentW;
        }

        private void PopulateSidebar()
        {
            btnAvatar.Text = GetInitials(_customer.Name);
            btnAvatar.FillColor = GetAvatarBg(_customer.Name);
            btnAvatar.ForeColor = GetAvatarFg(_customer.Name);
            btnAvatar.HoverState.FillColor = btnAvatar.FillColor;
            btnAvatar.HoverState.ForeColor = btnAvatar.ForeColor;
            btnAvatar.PressedColor = btnAvatar.FillColor;
            btnAvatar.Location = new Point((pnlSidebar.Width - btnAvatar.Width) / 2, 76);

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
                : Color.FromArgb(110, 175, 155);
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
                ProcessedAt = DateTime.Now   // real machine clock — date + time both accurate
            };

            TransactionComplete?.Invoke(this, result);

            var host = this.Parent;
            if (host == null) return;

            // ── Push ReceiptForm3 onto the same parent container ──────────────
            // ReceiptForm3 is in the SAME namespace (AdvancePayment_File) so no
            // extra using is needed — both classes live in this file's namespace.
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
            // Do NOT remove CollectionForm3 yet — ReceiptForm3's blur CopyFromScreen
            // needs the underlying content rendered. ReturnToPoscashier() inside
            // ReceiptForm3 disposes all UserControls in one shot when done.
        }

        private void BtnBackToConfig_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        // ─── Avatar helpers ───────────────────────────────────────────────────
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
            var p = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return p.Length >= 2
                ? $"{p[0][0]}{p[1][0]}".ToUpper()
                : name.Substring(0, Math.Min(2, name.Length)).ToUpper();
        }
    }

    // ── Result model — defined once here, referenced by ReceiptForm3 ──────────
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