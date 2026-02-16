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
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            loan_form_2 form_2 = new loan_form_2();
            form_2.ShowDialog();
            this.Show();
        }
    }
}
