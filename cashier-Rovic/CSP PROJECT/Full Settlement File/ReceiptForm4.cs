using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using POSCashierSystem;

namespace CSP_PROJECT.POSCashier.FullSettlement_File
{
    /// <summary>
    /// ReceiptForm4 — Official Receipt Modal for the Full Settlement flow.
    /// Accepts CollectionResult4 from CollectionForm4.
    ///
    /// Same structure and logic as ReceiptForm3, with these differences:
    ///   • Transaction type = "SETTLEMENT"
    ///   • Breakdown shows: Outstanding Principal Balance + Early Settlement Rebate
    ///   • Loan Status panel shows Remaining Balance = ₱0 and
    ///     "🎉 Loan fully settled — account closed" instead of a next due date
    ///   • Theme colour: purple (109, 40, 217) instead of teal
    /// </summary>
    public partial class ReceiptForm4 : UserControl
    {
        private readonly CollectionResult4 _result;

        public event EventHandler ResetRequested = delegate { };

        // ─────────────────────────────────────────────────────────────────────
        public ReceiptForm4(CollectionResult4 result)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));

            if (_result.Customer == null)
                throw new ArgumentException("CollectionResult4.Customer cannot be null.", nameof(result));

            if (_result.TotalDue < 0)
                throw new ArgumentException("CollectionResult4.TotalDue cannot be negative.", nameof(result));

            if (_result.AmountReceived < _result.TotalDue)
                throw new ArgumentException("AmountReceived cannot be less than TotalDue.", nameof(result));

            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        private void ReceiptForm4_Load(object sender, EventArgs e)
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
        // Blur background — identical to ReceiptForm3
        // ─────────────────────────────────────────────────────────────────────
        private void CaptureAndBlurBackground()
        {
            try
            {
                this.Visible = false;
                Application.DoEvents();

                Form topForm = this.FindForm();
                if (topForm == null) { this.Visible = true; return; }

                var bmp = new Bitmap(topForm.Width, topForm.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(bmp))
                    g.CopyFromScreen(topForm.PointToScreen(Point.Empty), Point.Empty,
                                     topForm.Size, CopyPixelOperation.SourceCopy);

                var blurred = BoxBlur(bmp, 8);
                bmp.Dispose();

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

        private void CenterModal()
        {
            int x = Math.Max(0, (this.Width - pnlModalCard.Width) / 2);
            int y = Math.Max(10, (int)(this.Height * 0.04));
            pnlModalCard.Location = new Point(x, y);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Populate receipt
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateReceipt()
        {
            DateTime processedAt = _result.ProcessedAt;

            // Receipt number: year + full HHmmss timestamp
            lblReceiptNumber.Text = $"OR-{processedAt:yyyy}-{processedAt:HHmmss}";

            lblCustomerValue.Text = _result.Customer?.Name ?? "—";
            lblDateTimeValue.Text = processedAt.ToString("M/d/yyyy, h:mm:ss tt");
            lblTransactionValue.Text = "SETTLEMENT";

            // Breakdown — Principal Balance row
            lblBreakdownItemKey.Text = "Outstanding Principal Balance";
            lblBreakdownItemValue.Text = $"₱{_result.OutstandingBalance:N2}";

            // Breakdown — Rebate row
            lblBreakdownRebateKey.Text = $"Early Settlement Rebate ({_result.RebatePercent:0.##}%)";
            lblBreakdownRebateValue.Text = $"-₱{_result.RebateAmount:N3}";

            // Hide rebate row if rebate percent is 0
            bool hasRebate = _result.RebatePercent > 0m;
            lblBreakdownRebateKey.Visible = hasRebate;
            lblBreakdownRebateValue.Visible = hasRebate;

            lblTotalPaidValue.Text = $"₱{_result.TotalDue:N3}";
            lblTenderedValue.Text = $"₱{_result.AmountReceived:N2}";
            lblChangeValue.Text = $"₱{_result.ChangeDue:N2}";

            PopulateLoanStatus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Loan Status — Full Settlement means balance = ₱0, loan is closed
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateLoanStatus()
        {
            // After full settlement, remaining balance is always ₱0
            lblRemainingBalanceValue.Text = "₱0";

            // lblSettledNote is already set in Designer:
            // "🎉  Loan fully settled — account closed"
            // No need to compute a next due date.

            pnlLoanStatus.Visible = true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Navigation — identical to ReceiptForm3
        // ─────────────────────────────────────────────────────────────────────
        private void ReturnToPoscashier()
        {
            try
            {
                Form topForm = this.FindForm();
                if (topForm == null) return;

                var toRemove = new System.Collections.Generic.List<Control>();
                foreach (Control ctrl in topForm.Controls)
                    if (ctrl is UserControl) toRemove.Add(ctrl);

                foreach (Control ctrl in toRemove)
                {
                    topForm.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }

                if (topForm is POSCashierSystem.POSCashier posCashier)
                    posCashier.RestoreMainView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to POS Cashier: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Button events — identical to ReceiptForm3
        // ─────────────────────────────────────────────────────────────────────
        private void BtnClose_Click(object sender, EventArgs e) => ReturnToPoscashier();
        private void BtnStartNew_Click(object sender, EventArgs e) => ReturnToPoscashier();

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
                            $"Future: email receipt to {_result.Customer?.Name ?? "customer"}",
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