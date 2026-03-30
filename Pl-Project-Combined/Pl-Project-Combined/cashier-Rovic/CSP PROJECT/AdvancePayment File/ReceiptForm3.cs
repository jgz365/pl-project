using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using POSCashierSystem;

namespace CSP_PROJECT.POSCashier.AdvancePayment_File
{
    public partial class ReceiptForm3 : UserControl
    {
        // ── DESIGN DIMENSIONS ─────────────────────────────────────────────────
        private const int DESIGN_CARD_WIDTH = 455;
        private const int DESIGN_CARD_HEIGHT = 610;
        private const int DESIGN_HEADER_HEIGHT = 58;
        private const int DESIGN_FOOTER_HEIGHT = 75;
        private const int DESIGN_BODY_HEIGHT = 477; // 610 - 58 - 75

        private const int PADDING_HORIZONTAL = 20;
        private const int PADDING_VERTICAL = 30;

        private readonly System.Collections.Generic.Dictionary<Control, (int x, int y, int w, int h)> OriginalDimensions = new();

        private readonly CollectionResult3 _result;
        public event EventHandler ResetRequested = delegate { };

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

        private void ReceiptForm3_Load(object sender, EventArgs e)
        {
            CaptureOriginalDimensions();

            try
            {
                PopulateReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error populating receipt:\n\n{ex.GetType().Name}: {ex.Message}\n\n{ex.StackTrace}",
                    "Receipt Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CaptureAndBlurBackground();
            ApplyResponsiveLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated && OriginalDimensions.Count > 0)
                ApplyResponsiveLayout();
        }

        // ─── Capture Original Dimensions ─────────────────────────────────────

        private void CaptureOriginalDimensions()
        {
            StoreOriginal(pnlModalCard, 166, 30, DESIGN_CARD_WIDTH, DESIGN_CARD_HEIGHT);
            StoreOriginal(pnlHeader, 0, 0, DESIGN_CARD_WIDTH, DESIGN_HEADER_HEIGHT);
            StoreOriginal(pnlBody, 0, DESIGN_HEADER_HEIGHT, DESIGN_CARD_WIDTH, DESIGN_BODY_HEIGHT);
            StoreOriginal(pnlFooter, 0, DESIGN_HEADER_HEIGHT + DESIGN_BODY_HEIGHT, DESIGN_CARD_WIDTH, DESIGN_FOOTER_HEIGHT);

            // Header children
            StoreOriginal(pnlCheckmarkSquare, 18, 14, 37, 32);
            StoreOriginal(lblCheckmark, 18, 14, 37, 32);
            StoreOriginal(lblHeaderTitle, 63, 10, 298, 20);
            StoreOriginal(lblHeaderSubtitle, 65, 32, 245, 15);
            StoreOriginal(btnClose, 411, 15, 32, 27);

            // Receipt card (relative to pnlBody)
            StoreOriginal(pnlReceiptCard, 28, 14, 399, 388);

            // Receipt card children (relative to pnlReceiptCard)
            StoreOriginal(lblCompanyName, 0, 15, 399, 26);
            StoreOriginal(lblOfficialReceipt, 0, 42, 399, 12);
            StoreOriginal(lblReceiptNumber, 0, 56, 399, 14);
            StoreOriginal(sep1, 21, 76, 357, 2);
            StoreOriginal(lblCustomerKey, 21, 86, 105, 20);
            StoreOriginal(lblCustomerValue, 175, 86, 203, 20);
            StoreOriginal(lblDateTimeKey, 21, 106, 105, 21);
            StoreOriginal(lblDateTimeValue, 122, 107, 256, 20);
            StoreOriginal(lblTransactionKey, 21, 129, 105, 20);
            StoreOriginal(lblTransactionValue, 175, 129, 203, 20);
            StoreOriginal(sep2, 21, 156, 357, 2);
            StoreOriginal(lblBreakdownItemKey, 21, 166, 192, 16);
            StoreOriginal(lblBreakdownItemValue, 245, 166, 133, 16);
            StoreOriginal(sep3, 0, 192, 399, 1);
            StoreOriginal(lblTotalPaidKey, 21, 202, 105, 26);
            StoreOriginal(lblTotalPaidValue, 140, 201, 238, 27);
            StoreOriginal(lblTenderedKey, 21, 234, 131, 15);
            StoreOriginal(lblTenderedValue, 245, 234, 133, 15);
            StoreOriginal(lblChangeKey, 21, 252, 131, 15);
            StoreOriginal(lblChangeValue, 245, 252, 133, 15);

            // Loan status panel — slightly taller than RF2 to hold lblMonthsNote
            StoreOriginal(pnlLoanStatus, 21, 282, 371, 95);

            // Loan status children (relative to pnlLoanStatus)
            StoreOriginal(lblLoanStatusTitle, 12, 8, 175, 11);
            StoreOriginal(lblRemainingBalanceKey, 12, 24, 140, 16);
            StoreOriginal(lblRemainingBalanceValue, 210, 24, 150, 16);
            StoreOriginal(lblNextDueKey, 12, 45, 140, 16);
            StoreOriginal(lblNextDueValue, 210, 45, 150, 16);
            StoreOriginal(lblMonthsNote, 12, 68, 340, 14);

            // Action buttons panel (relative to pnlBody)
            StoreOriginal(pnlActionButtons, 28, 400, 399, 84);

            // Button children (relative to pnlActionButtons)
            StoreOriginal(btnPrint, 0, 0, 194, 33);
            StoreOriginal(btnEmail, 205, 0, 194, 33);
            StoreOriginal(btnSavePDF, 0, 42, 194, 33);
            StoreOriginal(btnSMS, 205, 42, 194, 33);

            // Start New button (relative to pnlFooter)
            StoreOriginal(btnStartNew, 21, 18, 413, 39);
        }

        private void StoreOriginal(Control ctrl, int x, int y, int w, int h)
        {
            if (ctrl != null)
                OriginalDimensions[ctrl] = (x, y, w, h);
        }

        // ─── Responsive Layout ────────────────────────────────────────────────

        private void ApplyResponsiveLayout()
        {
            if (!IsHandleCreated || OriginalDimensions.Count == 0) return;

            pnlOverlay.Size = this.Size;
            pnlOverlay.Location = new Point(0, 0);

            int availableWidth = Math.Max(100, this.Width - PADDING_HORIZONTAL * 2);
            int availableHeight = Math.Max(100, this.Height - PADDING_VERTICAL * 2);

            float scaleFactorW = availableWidth / (float)DESIGN_CARD_WIDTH;
            float scaleFactorH = availableHeight / (float)DESIGN_CARD_HEIGHT;
            float uniformScale = Math.Min(scaleFactorW, scaleFactorH);

            int scaledCardW = ScaleValue(DESIGN_CARD_WIDTH, uniformScale);
            int scaledCardH = ScaleValue(DESIGN_CARD_HEIGHT, uniformScale);
            int scaledHeaderH = ScaleValue(DESIGN_HEADER_HEIGHT, uniformScale);
            int scaledFooterH = ScaleValue(DESIGN_FOOTER_HEIGHT, uniformScale);
            int scaledBodyH = scaledCardH - scaledHeaderH - scaledFooterH;

            int cardLeftX = (this.Width - scaledCardW) / 2;
            int cardTopY = (this.Height - scaledCardH) / 2;
            if (cardTopY < 10) cardTopY = 10;

            pnlModalCard.Location = new Point(Math.Max(0, cardLeftX), Math.Max(0, cardTopY));
            pnlModalCard.Size = new Size(scaledCardW, scaledCardH);

            // Header
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Size = new Size(scaledCardW, scaledHeaderH);

            ApplyScaledControl(pnlCheckmarkSquare, 18, 14, 37, 32, uniformScale);
            ApplyScaledControl(lblCheckmark, 18, 14, 37, 32, uniformScale);
            ApplyScaledControl(lblHeaderTitle, 63, 10, 298, 20, uniformScale);
            ApplyScaledControl(lblHeaderSubtitle, 65, 32, 245, 15, uniformScale);

            int btnCloseScaledW = ScaleValue(32, uniformScale);
            int btnCloseScaledH = ScaleValue(27, uniformScale);
            int rightMargin = ScaleValue(12, uniformScale);
            btnClose.Size = new Size(btnCloseScaledW, btnCloseScaledH);
            btnClose.Location = new Point(scaledCardW - btnCloseScaledW - rightMargin, ScaleValue(15, uniformScale));

            // Body
            pnlBody.Location = new Point(0, scaledHeaderH);
            pnlBody.Size = new Size(scaledCardW, scaledBodyH);
            pnlBody.AutoScroll = true;

            // Receipt card
            ApplyScaledControl(pnlReceiptCard, 28, 14, 399, 388, uniformScale);

            ApplyScaledChild(lblCompanyName, 0, 15, 399, 26, uniformScale);
            ApplyScaledChild(lblOfficialReceipt, 0, 42, 399, 12, uniformScale);
            ApplyScaledChild(lblReceiptNumber, 0, 56, 399, 14, uniformScale);
            ApplyScaledChild(sep1, 21, 76, 357, 2, uniformScale);
            ApplyScaledChild(lblCustomerKey, 21, 86, 105, 20, uniformScale);
            ApplyScaledChild(lblCustomerValue, 175, 86, 203, 20, uniformScale);
            ApplyScaledChild(lblDateTimeKey, 21, 106, 105, 21, uniformScale);
            ApplyScaledChild(lblDateTimeValue, 122, 107, 256, 20, uniformScale);
            ApplyScaledChild(lblTransactionKey, 21, 129, 105, 20, uniformScale);
            ApplyScaledChild(lblTransactionValue, 175, 129, 203, 20, uniformScale);
            ApplyScaledChild(sep2, 21, 156, 357, 2, uniformScale);
            ApplyScaledChild(lblBreakdownItemKey, 21, 166, 192, 16, uniformScale);
            ApplyScaledChild(lblBreakdownItemValue, 245, 166, 133, 16, uniformScale);
            ApplyScaledChild(sep3, 0, 192, 399, 1, uniformScale);
            ApplyScaledChild(lblTotalPaidKey, 21, 202, 105, 26, uniformScale);
            ApplyScaledChild(lblTotalPaidValue, 140, 201, 238, 27, uniformScale);
            ApplyScaledChild(lblTenderedKey, 21, 234, 131, 15, uniformScale);
            ApplyScaledChild(lblTenderedValue, 245, 234, 133, 15, uniformScale);
            ApplyScaledChild(lblChangeKey, 21, 252, 131, 15, uniformScale);
            ApplyScaledChild(lblChangeValue, 245, 252, 133, 15, uniformScale);

            // Loan status panel
            if (pnlLoanStatus != null)
            {
                ApplyScaledChild(pnlLoanStatus, 21, 282, 371, 95, uniformScale);

                ApplyScaledNested(lblLoanStatusTitle, pnlLoanStatus, 12, 8, 175, 11, uniformScale);
                ApplyScaledNested(lblRemainingBalanceKey, pnlLoanStatus, 12, 24, 140, 16, uniformScale);
                ApplyScaledNested(lblRemainingBalanceValue, pnlLoanStatus, 210, 24, 150, 16, uniformScale);
                ApplyScaledNested(lblNextDueKey, pnlLoanStatus, 12, 45, 140, 16, uniformScale);
                ApplyScaledNested(lblNextDueValue, pnlLoanStatus, 210, 45, 150, 16, uniformScale);
                ApplyScaledNested(lblMonthsNote, pnlLoanStatus, 12, 68, 340, 14, uniformScale);
            }

            // Action buttons panel
            ApplyScaledControl(pnlActionButtons, 28, 400, 399, 84, uniformScale);

            ApplyScaledNested(btnPrint, pnlActionButtons, 0, 0, 194, 33, uniformScale);
            ApplyScaledNested(btnEmail, pnlActionButtons, 205, 0, 194, 33, uniformScale);
            ApplyScaledNested(btnSavePDF, pnlActionButtons, 0, 42, 194, 33, uniformScale);
            ApplyScaledNested(btnSMS, pnlActionButtons, 205, 42, 194, 33, uniformScale);

            // Footer
            pnlFooter.Location = new Point(0, scaledHeaderH + scaledBodyH);
            pnlFooter.Size = new Size(scaledCardW, scaledFooterH);

            ApplyScaledNested(btnStartNew, pnlFooter, 21, 18, 413, 39, uniformScale);
        }

        // ─── Scaling Helpers ──────────────────────────────────────────────────

        private static int ScaleValue(int originalValue, float scaleFactor)
            => Math.Max(1, (int)Math.Round(originalValue * scaleFactor));

        private static void ApplyScaledControl(Control control, int designX, int designY, int designW, int designH, float scale)
        {
            if (control == null) return;
            control.Location = new Point(ScaleValue(designX, scale), ScaleValue(designY, scale));
            control.Size = new Size(ScaleValue(designW, scale), ScaleValue(designH, scale));
        }

        private static void ApplyScaledChild(Control control, int designX, int designY, int designW, int designH, float scale)
        {
            if (control == null) return;
            control.Location = new Point(ScaleValue(designX, scale), ScaleValue(designY, scale));
            control.Size = new Size(ScaleValue(designW, scale), ScaleValue(designH, scale));
        }

        private static void ApplyScaledNested(Control control, Control parentPanel, int designX, int designY, int designW, int designH, float scale)
        {
            if (control == null || parentPanel == null) return;
            control.Location = new Point(ScaleValue(designX, scale), ScaleValue(designY, scale));
            control.Size = new Size(ScaleValue(designW, scale), ScaleValue(designH, scale));
        }

        // ─── Blur Background ──────────────────────────────────────────────────

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
            int sw = Math.Max(1, w / (radius * 2));
            int sh = Math.Max(1, h / (radius * 2));

            var small = new Bitmap(sw, sh, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(small))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.DrawImage(source, 0, 0, sw, sh);
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

        // ─── Data Population ──────────────────────────────────────────────────

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

        private void PopulateLoanStatus(DateTime processedAt, int months)
        {
            if (pnlLoanStatus == null) return;

            var fs = _result.Customer?.FinancialStatus;
            if (fs == null) { pnlLoanStatus.Visible = false; return; }

            decimal remaining = fs.CurrentBalance - _result.TotalDue;
            if (remaining < 0m) remaining = 0m;

            if (lblRemainingBalanceValue != null)
                lblRemainingBalanceValue.Text = $"₱{remaining:N2}";

            DateTime nextDue = processedAt.Date.AddMonths(months);

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
                lblNextDueValue.ForeColor = nextDue.Date < DateTime.Today
                    ? Color.FromArgb(225, 29, 72)
                    : Color.FromArgb(5, 150, 105);
            }

            if (lblMonthsNote != null)
                lblMonthsNote.Text = $"Covers {months} month{(months > 1 ? "s" : "")} of payments";

            pnlLoanStatus.Visible = true;
        }

        // ─── Navigation ───────────────────────────────────────────────────────

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

        private void BtnClose_Click(object sender, EventArgs e) => ReturnToPoscashier();
        private void BtnStartNew_Click(object sender, EventArgs e) => ReturnToPoscashier();

        private void BtnPrint_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "Print functionality not yet implemented.\n\nFuture: integrate with a thermal printer driver or send to default Windows printer.",
                "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void BtnEmail_Click(object sender, EventArgs e)
            => MessageBox.Show(
                $"Email functionality not yet implemented.\n\nFuture: email receipt to {_result.Customer?.Name ?? "customer"}",
                "Email Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void BtnSavePDF_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "Save PDF functionality not yet implemented.\n\nFuture: generate PDF and prompt Save As dialog.",
                "Save PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void BtnSMS_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "SMS functionality not yet implemented.\n\nFuture: send receipt via SMS gateway.",
                "Send SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}