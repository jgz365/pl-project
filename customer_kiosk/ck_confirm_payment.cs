using System;
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
