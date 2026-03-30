using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotoDealerShop
{
    // ── IMPORTANT: Must be UserControl, NOT Form ─────────────────────────────
    public partial class UC_Candidates : UserControl
    {
        public UC_Candidates()
        {
            InitializeComponent();
        }

        // ── Shared helper: routes through whichever host Form is active ───────
        private void NavigateTo(UserControl uc)
        {
            var form = this.FindForm();
            if (form is inventory_ni_Percie.Form1 f1)
                f1.DisplayPage(uc);
            else if (form is inventory_ni_Percie.Form2 f2)
                f2.DisplayPage(uc);
        }

        // ── Tab button navigation ─────────────────────────────────────────────
        private void OBT_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_Overview());
        }

        private void CBT_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_Candidates());
        }

        private void ROBT_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_RecoveryOps());
        }
    }
}