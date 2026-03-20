using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class UC_Inventory : UserControl
    {
        public UC_Inventory()
        {
            InitializeComponent();
            // This runs the logic as soon as the Inventory screen loads
            PopulateInventory();
        }

        private void PopulateInventory()
        {
            // 1. Clear existing items
            flpInventory.Controls.Clear();

            // 2. Add Honda Card
            var card1 = new UC_MotorcycleCard { Margin = new Padding(5) };
            card1.SetMotorcycleData("Honda ADV 160", "2025 • Adventure", "₱166,900", "5", null, "AVAILABLE");
            card1.OnCardClick += (s, ev) => { if (this.FindForm() is Form1 main) main.ShowSpecsOverlay(); };
            flpInventory.Controls.Add(card1);

            // 3. Add Suzuki Card
            var card2 = new UC_MotorcycleCard { Margin = new Padding(5) };
            card2.SetMotorcycleData("Suzuki Raider R150", "2024 • Underbone", "₱119,900", "0", null, "PRE-ORDER");
            card2.OnCardClick += (s, ev) => { if (this.FindForm() is Form1 main) main.ShowSpecsOverlay(); };
            flpInventory.Controls.Add(card2);

            // 4. Add Yamaha Card
            var card3 = new UC_MotorcycleCard { Margin = new Padding(5) };
            card3.SetMotorcycleData("Yamaha NMAX 155", "2025 • Scooter", "₱151,900", "2", null, "LIMITED");
            card3.OnCardClick += (s, ev) => { if (this.FindForm() is Form1 main) main.ShowSpecsOverlay(); };
            flpInventory.Controls.Add(card3);
        }

        // Keep these only if you plan to add logic later; 
        // otherwise, they can be deleted from the Designer to stay clean.
        private void btnAddMotorcycle_Click(object sender, EventArgs e)
        {
            // Logic for adding a new bike goes here
        }

        private void guna2ShadowPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_Inventory_Load(object sender, EventArgs e)
        {

        }

        private void main_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void search_inventory_pnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpInventory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInventorySearchPnl_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel15_Click(object sender, EventArgs e)
        {

        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }


#nullable enable
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            // 1. Find the Main Form
            if (this.FindForm() is Form1 main)
            {
                // 2. Open the ADD form (the one you duplicated and renamed)
                // Ensure you fixed the class name inside UC_AddMotorcycle.cs first!
                main.DisplayPage(new UC_AddMotorcycle());
            }
        }



        private void pnlSidebarMaster_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

        }

        private void invenBtn_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Separator1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel18_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}