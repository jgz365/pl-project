using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using POSCashierSystem;

namespace CSP_PROJECT.POSCashier.MonthlyPayment_File
{
    public partial class ReceiptForm2 : UserControl
    {
        // ── DESIGN DIMENSIONS (as laid out in Visual Studio Designer) ──────────
        // These represent the ideal card dimensions at 100% scale (96 DPI)
        // The responsive system uses these as the baseline

        private const int DESIGN_CARD_WIDTH = 455;
        private const int DESIGN_CARD_HEIGHT = 610;
        private const int DESIGN_HEADER_HEIGHT = 58;
        private const int DESIGN_FOOTER_HEIGHT = 75;
        private const int DESIGN_BODY_HEIGHT = 477; // = 610 - 58 - 75

        // Minimal padding around the card — allows modal to fill more space
        private const int PADDING_HORIZONTAL = 20;
        private const int PADDING_VERTICAL = 30;

        // Store original designer positions for all controls
        private readonly Dictionary<Control, (int x, int y, int w, int h)> OriginalDimensions = new();

        private readonly CollectionResult2 _result;
        public event EventHandler ResetRequested = delegate { };

        public ReceiptForm2(CollectionResult2 result)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));
            InitializeComponent();
        }

        private void ReceiptForm2_Load(object sender, EventArgs e)
        {
            // Store original dimensions before any scaling is applied
            CaptureOriginalDimensions();

            PopulateReceipt();
            CaptureAndBlurBackground();
            ApplyResponsiveLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated && OriginalDimensions.Count > 0)
            {
                ApplyResponsiveLayout();
            }
        }

        // ─── Responsive Layout System ─────────────────────────────────────────
        //
        // This system ensures the modal card and all its contents scale uniformly
        // based on the available window space. The scale factor is calculated from
        // the width and height available, then applies to every single control.
        //
        // The modal GROWS with larger windows and SHRINKS with smaller windows,
        // keeping everything proportional. Text, buttons, separators — everything
        // scales together to maintain the design integrity.
        // ───────────────────────────────────────────────────────────────────────

        private void CaptureOriginalDimensions()
        {
            // pnlModalCard and its direct sections
            StoreOriginal(pnlModalCard, 166, 30, DESIGN_CARD_WIDTH, DESIGN_CARD_HEIGHT);
            StoreOriginal(pnlHeader, 0, 0, DESIGN_CARD_WIDTH, DESIGN_HEADER_HEIGHT);
            StoreOriginal(pnlBody, 0, DESIGN_HEADER_HEIGHT, DESIGN_CARD_WIDTH, DESIGN_BODY_HEIGHT);
            StoreOriginal(pnlFooter, 0, DESIGN_HEADER_HEIGHT + DESIGN_BODY_HEIGHT, DESIGN_CARD_WIDTH, DESIGN_FOOTER_HEIGHT);

            // Header children (relative to pnlHeader at 0,0)
            StoreOriginal(pnlCheckmarkSquare, 18, 14, 37, 32);
            StoreOriginal(lblCheckmark, 18, 14, 37, 32);
            StoreOriginal(lblHeaderTitle, 63, 10, 298, 20);
            StoreOriginal(lblHeaderSubtitle, 65, 32, 245, 15);
            StoreOriginal(btnClose, 411, 15, 32, 27);

            // Receipt card (relative to pnlBody)
            StoreOriginal(pnlReceiptCard, 28, 14, 399, 388);

            // Receipt card children (relative to pnlReceiptCard at 0,0)
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

            // Loan status panel (relative to pnlReceiptCard)
            StoreOriginal(pnlLoanStatus, 21, 282, 371, 75);

            // Loan status children (relative to pnlLoanStatus at 0,0)
            StoreOriginal(lblLoanStatusTitle, 12, 8, 175, 11);
            StoreOriginal(lblRemainingBalanceKey, 12, 24, 140, 16);
            StoreOriginal(lblRemainingBalanceValue, 210, 24, 150, 16);
            StoreOriginal(lblNextDueKey, 12, 45, 140, 16);
            StoreOriginal(lblNextDueValue, 210, 45, 150, 16);

            // Action buttons panel (relative to pnlBody)
            StoreOriginal(pnlActionButtons, 28, 417, 399, 84);

            // Button children (relative to pnlActionButtons at 0,0)
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
            {
                OriginalDimensions[ctrl] = (x, y, w, h);
            }
        }

        private void ApplyResponsiveLayout()
        {
            if (!IsHandleCreated || OriginalDimensions.Count == 0) return;

            // ── 1. Overlay always fills the entire UserControl ────────────────
            pnlOverlay.Size = this.Size;
            pnlOverlay.Location = new Point(0, 0);

            // ── 2. Calculate responsive scale factor ──────────────────────────
            // Available space = window size minus minimal padding
            int availableWidth = Math.Max(100, this.Width - PADDING_HORIZONTAL * 2);
            int availableHeight = Math.Max(100, this.Height - PADDING_VERTICAL * 2);

            // Scale factor: how much to scale the card to fit the available space
            // Unlike before, this ALLOWS upscaling — if window is large, card grows
            // The scale is based on whichever dimension is more restrictive (width or height)
            float scaleFactorW = availableWidth / (float)DESIGN_CARD_WIDTH;
            float scaleFactorH = availableHeight / (float)DESIGN_CARD_HEIGHT;

            // Use the smaller factor so the modal fits within both width and height constraints
            float uniformScale = Math.Min(scaleFactorW, scaleFactorH);

            // NO CAP — allow upscaling on large displays for proper visibility
            // uniformScale can be 0.5x on small windows, 1.5x on large displays, etc.

            // ── 3. Calculate scaled card dimensions ──────────────────────────
            int scaledCardW = ScaleValue(DESIGN_CARD_WIDTH, uniformScale);
            int scaledCardH = ScaleValue(DESIGN_CARD_HEIGHT, uniformScale);
            int scaledHeaderH = ScaleValue(DESIGN_HEADER_HEIGHT, uniformScale);
            int scaledFooterH = ScaleValue(DESIGN_FOOTER_HEIGHT, uniformScale);
            int scaledBodyH = scaledCardH - scaledHeaderH - scaledFooterH;

            // ── 4. Center the modal card on the screen ──────────────────────
            int cardLeftX = (this.Width - scaledCardW) / 2;
            int cardTopY = (this.Height - scaledCardH) / 2;

            // Keep some minimum margin at the top to show the header is visible
            if (cardTopY < 10) cardTopY = 10;

            pnlModalCard.Location = new Point(Math.Max(0, cardLeftX), Math.Max(0, cardTopY));
            pnlModalCard.Size = new Size(scaledCardW, scaledCardH);

            // ── 5. Scale and position pnlHeader ────────────────────────────
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Size = new Size(scaledCardW, scaledHeaderH);

            // Header children - all positioned relative to pnlHeader
            ApplyScaledControl(pnlCheckmarkSquare, 18, 14, 37, 32, uniformScale);
            ApplyScaledControl(lblCheckmark, 18, 14, 37, 32, uniformScale);
            ApplyScaledControl(lblHeaderTitle, 63, 10, 298, 20, uniformScale);
            ApplyScaledControl(lblHeaderSubtitle, 65, 32, 245, 15, uniformScale);

            // btnClose pinned to right edge
            int btnCloseScaledW = ScaleValue(32, uniformScale);
            int btnCloseScaledH = ScaleValue(27, uniformScale);
            int rightMargin = ScaleValue(12, uniformScale); // 455 - 411 - 32 = 12
            btnClose.Size = new Size(btnCloseScaledW, btnCloseScaledH);
            btnClose.Location = new Point(
                scaledCardW - btnCloseScaledW - rightMargin,
                ScaleValue(15, uniformScale)
            );

            // ── 6. Scale and position pnlBody ──────────────────────────────
            pnlBody.Location = new Point(0, scaledHeaderH);
            pnlBody.Size = new Size(scaledCardW, scaledBodyH);
            pnlBody.AutoScroll = true;

            // ── 7. Scale pnlReceiptCard and all its children ────────────────
            ApplyScaledControl(pnlReceiptCard, 28, 14, 399, 388, uniformScale);

            // Receipt content labels and separators
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

            // ── 8. Scale pnlLoanStatus and its children ──────────────────────
            if (pnlLoanStatus != null)
            {
                ApplyScaledChild(pnlLoanStatus, 21, 282, 371, 75, uniformScale);

                // Loan status children - relative to pnlLoanStatus
                ApplyScaledNested(lblLoanStatusTitle, pnlLoanStatus, 12, 8, 175, 11, uniformScale);
                ApplyScaledNested(lblRemainingBalanceKey, pnlLoanStatus, 12, 24, 140, 16, uniformScale);
                ApplyScaledNested(lblRemainingBalanceValue, pnlLoanStatus, 210, 24, 150, 16, uniformScale);
                ApplyScaledNested(lblNextDueKey, pnlLoanStatus, 12, 45, 140, 16, uniformScale);
                ApplyScaledNested(lblNextDueValue, pnlLoanStatus, 210, 45, 150, 16, uniformScale);
            }

            // ── 9. Scale pnlActionButtons and its children ───────────────────
            ApplyScaledControl(pnlActionButtons, 28, 417, 399, 84, uniformScale);

            // Buttons - relative to pnlActionButtons
            ApplyScaledNested(btnPrint, pnlActionButtons, 0, 0, 194, 33, uniformScale);
            ApplyScaledNested(btnEmail, pnlActionButtons, 205, 0, 194, 33, uniformScale);
            ApplyScaledNested(btnSavePDF, pnlActionButtons, 0, 42, 194, 33, uniformScale);
            ApplyScaledNested(btnSMS, pnlActionButtons, 205, 42, 194, 33, uniformScale);

            // ── 10. Scale pnlFooter and its children ───────────────────────
            pnlFooter.Location = new Point(0, scaledHeaderH + scaledBodyH);
            pnlFooter.Size = new Size(scaledCardW, scaledFooterH);

            ApplyScaledNested(btnStartNew, pnlFooter, 21, 18, 413, 39, uniformScale);
        }

        // ─── Scaling Helpers ──────────────────────────────────────────────────

        /// <summary>Scale a dimension value by the uniform scale factor.</summary>
        private static int ScaleValue(int originalValue, float scaleFactor)
        {
            return Math.Max(1, (int)Math.Round(originalValue * scaleFactor));
        }

        /// <summary>
        /// Apply scaled position and size to a control positioned relative to the UserControl.
        /// Used for top-level panels like pnlModalCard, pnlReceiptCard, etc.
        /// </summary>
        private static void ApplyScaledControl(Control control, int designX, int designY, int designW, int designH, float scale)
        {
            if (control == null) return;

            control.Location = new Point(
                ScaleValue(designX, scale),
                ScaleValue(designY, scale)
            );
            control.Size = new Size(
                ScaleValue(designW, scale),
                ScaleValue(designH, scale)
            );
        }

        /// <summary>
        /// Apply scaled position and size to a control positioned relative to its parent panel.
        /// Used for direct children of scaled panels.
        /// </summary>
        private static void ApplyScaledChild(Control control, int designX, int designY, int designW, int designH, float scale)
        {
            if (control == null) return;

            control.Location = new Point(
                ScaleValue(designX, scale),
                ScaleValue(designY, scale)
            );
            control.Size = new Size(
                ScaleValue(designW, scale),
                ScaleValue(designH, scale)
            );
        }

        /// <summary>
        /// Apply scaled position and size to a control positioned relative to a specific parent.
        /// Used for deeply nested controls like controls inside pnlLoanStatus.
        /// </summary>
        private static void ApplyScaledNested(Control control, Control parentPanel, int designX, int designY, int designW, int designH, float scale)
        {
            if (control == null || parentPanel == null) return;

            control.Location = new Point(
                ScaleValue(designX, scale),
                ScaleValue(designY, scale)
            );
            control.Size = new Size(
                ScaleValue(designW, scale),
                ScaleValue(designH, scale)
            );
        }

        // ─── Blur Background ──────────────────────────────────────────────────

        private void CaptureAndBlurBackground()
        {
            try
            {
                this.Visible = false;
                Application.DoEvents();

                if (this.Parent == null) { this.Visible = true; return; }
                Form topForm = this.FindForm();
                if (topForm == null) { this.Visible = true; return; }

                var bmp = new Bitmap(topForm.Width, topForm.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(bmp))
                    g.CopyFromScreen(topForm.PointToScreen(Point.Empty), Point.Empty,
                                     topForm.Size, CopyPixelOperation.SourceCopy);

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
            int w = source.Width;
            int h = source.Height;
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
            lblReceiptNumber.Text = $"OR-{DateTime.Now.Year}-{DateTime.Now:HHmmss}";
            lblCustomerValue.Text = _result?.Customer?.Name ?? "—";
            lblDateTimeValue.Text = _result != null ? _result.ProcessedAt.ToString("M/d/yyyy, h:mm:ss tt") : "—";
            lblTransactionValue.Text = "MONTHLY PAYMENT";
            lblBreakdownItemKey.Text = "Monthly Amortization";
            lblBreakdownItemValue.Text = $"₱{(_result?.TotalDue ?? 0m):N2}";
            lblTotalPaidValue.Text = $"₱{(_result?.TotalDue ?? 0m):N2}";
            lblTenderedValue.Text = $"₱{(_result?.AmountReceived ?? 0m):N2}";
            lblChangeValue.Text = $"₱{(_result?.ChangeDue ?? 0m):N2}";
            PopulateLoanStatus();
        }

        private void PopulateLoanStatus()
        {
            var fs = _result?.Customer?.FinancialStatus;
            if (fs == null) { pnlLoanStatus.Visible = false; return; }

            decimal rem = fs.CurrentBalance - (_result?.TotalDue ?? 0m);
            if (rem < 0m) rem = 0m;
            lblRemainingBalanceValue.Text = $"₱{rem:N2}";

            string label = "—";
            if (!string.IsNullOrWhiteSpace(fs.NextDueDate) &&
                DateTime.TryParse(fs.NextDueDate, out DateTime cur))
            {
                DateTime next = cur.AddMonths(1);
                label = next.ToString("MMM d, yyyy");
                lblNextDueValue.ForeColor = next < DateTime.Today
                    ? Color.FromArgb(225, 29, 72)
                    : Color.FromArgb(37, 99, 235);
            }
            else if (!string.IsNullOrWhiteSpace(fs.NextDueDate))
            {
                label = fs.NextDueDate;
            }

            lblNextDueValue.Text = label;
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
                foreach (Control c in topForm.Controls)
                    if (c is UserControl) toRemove.Add(c);

                foreach (Control c in toRemove)
                {
                    topForm.Controls.Remove(c);
                    c.Dispose();
                }

                if (topForm is POSCashierSystem.POSCashier pos)
                    pos.RestoreMainView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error returning to POS Cashier: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
            => ReturnToPoscashier();

        private void BtnStartNew_Click(object sender, EventArgs e)
            => ReturnToPoscashier();

        private void BtnPrint_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "Print functionality not yet implemented.\n\nFuture: integrate with a thermal printer driver or send to default Windows printer.",
                "Print Receipt",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        private void BtnEmail_Click(object sender, EventArgs e)
            => MessageBox.Show(
                $"Email functionality not yet implemented.\n\nFuture: email receipt to {_result?.Customer?.Name ?? "customer"}",
                "Email Receipt",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        private void BtnSavePDF_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "Save PDF functionality not yet implemented.\n\nFuture: generate PDF and prompt Save As dialog.",
                "Save PDF",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        private void BtnSMS_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "SMS functionality not yet implemented.\n\nFuture: send receipt via SMS gateway.",
                "Send SMS",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
    }
}