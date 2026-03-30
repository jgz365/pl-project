using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotoDealerShop
{
    public partial class UC_Overview : Form
    {
        public UC_Overview()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MonthlyRecoveryFunnel_Click(object sender, EventArgs e)
        {

        }

        private void tablelayoutpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        // Button Click Events for Navigation
        private void OBT_Click(object sender, EventArgs e)
        {
            FormManager.ShowOverview();
        }

        private void CBT_Click(object sender, EventArgs e)
        {
            FormManager.ShowCandidates();
        }

        private void ROBT_Click(object sender, EventArgs e)
        {
            FormManager.ShowRecoveryOps();
        }
    }
}

