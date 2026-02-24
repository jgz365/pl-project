using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using POSCashierSystem;

// ── CollectionResult3 is defined in CollectionForm3.cs in this same namespace ──
// ── No extra using needed — they share CSP_PROJECT.POSCashier.AdvancePayment_File ──

namespace CSP_PROJECT.POSCashier.AdvancePayment_File
{
    public partial class ReceiptForm3 : UserControl
    {
        private readonly CollectionResult3 _result;

        public event EventHandler ResetRequested = delegate { };

        // ─────────────────────────────────────────────────────────────────────
        public ReceiptForm3(CollectionResult3 result)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));

            if (_result.Customer == null)
                throw new ArgumentException("Customer cannot be null.", nameof(result));
            if (_result.NumberOfMonths < 1)
                throw new ArgumentException("NumberOfMonths must be ≥ 1.", nameof(result));
            if (_result.TotalDue < 0)
                throw new ArgumentException("TotalDue cannot be negative.", nameof(result));
            if (_result.AmountReceived < _result.TotalDue)
                throw new ArgumentException("AmountReceived cannot be less than TotalDue.", nameof(result));

            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        private void ReceiptForm3_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateReceipt();
            }
            catch (Exception ex)
            {
                // Surface the real error so you can see which control is null
                MessageBox.Show(
                    $"Error populating receipt:\n\n{ex.GetType().Name}: {ex.Message}\n\n{ex.StackTrace}",
                    "Receipt Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
        // Populate all receipt fields
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateReceipt()
        {
            DateTime processedAt = _result.ProcessedAt;

            lblReceiptNumber.Text = $"OR-{processedAt:yyyy}-{processedAt:HHmmss}";
            lblCustomerValue.Text = _result.Customer?.Name ?? "—";
            lblDateTimeValue.Text = processedAt.ToString("M/d/yyyy, h:mm:ss tt");
            lblTransactionValue.Text = "ADVANCE PAYMENT";

            int months = _result.NumberOfMonths;
            lblBreakdownItemKey.Text = $"Monthly Amortization (x{months})";
            lblBreakdownItemValue.Text = $"₱{_result.TotalDue:N2}";

            lblTotalPaidValue.Text = $"₱{_result.TotalDue:N2}";
            lblTenderedValue.Text = $"₱{_result.AmountReceived:N2}";
            lblChangeValue.Text = $"₱{_result.ChangeDue:N2}";

            PopulateLoanStatus(processedAt, months);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Loan status section — fully null-safe
        // ─────────────────────────────────────────────────────────────────────
        private void PopulateLoanStatus(DateTime processedAt, int months)
        {
            // ── Guard: if the whole panel doesn't exist in designer, skip silently ──
            if (pnlLoanStatus == null)
                return;

            var fs = _result.Customer?.FinancialStatus;
            if (fs == null)
            {
                pnlLoanStatus.Visible = false;
                return;
            }

            // ── Remaining balance — floor at zero ─────────────────────────────
            decimal remaining = fs.CurrentBalance - _result.TotalDue;
            if (remaining < 0m) remaining = 0m;

            if (lblRemainingBalanceValue != null)
                lblRemainingBalanceValue.Text = $"₱{remaining:N2}";

            // ── Next due date ─────────────────────────────────────────────────
            // Advance payment rule: next due = processedAt + N months
            DateTime nextDue = processedAt.Date.AddMonths(months);

            // Preserve the contract's original day-of-month when available
            if (!string.IsNullOrWhiteSpace(fs.NextDueDate) &&
                DateTime.TryParse(fs.NextDueDate, out DateTime originalDue))
            {
                int dueDay = originalDue.Day;
                int maxDay = DateTime.DaysInMonth(nextDue.Year, nextDue.Month);
                dueDay = Math.Min(dueDay, maxDay);
                nextDue = new DateTime(nextDue.Year, nextDue.Month, dueDay);
            }

            if (lblNextDueValue != null)
            {
                lblNextDueValue.Text = nextDue.ToString("MMM d, yyyy");
                // Teal = future (advance always should be), red = overdue safety fallback
                lblNextDueValue.ForeColor = nextDue.Date < DateTime.Today
                    ? Color.FromArgb(225, 29, 72)
                    : Color.FromArgb(5, 150, 105);
            }

            if (lblMonthsNote != null)
                lblMonthsNote.Text = $"Covers {months} month{(months > 1 ? "s" : "")} of payments";

            pnlLoanStatus.Visible = true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Return to POSCashier — disposes ALL UserControls from top form
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
        // Button events
        // ─────────────────────────────────────────────────────────────────────
        private void BtnClose_Click(object sender, EventArgs e) => ReturnToPoscashier();
        private void BtnStartNew_Click(object sender, EventArgs e) => ReturnToPoscashier();

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Print functionality not yet implemented.\n\nFuture: integrate with a thermal printer driver or send to default Windows printer.",
                "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"Email functionality not yet implemented.\n\nFuture: email receipt to {_result.Customer?.Name ?? "customer"}",
                "Email Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSavePDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Save PDF functionality not yet implemented.\n\nFuture: generate PDF and prompt Save As dialog.",
                "Save PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSMS_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "SMS functionality not yet implemented.\n\nFuture: send receipt via SMS gateway.",
                "Send SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}