using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_3 : Form
    {
        public loan_form_3()
        {
            InitializeComponent();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var loan4 = new loan_form_4();
            loan4.Show();
            this.Close();
        }

        private void button_loan_back_3_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var back2 = new loan_form_2();
            back2.Show();
            this.Close();
        }
    }
}
