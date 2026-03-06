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
    public partial class loan_form_1 : Form
    {
        private Product currentProduct = null;
        private LoanApplicationSession loanSession = null;

        public loan_form_1()
        {
            InitializeComponent();
        }

        public loan_form_1(Product product) : this()
        {
            currentProduct = product;
            loanSession = new LoanApplicationSession { SelectedProduct = product };
            PopulateProductDetails();
        }

        public loan_form_1(Product product, LoanApplicationSession session) : this()
        {
            currentProduct = product;
            loanSession = session ?? new LoanApplicationSession();
            if (loanSession.SelectedProduct == null) loanSession.SelectedProduct = product;
            PopulateProductDetails();
        }

        private void ckg_back_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail(currentProduct);
            product.Show();
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var loan2 = new loan_form_2(currentProduct, loanSession);
            loan2.Show();
            this.Close();
        }

        private void PopulateProductDetails()
        {
            if (currentProduct == null) return;

            try
            {
                // image
                try { productLoanImage.Image = Image.FromFile(currentProduct.ImagePath); } catch { }

                // title / name
                try { productNameLabel.Text = currentProduct.Name; } catch { }

                // price
                try { priceLabel.Text = $"₱ {currentProduct.Price:N0}"; } catch { }

                // color, perhaps set to category or something
                try { colorValueLabel.Text = currentProduct.Category; } catch { }
            }
            catch { }
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
    public partial class loan_form_1 : Form
    {
        public loan_form_1()
        {
            InitializeComponent();
        }
        private void ckg_back_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var loan2 = new loan_form_2();
            loan2.Show();
            this.Close();
        }
    }
}
>>>>>>> 76e9872bf621d0cf86062814b6d214c8db3f7103
