using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class pnlSpecsContainer : UserControl
    {
        public pnlSpecsContainer()
        {
            InitializeComponent();

            // Force the control to support transparency
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Set the dimming color: 140 is the 'see-through' level
            this.BackColor = Color.FromArgb(140, 27, 32, 46);
        }
        private void pnlSpecsContainer_Load(object sender, EventArgs e)
        {
            // 1. Enable scrolling
            flowLayoutPanel1.AutoScroll = true;

            // 2. IMPORTANT: Disable AutoSize so the panel doesn't grow with its content
            // This is the #1 reason scrollbars don't appear!
            flowLayoutPanel1.AutoSize = false;

            // 3. Set a fixed height that is SHORTER than your content
            // For your 950x620 card, a height of 350-400 is usually perfect.
            flowLayoutPanel1.Height = 380;

            // 4. Prevent horizontal scrolling (keeps it clean)
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
        }



        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            flowLayoutPanel1.AutoScroll = true;
            // This is the #1 reason scrollbars don't appear!
            flowLayoutPanel1.AutoSize = false;

            // 3. Set a fixed height that is SHORTER than your content
            // For your 950x620 card, a height of 350-400 is usually perfect.
            flowLayoutPanel1.Height = 380;

            // 4. Prevent horizontal scrolling (keeps it clean)
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void actualPnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel18_Click(object sender, EventArgs e)
        {

        }

        private void pnDimmer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel48_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel44_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel46_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            // 1. Find the Main Form (Form1) that holds all your pages
            if (this.FindForm() is Form1 mainForm)
            {
                // 2. Create a new instance of your Inventory UserControl
                // Assuming your main inventory control is named 'UC_Inventory'
                UC_Inventory inventoryPage = new UC_Inventory();

                // 3. Use your existing DisplayPage method to switch views
                // This ensures the sidebar and header remain while the content changes
                mainForm.DisplayPage(inventoryPage);
            }
            else
            {
                // Fallback: If for some reason Form1 isn't found, 
                // at least remove this control so it's not stuck on screen
                if (this.Parent != null)
                {
                    this.Parent.Controls.Remove(this);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UC_EditMotorcycle editPage = new UC_EditMotorcycle();

            // 2. Access the Main Form (where your pnl_Container is located)
            // We assume your main container is on Form1 or MainForm
            if (this.FindForm() is Form1 main)
            {
                // Use the switcher method we discussed earlier
                main.DisplayPage(editPage);
            }

        }
    }
}

