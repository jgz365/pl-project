using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace POSCashierSystem
{
    /// <summary>
    /// ReceiptForm — Official Receipt Modal for the Down Payment flow.
    /// Accepts CollectionResult from CollectionForm.
    /// Mirrors the architecture of ReceiptForm2 (blur overlay, ReturnToPoscashier).
    /// </summary>
    public partial class ReceiptForm : UserControl
    {
        // ── Injected data ─────────────────────────────────────────────────────
        private readonly CollectionResult _result;

        // ── Events ────────────────────────────────────────────────────────────
        /// <summary>Raised when the user clicks Close or "Start New Transaction".</summary>
        public event EventHandler ResetRequested = delegate { };

        // ─────────────────────────────────────────────────────────────────────
        // Constructor
        // ─────────────────────────────────────────────────────────────────────
        public ReceiptForm(CollectionResult result)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));
            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Load
        // ─────────────────────────────────────────────────────────────────────
        private void ReceiptForm_Load(object sender, EventArgs e)
        {
            PopulateReceipt();
            CaptureAndBlurBackground();
            CenterModal();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated)
            {
                pnlOverlay.Size = this.Size;
                CenterModal();
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Blur
        // ─────────────────────────────────────────────────────────────────────
        private void CaptureAndBlurBackground()
        {
            try
            {
                this.Visible = false;
                Application.DoEvents();

                var parent = this.Parent;
                if (parent == null) { this.Visible = true; return; }

                Form topForm = this.FindForm();
                if (topForm == null) { this.Visible = true; return; }

                var bmp = new Bitmap(topForm.Width, topForm.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(topForm.PointToScreen(Point.Empty), Point.Empty,
                                     topForm.Size, CopyPixelOperation.SourceCopy);
                }

                var blurred = BoxBlur(bmp, 8);
                bmp?.Dispose();

                using (var g2 = Graphics.FromImage(blurred))
                using (var brush = new SolidBrush(Color.FromArgb(140, 10, 16, 32)))
                    g2.FillRectangle(brush, 0, 0, blurred.Width, blurred.Height);

                pnlOverlay.BackgroundImage = blurred;
                pnlOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                pnlOverlay.BackColor = Color.Transparent;
            }
            catch
            {
                pnlOverlay.BackColor = Color.FromArgb(200, 15, 23, 42);
            }
            finally
            {
                this.Visible = true;
            }
        }

        private static Bitmap BoxBlur(Bitmap source, int radius)
        {
            int w = source.Width, h = source.Height;
            int smallW = Math.Max(1, w / (radius * 2));
            int smallH = Math.Max(1, h / (radius * 2));

            var small = new Bitmap(smallW, smallH, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(small))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.DrawImage(source, 0, 0, smallW, smallH);
            }

            var blurred = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(blurred))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.DrawImage(small, 0, 0, w, h);
            }

            small.Dispose();
            return blurred;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Center the modal card
        // ─────────────────────────────────────────────────────────────────────
        private void CenterModal()
        {
            int x = Math.Max(0, (this.Width - pnlModalCard.Width) / 2);
            int y = Math.Max(10, (int)(this.Height * 0.02));
            pnlModalCard.Location = new Point(x, y);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Populate all receipt labels from _result
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateReceipt()
        {
            string receiptNo = $"OR-{DateTime.Now.Year}-{DateTime.Now:HHmmss}";
            lblReceiptNumber.Text = receiptNo;

            lblCustomerValue.Text = _result?.Customer?.Name ?? "—";
            lblDateTimeValue.Text = _result != null ? _result.ProcessedAt.ToString("M/d/yyyy, h:mm:ss tt") : "—";
            lblTransactionValue.Text = "DOWN PAYMENT";

            lblBreakdownItemKey.Text = "Down Payment";
            lblBreakdownItemValue.Text = $"₱{(_result?.TotalDue ?? 0m):N2}";

            lblTotalPaidValue.Text = $"₱{(_result?.TotalDue ?? 0m):N2}";
            lblTenderedValue.Text = $"₱{(_result?.AmountReceived ?? 0m):N2}";
            lblChangeValue.Text = $"₱{(_result?.ChangeDue ?? 0m):N2}";

            PopulateLoanStatus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Loan Status — calculated from JSON data after down payment
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateLoanStatus()
        {
            var fs = _result?.Customer?.FinancialStatus;
            if (fs == null)
            {
                pnlLoanStatus.Visible = false;
                return;
            }

            // Remaining Balance = CurrentBalance minus the down payment just made
            decimal remainingBalance = fs.CurrentBalance - (_result?.TotalDue ?? 0m);
            if (remainingBalance < 0m) remainingBalance = 0m;

            lblRemainingBalanceValue.Text = $"₱{remainingBalance:N2}";

            // First Payment Due = FirstDueDate from JSON (down payment sets the first due date)
            string nextDueLabel = "—";
            if (!string.IsNullOrWhiteSpace(fs.NextDueDate))
            {
                if (DateTime.TryParse(fs.NextDueDate, out DateTime firstDue))
                {
                    nextDueLabel = firstDue.ToString("MMM d, yyyy");

                    // Colour-code: red if already past, blue otherwise
                    lblNextDueValue.ForeColor = firstDue < DateTime.Today
                        ? System.Drawing.Color.FromArgb(225, 29, 72)
                        : System.Drawing.Color.FromArgb(37, 99, 235);
                }
                else
                {
                    nextDueLabel = fs.NextDueDate; // fallback: show raw string
                }
            }

            lblNextDueValue.Text = nextDueLabel;
            pnlLoanStatus.Visible = true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Close everything and return to POSCashier
        // ─────────────────────────────────────────────────────────────────────
        private void ReturnToPoscashier()
        {
            try
            {
                Form topForm = this.FindForm();
                if (topForm == null) return;

                var toRemove = new System.Collections.Generic.List<Control>();
                foreach (Control ctrl in topForm.Controls)
                {
                    if (ctrl is UserControl)
                        toRemove.Add(ctrl);
                }

                foreach (Control ctrl in toRemove)
                {
                    topForm.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }

                // Fully qualify the POSCashier type to avoid namespace/type name conflicts.
                if (topForm is POSCashierSystem.POSCashier posCashier)
                {
                    posCashier.RestoreMainView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to POS Cashier: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Button events
        // ─────────────────────────────────────────────────────────────────────
        private void BtnClose_Click(object sender, EventArgs e)
        {
            ReturnToPoscashier();
        }

        private void BtnStartNew_Click(object sender, EventArgs e)
        {
            ReturnToPoscashier();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality not yet implemented.\n\n" +
                            "Future: integrate with a thermal printer driver or\n" +
                            "send to default Windows printer.",
                            "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Email functionality not yet implemented.\n\n" +
                            $"Future: email receipt to {_result?.Customer?.Name ?? "customer"}",
                            "Email Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSavePDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Save PDF functionality not yet implemented.\n\n" +
                            "Future: generate PDF and prompt Save As dialog.",
                            "Save PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSMS_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SMS functionality not yet implemented.\n\n" +
                            "Future: send receipt via SMS gateway.",
                            "Send SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}