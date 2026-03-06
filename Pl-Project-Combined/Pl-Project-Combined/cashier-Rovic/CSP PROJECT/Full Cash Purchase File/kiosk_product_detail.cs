using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSP_PROJECT
{
    /// <summary>
    /// UserControl version of kiosk_product_detail.
    /// Navigation: CkForm loads this; this loads ck_confirm_payment (UC).
    /// Back (return) button fires CloseRequested so CkForm can restore itself.
    /// </summary>
    public partial class kiosk_product_detail : UserControl
    {
        public event EventHandler CloseRequested;

        private Guna.UI2.WinForms.Guna2TileButton selectedVariationButton = null;

        public kiosk_product_detail()
        {
            InitializeComponent();
            SetupPurchaseOptionTiles();
            SetupVariationButtons();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Responsive layout — called on every resize
        // ─────────────────────────────────────────────────────────────────────
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RepositionPanels();
        }

        private void RepositionPanels()
        {
            if (this.Width < 100 || this.Height < 100) return;

            int w = this.Width;
            int h = this.Height;

            // ── Header bar: full width ──────────────────────────────────────
            kiosk_prod_panel.Width = w;

            // Product name labels — top-right inside header
            label1.Location = new Point(w - 260, 27);
            label2.Location = new Point(w - 185, 64);

            int headerBottom = kiosk_prod_panel.Bottom + 10;

            // ── Left column (image + specs) ─────────────────────────────────
            int leftX = (int)(w * 0.02);
            int leftW = (int)(w * 0.34);

            // Image panel
            guna2ShadowPanel4.Location = new Point(leftX, headerBottom);
            guna2ShadowPanel4.Width = leftW;
            guna2ShadowPanel4.Height = (int)(h * 0.38);
            gpic1.Size = new Size(guna2ShadowPanel4.Width - 42,
                                  guna2ShadowPanel4.Height - 40);

            // Specs panel — fills remaining height below image
            int specsTop = guna2ShadowPanel4.Bottom + 10;
            guna2ShadowPanel1.Location = new Point(leftX, specsTop);
            guna2ShadowPanel1.Width = leftW;
            guna2ShadowPanel1.Height = h - specsTop - 10;

            // ── Right column (product info + purchase options) ───────────────
            int rightX = guna2ShadowPanel4.Right + 20;
            int rightW = w - rightX - 20;

            // Product info/variation panel
            guna2ShadowPanel2.Location = new Point(rightX, headerBottom);
            guna2ShadowPanel2.Width = rightW;
            guna2ShadowPanel2.Height = (int)(h * 0.30);

            // Purchase options panel — fills remaining height
            int purchaseTop = guna2ShadowPanel2.Bottom + 10;
            guna2ShadowPanel3.Location = new Point(rightX, purchaseTop);
            guna2ShadowPanel3.Width = rightW;
            guna2ShadowPanel3.Height = h - purchaseTop - 10;

            // Stretch the two purchase-option tiles to fill their parent width
            int tileW = guna2ShadowPanel3.Width - 32;
            if (tileW > 0)
            {
                guna2ShadowPanel7.Width = tileW;
                guna2ShadowPanel8.Width = tileW;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Variation button selection
        // ─────────────────────────────────────────────────────────────────────
        private void SetupVariationButtons()
        {
            selection1.Click += (s, e) => SelectVariation(selection1);
            guna2TileButton1.Click += (s, e) => SelectVariation(guna2TileButton1);
            guna2TileButton2.Click += (s, e) => SelectVariation(guna2TileButton2);
        }

        private void SelectVariation(Guna.UI2.WinForms.Guna2TileButton button)
        {
            if (selectedVariationButton == button)
            {
                selectedVariationButton.BorderColor = Color.LightGray;
                selectedVariationButton.BorderThickness = 2;
                selectedVariationButton.FillColor = Color.Gainsboro;
                selectedVariationButton.ForeColor = Color.DarkGray;
                selectedVariationButton = null;
                return;
            }

            if (selectedVariationButton != null)
            {
                selectedVariationButton.BorderColor = Color.LightGray;
                selectedVariationButton.BorderThickness = 2;
                selectedVariationButton.FillColor = Color.Gainsboro;
                selectedVariationButton.ForeColor = Color.DarkGray;
            }

            selectedVariationButton = button;
            selectedVariationButton.BorderColor = Color.FromArgb(0, 150, 136);
            selectedVariationButton.BorderThickness = 3;
            selectedVariationButton.FillColor = Color.FromArgb(240, 255, 240);
            selectedVariationButton.ForeColor = Color.ForestGreen;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Purchase option tiles hover effects
        // ─────────────────────────────────────────────────────────────────────
        private void SetupPurchaseOptionTiles()
        {
            // On-Cash Payment tile
            guna2ShadowPanel7.MouseEnter += (s, e) =>
            {
                guna2ShadowPanel7.FillColor = Color.FromArgb(220, 255, 220);
                guna2ShadowPanel7.ShadowDepth = 60;
                label7.ForeColor = Color.DarkGreen;
                label12.ForeColor = Color.DarkGreen;
                label13.ForeColor = Color.DarkGreen;
            };
            guna2ShadowPanel7.MouseLeave += (s, e) =>
            {
                guna2ShadowPanel7.FillColor = Color.FromArgb(240, 255, 240);
                guna2ShadowPanel7.ShadowDepth = 50;
                label7.ForeColor = Color.ForestGreen;
                label12.ForeColor = Color.Gray;
                label13.ForeColor = Color.Gray;
            };
            guna2ShadowPanel7.Click += OnCashPaymentTile_Click;

            // Easy Financing tile
            guna2ShadowPanel8.MouseEnter += (s, e) =>
            {
                guna2ShadowPanel8.FillColor = Color.FromArgb(220, 240, 255);
                guna2ShadowPanel8.ShadowDepth = 60;
                label14.ForeColor = Color.DarkBlue;
                label9.ForeColor = Color.DarkBlue;
                label8.ForeColor = Color.DarkBlue;
            };
            guna2ShadowPanel8.MouseLeave += (s, e) =>
            {
                guna2ShadowPanel8.FillColor = Color.FromArgb(240, 248, 255);
                guna2ShadowPanel8.ShadowDepth = 50;
                label14.ForeColor = Color.DodgerBlue;
                label9.ForeColor = Color.Gray;
                label8.ForeColor = Color.Gray;
            };
            guna2ShadowPanel8.Click += EasyFinancingTile_Click;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Navigation — panel-swap into parent container
        // ─────────────────────────────────────────────────────────────────────
        private void ShowConfirmPayment()
        {
            if (this.Parent == null) return;

            var parent = this.Parent;

            for (int i = parent.Controls.Count - 1; i >= 0; i--)
                if (parent.Controls[i] is ck_confirm_payment)
                    parent.Controls.RemoveAt(i);

            var confirmUC = new ck_confirm_payment
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Margin = new Padding(0)
            };

            confirmUC.CloseRequested += (s, args) =>
            {
                parent.Controls.Remove(confirmUC);
                confirmUC.Dispose();
                this.Visible = true;
                this.BringToFront();
                this.Focus();
            };

            this.Visible = false;
            parent.Controls.Add(confirmUC);
            confirmUC.BringToFront();
            confirmUC.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Click handlers
        // ─────────────────────────────────────────────────────────────────────
        private void OnCashPaymentTile_Click(object sender, EventArgs e) => ShowConfirmPayment();

        private void EasyFinancingTile_Click(object sender, EventArgs e)
        {
            // Hook up your loan_form_1 UserControl here using the same panel-swap pattern
        }

        private void payment_button_Click(object sender, EventArgs e) => ShowConfirmPayment();

        // ── BACK / RETURN BUTTON ─────────────────────────────────────────────
        private void return_button_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }

        private void label4_Click(object sender, EventArgs e) { }
    }
}