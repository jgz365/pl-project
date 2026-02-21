using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class ck : Form
    {
        private List<Product> products = new List<Product>();
        // current maximum price selected by the price filter; int.MaxValue means no upper bound
        private int currentMaxPrice = int.MaxValue;

        public ck()
        {
            InitializeComponent();
            LoadDummyProducts();
            InitializeFilters();
            ApplyFilters();
        }

        private void InitializeFilters()
        {
            // wire radio buttons inside designer panels filterPanel1 (brand) and filterPanel2 (category)
            try
            {
                if (filterPanel1 != null)
                {
                    foreach (var rb in filterPanel1.Controls.OfType<Guna.UI2.WinForms.Guna2RadioButton>())
                        rb.CheckedChanged += FilterControlChanged;
                }
            }
            catch { }

            try
            {
                if (filterPanel2 != null)
                {
                    foreach (var rb in filterPanel2.Controls.OfType<Guna.UI2.WinForms.Guna2RadioButton>())
                        rb.CheckedChanged += FilterControlChanged;
                }
            }
            catch { }

            // wire combobox for price ranges
            try { filterBox.SelectedIndexChanged += (s, e) => ApplyFilters(); } catch { }

            // reset button
            try { button_filter.Click += (s, e) => ResetFilters(); } catch { }

            // default the combo box to 'All' if present
            try
            {
                if (filterBox != null && filterBox.Items.Count > 0)
                {
                    var idx = filterBox.Items.IndexOf("All");
                    filterBox.SelectedIndex = (idx >= 0) ? idx : filterBox.Items.Count - 1;
                }
            }
            catch { }
        }

        private void FilterControlChanged(object? sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2RadioButton rb)
            {
                if (!rb.Checked) return; // only react when a radio becomes checked
            }

            ApplyFilters();
        }

        private void ResetFilters()
        {
            // reset brand/category radio groups to 'All' (if present)
            try
            {
                if (filterPanel1 != null)
                {
                    var all = filterPanel1.Controls.OfType<Guna.UI2.WinForms.Guna2RadioButton>().FirstOrDefault(r => string.Equals(r.Text, "All", StringComparison.OrdinalIgnoreCase));
                    if (all != null) all.Checked = true;
                }
            }
            catch { }

            try
            {
                if (filterPanel2 != null)
                {
                    var all = filterPanel2.Controls.OfType<Guna.UI2.WinForms.Guna2RadioButton>().FirstOrDefault(r => string.Equals(r.Text, "All", StringComparison.OrdinalIgnoreCase));
                    if (all != null) all.Checked = true;
                }
            }
            catch { }

            // reset price combobox to All
            try
            {
                if (filterBox != null && filterBox.Items.Count > 0)
                {
                    var idx = filterBox.Items.IndexOf("All");
                    filterBox.SelectedIndex = (idx >= 0) ? idx : filterBox.Items.Count - 1;
                }
            }
            catch { }

            // clear price cap
            currentMaxPrice = int.MaxValue;

            ApplyFilters();
        }

        private void LoadDummyProducts()
        {
            products.Clear();

            products.Add(new Product
            {
                Category = "STANDARD",
                Brand = "Honda",
                Name = "Dream D",
                Year = "1949",
                Displacement = "98cc",
                Price = 494000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "STANDARD",
                Brand = "Honda",
                Name = "Honda CB150R",
                Year = "2024",
                Displacement = "150cc",
                Price = 150000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "SPORT BIKE",
                Brand = "Yamaha",
                Name = "Yamaha R15 V4",
                Year = "2024",
                Displacement = "155cc",
                Price = 185000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "SCOOTER",
                Brand = "Suzuki",
                Name = "Suzuki Burgman",
                Year = "2023",
                Displacement = "125cc",
                Price = 120000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "UNDERBONE",
                Brand = "Kawasaki",
                Name = "Kawasaki Barako",
                Year = "2024",
                Displacement = "110cc",
                Price = 95000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "SCOOTER",
                Brand = "Honda",
                Name = "Honda Click 125i",
                Year = "2025",
                Displacement = "125cc",
                Price = 78000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "SPORT BIKE",
                Brand = "Yamaha",
                Name = "Yamaha R15",
                Year = "2024",
                Displacement = "155cc",
                Price = 165000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "STANDARD",
                Brand = "Suzuki",
                Name = "Suzuki Gixxer 150",
                Year = "2023",
                Displacement = "150cc",
                Price = 95000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "UNDERBONE",
                Brand = "Honda",
                Name = "Honda XRM 125",
                Year = "2025",
                Displacement = "125cc",
                Price = 75000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "SPORT BIKE",
                Brand = "Kawasaki",
                Name = "Kawasaki Ninja 400",
                Year = "2024",
                Displacement = "399cc",
                Price = 315000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "SCOOTER",
                Brand = "Yamaha",
                Name = "Yamaha NMAX",
                Year = "2025",
                Displacement = "155cc",
                Price = 150000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "STANDARD",
                Brand = "Kawasaki",
                Name = "Kawasaki W175",
                Year = "2024",
                Displacement = "175cc",
                Price = 130000,
                Image = gpic1.Image
            });

            products.Add(new Product
            {
                Category = "UNDERBONE",
                Brand = "Suzuki",
                Name = "Suzuki Raider R150",
                Year = "2025",
                Displacement = "150cc",
                Price = 110000,
                Image = gpic1.Image
            });
        }

        private void PopulateProductTiles()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in products)
                flowLayoutPanel1.Controls.Add(CreateProductTile(product));
        }

        private void PopulateProductTiles(IEnumerable<Product> list)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in list)
                flowLayoutPanel1.Controls.Add(CreateProductTile(product));
        }

        private void ApplyFilters()
        {
            var query = products.AsEnumerable();

            // price filter from combo box (preferred)
            try
            {
                if (filterBox != null && filterBox.SelectedItem != null)
                {
                    var sel = filterBox.SelectedItem.ToString();
                    if (!string.Equals(sel, "All", StringComparison.OrdinalIgnoreCase))
                    {
                        // match numbers that may include thousand separators (commas)
                        var matches = Regex.Matches(sel ?? "", "[\\d,]+");
                        if (matches.Count == 1 && sel.Contains("+"))
                        {
                            var low = int.Parse(matches[0].Value.Replace(",", string.Empty));
                            query = query.Where(p => p.Price >= Convert.ToDecimal(low));
                        }
                        else if (matches.Count >= 2)
                        {
                            var low = int.Parse(matches[0].Value.Replace(",", string.Empty));
                            var high = int.Parse(matches[1].Value.Replace(",", string.Empty));
                            query = query.Where(p => p.Price >= Convert.ToDecimal(low) && p.Price <= Convert.ToDecimal(high));
                        }
                    }
                }
            }
            catch { }

            // brand
            string selectedBrand = null;
            try
            {
                if (filterPanel1 != null)
                {
                    var rb = filterPanel1.Controls.OfType<Guna.UI2.WinForms.Guna2RadioButton>().FirstOrDefault(r => r.Checked);
                    if (rb != null && !string.Equals(rb.Text, "All", StringComparison.OrdinalIgnoreCase))
                        selectedBrand = rb.Text;
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(selectedBrand))
                query = query.Where(p => string.Equals(p.Brand, selectedBrand, StringComparison.OrdinalIgnoreCase));

            // category
            string selectedCategory = null;
            try
            {
                if (filterPanel2 != null)
                {
                    var rb = filterPanel2.Controls.OfType<Guna.UI2.WinForms.Guna2RadioButton>().FirstOrDefault(r => r.Checked);
                    if (rb != null && !string.Equals(rb.Text, "All", StringComparison.OrdinalIgnoreCase))
                        selectedCategory = rb.Text;
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(selectedCategory))
                query = query.Where(p => string.Equals(p.Category, selectedCategory, StringComparison.OrdinalIgnoreCase));

            PopulateProductTiles(query);
        }

        private Guna.UI2.WinForms.Guna2ShadowPanel CreateProductTile(Product product)
        {
            var shadowPanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            shadowPanel.Size = new Size(325, 267);
            shadowPanel.Radius = 8;
            shadowPanel.ShadowColor = Color.Black;
            shadowPanel.ShadowDepth = 45;
            shadowPanel.Margin = new Padding(18, 15, 3, 3);

            var pictureBox = new Guna.UI2.WinForms.Guna2PictureBox();
            pictureBox.Image = product.Image;
            pictureBox.ImageRotate = 0F;
            pictureBox.Location = new Point(5, 11);
            pictureBox.Size = new Size(312, 136);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabStop = false;

            var labelCategory = new Label();
            labelCategory.Text = product.Category;
            labelCategory.Font = new Font("Franklin Gothic Demi", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCategory.ForeColor = Color.DarkGray;
            labelCategory.Location = new Point(9, 156);
            labelCategory.AutoSize = true;

            var labelName = new Label();
            labelName.Text = product.Name;
            labelName.Font = new Font("Franklin Gothic Demi", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelName.ForeColor = Color.Black;
            labelName.Location = new Point(9, 177);
            labelName.AutoSize = true;

            var labelSpec = new Label();
            labelSpec.Text = $"{product.Year} · {product.Displacement} · NxCC";
            labelSpec.Font = new Font("Franklin Gothic Demi", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSpec.ForeColor = Color.DarkGray;
            labelSpec.Location = new Point(9, 203);
            labelSpec.AutoSize = true;

            var labelPrice = new Label();
            labelPrice.Text = $"₱ {product.Price:N0}";
            labelPrice.Font = new Font("Franklin Gothic Demi", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPrice.ForeColor = Color.DarkGreen;
            labelPrice.Location = new Point(9, 229);
            labelPrice.AutoSize = true;

            var viewButton = new Guna.UI2.WinForms.Guna2Button();
            viewButton.Text = "View";
            viewButton.Font = new Font("Franklin Gothic Demi", 16F);
            viewButton.ForeColor = Color.White;
            viewButton.FillColor = Color.FromArgb(5, 150, 105);
            viewButton.BorderRadius = 8;
            viewButton.Location = new Point(230, 218);
            viewButton.Size = new Size(82, 38);
            viewButton.Click += (s, e) =>
            {
                // open detail form for this product
                try
                {
                    var detail = new kiosk_product_detail(product);
                    detail.Show();
                    this.Hide();
                }
                catch
                {
                    // fallback to default behavior
                    try { var detail = new kiosk_product_detail(); detail.Show(); this.Hide(); } catch { }
                }
            };

            shadowPanel.Controls.Add(pictureBox);
            shadowPanel.Controls.Add(labelCategory);
            shadowPanel.Controls.Add(labelName);
            shadowPanel.Controls.Add(labelSpec);
            shadowPanel.Controls.Add(labelPrice);
            shadowPanel.Controls.Add(viewButton);

            return shadowPanel;
        }

        private void ck_kiosk_clock_Tick(object sender, EventArgs e)
        {
            ck_clock_label.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void ckg_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_view_test_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }
    }
}
