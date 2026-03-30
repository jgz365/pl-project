using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSP_PROJECT
{
    /// <summary>
    /// CollectionForm5 — Cashier payment collection screen for the kiosk full-cash flow.
    /// Receives customer name, charges breakdown, and total from ck_confirm_payment.
    /// Same UX pattern as CollectionForm4 (Full Settlement).
    /// </summary>
    public partial class CollectionForm5 : UserControl
    {
        // ── Data passed in from ck_confirm_payment ────────────────────────────
        private string _customerName = string.Empty;
        private string _mobileNumber = string.Empty;
        private List<(string Label, decimal Amount)> _charges = new();
        private decimal _totalDue = 0m;
        private decimal _amountReceived = 0m;

        public event EventHandler BackRequested = delegate { };

        public CollectionForm5()
        {
            InitializeComponent();
        }

        // ── Called by ck_confirm_payment before adding to parent ──────────────
        public void SetData(string customerName, string mobileNumber,
                            List<(string Label, decimal Amount)> charges,
                            decimal totalDue)
        {
            _customerName = customerName ?? string.Empty;
            _mobileNumber = mobileNumber ?? string.Empty;
            _charges = charges ?? new List<(string, decimal)>();
            _totalDue = totalDue;
        }

        // ── Load ─────────────────────────────────────────────────────────────
        private void CollectionForm5_Load(object sender, EventArgs e)
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

        // ── Responsive layout ─────────────────────────────────────────────────
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
            btnAdd100.Location = new Point(contentX, 200);
            btnAdd500.Location = new Point(contentX + btnW + 12, 200);
            btnAdd1000.Location = new Point(contentX + (btnW + 12) * 2, 200);
            btnExact.Location = new Point(contentX + (btnW + 12) * 3, 200);
            btnAdd100.Width = btnAdd500.Width = btnAdd1000.Width = btnExact.Width = btnW;

            pnlChangeDue.Location = new Point(contentX, 295);
            pnlChangeDue.Width = contentW;
            lblChangeDueValue.Location = new Point(pnlChangeDue.Width - 240, 14);
            lblChangeDueValue.Width = 216;

            btnProcess.Location = new Point(contentX, 415);
            btnProcess.Width = contentW;
        }

        // ── Populate sidebar with customer name + charges ─────────────────────
        private void PopulateSidebar()
        {
            // Avatar
            string initials = GetInitials(_customerName);
            btnAvatar.Text = initials;
            btnAvatar.FillColor = GetAvatarBg(_customerName);
            btnAvatar.ForeColor = GetAvatarFg(_customerName);
            btnAvatar.HoverState.FillColor = btnAvatar.FillColor;
            btnAvatar.HoverState.ForeColor = btnAvatar.ForeColor;
            btnAvatar.PressedColor = btnAvatar.FillColor;
            btnAvatar.Location = new Point((pnlSidebar.Width - btnAvatar.Width) / 2, 76);

            lblCustomerName.Text = _customerName;
            lblCustomerSubtitle.Text = string.IsNullOrWhiteSpace(_mobileNumber)
                ? "Full Cash Purchase"
                : _mobileNumber;

            // Build charges rows dynamically
            BuildChargesRows();
        }

        private void BuildChargesRows()
        {
            // Clear existing dynamic rows (keep title + separator + total)
            pnlBreakdownCard.Controls.Clear();
            pnlBreakdownCard.Controls.Add(lblBreakdownTitle);
            pnlBreakdownCard.Controls.Add(sep1);

            int y = 56;
            const int rowH = 28;

            foreach (var (label, amount) in _charges)
            {
                var key = new Label
                {
                    BackColor = System.Drawing.Color.Transparent,
                    Font = new System.Drawing.Font("Segoe UI", 9F),
                    ForeColor = System.Drawing.Color.FromArgb(71, 85, 105),
                    Location = new Point(18, y),
                    Size = new System.Drawing.Size(190, 24),
                    Text = label
                };
                var val = new Label
                {
                    BackColor = System.Drawing.Color.Transparent,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(30, 41, 59),
                    Location = new Point(200, y),
                    Size = new System.Drawing.Size(120, 24),
                    Text = $"₱{amount:N2}",
                    TextAlign = System.Drawing.ContentAlignment.TopRight
                };
                pnlBreakdownCard.Controls.Add(key);
                pnlBreakdownCard.Controls.Add(val);
                y += rowH;
            }

            // Separator before total
            sep2.Location = new Point(0, y + 4);
            sep2.Size = new System.Drawing.Size(302, 1);
            pnlBreakdownCard.Controls.Add(sep2);

            // Total Due row
            lblTotalDueKey.Location = new Point(18, y + 14);
            lblTotalDueValue.Location = new Point(18, y + 38);
            lblTotalDueValue.Text = $"₱{_totalDue:N0}";
            pnlBreakdownCard.Controls.Add(lblTotalDueKey);
            pnlBreakdownCard.Controls.Add(lblTotalDueValue);

            // Resize card to fit content
            pnlBreakdownCard.Height = y + 80;
        }

        // ── Payment state ─────────────────────────────────────────────────────
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
                lblChangeDueValue.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            }
            else if (paid)
            {
                lblChangeDueValue.Text = $"₱{change:N0}";
                lblChangeDueValue.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105); // green
            }
            else
            {
                decimal shortage = _totalDue - _amountReceived;
                lblChangeDueValue.Text = $"-₱{shortage:N0}";
                lblChangeDueValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113); // red
            }

            btnProcess.Enabled = paid;
            btnProcess.FillColor = paid
                ? System.Drawing.Color.FromArgb(5, 150, 105)   // teal when ready
                : System.Drawing.Color.FromArgb(167, 243, 208); // light teal disabled
        }

        // ── Amount buttons ────────────────────────────────────────────────────
        private void TxtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            _amountReceived = decimal.TryParse(txtAmountReceived.Text, out decimal v) ? v : 0m;
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

        // ── Process transaction ───────────────────────────────────────────────
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            if (_amountReceived < _totalDue) return;

            var result = new CollectionResult5
            {
                CustomerName = _customerName,
                MobileNumber = _mobileNumber,
                Charges = _charges,
                TotalDue = _totalDue,
                AmountReceived = _amountReceived,
                ChangeDue = _amountReceived - _totalDue,
                ProcessedAt = DateTime.Now
            };

            POSCashierSystem.TransactionStore.Add(new POSCashierSystem.Transaction
            {
                TransactionId = $"OR-{result.ProcessedAt:yyyy}-{result.ProcessedAt:HHmmss}",
                DateTime = result.ProcessedAt,
                PaymentType = "Full Cash",
                CustomerName = result.CustomerName,
                UnitModel = result.Charges.Count > 0 ? result.Charges[0].Label : "Unit Purchase",
                Amount = result.TotalDue,
                Status = "Paid"
            });

            var host = this.Parent;
            if (host == null) return;

            var receiptScreen = new ReceiptForm5(result)
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

        // ── Back button ───────────────────────────────────────────────────────
        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        // ── Avatar helpers (same palette as CollectionForm4) ──────────────────
        private static readonly System.Drawing.Color[] _bgPalette =
        {
            System.Drawing.Color.FromArgb(219, 234, 254),
            System.Drawing.Color.FromArgb(209, 250, 229),
            System.Drawing.Color.FromArgb(254, 243, 199),
            System.Drawing.Color.FromArgb(237, 233, 254),
            System.Drawing.Color.FromArgb(255, 228, 230),
        };
        private static readonly System.Drawing.Color[] _fgPalette =
        {
            System.Drawing.Color.FromArgb(37,  99, 235),
            System.Drawing.Color.FromArgb(5,  150, 105),
            System.Drawing.Color.FromArgb(217, 119,   6),
            System.Drawing.Color.FromArgb(109,  40, 217),
            System.Drawing.Color.FromArgb(225,  29,  72),
        };
        private static System.Drawing.Color GetAvatarBg(string name)
            => string.IsNullOrWhiteSpace(name) ? _bgPalette[0]
               : _bgPalette[char.ToUpper(name[0]) % _bgPalette.Length];
        private static System.Drawing.Color GetAvatarFg(string name)
            => string.IsNullOrWhiteSpace(name) ? _fgPalette[0]
               : _fgPalette[char.ToUpper(name[0]) % _fgPalette.Length];
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