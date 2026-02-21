using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class kiosk_product_detail : Form
    {
        private Guna.UI2.WinForms.Guna2TileButton selectedVariationButton = null;
        private Product currentProduct = null;

        public kiosk_product_detail()
        {
            InitializeComponent();
            SetupPurchaseOptionTiles();
            SetupVariationButtons();
        }

        // New constructor to accept a Product and display its details
        public kiosk_product_detail(Product product) : this()
        {
            if (product == null) return;
            currentProduct = product;
            PopulateProductDetails();
        }

        private void SetupVariationButtons()
        {
            // Setup variation button clicks
            selection1.Click += (s, e) => SelectVariation(selection1);
            guna2TileButton1.Click += (s, e) => SelectVariation(guna2TileButton1);
            guna2TileButton2.Click += (s, e) => SelectVariation(guna2TileButton2);
        }

        private void PopulateProductDetails()
        {
            try
            {
                // image
                try { gpic1.Image = currentProduct.Image; } catch { }

                // title / name
                try { label10.Text = currentProduct.Name; } catch { }

                // price
                try { label11.Text = $"₱ {currentProduct.Price:N0}"; } catch { }

                // brand and small title at top-right
                try { label2.Text = currentProduct.Brand; } catch { }
                try { label1.Text = currentProduct.Name; } catch { }

                // description / specs (reuse existing labels)
                try { label17.Text = $"{currentProduct.Year} · {currentProduct.Displacement}"; } catch { }

                // Optionally populate some spec tiles if present
                try { label26.Text = currentProduct.Category; } catch { }
            }
            catch { }
        }

        private void SelectVariation(Guna.UI2.WinForms.Guna2TileButton button)
        {
            // If clicking the already selected button, deselect it
            if (selectedVariationButton == button)
            {
                selectedVariationButton.BorderColor = Color.LightGray;
                selectedVariationButton.BorderThickness = 2;
                selectedVariationButton.FillColor = Color.Gainsboro;
                selectedVariationButton.ForeColor = Color.DarkGray;
                selectedVariationButton = null;
                return;
            }

            // Deselect previous button
            if (selectedVariationButton != null)
            {
                selectedVariationButton.BorderColor = Color.LightGray;
                selectedVariationButton.BorderThickness = 2;
                selectedVariationButton.FillColor = Color.Gainsboro;
                selectedVariationButton.ForeColor = Color.DarkGray;
            }

            // Select new button
            selectedVariationButton = button;
            selectedVariationButton.BorderColor = Color.FromArgb(0, 150, 136);
            selectedVariationButton.BorderThickness = 3;
            selectedVariationButton.FillColor = Color.FromArgb(240, 255, 240);
            selectedVariationButton.ForeColor = Color.ForestGreen;
        }

        private void SetupPurchaseOptionTiles()
        {
            // Setup On-Cash Payment tile
            guna2ShadowPanel7.MouseEnter += (object? s, EventArgs e) =>
            {
                guna2ShadowPanel7.FillColor = Color.FromArgb(220, 255, 220);
                guna2ShadowPanel7.ShadowDepth = 60;
                label7.ForeColor = Color.DarkGreen;
                label12.ForeColor = Color.DarkGreen;
                label13.ForeColor = Color.DarkGreen;
            };
            guna2ShadowPanel7.MouseLeave += (object? s, EventArgs e) =>
            {
                guna2ShadowPanel7.FillColor = Color.FromArgb(240, 255, 240);
                guna2ShadowPanel7.ShadowDepth = 50;
                label7.ForeColor = Color.ForestGreen;
                label12.ForeColor = Color.Gray;
                label13.ForeColor = Color.Gray;
            };
            guna2ShadowPanel7.Click += OnCashPaymentTile_Click;

            // Setup Easy Financing tile
            guna2ShadowPanel8.MouseEnter += (object? s, EventArgs e) =>
            {
                guna2ShadowPanel8.FillColor = Color.FromArgb(220, 240, 255);
                guna2ShadowPanel8.ShadowDepth = 60;
                label14.ForeColor = Color.DarkBlue;
                label9.ForeColor = Color.DarkBlue;
                label8.ForeColor = Color.DarkBlue;
            };
            guna2ShadowPanel8.MouseLeave += (object? s, EventArgs e) =>
            {
                guna2ShadowPanel8.FillColor = Color.FromArgb(240, 248, 255);
                guna2ShadowPanel8.ShadowDepth = 50;
                label14.ForeColor = Color.DodgerBlue;
                label9.ForeColor = Color.Gray;
                label8.ForeColor = Color.Gray;
            };
            guna2ShadowPanel8.Click += EasyFinancingTile_Click;
        }

        private void OnCashPaymentTile_Click(object sender, EventArgs e)
        {
            var payment = new ck_confirm_payment();
            payment.Show();
            this.Close();
        }

        private void EasyFinancingTile_Click(object sender, EventArgs e)
        {
            var loan1 = new loan_form_1(); 
            loan1.Show();
            this.Close();
        }

        private void payment_button_Click(object sender, EventArgs e)
        {
            var success = new payment_confirmed_window();
            success.Show();
            this.Close();
        }

        private void return_button_Click(object sender, EventArgs e)
        {
            var main = Application.OpenForms["ck"] as ck;
            if (main != null)
            {
                main.Show();
            }
            this.Close();
        }
    }
}
