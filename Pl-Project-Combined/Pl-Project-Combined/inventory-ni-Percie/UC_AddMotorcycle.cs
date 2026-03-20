#nullable enable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class UC_AddMotorcycle : UserControl
    {
        public UC_AddMotorcycle()
        {
            InitializeComponent();
        }

        // The Add/Save Button Logic
        private void btnAddMotorcycle_Click(object sender, EventArgs e)
        {
            Control? firstEmpty = FindFirstEmptyField(this);

            if (firstEmpty == null)
            {
                MessageBox.Show("Motorcycle Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAllFields(this);
            }
            else
            {
                MessageBox.Show("Please fill all fields.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnsureControlVisible(firstEmpty);
                firstEmpty.Focus();
            }
        }

        // --- HELPER METHODS (Required to fix 'does not exist' errors) ---

        private Control? FindFirstEmptyField(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2TextBox txt && string.IsNullOrWhiteSpace(txt.Text)) return txt;
                if (c is Guna.UI2.WinForms.Guna2ComboBox cb && cb.SelectedIndex == -1) return cb;
                if (c.HasChildren) { var found = FindFirstEmptyField(c); if (found != null) return found; }
            }
            return null;
        }

        private void EnsureControlVisible(Control c)
        {
            Control? parent = c.Parent;
            while (parent != null)
            {
                if (parent is TabPage tp && tp.Parent is TabControl tc) { tc.SelectedTab = tp; break; }
                parent = parent.Parent;
            }
        }

        private void ResetAllFields(Control container)
        {
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2TextBox tb) tb.Clear();
                else if (ctrl is Guna.UI2.WinForms.Guna2ComboBox cb) cb.SelectedIndex = -1;
                if (ctrl.HasChildren) ResetAllFields(ctrl);
            }
        }

        // --- GHOST EVENT HANDLERS (Required to satisfy the Designer) ---

        private void cancel_Btn_Click(object sender, EventArgs e) => ResetAllFields(this);

        private void close_Btn_EditMoto_Click(object sender, EventArgs e)
        {
            if (this.FindForm() is Form1 main) main.DisplayPage(new UC_Inventory());
        }

        private void motoYeartxt_Description_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel7_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel24_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e) { }
    }
}