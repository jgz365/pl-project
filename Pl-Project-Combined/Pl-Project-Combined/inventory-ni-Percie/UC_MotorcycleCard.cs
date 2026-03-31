using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    public partial class UC_MotorcycleCard : UserControl
    {
        private static readonly HttpClient ImageHttpClient = new();

        public UC_MotorcycleCard()
        {
            InitializeComponent();
            btnOptions.Click += BtnOptions_Click;
        }
        public event EventHandler? OnCardClick;
        public event EventHandler? OnOptionsClick;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ProductId { get; set; }

        // The "Automation" Method
        public void SetMotorcycleData(string model, string yearType, string price, string stock, Image? bikeImg, string status)
        {
            lblModel.Text = model;       // e.g. "Suzuki Raider R150"
            lblSubInfo.Text = yearType;      // e.g. "2024 • Underbone"
            lblPrice.Text = price;           // e.g. "₱119,900"
            guna2HtmlLabel1.Text = stock + " STOCK";
            if (bikeImg != null)
            {
                pictureBoxMoto.Image = bikeImg;         // The uploaded photo
            }
            chipStatus.Text = status;        // e.g. "PRE-ORDER"

            // Color Logic: Automate the status color based on text
            if (status == "PRE-ORDER")
            {
                chipStatus.FillColor = Color.FromArgb(191, 219, 254); // Light Blue
                chipStatus.ForeColor = Color.FromArgb(30, 64, 175);
            }
            else if (status == "AVAILABLE")
            {
                chipStatus.FillColor = Color.FromArgb(187, 247, 208); // Light Green
            }
        }

        public async void SetImageFromUrl(string? imageUrl)
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

                    var old = pictureBoxMoto.Image;
                    pictureBoxMoto.Image = imageCopy;
                    pictureBoxMoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (old != null && !ReferenceEquals(old, imageCopy))
                    {
                        old.Dispose();
                    }
                }));
            }
            catch
            {
                // Keep designer/default image if URL loading fails.
            }
        }

        private void UC_MotorcycleCard_Click(object sender, EventArgs e)
        {
            OnCardClick?.Invoke(this, EventArgs.Empty);
        }
        private void UC_MotorcycleCard_MouseEnter(object sender, EventArgs e)
        {
            // 1. Highlight the Border of the inner Guna2Panel
            pnlMainBody.BorderColor = Color.FromArgb(37, 99, 235); // Blue Accent

            // 2. Change the Model Label to Blue
            lblModel.ForeColor = Color.FromArgb(37, 99, 235);

            // 3. PictureBox "Lift" (Slide Y up by 5)
            pictureBoxMoto.Location = new Point(pictureBoxMoto.Location.X, 5);

            // 4. Deeper Shadow for "Lifted" effect
            shadowPnlMotoCard.ShadowDepth = 15;

            this.Cursor = Cursors.Hand;
        }

        private void UC_MotorcycleCard_MouseLeave(object sender, EventArgs e)
        {
            // Reset to original look
            pnlMainBody.BorderColor = Color.Transparent;
            lblModel.ForeColor = Color.FromArgb(27, 32, 46); // Original Dark Navy
            pictureBoxMoto.Location = new Point(pictureBoxMoto.Location.X, 10);
            shadowPnlMotoCard.ShadowDepth = 5;

            this.Cursor = Cursors.Default;
        }
        // Inside UC_MotorcycleCard.cs
        private void UniversalClick_Handler(object sender, EventArgs e)
        {
            // This 'shouts' to the Inventory UI that the card was clicked
            OnCardClick?.Invoke(this, EventArgs.Empty);
        }

        private void pnlMainBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnOptions_Click(object? sender, EventArgs e)
        {
            OnOptionsClick?.Invoke(this, EventArgs.Empty);
        }
    }
}

