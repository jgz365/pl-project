using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotoDealerShop
{
    public partial class UserPanel3 : Form
    {
        public UserPanel3()
        {
            InitializeComponent();
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Label1_Click_2(object sender, EventArgs e)
        {

        }

        private void TicketNumber_Click(object sender, EventArgs e)
        {

        }

        private void PayNowButton_Click(object sender, EventArgs e)
        {
            UserPAnel4 userPanel4 = new UserPAnel4();
            userPanel4.Show();
            this.Hide();
        }

        private void ScheduleButton_Click(object sender, EventArgs e)
        {
            UserPanel5 userPanel5 = new UserPanel5();
            userPanel5.Show();
            this.Hide();
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            UserPanel6 userPanel6 = new UserPanel6();
            userPanel6.Show();
            this.Hide();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            UserPanel7 userPanel7 = new UserPanel7();
            userPanel7.Show();
            this.Hide();
        }

        private void ViewAllButton_Click(object sender, EventArgs e)
        {
            UserPanel6 userPanel6 = new UserPanel6();
            userPanel6.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
