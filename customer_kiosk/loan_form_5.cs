using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_5 : Form
    {
        public loan_form_5()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            loan_form_6 form_6 = new loan_form_6();
            form_6.ShowDialog();
            this.Show();
        }

        private void button_loan_back_5_Click(object sender, EventArgs e)
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
