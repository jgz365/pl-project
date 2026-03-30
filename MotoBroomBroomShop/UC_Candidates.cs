using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotoDealerShop
{
    public partial class UC_Candidates : Form
    {
        public UC_Candidates()
        {
            InitializeComponent();
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
