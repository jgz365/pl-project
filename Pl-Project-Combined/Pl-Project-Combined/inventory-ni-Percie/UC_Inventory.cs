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
    public partial class UC_Inventory : UserControl
    {
        private bool showArchivedProducts;
        private string searchQuery = string.Empty;

        public UC_Inventory()
        {
            InitializeComponent();

            SizeChanged += UC_Inventory_SizeChanged;
            VisibleChanged += UC_Inventory_VisibleChanged;

            // This runs the logic as soon as the Inventory screen loads
            PopulateInventory();
            ApplyInventoryLayout();
            ApplyInventoryVisualPolish();
        }

        private void UC_Inventory_VisibleChanged(object? sender, EventArgs e)
        {
            if (Visible)
            {
                PopulateInventory();
            }
        }

        private void PopulateInventory()
        {
            // 1. Clear existing items
            flpInventory.Controls.Clear();

            var products = DatabaseManager.GetCatalogProducts(showArchivedProducts);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                string q = searchQuery.Trim();
                products = products
                    .Where(p => p.Title.Contains(q, StringComparison.OrdinalIgnoreCase)
                             || p.Sub.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            UpdateSummaryCards(products);

            foreach (var p in products)
            {
                var card = new UC_MotorcycleCard
                {
                    Margin = new Padding(5),
                    ProductId = p.Id
                };
                card.SetMotorcycleData(
                    p.Title,
                    p.Sub,
                    p.Price,
                    ParseStockLabel(p.Stock),
                    null,
                    ResolveStatusFromStock(p.Stock));
                card.SetImageFromUrl(p.ImageUrl);

                card.OnCardClick += (s, ev) =>
                {
                    if (this.FindForm() is Form1 main) main.DisplayPage(new pnlSpecsContainer(p));
                    if (this.FindForm() is Form2 manager) manager.DisplayPage(new pnlSpecsContainer(p));
                };

                card.OnOptionsClick += (s, ev) => ShowProductOptions(card, p);

                flpInventory.Controls.Add(card);
            }

            ApplyInventoryLayout();
        }

        private void ShowProductOptions(Control anchor, DatabaseManager.CatalogProduct product)
        {
            var menu = new ContextMenuStrip();
            bool archiveAction = !showArchivedProducts;

            var archiveItem = new ToolStripMenuItem(archiveAction ? "Archive Product" : "Restore Product");
            archiveItem.Click += (_, __) =>
            {
                bool ok = DatabaseManager.SetProductArchived(product.Id, archiveAction);
                if (!ok)
                {
                    MessageBox.Show("Unable to update product archive status.", "Archive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PopulateInventory();
            };

            menu.Items.Add(archiveItem);
            menu.Show(anchor, new Point(anchor.Width - 8, 30));
        }

        private static string ParseStockLabel(string stockText)
        {
            if (string.IsNullOrWhiteSpace(stockText))
            {
                return "0";
            }

            var digits = new string(stockText.TakeWhile(ch => char.IsDigit(ch)).ToArray());
            return string.IsNullOrWhiteSpace(digits) ? "0" : digits;
        }

        private static int ParseStockValue(string stockText)
        {
            return int.TryParse(ParseStockLabel(stockText), out int stock) ? stock : 0;
        }

        private static decimal ParsePriceValue(string priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText))
            {
                return 0m;
            }

            string cleaned = new string(priceText.Where(ch => char.IsDigit(ch) || ch == '.' || ch == '-').ToArray());
            return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value)
                ? value
                : 0m;
        }

        private void UpdateSummaryCards(IReadOnlyList<DatabaseManager.CatalogProduct> products)
        {
            int available = 0;
            int reserved = 0;
            int lowStock = 0;
            decimal totalValue = 0m;

            foreach (var p in products)
            {
                int stock = ParseStockValue(p.Stock);
                decimal price = ParsePriceValue(p.Price);
                totalValue += price * stock;

                if (stock <= 0)
                {
                    reserved++;
                }
                else if (stock <= 2)
                {
                    lowStock++;
                }
                else
                {
                    available++;
                }
            }

            guna2HtmlLabel8.Text = FormatCompactPeso(totalValue);
            guna2HtmlLabel10.Text = $"{available} UNITS";
            guna2HtmlLabel11.Text = $"{reserved} UNITS";
            guna2HtmlLabel12.Text = $"{lowStock} UNITS";
        }

        private static string FormatCompactPeso(decimal value)
        {
            if (value >= 1_000_000m)
            {
                return $"₱{value / 1_000_000m:0.#}M";
            }

            if (value >= 1_000m)
            {
                return $"₱{value / 1_000m:0.#}K";
            }

            return $"₱{value:0}";
        }

        private static string ResolveStatusFromStock(string stockText)
        {
            if (!int.TryParse(ParseStockLabel(stockText), out int stock))
            {
                return "AVAILABLE";
            }

            if (stock <= 0) return "PRE-ORDER";
            if (stock <= 2) return "LIMITED";
            return "AVAILABLE";
        }

        private void UC_Inventory_SizeChanged(object? sender, EventArgs e)
        {
            ApplyInventoryLayout();
        }

        private void ApplyInventoryLayout()
        {
            if (Width <= 0 || Height <= 0 || flpInventory == null || pnlGridContainer == null)
            {
                return;
            }

            SuspendLayout();

            bool isFullHdLike = Width >= 1800 && Height >= 950;

            pnlHeaderGroup.Height = isFullHdLike ? 185 : 159;
            search_inventory_pnl.Height = isFullHdLike ? 108 : 101;

            summarycardpnl.Left = 12;
            summarycardpnl.Width = Math.Max(620, pnlHeaderGroup.ClientSize.Width - 24);
            summarycardpnl.Top = Math.Max(74, pnlHeaderGroup.Height - summarycardpnl.Height - 10);
            summarycardpnl.Height = isFullHdLike ? 90 : 82;

            int searchTop = Math.Max(50, search_inventory_pnl.ClientSize.Height - txt_Search.Height - 8);
            txt_Search.Top = searchTop;

            int right = search_inventory_pnl.ClientSize.Width - 16;
            guna2Button9.Left = right - guna2Button9.Width;
            guna2Button9.Top = searchTop + ((txt_Search.Height - guna2Button9.Height) / 2);

            guna2Button10.Left = guna2Button9.Left - guna2Button10.Width - 8;
            guna2Button10.Top = guna2Button9.Top;

            guna2Button8.Left = guna2Button10.Left - guna2Button8.Width - 10;
            guna2Button8.Top = searchTop + ((txt_Search.Height - guna2Button8.Height) / 2);
            guna2Button8.Size = new Size(34, 34);
            guna2Button8.ImageSize = new Size(18, 18);

            txt_Search.Left = 24;
            txt_Search.Width = Math.Max(280, guna2Button8.Left - txt_Search.Left - 12);

            flpInventory.AutoSize = false;
            flpInventory.WrapContents = true;
            flpInventory.Padding = isFullHdLike ? new Padding(18, 10, 18, 10) : new Padding(14, 8, 14, 8);

            int cardCount = flpInventory.Controls.Count;
            if (cardCount > 0)
            {
                int available = flpInventory.ClientSize.Width - flpInventory.Padding.Left - flpInventory.Padding.Right;
                int cardsPerRow = available >= 1500 ? 4 : available >= 980 ? 3 : 2;
                int horizontalGap = 14;
                int cardWidth = Math.Clamp((available - ((cardsPerRow - 1) * horizontalGap)) / cardsPerRow, 250, 420);

                foreach (Control control in flpInventory.Controls)
                {
                    if (control is UC_MotorcycleCard card)
                    {
                        card.Margin = new Padding(7);
                        card.Width = cardWidth;
                    }
                }
            }

            ApplySummaryIconLayout(isFullHdLike);

            ResumeLayout(true);
        }

        private void ApplySummaryIconLayout(bool isFullHdLike)
        {
            int iconSize = isFullHdLike ? 34 : 30;
            int iconFont = isFullHdLike ? 11 : 9;

            LayoutSummaryIcon(guna2ShadowPanel7, guna2Button6, iconSize, iconFont);
            LayoutSummaryIcon(guna2ShadowPanel1, guna2Button1, iconSize, iconFont);
            LayoutSummaryIcon(guna2ShadowPanel2, guna2Button4, iconSize, iconFont);
            LayoutSummaryIcon(guna2ShadowPanel3, guna2Button5, iconSize, iconFont);
        }

        private static void LayoutSummaryIcon(Guna.UI2.WinForms.Guna2ShadowPanel host, Guna.UI2.WinForms.Guna2Button iconButton, int size, int fontSize)
        {
            iconButton.Size = new Size(size, size);
            iconButton.BorderRadius = Math.Max(8, size / 4);
            iconButton.Font = new Font("Segoe UI Emoji", fontSize, FontStyle.Regular);
            iconButton.TextAlign = HorizontalAlignment.Center;
            iconButton.TextOffset = Point.Empty;

            int rightPadding = 14;
            iconButton.Left = Math.Max(8, host.ClientSize.Width - iconButton.Width - rightPadding);
            iconButton.Top = Math.Max(8, (host.ClientSize.Height - iconButton.Height) / 2);
        }

        private void ApplyInventoryVisualPolish()
        {
            BackColor = Color.FromArgb(248, 250, 252);
            pnlHeaderGroup.BackColor = BackColor;
            search_inventory_pnl.BackColor = BackColor;
            flpInventory.BackColor = BackColor;
            pnlGridContainer.FillColor = BackColor;

            pnlSidebarMaster.Visible = false;
            pnlSidebarMaster.Width = 0;

            invenBtn.Visible = false;
            guna2Button3.Visible = false;
            guna2Button7.Visible = false;
            guna2Button10.Text = showArchivedProducts ? "Back to Inventory" : "Archive";
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
            ApplyInventoryLayout();
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
            searchQuery = txt_Search.Text;
            PopulateInventory();
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
                return;
            }

            if (this.FindForm() is Form2 manager)
            {
                manager.DisplayPage(new UC_AddMotorcycle());
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
            showArchivedProducts = !showArchivedProducts;
            ApplyInventoryVisualPolish();
            PopulateInventory();
        }

        private void invenBtn_Click(object sender, EventArgs e)
        {
            showArchivedProducts = false;
            ApplyInventoryVisualPolish();
            PopulateInventory();
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