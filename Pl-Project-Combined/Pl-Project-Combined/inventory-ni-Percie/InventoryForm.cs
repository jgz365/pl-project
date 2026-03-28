using System;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    // Simple Form that hosts the UC_Inventory UserControl so it can be shown like a Form.
    public class InventoryForm : Form
    {
        private UC_Inventory ucInventory;

        public InventoryForm()
        {
            Text = "Inventory";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            // Build UI programmatically so no designer file is required.
            ucInventory = new UC_Inventory { Dock = DockStyle.Fill };
            Controls.Add(ucInventory);
        }
    }
}