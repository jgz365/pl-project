using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class UC_EditMotorcycle : UserControl
    {
        private Guna.UI2.WinForms.Guna2TextBox txt_Description = default!;
        private Guna.UI2.WinForms.Guna2TextBox color_Moto_Box = default!;
        private Guna.UI2.WinForms.Guna2TextBox motoYeartxt_Description = default!;

        private int currentProductId;
        private string currentProductTitle = string.Empty;

        public UC_EditMotorcycle()
        {
            InitializeComponent();
            SizeChanged += UC_EditMotorcycle_SizeChanged;
            ConfigureInventoryFocusedEditing();
            ApplyCompactInventoryLayout();
        }

        public UC_EditMotorcycle(DatabaseManager.CatalogProduct product) : this()
        {
            LoadMotorcycleData(product);
        }

        public void LoadMotorcycleData(DatabaseManager.CatalogProduct product)
        {
            if (product == null)
            {
                return;
            }

            currentProductId = product.Id;
            currentProductTitle = product.Title;

            string[] titleParts = product.Title.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string brand = titleParts.Length > 0 ? titleParts[0] : product.Title;
            string model = titleParts.Length > 1 ? titleParts[1] : product.Title;

            motorNameTxtBox.Text = brand;
            motoModelNameTxtBox.Text = model;

            var subParts = product.Sub.Split('•', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            motoCateg.Text = subParts.Length > 1 ? subParts[1] : string.Empty;

            initialStockNumUpDown.Value = TryParseStock(product.Stock);
            guna2NumericUpDown7.Value = TryParsePrice(product.Price);

            guna2HtmlLabel3.Text = $"Inventory update for {product.Title}";
        }

        private void ConfigureInventoryFocusedEditing()
        {
            guna2HtmlLabel4.Text = "Update Inventory";
            guna2HtmlLabel3.Text = "Stock and price maintenance";
            update_moto_Btn.Text = "Save Stock Update";

            tabControl.SelectedTab = tabPage1;
            tabPage2.Enabled = false;
            tabPage2.Parent = null;

            pic_Motorcycle.Visible = false;
            motorNameTxtBox.Visible = false;
            motoModelNameTxtBox.Visible = false;
            txt_Description.Visible = false;
            color_Moto_Box.Visible = false;
            motoYeartxt_Description.Visible = false;
            motoCateg.Visible = false;

            guna2HtmlLabel5.Visible = false;
            guna2HtmlLabel6.Visible = false;
            guna2HtmlLabel7.Visible = false;
            guna2HtmlLabel8.Visible = false;
            guna2HtmlLabel9.Visible = false;
            guna2HtmlLabel11.Visible = false;
            guna2HtmlLabel12.Visible = false;

            motorNameTxtBox.ReadOnly = true;
            motoModelNameTxtBox.ReadOnly = true;
            motoCateg.Enabled = false;

            initialStockNumUpDown.Enabled = true;
            guna2NumericUpDown7.Enabled = true;

            cancel_Btn.Visible = false;
            lblStatus.Visible = false;

            guna2HtmlLabel10.Text = "PRICE (₱)";
            guna2HtmlLabel13.Text = "INITIAL STOCK";
            guna2HtmlLabel10.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            guna2HtmlLabel13.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            guna2NumericUpDown7.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            initialStockNumUpDown.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            guna2NumericUpDown7.BorderRadius = 12;
            initialStockNumUpDown.BorderRadius = 12;
        }

        private void UC_EditMotorcycle_SizeChanged(object? sender, EventArgs e)
        {
            ApplyCompactInventoryLayout();
        }

        private void ApplyCompactInventoryLayout()
        {
            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            int leftOffset = GetHostSidebarOffset();
            int contentWidth = Math.Max(420, Width - leftOffset);

            guna2Panel1.Dock = DockStyle.None;
            guna2Panel1.Location = new Point(leftOffset, 0);
            guna2Panel1.Size = new Size(contentWidth, Height);

            guna2Panel2.Height = 92;
            guna2Panel3.Height = 92;
            tabControl.ItemSize = new Size(220, 44);

            int headerRight = guna2Panel3.ClientSize.Width - 18;
            close_Btn_EditMoto.Top = 47;
            close_Btn_EditMoto.Left = headerRight - close_Btn_EditMoto.Width;
            update_moto_Btn.Top = 47;
            update_moto_Btn.Left = close_Btn_EditMoto.Left - update_moto_Btn.Width - 10;

            tabPage1.Padding = new Padding(16, 12, 16, 12);
            guna2Panel4.Dock = DockStyle.Fill;

            int bodyWidth = guna2Panel4.ClientSize.Width;
            int fieldWidth = Math.Clamp((int)Math.Round(bodyWidth * 0.42), 280, 520);
            int fieldLeft = Math.Max(24, (bodyWidth - fieldWidth) / 2);

            guna2HtmlLabel10.Location = new Point(fieldLeft, 64);
            guna2NumericUpDown7.Location = new Point(fieldLeft, 90);
            guna2NumericUpDown7.Size = new Size(fieldWidth, 52);

            guna2HtmlLabel13.Location = new Point(fieldLeft, 178);
            initialStockNumUpDown.Location = new Point(fieldLeft, 204);
            initialStockNumUpDown.Size = new Size(fieldWidth, 52);
        }

        private int GetHostSidebarOffset()
        {
            Control? form = FindForm();
            if (form == null)
            {
                return 0;
            }

            Control? sidebar = form.Controls["pnlSidebar"];
            if (sidebar != null && sidebar.Visible)
            {
                return sidebar.Width;
            }

            return 0;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (currentProductId <= 0)
            {
                MessageBox.Show("No product selected for update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool updated = DatabaseManager.UpdateProductInventory(
                currentProductId,
                (int)initialStockNumUpDown.Value,
                guna2NumericUpDown7.Value);

            if (!updated)
            {
                MessageBox.Show("Failed to update inventory. Please check DB connection.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Inventory values updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            close_Btn_EditMoto_Click(sender, e);
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
            DatabaseManager.CatalogProduct? selected = null;
            if (currentProductId > 0)
            {
                selected = DatabaseManager.GetCatalogProducts().FirstOrDefault(p => p.Id == currentProductId);
            }

            if (this.FindForm() is Form1 main)
            {
                main.DisplayPage(selected != null ? new pnlSpecsContainer(selected) : new UC_Inventory());
                return;
            }

            if (this.FindForm() is Form2 manager)
            {
                manager.DisplayPage(selected != null ? new pnlSpecsContainer(selected) : new UC_Inventory());
            }
        }

        private void cancel_Btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all fields?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetAllFields(this);
            }
        }

        private static decimal TryParsePrice(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0m;
            }

            string cleaned = new string(value.Where(ch => char.IsDigit(ch) || ch == '.' || ch == '-').ToArray());
            return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal parsed)
                ? parsed
                : 0m;
        }

        private static decimal TryParseStock(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0m;
            }

            string digits = new string(value.TakeWhile(char.IsDigit).ToArray());
            return decimal.TryParse(digits, NumberStyles.Integer, CultureInfo.InvariantCulture, out decimal parsed)
                ? parsed
                : 0m;
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