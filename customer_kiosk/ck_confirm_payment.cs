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
            this.Hide();
            payment_confirmed_window confirmed_window = new payment_confirmed_window();
            confirmed_window.ShowDialog();
            this.Show();
        }

        private void ckg_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
