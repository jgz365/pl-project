<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace customer_kiosk
{
    public partial class ck_confirm_payment : Form
    {
        private Product currentProduct = null;

        public ck_confirm_payment()
        {
            InitializeComponent();
        }

        public ck_confirm_payment(Product product) : this()
        {
            currentProduct = product;
            PopulateProductDetails();
        }

        private void PopulateProductDetails()
        {
            if (currentProduct == null) return;

            try
            {
                // image
                try { purchaseImage.Image = Image.FromFile(currentProduct.ImagePath); } catch { }

                // product name
                try { productNameLabel.Text = currentProduct.Name; } catch { }

                // calculations
                decimal unitPrice = currentProduct.Price;
                decimal processingFee = Math.Round(unitPrice * 0.01m, 2); // 1% processing fee
                decimal totalDue = unitPrice + processingFee;

                // set labels
                try { unitPriceValue.Text = $"₱{unitPrice:N0}"; } catch { }
                try { processingFeeValue.Text = $"₱{processingFee:N0}"; } catch { }
                try { totalDueValue.Text = $"₱{totalDue:N0}"; } catch { }
            }
            catch { }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var confirm = new payment_confirmed_window();
            confirm.Show();
            this.Close();
        }

        private void ckg_back_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail(currentProduct);
            product.Show();
            this.Close();
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class ck_confirm_payment : Form
    {
        public ck_confirm_payment()
        {
            InitializeComponent();
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            var confirm = new payment_confirmed_window();
            confirm.Show();
            this.Close();
        }

        private void ckg_back_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }
    }
}
>>>>>>> 76e9872bf621d0cf86062814b6d214c8db3f7103
