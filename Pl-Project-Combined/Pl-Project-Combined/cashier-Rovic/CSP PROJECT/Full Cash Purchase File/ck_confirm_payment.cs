using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSP_PROJECT
{
    public partial class ck_confirm_payment : UserControl
    {
        // ── Navigation event (back to kiosk_product_detail / UC_main_menu) ───
        public event EventHandler CloseRequested;

        // ── Add-on prices ─────────────────────────────────────────────────────
        private const decimal BASE_UNIT_PRICE = 151_900m;
        private const decimal DOC_STAMP_TAX = 600m;
        private const decimal LTO_REG_PRICE = 1_500m;
        private const decimal TPL_INSURANCE_PRICE = 950m;

        private bool _ltoChecked = true;
        private bool _tplChecked = true;

        // ── Reference design dimensions (designer baseline, 96 DPI) ──────────
        // Form:   1280 × 660
        // Header: 0,0  → 1280 × 72
        // Panels: y=88, h=480
        //   pnlSummary:        x=24,  w=390
        //   pnlAddons:         x=434, w=390  (gap=20)
        //   guna2ShadowPanel1: x=844, w=400  (gap=20, right margin=36)
        private const float REF_W = 1280f;
        private const float REF_H = 660f;
        private const float REF_HDR_H = 72f;
        private const float REF_TOP_Y = 88f;
        private const float REF_PNL_H = 480f;
        private const float REF_PAD_L = 24f;
        private const float REF_PAD_R = 36f;
        private const float REF_GAP = 20f;
        private const float REF_PNL_W1 = 390f;   // pnlSummary
        private const float REF_PNL_W2 = 390f;   // pnlAddons
        private const float REF_PNL_W3 = 400f;   // guna2ShadowPanel1 (Customer)

        // ─────────────────────────────────────────────────────────────────────
        public ck_confirm_payment()
        {
            InitializeComponent();
            WireAddOnEvents();
            RefreshTotal();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Public: set product info from the catalog selection
        // ─────────────────────────────────────────────────────────────────────
        public void SetProduct(string title, string sub, string price, string imageUrl)
        {
            productNameLabel.Text = string.IsNullOrWhiteSpace(title) ? "Selected Product" : title;
            productDetailsLabel.Text = string.IsNullOrWhiteSpace(sub) ? string.Empty : sub;

            if (decimal.TryParse(
                    price?.Replace("₱", "").Replace(",", "").Trim(),
                    out decimal parsedPrice) && parsedPrice > 0)
            {
                // Future: could override BASE_UNIT_PRICE dynamically here
            }

            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                try
                {
                    productImage.LoadAsync(imageUrl);
                }
                catch { /* image optional */ }
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Responsive layout
        // ─────────────────────────────────────────────────────────────────────
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (ClientSize.Width < 200 || ClientSize.Height < 100) return;

            float w = ClientSize.Width;
            float h = ClientSize.Height;
            float sx = w / REF_W;
            float sy = h / REF_H;

            // ── Header bar ────────────────────────────────────────────────────
            int hdrH = Math.Max(48, (int)(REF_HDR_H * sy));
            kiosk_prod_panel.SetBounds(0, 0, (int)w, hdrH);
            ckg_back.Location = new Point(8, (hdrH - ckg_back.Height) / 2);
            label10.Location = new Point(74, Math.Max(4, (hdrH / 2) - label10.Height - 1));
            stepLabel.Location = new Point(74, hdrH / 2 + 1);

            // ── Three outer content panels ─────────────────────────────────────
            int padL = Math.Max(8, (int)(REF_PAD_L * sx));
            int padR = Math.Max(8, (int)(REF_PAD_R * sx));
            int gap = Math.Max(4, (int)(REF_GAP * sx));
            int topY = Math.Max(hdrH + 4, (int)(REF_TOP_Y * sy));
            int contentH = Math.Max(200, Math.Min((int)(REF_PNL_H * sy), (int)h - topY - 8));

            // Distribute width across three panels in their ref proportions (390:390:400 = 1180 total)
            int available = (int)w - padL - padR - gap * 2;
            int pW1 = (int)(available * (REF_PNL_W1 / (REF_PNL_W1 + REF_PNL_W2 + REF_PNL_W3)));
            int pW2 = (int)(available * (REF_PNL_W2 / (REF_PNL_W1 + REF_PNL_W2 + REF_PNL_W3)));
            int pW3 = available - pW1 - pW2;

            int x1 = padL;
            int x2 = x1 + pW1 + gap;
            int x3 = x2 + pW2 + gap;

            pnlSummary.SetBounds(x1, topY, pW1, contentH);
            pnlAddons.SetBounds(x2, topY, pW2, contentH);
            guna2ShadowPanel1.SetBounds(x3, topY, pW3, contentH);

            // ── Interior scaling (each uses its own panel-relative scale) ──────
            ScaleSummaryInterior(pW1, contentH);
            ScaleAddonsInterior(pW2, contentH);
            ScaleCustomerInterior(pW3, contentH);
        }

        // Round-scale helper — all values are relative to the caller's own reference frame.
        private static int S(float refVal, float scale) => (int)Math.Round(refVal * scale);

        // ─────────────────────────────────────────────────────────────────────
        // pnlSummary interior   (designer reference: 390 × 480)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleSummaryInterior(int panelW, int panelH)
        {
            const float refPW = 390f;
            const float refPH = 480f;

            float scX = panelW / refPW;
            float scY = panelH / refPH;
            float sc = Math.Min(scX, scY);

            // ── "Purchase Summary" title — ref (18, 18) ────────────────────
            int padX = S(18f, sc);
            purchaseSummaryLabel.Location = new Point(padX, S(18f, sc));

            // ── productPanel — ref (16, 46, 354, 80) ──────────────────────
            int ppPad = S(16f, sc);
            int ppY = S(46f, sc);
            int ppW = panelW - ppPad * 2;
            int ppH = S(80f, sc);
            productPanel.SetBounds(ppPad, ppY, ppW, ppH);

            // Controls inside productPanel — designer ref: 354 × 80
            float ppSc = Math.Min(ppW / 354f, ppH / 80f);
            productImage.SetBounds(S(12f, ppSc), S(10f, ppSc), S(60f, ppSc), S(60f, ppSc));
            productNameLabel.Location = new Point(S(82f, ppSc), S(14f, ppSc));
            productDetailsLabel.Location = new Point(S(82f, ppSc), S(38f, ppSc));

            // ── "PAYMENT BREAKDOWN" sub-title — ref (18, 144) ─────────────
            lblBreakdownTitle.Location = new Point(padX, S(144f, sc));

            // Right-column value labels: ref x=200 out of 390 px panel width.
            // Pin proportionally to panel width so they stay right-aligned.
            int rightX = (int)(panelW * (200f / refPW));
            int rightW = panelW - rightX - padX;

            // Unit Price row — ref y=168
            lblUnitPriceKey.Location = new Point(padX, S(168f, sc));
            lblUnitPriceVal.SetBounds(rightX, S(168f, sc), rightW, lblUnitPriceVal.Height);

            // Doc Stamp row — ref y=192
            lblDocStampKey.Location = new Point(padX, S(192f, sc));
            lblDocStampVal.SetBounds(rightX, S(192f, sc), rightW, lblDocStampVal.Height);

            // LTO row — ref y=216
            ltoBreakdownRow.Location = new Point(padX, S(216f, sc));
            ltoBreakdownAmt.SetBounds(rightX, S(216f, sc), rightW, ltoBreakdownAmt.Height);

            // TPL row — ref y=240
            tplBreakdownRow.Location = new Point(padX, S(240f, sc));
            tplBreakdownAmt.SetBounds(rightX, S(240f, sc), rightW, tplBreakdownAmt.Height);

            // ── Separator — ref (18, 270, 354, 1) ─────────────────────────
            sepTotal.SetBounds(padX, S(270f, sc), panelW - padX * 2, 1);

            // ── Total Amount — ref label(18,282), value(160,276,212,34) ───
            totalAmountLabel.Location = new Point(padX, S(282f, sc));
            int totValX = (int)(panelW * (160f / refPW));
            totalAmountValue.SetBounds(totValX, S(276f, sc), panelW - totValX - padX, S(34f, sc));
        }

        // ─────────────────────────────────────────────────────────────────────
        // pnlAddons interior   (designer reference: 390 × 480)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleAddonsInterior(int panelW, int panelH)
        {
            const float refPW = 390f;
            const float refPH = 480f;

            float scX = panelW / refPW;
            float scY = panelH / refPH;
            float sc = Math.Min(scX, scY);

            int padX = S(18f, sc);

            // ── "Recommended Add-ons" title — ref (18, 18) ────────────────
            addonsTitle.Location = new Point(padX, S(18f, sc));

            // Addon rows share the same reference dimensions (354 × 72)
            int rowW = panelW - padX * 2;
            int rowH = S(72f, sc);

            // ── ltoPanel — ref (18, 52, 354, 72) ──────────────────────────
            ltoPanel.SetBounds(padX, S(52f, sc), rowW, rowH);

            // Controls inside ltoPanel — designer ref: 354 × 72
            float rowSc = Math.Min(rowW / 354f, rowH / 72f);
            ltoCheck.Location = new Point(S(10f, rowSc), (rowH - ltoCheck.Height) / 2);
            ltoNameLabel.Location = new Point(S(36f, rowSc), S(12f, rowSc));
            ltoDescLabel.Location = new Point(S(36f, rowSc), S(34f, rowSc));
            // ltoPriceLabel right-aligned — ref x=250 in 354px row, w=90
            ltoPriceLabel.SetBounds(rowW - S(90f, rowSc) - S(4f, rowSc), S(26f, rowSc), S(90f, rowSc), ltoPriceLabel.Height);

            // ── tplPanel — ref (18, 136, 354, 72) ─────────────────────────
            tplPanel.SetBounds(padX, S(136f, sc), rowW, rowH);

            // Controls inside tplPanel — same ref dimensions as ltoPanel
            tplCheck.Location = new Point(S(10f, rowSc), (rowH - tplCheck.Height) / 2);
            tplNameLabel.Location = new Point(S(36f, rowSc), S(12f, rowSc));
            tplDescLabel.Location = new Point(S(36f, rowSc), S(34f, rowSc));
            // tplPriceLabel right-aligned — same proportions as ltoPanel
            tplPriceLabel.SetBounds(rowW - S(90f, rowSc) - S(4f, rowSc), S(26f, rowSc), S(90f, rowSc), tplPriceLabel.Height);

            // ── pnlTransactionNote — ref (18, 228, 354, 82) ───────────────
            int noteH = S(82f, sc);
            pnlTransactionNote.SetBounds(padX, S(228f, sc), rowW, noteH);

            // Controls inside pnlTransactionNote — designer ref: 354 × 82
            float noteSc = Math.Min(rowW / 354f, noteH / 82f);
            txnNoteTitle.Location = new Point(S(10f, noteSc), S(10f, noteSc));
            txnNoteText.SetBounds(S(10f, noteSc), S(30f, noteSc), rowW - S(20f, noteSc), noteH - S(36f, noteSc));
        }

        // ─────────────────────────────────────────────────────────────────────
        // guna2ShadowPanel1 interior   (designer reference: 400 × 480)
        // ─────────────────────────────────────────────────────────────────────
        private void ScaleCustomerInterior(int panelW, int panelH)
        {
            const float refPW = 400f;
            const float refPH = 480f;

            float scX = panelW / refPW;
            float scY = panelH / refPH;
            float sc = Math.Min(scX, scY);

            int padX = S(18f, sc);
            int inputW = panelW - padX * 2;

            // ── "Customer Information" title — ref (18, 18) ───────────────
            customerInfoLabel.Location = new Point(padX, S(18f, sc));

            // ── Full Name label — ref (18, 52) ────────────────────────────
            fullNameLabel.Location = new Point(padX, S(52f, sc));

            // ── fullNameTextBox — ref (18, 70, 364, 38) ───────────────────
            fullNameTextBox.SetBounds(padX, S(70f, sc), inputW, S(38f, sc));

            // ── Mobile Number label — ref (18, 118) ───────────────────────
            mobileNumberLabel.Location = new Point(padX, S(118f, sc));

            // ── mobileNumberTextBox — ref (18, 136, 364, 38) ──────────────
            mobileNumberTextBox.SetBounds(padX, S(136f, sc), inputW, S(38f, sc));

            // ── "Required Documents to Bring" label — ref (18, 186) ───────
            requiredDocsLabel.Location = new Point(padX, S(186f, sc));

            // ── Checkboxes — ref (28, 210), (28, 234), (28, 258) ──────────
            int cbX = S(28f, sc);
            validIdCheckBox.Location = new Point(cbX, S(210f, sc));
            proofAddressCheckBox.Location = new Point(cbX, S(234f, sc));
            tinNumberCheckBox.Location = new Point(cbX, S(258f, sc));

            // ── confirmButton — ref (18, 408, 364, 52) ────────────────────
            // Bottom margin in designer: 480 - (408 + 52) = 20 px.
            // We anchor to the bottom of the panel so it stays at the bottom
            // regardless of panel height, using the scaled button height.
            int btnH = Math.Max(44, S(52f, sc));
            int btnY = panelH - btnH - S(20f, sc);
            confirmButton.SetBounds(padX, btnY, inputW, btnH);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Add-on toggle logic
        // ─────────────────────────────────────────────────────────────────────
        private void WireAddOnEvents()
        {
            ltoPanel.Click += (s, e) => ToggleLTO();
            ltoCheck.Click += (s, e) => ToggleLTO();
            ltoNameLabel.Click += (s, e) => ToggleLTO();
            ltoDescLabel.Click += (s, e) => ToggleLTO();
            ltoPriceLabel.Click += (s, e) => ToggleLTO();

            tplPanel.Click += (s, e) => ToggleTPL();
            tplCheck.Click += (s, e) => ToggleTPL();
            tplNameLabel.Click += (s, e) => ToggleTPL();
            tplDescLabel.Click += (s, e) => ToggleTPL();
            tplPriceLabel.Click += (s, e) => ToggleTPL();
        }

        private void ToggleLTO()
        {
            _ltoChecked = !_ltoChecked;
            ltoCheck.Checked = _ltoChecked;
            ApplyAddonStyle(ltoPanel, ltoCheck, _ltoChecked);
            RefreshTotal();
        }

        private void ToggleTPL()
        {
            _tplChecked = !_tplChecked;
            tplCheck.Checked = _tplChecked;
            ApplyAddonStyle(tplPanel, tplCheck, _tplChecked);
            RefreshTotal();
        }

        private static void ApplyAddonStyle(Panel panel, CheckBox check, bool active)
        {
            panel.BackColor = active
                ? Color.FromArgb(236, 253, 245)
                : Color.FromArgb(248, 250, 252);
            check.ForeColor = active
                ? Color.FromArgb(5, 150, 105)
                : Color.Gray;
        }

        private void RefreshTotal()
        {
            decimal total = BASE_UNIT_PRICE + DOC_STAMP_TAX;
            if (_ltoChecked) total += LTO_REG_PRICE;
            if (_tplChecked) total += TPL_INSURANCE_PRICE;

            ltoBreakdownRow.Visible = _ltoChecked;
            ltoBreakdownAmt.Visible = _ltoChecked;
            tplBreakdownRow.Visible = _tplChecked;
            tplBreakdownAmt.Visible = _tplChecked;

            totalAmountValue.Text = $"₱{total:N0}";
        }

        private decimal ComputeTotal()
        {
            decimal total = BASE_UNIT_PRICE + DOC_STAMP_TAX;
            if (_ltoChecked) total += LTO_REG_PRICE;
            if (_tplChecked) total += TPL_INSURANCE_PRICE;
            return total;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Confirm → open CollectionForm5
        // ─────────────────────────────────────────────────────────────────────
        private void confirmButton_Click(object sender, EventArgs e)
        {
            string customerName = fullNameTextBox.Text.Trim();
            string mobileNumber = mobileNumberTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Please enter the customer's full name.", "Required Field",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fullNameTextBox.Focus();
                return;
            }

            var charges = new List<(string Label, decimal Amount)>
            {
                ($"Unit: {productNameLabel.Text}", BASE_UNIT_PRICE),
                ("Documentary Stamp Tax", DOC_STAMP_TAX),
            };
            if (_ltoChecked) charges.Add(("LTO Registration (3 Yrs)", LTO_REG_PRICE));
            if (_tplChecked) charges.Add(("TPL Insurance (1 Yr)", TPL_INSURANCE_PRICE));

            decimal totalDue = ComputeTotal();

            if (this.Parent == null) return;
            var parent = this.Parent;

            for (int i = parent.Controls.Count - 1; i >= 0; i--)
                if (parent.Controls[i] is CollectionForm5)
                    parent.Controls.RemoveAt(i);

            var col5 = new CollectionForm5();
            col5.SetData(customerName, mobileNumber, charges, totalDue);
            col5.Dock = DockStyle.Fill;
            col5.Location = new Point(0, 0);
            col5.Margin = new Padding(0);

            col5.BackRequested += (s, args) =>
            {
                parent.Controls.Remove(col5);
                col5.Dispose();
                this.Visible = true;
                this.BringToFront();
                this.Focus();
            };

            this.Visible = false;
            parent.Controls.Add(col5);
            col5.BringToFront();
            col5.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Back button
        // ─────────────────────────────────────────────────────────────────────
        private void ckg_back_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }
    }
}