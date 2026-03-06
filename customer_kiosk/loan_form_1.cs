using System;
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
