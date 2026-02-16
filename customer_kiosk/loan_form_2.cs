using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_2 : Form
    {
        public loan_form_2()
        {
            InitializeComponent();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            loan_form_3 form_3 = new loan_form_3();
            form_3.ShowDialog();
            this.Show();
        }

        private void button_loan_back_2_Click(object sender, EventArgs e)
        {
            this.Hide();
            kiosk_product_detail product_detail = new kiosk_product_detail();
            product_detail.ShowDialog();
            this.Show();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
