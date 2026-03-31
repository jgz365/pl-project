using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class pnlSpecsContainer : UserControl
    {
        private static readonly HttpClient ImageHttpClient = new();
        private readonly DatabaseManager.CatalogProduct? product;

        public pnlSpecsContainer()
        {
            InitializeComponent();

            // Force the control to support transparency
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Set the dimming color: 140 is the 'see-through' level
            this.BackColor = Color.FromArgb(140, 27, 32, 46);

            SizeChanged += PnlSpecsContainer_SizeChanged;
        }

        public pnlSpecsContainer(DatabaseManager.CatalogProduct product) : this()
        {
            this.product = product;
        }
        private void pnlSpecsContainer_Load(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
            BindProductDetails();
        }



        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void PnlSpecsContainer_SizeChanged(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            int leftOffset = 0;
            Control? form = FindForm();
            if (form != null)
            {
                Control? sidebar = form.Controls["pnlSidebar"];
                if (sidebar != null && sidebar.Visible)
                {
                    leftOffset = sidebar.Width;
                }
            }

            int usableWidth = Math.Max(400, Width - leftOffset);
            int modalWidth = Math.Clamp((int)Math.Round(usableWidth * 0.88), 980, 1500);
            int modalHeight = Math.Clamp((int)Math.Round(Height * 0.86), 620, 900);

            actualPnl.Size = new Size(modalWidth, modalHeight);
            actualPnl.Location = new Point(
                leftOffset + Math.Max(0, (usableWidth - modalWidth) / 2),
                Math.Max(0, (Height - modalHeight) / 2));

            int imageWidth = Math.Clamp((int)Math.Round(modalWidth * 0.36), 360, 560);
            guna2PictureBox1.Location = new Point(8, 0);
            guna2PictureBox1.Size = new Size(imageWidth, modalHeight);

            int detailsLeft = imageWidth + 8;
            pnDimmer.Location = new Point(detailsLeft, 4);
            pnDimmer.Size = new Size(modalWidth - detailsLeft - 8, modalHeight - 8);

            guna2Panel1.Location = new Point(18, 147);
            guna2Panel1.Size = new Size(pnDimmer.Width - 36, pnDimmer.Height - 156);

            flowLayoutPanel1.Location = new Point(6, 7);
            flowLayoutPanel1.Size = new Size(guna2Panel1.Width - 12, guna2Panel1.Height - 14);
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.AutoSize = false;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;

            LayoutSpecificationCards();
            ApplyTypographyLayout();

            btnClose.Top = 113;
            guna2Button3.Top = 113;
            btnClose.Left = Math.Max(220, pnDimmer.Width - 225);
            guna2Button3.Left = btnClose.Right + 6;
        }

        private void LayoutSpecificationCards()
        {
            var specCards = new[]
            {
                guna2Panel3, guna2Panel5, guna2Panel4,
                guna2Panel6, guna2Panel7, guna2Panel8,
                guna2Panel9, guna2Panel10, guna2Panel11,
                guna2Panel12, guna2Panel13, guna2Panel14
            };

            int contentWidth = Math.Max(320, flowLayoutPanel1.ClientSize.Width - 10);
            int cols = contentWidth >= 820 ? 3 : 2;
            int gap = 8;
            int cardWidth = Math.Max(190, (contentWidth - (gap * (cols + 1))) / cols);

            foreach (var card in specCards)
            {
                card.Width = cardWidth;
                card.Height = 74;
                card.Margin = new Padding(gap / 2);
            }

            guna2Panel15.Width = Math.Max(260, contentWidth - gap);
            guna2Panel15.Margin = new Padding(gap / 2, 6, gap / 2, 6);
            guna2Panel16.Width = Math.Max(240, guna2Panel15.Width - 12);

            guna2HtmlLabel3.MaximumSize = new Size(Math.Max(200, guna2Panel16.Width - 26), 0);
            guna2HtmlLabel3.AutoSize = true;

            ApplySpecValueLabelSizing();
        }

        private void ApplyTypographyLayout()
        {
            guna2HtmlLabel43.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            guna2HtmlLabel44.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            guna2HtmlLabel46.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            guna2HtmlLabel47.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            guna2HtmlLabel48.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            guna2Button1.Font = new Font("Segoe UI Semibold", 8.75F, FontStyle.Bold);

            guna2Button1.Size = new Size(96, 26);
            guna2Button1.BorderRadius = 8;
            guna2HtmlLabel45.Location = new Point(guna2Button1.Right + 8, guna2Button1.Top + 2);

            guna2HtmlLabel47.Location = new Point(guna2HtmlLabel46.Left + 6, guna2HtmlLabel46.Bottom - 2);
            guna2HtmlLabel47.ForeColor = Color.FromArgb(107, 114, 128);

            NormalizeSpecIcons();

            LayoutSpecCardText(guna2Panel3, guna2HtmlLabel16, guna2HtmlLabel17, guna2HtmlLabel18);
            LayoutSpecCardText(guna2Panel5, guna2HtmlLabel15, guna2HtmlLabel30, guna2HtmlLabel19);
            LayoutSpecCardText(guna2Panel4, guna2HtmlLabel14, guna2HtmlLabel31, guna2HtmlLabel20);
            LayoutSpecCardText(guna2Panel6, guna2HtmlLabel11, guna2HtmlLabel32, guna2HtmlLabel21);
            LayoutSpecCardText(guna2Panel7, guna2HtmlLabel12, guna2HtmlLabel33, guna2HtmlLabel22);
            LayoutSpecCardText(guna2Panel8, guna2HtmlLabel13, guna2HtmlLabel34, guna2HtmlLabel23);
            LayoutSpecCardText(guna2Panel9, guna2HtmlLabel10, guna2HtmlLabel35, guna2HtmlLabel26);
            LayoutSpecCardText(guna2Panel10, guna2HtmlLabel9, guna2HtmlLabel36, guna2HtmlLabel25);
            LayoutSpecCardText(guna2Panel11, guna2HtmlLabel8, guna2HtmlLabel37, guna2HtmlLabel24);
            LayoutSpecCardText(guna2Panel12, guna2HtmlLabel5, guna2HtmlLabel40, guna2HtmlLabel29);
            LayoutSpecCardText(guna2Panel13, guna2HtmlLabel6, guna2HtmlLabel39, guna2HtmlLabel28);
            LayoutSpecCardText(guna2Panel14, guna2HtmlLabel7, guna2HtmlLabel38, guna2HtmlLabel27);

            guna2HtmlLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2HtmlLabel3.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            guna2HtmlLabel3.ForeColor = Color.Gainsboro;
            guna2HtmlLabel3.Location = new Point(21, 34);
            guna2HtmlLabel3.MaximumSize = new Size(Math.Max(220, guna2Panel16.Width - 36), 0);
            guna2HtmlLabel4.Visible = false;
        }

        private void NormalizeSpecIcons()
        {
            guna2HtmlLabel18.Text = "⚡";
            guna2HtmlLabel19.Text = "◈";
            guna2HtmlLabel20.Text = "○";
            guna2HtmlLabel21.Text = "⛽";
            guna2HtmlLabel22.Text = "❄";
            guna2HtmlLabel23.Text = "⛽";
            guna2HtmlLabel24.Text = "↕";
            guna2HtmlLabel25.Text = "⚖";
            guna2HtmlLabel26.Text = "↕";
            guna2HtmlLabel27.Text = "≋";
            guna2HtmlLabel28.Text = "◉";
            guna2HtmlLabel29.Text = "↔";

            var iconLabels = new[]
            {
                guna2HtmlLabel18, guna2HtmlLabel19, guna2HtmlLabel20, guna2HtmlLabel21,
                guna2HtmlLabel22, guna2HtmlLabel23, guna2HtmlLabel24, guna2HtmlLabel25,
                guna2HtmlLabel26, guna2HtmlLabel27, guna2HtmlLabel28, guna2HtmlLabel29
            };

            foreach (var icon in iconLabels)
            {
                icon.Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular);
                icon.AutoSize = false;
                icon.Size = new Size(14, 18);
            }
        }

        private static void LayoutSpecCardText(Control panel, Guna.UI2.WinForms.Guna2HtmlLabel titleLabel, Guna.UI2.WinForms.Guna2HtmlLabel valueLabel, Guna.UI2.WinForms.Guna2HtmlLabel? iconLabel)
        {
            titleLabel.AutoSize = false;
            titleLabel.Font = new Font("Segoe UI Semibold", 7.5F, FontStyle.Bold);
            titleLabel.Location = new Point(8, 6);
            titleLabel.Size = new Size(Math.Max(80, panel.Width - 16), 16);

            int valueLeft = 8;
            if (iconLabel != null)
            {
                iconLabel.AutoSize = false;
                iconLabel.Font = new Font("Segoe UI Emoji", 8.5F, FontStyle.Regular);
                iconLabel.Location = new Point(8, 30);
                iconLabel.Size = new Size(16, 22);
                valueLeft = 24;
            }

            valueLabel.AutoSize = false;
            valueLabel.Font = new Font("Segoe UI", 8.7F, FontStyle.Bold);
            valueLabel.Location = new Point(valueLeft, 31);
            valueLabel.Size = new Size(Math.Max(80, panel.Width - valueLeft - 8), 32);
        }

        private void ApplySpecValueLabelSizing()
        {
            ApplyValueLabelSizing(guna2Panel3, guna2HtmlLabel17, 24);
            ApplyValueLabelSizing(guna2Panel5, guna2HtmlLabel30, 26);
            ApplyValueLabelSizing(guna2Panel4, guna2HtmlLabel31, 22);
            ApplyValueLabelSizing(guna2Panel6, guna2HtmlLabel32, 22);
            ApplyValueLabelSizing(guna2Panel7, guna2HtmlLabel33, 32);
            ApplyValueLabelSizing(guna2Panel8, guna2HtmlLabel34, 24);
            ApplyValueLabelSizing(guna2Panel9, guna2HtmlLabel35, 24);
            ApplyValueLabelSizing(guna2Panel10, guna2HtmlLabel36, 24);
            ApplyValueLabelSizing(guna2Panel11, guna2HtmlLabel37, 24);
            ApplyValueLabelSizing(guna2Panel12, guna2HtmlLabel40, 24);
            ApplyValueLabelSizing(guna2Panel13, guna2HtmlLabel39, 24);
            ApplyValueLabelSizing(guna2Panel14, guna2HtmlLabel38, 24);
        }

        private static void ApplyValueLabelSizing(Control panel, Guna.UI2.WinForms.Guna2HtmlLabel valueLabel, int left)
        {
            valueLabel.MaximumSize = new Size(Math.Max(110, panel.Width - left - 6), 0);
            valueLabel.AutoSize = true;
        }

        private void BindProductDetails()
        {
            if (product == null)
            {
                return;
            }

            guna2HtmlLabel43.Text = product.Title.ToUpperInvariant();
            guna2HtmlLabel44.Text = product.Sub;
            guna2HtmlLabel46.Text = product.Price;
            guna2HtmlLabel45.Text = ParseStockDisplay(product.Stock);

            string status = ResolveStatusFromStock(product.Stock);
            guna2Button1.Text = status;
            ApplyStatusColor(status);

            guna2HtmlLabel17.Text = " " + product.MaxPower;
            guna2HtmlLabel30.Text = product.MaxTorque;
            guna2HtmlLabel31.Text = string.IsNullOrWhiteSpace(product.Transmission) ? "N/A" : product.Transmission;
            guna2HtmlLabel32.Text = product.FuelSystem;
            guna2HtmlLabel33.Text = product.Cooling;
            guna2HtmlLabel34.Text = product.FuelCapacity;
            guna2HtmlLabel35.Text = product.SeatHeight;
            guna2HtmlLabel36.Text = product.CurbWeight;
            guna2HtmlLabel37.Text = product.GroundClearance;
            guna2HtmlLabel40.Text = product.Wheelbase;
            guna2HtmlLabel39.Text = product.BrakeSystem;
            guna2HtmlLabel38.Text = product.Suspension;

            string description = string.IsNullOrWhiteSpace(product.Desc)
                ? "No description available."
                : product.Desc.Trim();
            guna2HtmlLabel3.Text = description;
            guna2HtmlLabel4.Text = string.Empty;
            guna2HtmlLabel3.MaximumSize = new Size(Math.Max(200, guna2Panel16.Width - 26), 0);

            _ = LoadProductImageFromUrlAsync(product.ImageUrl);
        }

        private async System.Threading.Tasks.Task LoadProductImageFromUrlAsync(string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return;
            }

            try
            {
                byte[] bytes = await ImageHttpClient.GetByteArrayAsync(imageUrl.Trim());
                using var ms = new MemoryStream(bytes);
                using var source = Image.FromStream(ms);
                var imageCopy = new Bitmap(source);

                if (IsDisposed)
                {
                    imageCopy.Dispose();
                    return;
                }

                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed)
                    {
                        imageCopy.Dispose();
                        return;
                    }

                    var old = guna2PictureBox1.Image;
                    guna2PictureBox1.Image = imageCopy;
                    guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (old != null && !ReferenceEquals(old, imageCopy))
                    {
                        old.Dispose();
                    }
                }));
            }
            catch
            {
                if (product != null)
                {
                    _ = LoadProductImageFromUrlAsync(BuildFallbackImageUrl(product.Title));
                }
            }
        }

        private static string BuildFallbackImageUrl(string title)
        {
            string seed = string.IsNullOrWhiteSpace(title)
                ? "motorcycle-default"
                : new string(title
                    .ToLowerInvariant()
                    .Select(ch => char.IsLetterOrDigit(ch) ? ch : '-')
                    .ToArray())
                    .Trim('-');

            if (string.IsNullOrWhiteSpace(seed))
            {
                seed = "motorcycle-default";
            }

            return $"https://picsum.photos/seed/{seed}/900/500";
        }

        private static string ParseStockDisplay(string stockText)
        {
            if (string.IsNullOrWhiteSpace(stockText))
            {
                return "0 STOCKS";
            }

            var digits = new string(stockText.TakeWhile(char.IsDigit).ToArray());
            return string.IsNullOrWhiteSpace(digits) ? "0 STOCKS" : $"{digits} STOCKS";
        }

        private static string ResolveStatusFromStock(string stockText)
        {
            var digits = new string(stockText.TakeWhile(char.IsDigit).ToArray());
            int stock = int.TryParse(digits, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n) ? n : 0;
            if (stock <= 0) return "PRE-ORDER";
            if (stock <= 2) return "LIMITED";
            return "AVAILABLE";
        }

        private void ApplyStatusColor(string status)
        {
            if (status == "PRE-ORDER")
            {
                guna2Button1.FillColor = Color.FromArgb(191, 219, 254);
                guna2Button1.ForeColor = Color.FromArgb(30, 64, 175);
                return;
            }

            if (status == "LIMITED")
            {
                guna2Button1.FillColor = Color.FromArgb(254, 226, 226);
                guna2Button1.ForeColor = Color.FromArgb(185, 28, 28);
                return;
            }

            guna2Button1.FillColor = Color.FromArgb(187, 247, 208);
            guna2Button1.ForeColor = Color.FromArgb(22, 101, 52);
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
            UC_EditMotorcycle editPage = product != null
                ? new UC_EditMotorcycle(product)
                : new UC_EditMotorcycle();

            // 2. Access the Main Form (where your pnl_Container is located)
            // We assume your main container is on Form1 or MainForm
            if (this.FindForm() is Form1 main)
            {
                // Use the switcher method we discussed earlier
                main.DisplayPage(editPage);
                return;
            }

            if (this.FindForm() is Form2 manager)
            {
                manager.DisplayPage(editPage);
            }

        }
    }
}

