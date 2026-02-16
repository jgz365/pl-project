using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_6 : Form
    {
        public loan_form_6()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ck ck = new ck();
            ck.ShowDialog();
            this.Show();
        }
    }
}
