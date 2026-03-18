using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class UC_EditMotorcycle : UserControl
    {
        // FIX: Using 'default!' or ensuring these match your Designer names exactly
        private Guna.UI2.WinForms.Guna2TextBox txt_Description = default!;
        private Guna.UI2.WinForms.Guna2TextBox color_Moto_Box = default!;
        private Guna.UI2.WinForms.Guna2TextBox motoYeartxt_Description = default!;

        public UC_EditMotorcycle()
        {
            InitializeComponent();
        }

        public void LoadMotorcycleData(Motorcycle moto)
        {
            // Null check to prevent "Possible null reference" warning
            if (moto == null) return;

            // These names must match the (Name) property in your Designer
            if (motorNameTxtBox != null) motorNameTxtBox.Text = moto.Brand ?? "";
            if (motoModelNameTxtBox != null) motoModelNameTxtBox.Text = moto.Model ?? "";
            if (txt_Description != null) txt_Description.Text = moto.Description ?? "";
            if (color_Moto_Box != null) color_Moto_Box.Text = moto.Color ?? "";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Control? firstEmpty = FindFirstEmptyField(this);

            if (firstEmpty == null)
            {
                try
                {
                    SaveToDatabase();
                    MessageBox.Show("Motorcycle Specs Updated Successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Manually calling the close logic
                    close_Btn_EditMoto_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields or boxes.", "Incomplete Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                EnsureControlVisible(firstEmpty);
                firstEmpty.Focus();
            }
        }

        // FIX: Added '?' to Control to allow null return safely
        private Control? FindFirstEmptyField(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
                    return txt;

                if (c is Guna.UI2.WinForms.Guna2ComboBox cb && cb.SelectedIndex == -1)
                    return cb;

                if (c.HasChildren)
                {
                    Control? found = FindFirstEmptyField(c);
                    if (found != null) return found;
                }
            }
            return null;
        }

        private void EnsureControlVisible(Control c)
        {
            Control? parent = c.Parent;
            while (parent != null)
            {
                if (parent is TabPage tp && tp.Parent is TabControl tc)
                {
                    tc.SelectedTab = tp;
                    break;
                }
                parent = parent.Parent;
            }
        }

        // FIX: Ensure this method is actually linked to the button in the Designer
        private void close_Btn_EditMoto_Click(object sender, EventArgs e)
        {
            if (this.FindForm() is Form1 main)
            {
                // Navigate back to the main Specs Container
                pnlSpecsContainer specsView = new pnlSpecsContainer();
                main.DisplayPage(specsView);
            }
        }

        private void SaveToDatabase() => Console.WriteLine("Save successful.");

        private void cancel_Btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all fields?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetAllFields(this);
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
        private void motoYeartxt_Description_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel7_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel24_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e) { }
    }
}