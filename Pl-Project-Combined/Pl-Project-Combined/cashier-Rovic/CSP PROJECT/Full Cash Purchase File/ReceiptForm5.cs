using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CSP_PROJECT
{
    /// <summary>
    /// ReceiptForm5 — Official Receipt Modal for the Full Cash Purchase (Kiosk) flow.
    /// Accepts CollectionResult5 from CollectionForm5.
    ///
    /// Differences from ReceiptForm4 (Full Settlement):
    ///   • Transaction type = "CASH PURCHASE"
    ///   • Breakdown shows the dynamic charges list (unit price, taxes, add-ons)
    ///   • Status panel shows "Purchase Complete" info instead of loan status
    ///   • Theme colour: teal (5, 150, 105) instead of purple
    /// </summary>
    public partial class ReceiptForm5 : UserControl
    {
        private readonly CollectionResult5 _result;

        public event EventHandler ResetRequested = delegate { };

        // ─────────────────────────────────────────────────────────────────────
        public ReceiptForm5(CollectionResult5 result)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));

            if (string.IsNullOrWhiteSpace(_result.CustomerName))
                throw new ArgumentException("CollectionResult5.CustomerName cannot be empty.", nameof(result));

            if (_result.TotalDue < 0)
                throw new ArgumentException("CollectionResult5.TotalDue cannot be negative.", nameof(result));

            if (_result.AmountReceived < _result.TotalDue)
                throw new ArgumentException("AmountReceived cannot be less than TotalDue.", nameof(result));

            InitializeComponent();
        }

        // ─────────────────────────────────────────────────────────────────────
        private void ReceiptForm5_Load(object sender, EventArgs e)
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
        // Blur background — identical to ReceiptForm4
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

            lblReceiptNumber.Text = $"OR-{processedAt:yyyy}-{processedAt:HHmmss}";
            lblCustomerValue.Text = _result.CustomerName;
            lblDateTimeValue.Text = processedAt.ToString("M/d/yyyy, h:mm:ss tt");
            lblTransactionValue.Text = "CASH PURCHASE";

            // Build dynamic charge rows inside pnlChargesContainer
            BuildChargeRows();

            lblTotalPaidValue.Text = $"₱{_result.TotalDue:N0}";
            lblTenderedValue.Text = $"₱{_result.AmountReceived:N0}";
            lblChangeValue.Text = $"₱{_result.ChangeDue:N0}";

            // Status panel
            lblPurchaseNote.Text = "🎉  Cash purchase complete — unit ready for release";
        }

        private void BuildChargeRows()
        {
            pnlChargesContainer.Controls.Clear();

            int y = 0;
            const int rowH = 22;

            foreach (var (label, amount) in _result.Charges)
            {
                var key = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    Location = new Point(0, y),
                    Size = new Size(240, 20),
                    Text = label
                };
                var val = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    Location = new Point(256, y),
                    Size = new Size(152, 20),
                    Text = $"₱{amount:N2}",
                    TextAlign = ContentAlignment.MiddleRight
                };
                pnlChargesContainer.Controls.Add(key);
                pnlChargesContainer.Controls.Add(val);
                y += rowH;
            }

            // Resize the container to fit all rows
            pnlChargesContainer.Height = Math.Max(rowH, y);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Navigation — same pattern as ReceiptForm4
        // ─────────────────────────────────────────────────────────────────────
        private void ReturnToPoscashier()
        {
            try
            {
                Form topForm = this.FindForm();
                if (topForm == null) return;

                var toRemove = new List<Control>();
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
            MessageBox.Show("Print functionality not yet implemented.\n\n" +
                            "Future: integrate with a thermal printer driver or\n" +
                            "send to default Windows printer.",
                            "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Email functionality not yet implemented.\n\n" +
                            $"Future: email receipt to {_result.CustomerName}",
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

    // ── Result model ──────────────────────────────────────────────────────────
    public class CollectionResult5
    {
        public string CustomerName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public List<(string Label, decimal Amount)> Charges { get; set; } = new();
        public decimal TotalDue { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ChangeDue { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}