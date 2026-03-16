using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotoDealerShop
{
    public partial class UserPanel2 : Form
    {
        public UserPanel2()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void AccountText_Click(object sender, EventArgs e)
        {

        }

        private void AccountText2_Click(object sender, EventArgs e)
        {

        }

        private void AccountTicketNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MobileNumberDescription_Click(object sender, EventArgs e)
        {

        }

        private void UserPanel2ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AccessAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                new UserPanel3().ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error navigating to User Panel 3: " + ex.Message);
            }
        }
    }
}
