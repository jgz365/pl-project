using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_5 : Form
    {
        // keep uploaded images in-memory for this session
        private Image validIDImage = null;
        private Image proofIncomeImage = null;
        private Image proofAddressImage = null;

        public loan_form_5()
        {
            InitializeComponent();
            WireUploadButtons();
            // hide designer remove buttons at runtime and wire their handlers
            try
            {
                if (validIDUploadButton_removeButton != null)
                {
                    validIDUploadButton_removeButton.Visible = false;
                    validIDUploadButton_removeButton.Click += (s, e) =>
                    {
                        try { validIDImage?.Dispose(); } catch { }
                        validIDImage = null;
                        try { validIDUploadButton_removeButton.Visible = false; } catch { }
                        try { validIDUploadButton.Text = "\u2193 Upload"; } catch { }
                        try { govLabel.Text = string.Empty; govLabel.ReadOnly = true; } catch { }
                    };
                }

                if (proofIncomeUploadButton_removeButton != null)
                {
                    proofIncomeUploadButton_removeButton.Visible = false;
                    proofIncomeUploadButton_removeButton.Click += (s, e) =>
                    {
                        try { proofIncomeImage?.Dispose(); } catch { }
                        proofIncomeImage = null;
                        try { proofIncomeUploadButton_removeButton.Visible = false; } catch { }
                        try { proofIncomeUploadButton.Text = "\u2193 Upload"; } catch { }
                        try { incomeLabel.Text = string.Empty; incomeLabel.ReadOnly = true; } catch { }
                    };
                }

                if (proofAddressUploadButton_removeButton != null)
                {
                    proofAddressUploadButton_removeButton.Visible = false;
                    proofAddressUploadButton_removeButton.Click += (s, e) =>
                    {
                        try { proofAddressImage?.Dispose(); } catch { }
                        proofAddressImage = null;
                        try { proofAddressUploadButton_removeButton.Visible = false; } catch { }
                        try { proofAddressUploadButton.Text = "\u2193 Upload"; } catch { }
                        try { addressLabel.Text = string.Empty; addressLabel.ReadOnly = true; } catch { }
                    };
                }

                // hide the designer visible remove placeholders at runtime
                try { guna2Button1.Visible = false; } catch { }
                try { incomeRemovebutton.Visible = false; } catch { }
                try { addressRemovebutton.Visible = false; } catch { }
            }
            catch { }
        }

        private void WireUploadButtons()
        {
            try { validIDUploadButton.Click += (s, e) => HandleUploadForTextBox(validIDUploadButton, govLabel, () => validIDImage, img => validIDImage = img, guna2Button1); } catch { }
            try { proofIncomeUploadButton.Click += (s, e) => HandleUploadForTextBox(proofIncomeUploadButton, incomeLabel, () => proofIncomeImage, img => proofIncomeImage = img, incomeRemovebutton); } catch { }
            try { proofAddressUploadButton.Click += (s, e) => HandleUploadForTextBox(proofAddressUploadButton, addressLabel, () => proofAddressImage, img => proofAddressImage = img, addressRemovebutton); } catch { }
        }

        private void HandleUploadForTextBox(Guna.UI2.WinForms.Guna2Button uploadButton, Guna.UI2.WinForms.Guna2TextBox targetBox, Func<Image> getImage, Action<Image> setImage, Guna.UI2.WinForms.Guna2Button removeButton)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.Multiselect = false;
            ofd.Title = "Select image";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var fs = System.IO.File.OpenRead(ofd.FileName);
                var img = Image.FromStream(fs);
                var previous = getImage();
                try { previous?.Dispose(); } catch { }
                setImage(new Bitmap(img));

                // set textbox to filename and make it read-only/display-only if provided
                var filename = System.IO.Path.GetFileName(ofd.FileName);
                try { if (targetBox != null) { targetBox.Text = filename; targetBox.ReadOnly = true; } } catch { }

                // if no textbox present (you removed it in Designer), try to update the panel fileLabel instead
                try
                {
                    if (targetBox == null)
                    {
                        var parent = uploadButton.Parent;
                        if (parent != null)
                        {
                            var fileLabelName = uploadButton.Name + "_fileLabel";
                            var fileBoxName = uploadButton.Name + "_fileBox";
                            var fileLabel = parent.Controls.Find(fileLabelName, true).FirstOrDefault() as Label;
                            var fileBox = parent.Controls.Find(fileBoxName, true).FirstOrDefault() as Panel;
                            if (fileLabel != null) fileLabel.Text = filename;
                            if (fileBox != null) fileBox.Visible = true;
                        }
                    }
                }
                catch { }

                // show remove button (was hidden by default)
                try { if (removeButton != null) removeButton.Visible = true; } catch { }

                // update upload button text
                try { uploadButton.Text = "Change"; } catch { }

                // wire remove action for this removeButton (ensure single handler)
                try
                {
                    if (removeButton != null)
                    {
                        removeButton.Click -= RemoveButton_Click_Generic;
                        removeButton.Click += RemoveButton_Click_Generic;
                        // store association via Tag so generic handler knows which textbox/image to clear
                        removeButton.Tag = new Tuple<Guna.UI2.WinForms.Guna2TextBox, Action<Image>, Guna.UI2.WinForms.Guna2Button>(targetBox, setImage, uploadButton);
                    }
                }
                catch { }
            }
            catch
            {
                MessageBox.Show("Failed to load image. Please select a valid image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveButton_Click_Generic(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btn && btn.Tag is Tuple<Guna.UI2.WinForms.Guna2TextBox, Action<Image>, Guna.UI2.WinForms.Guna2Button> info)
            {
                var textBox = info.Item1;
                var setImage = info.Item2;
                var uploadBtn = info.Item3;
                try { setImage(null); } catch { }
                try
                {
                    if (textBox != null)
                    {
                        textBox.Text = string.Empty;
                        textBox.ReadOnly = true;
                    }
                    else
                    {
                        // try to clear the panel fileLabel if textbox was removed in Designer
                        try
                        {
                            var parent = btn.Parent;
                            if (parent != null)
                            {
                                var fileLabelName = btn.Name.Replace("_removeButton", "_fileLabel");
                                var fileBoxName = btn.Name.Replace("_removeButton", "_fileBox");
                                var fileLabel = parent.Controls.Find(fileLabelName, true).FirstOrDefault() as Label;
                                var fileBox = parent.Controls.Find(fileBoxName, true).FirstOrDefault() as Panel;
                                if (fileLabel != null) fileLabel.Text = string.Empty;
                                if (fileBox != null) { fileBox.Visible = false; parent.Controls.Remove(fileBox); }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
                try { btn.Visible = false; } catch { }
                try { if (uploadBtn != null) uploadBtn.Text = "Upload"; } catch { }
            }
        }

        // Deprecated: dynamic file-box flow removed in favor of textbox-based labels.

        private void submitButton_Click(object sender, EventArgs e)
        {
            var loan6 = new loan_form_6();
            loan6.Show();
            this.Close();
        }

        private void button_loan_back_5_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var back4 = new loan_form_4();
            back4.Show();
            this.Close();
        }
    }
}
