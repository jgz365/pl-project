using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CSP_PROJECT
{
    public partial class ReceiptForm5 : UserControl
    {
        // ── Actual designer reference values (96 DPI, from Designer.cs) ──────────
        // Form / overlay:    900 × 918
        // pnlModalCard:      at (190, 40)  size 520 × 834
        // pnlHeader:         at (0, 0)     size 520 × 78   (inside card)
        // pnlBody:           at (0, 78)    size 520 × 656  (inside card)
        // pnlFooter:         at (0, 734)   size 520 × 100  (inside card)
        private const float REF_FORM_W = 900f;
        private const float REF_FORM_H = 918f;
        private const float REF_CARD_W = 520f;
        private const float REF_CARD_H = 834f;
        private const float REF_CARD_X = 190f;
        private const float REF_CARD_Y = 40f;
        private const float REF_HDR_H = 78f;
        private const float REF_BODY_H = 656f;
        private const float REF_FOOTER_H = 100f;

        // pnlReceiptCard:    at (32, 24)   size 456 × 542  (inside body)
        private const float REF_RC_X = 32f;
        private const float REF_RC_Y = 24f;
        private const float REF_RC_W = 456f;
        private const float REF_RC_H = 542f;

        // pnlActionButtons:  at (32, 572)  size 456 × 112  (inside body)
        private const float REF_AB_X = 32f;
        private const float REF_AB_Y = 572f;
        private const float REF_AB_W = 456f;
        private const float REF_AB_H = 112f;

        // Charges reference (inside pnlReceiptCard)
        // First row starts at y=208; designer has 4 rows at 22px each = 88px container
        private const float REF_CHARGES_Y = 208f;
        private const float REF_CHARGES_ROW_H = 22f;
        private const float REF_CHARGES_GAP = 8f;   // gap between container bottom and sep3

        // ─────────────────────────────────────────────────────────────────────────
        private readonly CollectionResult5 _result;
        public event EventHandler ResetRequested = delegate { };

        public ReceiptForm5(CollectionResult5 result)
        {
            _result = result ?? throw new ArgumentNullException(nameof(result));

            if (string.IsNullOrWhiteSpace(_result.CustomerName))
                throw new ArgumentException("CustomerName cannot be empty.", nameof(result));
            if (_result.TotalDue < 0)
                throw new ArgumentException("TotalDue cannot be negative.", nameof(result));
            if (_result.AmountReceived < _result.TotalDue)
                throw new ArgumentException("AmountReceived cannot be less than TotalDue.", nameof(result));

            InitializeComponent();
        }

        // ─── Lifecycle ────────────────────────────────────────────────────────────
        private void ReceiptForm5_Load(object sender, EventArgs e)
        {
            PopulateReceipt();
            CaptureAndBlurBackground();
            ApplyResponsiveLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) ApplyResponsiveLayout();
        }

        // ─── Round-scale helper ───────────────────────────────────────────────────
        private static int S(float refVal, float scale) => (int)Math.Round(refVal * scale);

        // ─── Master responsive layout ─────────────────────────────────────────────
        private void ApplyResponsiveLayout()
        {
            if (ClientSize.Width < 200 || ClientSize.Height < 200) return;

            float formW = ClientSize.Width;
            float formH = ClientSize.Height;

            // ── Overlay fills the whole UserControl ───────────────────────────────
            pnlOverlay.SetBounds(0, 0, (int)formW, (int)formH);

            // ── Single uniform scale so the modal card fits without distortion ────
            float sc = Math.Min(formW / REF_FORM_W, formH / REF_FORM_H);

            int cardW = S(REF_CARD_W, sc);
            int cardH = S(REF_CARD_H, sc);
            int cardX = Math.Max(0, (int)formW - cardW) / 2;
            int cardY = Math.Max(10, ((int)formH - cardH) / 2);
            pnlModalCard.SetBounds(cardX, cardY, cardW, cardH);

            // ── Header (inside card) ──────────────────────────────────────────────
            int hdrH = S(REF_HDR_H, sc);
            pnlHeader.SetBounds(0, 0, cardW, hdrH);
            ScaleHeaderInterior(cardW, hdrH, sc);

            // ── Footer (inside card) ──────────────────────────────────────────────
            int footerH = S(REF_FOOTER_H, sc);
            int footerY = cardH - footerH;
            pnlFooter.SetBounds(0, footerY, cardW, footerH);
            ScaleFooterInterior(cardW, footerH, sc);

            // ── Body (inside card, between header and footer) ─────────────────────
            int bodyH = footerY - hdrH;
            pnlBody.SetBounds(0, hdrH, cardW, bodyH);
            ScaleBodyInterior(cardW, bodyH, sc);
        }

        // ─── Header interior   (designer reference: 520 × 78) ────────────────────
        private void ScaleHeaderInterior(int panelW, int panelH, float sc)
        {
            // pnlCheckmarkSquare — ref (20, 18, 42, 42)
            int csqSize = S(42f, sc);
            pnlCheckmarkSquare.SetBounds(S(20f, sc), S(18f, sc), csqSize, csqSize);

            // lblCheckmark fills the checkmark square
            lblCheckmark.SetBounds(0, 0, csqSize, csqSize);

            // lblHeaderTitle — ref (72, 14, 340, 26)
            lblHeaderTitle.SetBounds(S(72f, sc), S(14f, sc), S(340f, sc), S(26f, sc));

            // lblHeaderSubtitle — ref (74, 42, 280, 20)
            lblHeaderSubtitle.SetBounds(S(74f, sc), S(42f, sc), S(280f, sc), S(20f, sc));

            // btnClose — ref right edge: 470+36=506, right margin from card = 520-506=14
            // Keep right-anchored so: X = panelW - S(36,sc) - S(14,sc)
            int btnCloseW = S(36f, sc);
            int btnCloseH = S(36f, sc);
            btnClose.SetBounds(panelW - btnCloseW - S(14f, sc), S(20f, sc), btnCloseW, btnCloseH);
        }

        // ─── Footer interior   (designer reference: 520 × 100) ───────────────────
        private void ScaleFooterInterior(int panelW, int panelH, float sc)
        {
            // btnStartNew — ref (24, 24, 472, 52)
            int btnW = panelW - S(24f, sc) * 2;
            int btnH = S(52f, sc);
            int btnY = (panelH - btnH) / 2;
            btnStartNew.SetBounds(S(24f, sc), btnY, btnW, btnH);
        }

        // ─── Body interior   (designer reference: 520 × 656) ─────────────────────
        private void ScaleBodyInterior(int panelW, int panelH, float sc)
        {
            // ── pnlReceiptCard — ref (32, 24, 456, 542) ──────────────────────────
            int rcX = S(REF_RC_X, sc);
            int rcY = S(REF_RC_Y, sc);
            int rcW = panelW - rcX * 2;   // stay centred with same left/right margin
            // Height: determined by content flow — computed inside ScaleReceiptCard
            int rcH = ScaleReceiptCard(rcW, rcX, rcY, sc);
            pnlReceiptCard.SetBounds(rcX, rcY, rcW, rcH);

            // ── pnlActionButtons — ref (32, 572, 456, 112) ────────────────────────
            // Position it directly below the receipt card with the ref gap preserved
            int abGap = S(REF_AB_Y - (REF_RC_Y + REF_RC_H), sc);   // ref gap = 572-(24+542)=6
            int abY = rcY + rcH + abGap;
            int abW = rcW;
            int abH = S(REF_AB_H, sc);
            pnlActionButtons.SetBounds(rcX, abY, abW, abH);
            ScaleActionButtons(abW, abH, sc);
        }

        // ─── Receipt card interior   (designer reference: 456 × 542)
        //     Returns the actual scaled height of the card (content-driven).
        private int ScaleReceiptCard(int cardW, int cardX, int cardY, float sc)
        {
            const float refRCW = 456f;

            // Derive local scale from card width only (width drives everything here;
            // height flows from content).
            float lsc = cardW / refRCW;

            int padX = S(24f, lsc);
            int innerW = cardW - padX * 2;

            // ── Company name — ref (0, 20, 456, 34) ──────────────────────────────
            lblCompanyName.SetBounds(0, S(20f, lsc), cardW, S(34f, lsc));

            // ── Official receipt label — ref (0, 56, 456, 16) ────────────────────
            lblOfficialReceipt.SetBounds(0, S(56f, lsc), cardW, S(16f, lsc));

            // ── Receipt number — ref (0, 74, 456, 18) ────────────────────────────
            lblReceiptNumber.SetBounds(0, S(74f, lsc), cardW, S(18f, lsc));

            // ── sep1 — ref (24, 104, 408, 1) ─────────────────────────────────────
            sep1.SetBounds(padX, S(104f, lsc), innerW, 1);

            // ── Customer/Date/Transaction rows — ref start y=118, rowH=24 ─────────
            int rowH = S(24f, lsc);
            int valX = S(240f, lsc);
            int keyW = valX - padX - S(4f, lsc);
            int valW = cardW - valX - padX;

            // Customer row — ref y=118
            lblCustomerKey.SetBounds(padX, S(118f, lsc), keyW, rowH);
            lblCustomerValue.SetBounds(valX, S(118f, lsc), valW, rowH);

            // Date/Time row — ref y=142
            lblDateTimeKey.SetBounds(padX, S(142f, lsc), keyW, rowH);
            // DateTimeValue has a wider span starting at x=180
            lblDateTimeValue.SetBounds(S(180f, lsc), S(142f, lsc), cardW - S(180f, lsc) - padX, rowH);

            // Transaction row — ref y=166
            lblTransactionKey.SetBounds(padX, S(166f, lsc), keyW, rowH);
            lblTransactionValue.SetBounds(valX, S(166f, lsc), valW, rowH);

            // ── sep2 — ref (24, 196, 408, 1) ─────────────────────────────────────
            sep2.SetBounds(padX, S(196f, lsc), innerW, 1);

            // ── Charges container — ref starts at y=208, row height=22 ────────────
            int chargesY = S(REF_CHARGES_Y, lsc);
            int chargesRH = S(REF_CHARGES_ROW_H, lsc);
            int chargesH = BuildChargeRows(innerW, chargesRH);
            pnlChargesContainer.SetBounds(padX, chargesY, innerW, chargesH);

            // ── Everything below charges flows from actual container bottom ─────────
            int afterCharges = chargesY + chargesH + S(REF_CHARGES_GAP, lsc);

            // sep3 — ref gap between chargesContainer bottom and sep3 = 304-(208+88)=8 (= REF_CHARGES_GAP)
            sep3.SetBounds(padX, afterCharges, innerW, 1);
            int belowSep3 = afterCharges + 1;

            // lblTotalPaidKey — ref (24, 318) — offset from sep3 = 318-304=14
            int totalRowY = belowSep3 + S(14f, lsc);
            lblTotalPaidKey.SetBounds(padX, totalRowY, S(150f, lsc), S(28f, lsc));
            // lblTotalPaidValue — ref x=200, same row y=314 (≈ totalRowY offset by -4)
            lblTotalPaidValue.SetBounds(S(200f, lsc), totalRowY - S(4f, lsc), cardW - S(200f, lsc) - padX, S(32f, lsc));

            // lblTenderedKey/Value — ref y=358 → offset from sep3 = 358-304 = 54
            int tenderedY = belowSep3 + S(54f, lsc);
            lblTenderedKey.SetBounds(padX, tenderedY, S(150f, lsc), rowH);
            lblTenderedValue.SetBounds(S(280f, lsc), tenderedY, cardW - S(280f, lsc) - padX, rowH);

            // lblChangeKey/Value — ref y=382 → offset from sep3 = 382-304 = 78
            int changeY = belowSep3 + S(78f, lsc);
            lblChangeKey.SetBounds(padX, changeY, S(150f, lsc), rowH);
            lblChangeValue.SetBounds(S(280f, lsc), changeY, cardW - S(280f, lsc) - padX, rowH);

            // ── pnlPurchaseStatus — ref (24, 420) → offset from sep3 = 420-304 = 116
            int statusY = belowSep3 + S(116f, lsc);
            int statusW = innerW;
            int statusH = S(70f, lsc);
            pnlPurchaseStatus.SetBounds(padX, statusY, statusW, statusH);

            // Controls inside pnlPurchaseStatus — ref: 408 × 70
            float psSc = Math.Min(statusW / 408f, statusH / 70f);
            lblPurchaseStatusTitle.SetBounds(S(14f, psSc), S(10f, psSc), statusW - S(28f, psSc), S(15f, psSc));
            lblPurchaseNote.SetBounds(S(14f, psSc), S(34f, psSc), statusW - S(28f, psSc), S(24f, psSc));

            // ── Receipt card height — content bottom + bottom padding ─────────────
            // ref: card height = 542, last element ends at 420+70=490, bottom pad = 52
            int cardBottom = statusY + statusH + S(52f, lsc);
            return cardBottom;
        }

        // ─── Action buttons interior   (designer reference: 456 × 112) ───────────
        private void ScaleActionButtons(int panelW, int panelH, float sc)
        {
            const float refABW = 456f;
            const float refABH = 112f;
            float lsc = Math.Min(panelW / refABW, panelH / refABH);

            // ref: (0,0,222,44)  (234,0,222,44)  (0,56,222,44)  (234,56,222,44)
            int btnW = (panelW - S(12f, lsc)) / 2;   // half width with a small gap
            int btnH = S(44f, lsc);
            int gap = panelW - btnW * 2;

            btnPrint.SetBounds(0, 0, btnW, btnH);
            btnEmail.SetBounds(btnW + gap, 0, btnW, btnH);
            btnSavePDF.SetBounds(0, S(56f, lsc), btnW, btnH);
            btnSMS.SetBounds(btnW + gap, S(56f, lsc), btnW, btnH);
        }

        // ─── Build dynamic charge rows, returns total height ──────────────────────
        private int BuildChargeRows(int containerW, int scaledRowH)
        {
            pnlChargesContainer.Controls.Clear();

            int y = 0;
            int keyW = (int)(containerW * 0.58f);
            int valX = keyW + 8;
            int valW = containerW - valX;

            foreach (var (label, amount) in _result.Charges)
            {
                var key = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    Location = new Point(0, y),
                    Size = new Size(keyW, scaledRowH),
                    Text = label
                };
                var val = new Label
                {
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    Location = new Point(valX, y),
                    Size = new Size(valW, scaledRowH),
                    Text = $"₱{amount:N2}",
                    TextAlign = ContentAlignment.MiddleRight
                };
                pnlChargesContainer.Controls.Add(key);
                pnlChargesContainer.Controls.Add(val);
                y += scaledRowH;
            }

            return Math.Max(scaledRowH, y);
        }

        // ─── Populate receipt with data ───────────────────────────────────────────
        private void PopulateReceipt()
        {
            DateTime at = _result.ProcessedAt;
            lblReceiptNumber.Text = $"OR-{at:yyyy}-{at:HHmmss}";
            lblCustomerValue.Text = _result.CustomerName;
            lblDateTimeValue.Text = at.ToString("M/d/yyyy, h:mm:ss tt");
            lblTransactionValue.Text = "CASH PURCHASE";

            lblTotalPaidValue.Text = $"₱{_result.TotalDue:N0}";
            lblTenderedValue.Text = $"₱{_result.AmountReceived:N0}";
            lblChangeValue.Text = $"₱{_result.ChangeDue:N0}";

            lblPurchaseNote.Text = "🎉  Cash purchase complete — unit ready for release";
        }

        // ─── Blur background ──────────────────────────────────────────────────────
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

        // ─── Navigation ───────────────────────────────────────────────────────────
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

        private void BtnClose_Click(object sender, EventArgs e) => ReturnToPoscashier();
        private void BtnStartNew_Click(object sender, EventArgs e) => ReturnToPoscashier();

        private void BtnPrint_Click(object sender, EventArgs e)
            => MessageBox.Show(
                "Print functionality not yet implemented.\n\nFuture: integrate with thermal printer or default Windows printer.",
                "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void BtnEmail_Click(object sender, EventArgs e)
            => MessageBox.Show(
                $"Email functionality not yet implemented.\n\nFuture: email receipt to {_result.CustomerName}",
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

    // ── Result model ──────────────────────────────────────────────────────────────
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