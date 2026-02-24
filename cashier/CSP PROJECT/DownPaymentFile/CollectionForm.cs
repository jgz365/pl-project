using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSCashierSystem
{
    public partial class CollectionForm : UserControl
    {
        private readonly CustomerSummary _customer;
        private readonly decimal _totalDue;
        private decimal _amountReceived = 0m;

        public event EventHandler BackRequested = delegate { };
        public event EventHandler<CollectionResult> TransactionComplete = delegate { };

        public CollectionForm(CustomerSummary customer, decimal totalDue)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _totalDue = totalDue;
            InitializeComponent();
        }

        private void CollectionForm_Load(object sender, EventArgs e)
        {
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
            // ── Avatar ──────────────────────────────────────────────────────────
            // btnAvatar is a Guna2Button styled as a circle. It renders its own
            // text clipped inside its own rounded shape, so there is no rectangular
            // child label that can bleed outside the circle corners.
            btnAvatar.Text = GetInitials(_customer.Name);
            btnAvatar.FillColor = GetAvatarBg(_customer.Name);
            btnAvatar.ForeColor = GetAvatarFg(_customer.Name);
            // Keep HoverState/PressedColor in sync so no color flash on hover
            btnAvatar.HoverState.FillColor = btnAvatar.FillColor;
            btnAvatar.HoverState.ForeColor = btnAvatar.ForeColor;
            btnAvatar.PressedColor = btnAvatar.FillColor;

            // Center horizontally in the sidebar
            btnAvatar.Location = new Point((pnlSidebar.Width - btnAvatar.Width) / 2, 76);

            lblCustomerName.Text = _customer.Name;
            lblQueueTicket.Text = _customer.QueueTicket;

            lblBreakdownValue.Text = $"₱{_totalDue:N2}";
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
            if (decimal.TryParse(txtAmountReceived.Text, out decimal val))
                _amountReceived = val;
            else
                _amountReceived = 0m;

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

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (_amountReceived < _totalDue) return;

            decimal change = _amountReceived - _totalDue;

            var result = new CollectionResult
            {
                Customer = _customer,
                TotalDue = _totalDue,
                AmountReceived = _amountReceived,
                ChangeDue = change,
                ProcessedAt = DateTime.Now
            };

            TransactionComplete?.Invoke(this, result);

            var host = this.Parent;
            if (host == null) return;

            var receiptScreen = new ReceiptForm(result)
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            receiptScreen.ResetRequested += (s, args) =>
            {
                host.Controls.Remove(receiptScreen);
            };

            host.Controls.Add(receiptScreen);
            receiptScreen.BringToFront();
        }

        private void BtnBackToConfig_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

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

    public class CollectionResult
    {
        public required CustomerSummary Customer { get; set; }
        public decimal TotalDue { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ChangeDue { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}