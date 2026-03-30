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

            btnAddMotorcycle.Click -= btnAdd_Click;
            btnAddMotorcycle.Click += btnAddMotorcycle_Click;

            Load += UC_AddMotorcycle_Load;
            SizeChanged += UC_AddMotorcycle_SizeChanged;
        }

        private void UC_AddMotorcycle_Load(object? sender, EventArgs e)
        {
            ApplyVisualPolish();
            cancel_Btn.Visible = false;
            close_Btn_EditMoto.Visible = false;
            ApplyResponsiveLayout();
        }

        private void UC_AddMotorcycle_SizeChanged(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            SuspendLayout();

            int leftOffset = GetHostSidebarOffset();
            int contentWidth = Math.Max(400, Width - leftOffset);

            guna2Panel1.Dock = DockStyle.None;
            guna2Panel1.Location = new Point(leftOffset, 0);
            guna2Panel1.Size = new Size(contentWidth, Height);

            guna2Panel1.BackColor = Color.FromArgb(248, 250, 252);
            guna2Panel2.Height = 78;
            guna2Panel3.Height = 78;
            tabControl.ItemSize = new Size(220, 46);
            tabPage1.Padding = new Padding(8);
            guna2Panel4.Dock = DockStyle.Fill;
            guna2Panel4.BackColor = Color.FromArgb(235, 235, 238);

            int right = guna2Panel3.ClientSize.Width - 12;
            btnAddMotorcycle.Left = right - btnAddMotorcycle.Width;

            int actionTop = Math.Max(28, (guna2Panel3.ClientSize.Height - btnAddMotorcycle.Height) / 2);
            btnAddMotorcycle.Top = actionTop;

            ApplyBasicInformationLayout();
            ApplyTechnicalTabLayout();
            ApplyReadableSizing();

            ResumeLayout(true);
        }

        private void ApplyVisualPolish()
        {
            BackColor = Color.FromArgb(248, 250, 252);

            // Remove stray debug/status marker
            lblStatus.Visible = false;
            lblStatus.Text = string.Empty;

            // Container styling
            guna2Panel1.BorderRadius = 12;
            guna2Panel1.FillColor = Color.FromArgb(248, 250, 252);

            guna2Panel2.FillColor = Color.FromArgb(30, 41, 59);
            guna2Panel3.FillColor = Color.FromArgb(30, 41, 59);

            guna2Panel4.BorderRadius = 10;
            guna2Panel4.BorderColor = Color.FromArgb(220, 224, 230);
            guna2Panel4.BorderThickness = 1;
            guna2Panel4.FillColor = Color.FromArgb(240, 242, 246);

            tabPage1.BackColor = Color.FromArgb(240, 242, 246);
            tabPage2.BackColor = Color.FromArgb(240, 242, 246);

            SetHtmlLabelsTransparent(this);

            // Soften corners of heavy controls
            pic_Motorcycle.BorderRadius = 12;
            txt_Description.BorderRadius = 14;

            initialStockNumUpDown.BorderRadius = 10;
            guna2NumericUpDown7.BorderRadius = 10;
            guna2NumericUpDown1.BorderRadius = 10;
            guna2NumericUpDown2.BorderRadius = 10;
            guna2NumericUpDown3.BorderRadius = 10;
            guna2NumericUpDown4.BorderRadius = 10;
            guna2NumericUpDown5.BorderRadius = 10;
            guna2NumericUpDown6.BorderRadius = 10;
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

        private void ApplyBasicInformationLayout()
        {
            int panelPadding = 12;
            int imageWidth = Math.Clamp((int)Math.Round(guna2Panel4.ClientSize.Width * 0.23), 190, 260);
            int gap = 14;

            int contentLeft = panelPadding + imageWidth + 20;
            int contentRight = guna2Panel4.ClientSize.Width - panelPadding;
            int contentWidth = Math.Max(360, contentRight - contentLeft);

            int colHalf = (contentWidth - gap) / 2;
            int colThird = (contentWidth - (gap * 2)) / 3;

            pic_Motorcycle.Location = new Point(panelPadding, 22);
            pic_Motorcycle.Size = new Size(imageWidth, 163);

            motorNameTxtBox.Location = new Point(contentLeft, 22);
            motorNameTxtBox.Width = colHalf;
            guna2HtmlLabel6.Location = new Point(contentLeft + 6, 5);

            motoModelNameTxtBox.Location = new Point(contentLeft + colHalf + gap, 22);
            motoModelNameTxtBox.Width = colHalf;
            guna2HtmlLabel7.Location = new Point(contentLeft + colHalf + gap + 6, 5);

            motoYeartxt_Description.Location = new Point(contentLeft, 86);
            motoYeartxt_Description.Width = colThird;
            guna2HtmlLabel8.Location = new Point(contentLeft + 6, 69);

            motoCateg.Location = new Point(contentLeft + colThird + gap, 86);
            motoCateg.Width = colThird;
            guna2HtmlLabel9.Location = new Point(contentLeft + colThird + gap + 6, 69);

            guna2NumericUpDown7.Location = new Point(contentLeft + (colThird * 2) + (gap * 2), 88);
            guna2NumericUpDown7.Width = colThird;
            guna2HtmlLabel10.Location = new Point(contentLeft + (colThird * 2) + (gap * 2) + 6, 69);

            color_Moto_Box.Location = new Point(contentLeft, 149);
            color_Moto_Box.Width = contentWidth;
            guna2HtmlLabel11.Location = new Point(contentLeft + 6, 132);

            txt_Description.Location = new Point(contentLeft, 211);
            txt_Description.Width = contentWidth;
            txt_Description.Height = Math.Max(72, guna2Panel4.ClientSize.Height - txt_Description.Top - 14);
            guna2HtmlLabel12.Location = new Point(contentLeft + 6, 192);

            initialStockNumUpDown.Location = new Point(panelPadding, 215);
            initialStockNumUpDown.Width = imageWidth;
            guna2HtmlLabel13.Location = new Point(panelPadding + 4, 192);
        }

        private void ApplyTechnicalTabLayout()
        {
            int left = 14;
            int gap = 12;
            int cols = 4;
            int totalWidth = tabPage2.ClientSize.Width - (left * 2);
            int col = Math.Max(180, (totalWidth - ((cols - 1) * gap)) / cols);

            int labelY1 = 36;
            int row1Y = 54;
            int labelY2 = 98;
            int row2Y = 116;
            int section2TitleY = 152;
            int separator2Y = 170;
            int labelY3 = 186;
            int row3Y = 204;
            int labelY4 = 248;
            int row4Y = 266;
            int labelY5 = 310;
            int row5Y = 328;
            int rowHeight = 38;

            int x0 = left;
            int x1 = x0 + col + gap;
            int x2 = x1 + col + gap;
            int x3 = x2 + col + gap;

            guna2Separator1.SetBounds(left, 26, totalWidth, 10);
            guna2Separator2.SetBounds(left, separator2Y, totalWidth, 10);

            guna2HtmlLabel15.Location = new Point(x0 + 4, labelY1);
            guna2HtmlLabel16.Location = new Point(x1 + 4, labelY1);
            guna2HtmlLabel17.Location = new Point(x2 + 4, labelY1);
            guna2HtmlLabel18.Location = new Point(x3 + 4, labelY1);

            guna2HtmlLabel19.Location = new Point(x0 + 4, labelY2);
            guna2HtmlLabel20.Location = new Point(x1 + 4, labelY2);
            guna2HtmlLabel21.Location = new Point(x2 + 4, labelY2);

            guna2HtmlLabel23.Location = new Point(x0 + 4, labelY3);
            guna2HtmlLabel24.Location = new Point(x1 + 4, labelY3);
            guna2HtmlLabel25.Location = new Point(x2 + 4, labelY3);
            guna2HtmlLabel26.Location = new Point(x3 + 4, labelY3);

            guna2HtmlLabel27.Location = new Point(x0 + 4, labelY4);
            guna2HtmlLabel28.Location = new Point(x2 + 4, labelY4);

            guna2HtmlLabel29.Location = new Point(x0 + 4, labelY5);
            guna2HtmlLabel30.Location = new Point(x1 + 4, labelY5);
            guna2HtmlLabel31.Location = new Point(x2 + 4, labelY5);
            guna2HtmlLabel32.Location = new Point(x3 + 4, labelY5);

            guna2HtmlLabel22.Location = new Point(20, section2TitleY);

            // Engine & Performance
            guna2TextBox1.SetBounds(x0, row1Y, col, rowHeight);
            guna2NumericUpDown1.SetBounds(x1, row1Y, col, rowHeight);
            guna2TextBox3.SetBounds(x2, row1Y, col, rowHeight);
            guna2TextBox4.SetBounds(x3, row1Y, col, rowHeight);

            guna2ComboBox2.SetBounds(x0, row2Y, col, rowHeight);
            guna2TextBox2.SetBounds(x1, row2Y, col, rowHeight);
            guna2TextBox5.SetBounds(x2, row2Y, col * 2 + gap, rowHeight);

            // Chassis & Dimensions
            guna2ComboBox3.SetBounds(x0, row3Y, col, rowHeight);
            guna2NumericUpDown2.SetBounds(x1, row3Y, col, rowHeight);
            guna2NumericUpDown3.SetBounds(x2, row3Y, col, rowHeight);
            guna2NumericUpDown4.SetBounds(x3, row3Y, col, rowHeight);

            guna2NumericUpDown5.SetBounds(x0, row4Y, col * 2 + gap, rowHeight);
            guna2NumericUpDown6.SetBounds(x2, row4Y, col * 2 + gap, rowHeight);

            guna2TextBox9.SetBounds(x0, row5Y, col, rowHeight);
            guna2TextBox10.SetBounds(x1, row5Y, col, rowHeight);
            guna2TextBox11.SetBounds(x2, row5Y, col, rowHeight);
            guna2TextBox12.SetBounds(x3, row5Y, col, rowHeight);

            tabPage2.AutoScrollMinSize = new Size(totalWidth, row5Y + rowHeight + 30);
        }

        private void ApplyReadableSizing()
        {
            SetControlFont(guna2HtmlLabel4, 30, true);
            SetControlFont(lblStatus, 8, false);

            btnAddMotorcycle.Text = "+ Add Motorcycle";
            btnAddMotorcycle.TextOffset = new Point(0, -1);
            SetControlFont(btnAddMotorcycle, 10, true);
            SetControlFont(motorNameTxtBox, 10, false);
            SetControlFont(motoModelNameTxtBox, 10, false);
            SetControlFont(motoYeartxt_Description, 10, false);
            SetControlFont(motoCateg, 10, false);
            SetControlFont(guna2NumericUpDown7, 10, false);
            SetControlFont(color_Moto_Box, 10, false);
            SetControlFont(initialStockNumUpDown, 10, false);
            SetControlFont(txt_Description, 10, false);

            foreach (Control c in tabPage2.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2HtmlLabel lbl)
                {
                    lbl.BackColor = Color.Transparent;
                    bool isSectionTitle = ReferenceEquals(lbl, guna2HtmlLabel14) || ReferenceEquals(lbl, guna2HtmlLabel22);
                    SetControlFont(lbl, isSectionTitle ? 12 : 8.8f, isSectionTitle);
                }
                else if (c is Guna.UI2.WinForms.Guna2TextBox tb)
                {
                    tb.BorderRadius = 12;
                    SetControlFont(tb, 10, false);
                }
                else if (c is Guna.UI2.WinForms.Guna2ComboBox cb)
                {
                    cb.BorderRadius = 12;
                    cb.ItemHeight = 30;
                    SetControlFont(cb, 10, false);
                }
                else if (c is Guna.UI2.WinForms.Guna2NumericUpDown num)
                {
                    num.BorderRadius = 10;
                    SetControlFont(num, 10, false);
                }
            }
        }

        private static void SetControlFont(Control control, float size, bool bold)
        {
            var style = bold ? FontStyle.Bold : FontStyle.Regular;
            control.Font = new Font("Segoe UI", size, style);
        }

        private static void SetHtmlLabelsTransparent(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2HtmlLabel lbl)
                {
                    lbl.BackColor = Color.Transparent;
                }

                if (control.HasChildren)
                {
                    SetHtmlLabelsTransparent(control);
                }
            }
        }

        // The Add/Save Button Logic
        private void btnAddMotorcycle_Click(object sender, EventArgs e)
        {
            Control? firstEmpty = FindFirstEmptyField(this);

            if (firstEmpty != null)
            {
                MessageBox.Show("Please fill all fields.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnsureControlVisible(firstEmpty);
                firstEmpty.Focus();
                return;
            }

            string brand = motorNameTxtBox.Text.Trim();
            string model = motoModelNameTxtBox.Text.Trim();
            string title = $"{brand} {model}".Trim();

            var input = new DatabaseManager.ProductInput
            {
                Title = title,
                ModelYear = motoYeartxt_Description.Text.Trim(),
                Category = string.IsNullOrWhiteSpace(motoCateg.Text) ? brand : motoCateg.Text.Trim(),
                Price = guna2NumericUpDown7.Value,
                Stock = (int)initialStockNumUpDown.Value,
                Description = txt_Description.Text.Trim(),
                MaxPower = guna2TextBox3.Text.Trim(),
                MaxTorque = guna2TextBox4.Text.Trim(),
                Transmission = guna2ComboBox2.Text.Trim(),
                FuelSystem = guna2TextBox2.Text.Trim(),
                Cooling = guna2TextBox5.Text.Trim(),
                FuelCapacity = guna2NumericUpDown2.Value.ToString("0.##"),
                SeatHeight = guna2NumericUpDown3.Value.ToString("0.##"),
                CurbWeight = guna2NumericUpDown4.Value.ToString("0.##"),
                GroundClearance = guna2NumericUpDown5.Value.ToString("0.##"),
                Wheelbase = guna2NumericUpDown6.Value.ToString("0.##"),
                BrakeSystem = guna2TextBox9.Text.Trim(),
                Suspension = $"Front: {guna2TextBox11.Text.Trim()} | Rear: {guna2TextBox12.Text.Trim()}",
                Colors = NormalizePipeList(color_Moto_Box.Text),
                Features = string.Empty
            };

            bool saved = DatabaseManager.AddProduct(input);
            if (!saved)
            {
                MessageBox.Show("Failed to save motorcycle. Please check database connection.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Motorcycle saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetAllFields(this);
        }

        private static string NormalizePipeList(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return string.Join("|", value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
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