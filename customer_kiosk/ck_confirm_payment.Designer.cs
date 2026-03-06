namespace customer_kiosk
{
    partial class ck_confirm_payment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ck_confirm_payment));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            kiosk_prod_panel = new Guna.UI2.WinForms.Guna2Panel();
            ckg_back = new Guna.UI2.WinForms.Guna2ImageButton();
            label10 = new Label();
            payment_form = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            customerInfoLabel = new Label();
            fullNameLabel = new Label();
            fullNameTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            mobileNumberLabel = new Label();
            mobileNumberTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            requiredDocsLabel = new Label();
            validIdCheckBox = new CheckBox();
            proofAddressCheckBox = new CheckBox();
            tinNumberCheckBox = new CheckBox();
            confirmButton = new Guna.UI2.WinForms.Guna2Button();
            guna2ShadowPanel3 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            instantOwnershipLabel = new Label();
            instantOwnershipText = new Label();
            purchaseSummaryLabel = new Label();
            productPanel = new Guna.UI2.WinForms.Guna2Panel();
            productImage = new PictureBox();
            productNameLabel = new Label();
            productDetailsLabel = new Label();
            unitPriceLabel = new Label();
            totalDueValue = new Label();
            unitPriceValue = new Label();
            totalDueLabel = new Label();
            processingFeeLabel = new Label();
            processingFeeValue = new Label();
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            kiosk_prod_panel.SuspendLayout();
            guna2ShadowPanel3.SuspendLayout();
            guna2ShadowPanel2.SuspendLayout();
            productPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productImage).BeginInit();
            guna2ShadowPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // kiosk_prod_panel
            // 
            kiosk_prod_panel.BackColor = Color.White;
            kiosk_prod_panel.BorderColor = Color.Black;
            kiosk_prod_panel.Controls.Add(ckg_back);
            kiosk_prod_panel.Controls.Add(label10);
            kiosk_prod_panel.CustomizableEdges = customizableEdges2;
            kiosk_prod_panel.Location = new Point(-3, 1);
            kiosk_prod_panel.Name = "kiosk_prod_panel";
            kiosk_prod_panel.ShadowDecoration.CustomizableEdges = customizableEdges3;
            kiosk_prod_panel.Size = new Size(1279, 87);
            kiosk_prod_panel.TabIndex = 4;
            // 
            // ckg_back
            // 
            ckg_back.Image = (Image)resources.GetObject("ckg_back.Image");
            ckg_back.ImageOffset = new Point(0, 0);
            ckg_back.ImageRotate = 0F;
            ckg_back.ImageSize = new Size(48, 48);
            ckg_back.Location = new Point(3, 3);
            ckg_back.Name = "ckg_back";
            ckg_back.ShadowDecoration.CustomizableEdges = customizableEdges1;
            ckg_back.Size = new Size(90, 81);
            ckg_back.TabIndex = 0;
            ckg_back.Click += ckg_back_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Franklin Gothic Demi", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(129, 31);
            label10.Name = "label10";
            label10.Size = new Size(325, 26);
            label10.TabIndex = 22;
            label10.Text = "CASH PURCHASE CONFIRMATION";
            // 
            // payment_form
            // 
            payment_form.BorderRadius = 12;
            payment_form.ContainerControl = this;
            payment_form.DockIndicatorTransparencyValue = 0.6D;
            payment_form.ResizeForm = false;
            payment_form.TransparentWhileDrag = true;
            // 
            // customerInfoLabel
            // 
            customerInfoLabel.AutoSize = true;
            customerInfoLabel.BackColor = Color.Transparent;
            customerInfoLabel.Font = new Font("Arial", 11F, FontStyle.Bold);
            customerInfoLabel.ForeColor = Color.Black;
            customerInfoLabel.Location = new Point(14, 22);
            customerInfoLabel.Name = "customerInfoLabel";
            customerInfoLabel.Size = new Size(161, 18);
            customerInfoLabel.TabIndex = 0;
            customerInfoLabel.Text = "Customer Information";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.BackColor = Color.Transparent;
            fullNameLabel.Font = new Font("Arial", 9F);
            fullNameLabel.ForeColor = Color.Gray;
            fullNameLabel.Location = new Point(14, 57);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new Size(64, 15);
            fullNameLabel.TabIndex = 1;
            fullNameLabel.Text = "Full Name";
            // 
            // fullNameTextBox
            // 
            fullNameTextBox.BackColor = Color.Transparent;
            fullNameTextBox.BorderRadius = 5;
            fullNameTextBox.CustomizableEdges = customizableEdges6;
            fullNameTextBox.DefaultText = "";
            fullNameTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            fullNameTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            fullNameTextBox.DisabledState.ForeColor = Color.FromArgb(166, 166, 166);
            fullNameTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(166, 166, 166);
            fullNameTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            fullNameTextBox.Font = new Font("Segoe UI", 9F);
            fullNameTextBox.ForeColor = Color.Black;
            fullNameTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            fullNameTextBox.Location = new Point(14, 77);
            fullNameTextBox.Margin = new Padding(3, 4, 3, 4);
            fullNameTextBox.Name = "fullNameTextBox";
            fullNameTextBox.PlaceholderText = "Juan Dela Cruz";
            fullNameTextBox.SelectedText = "";
            fullNameTextBox.ShadowDecoration.CustomizableEdges = customizableEdges7;
            fullNameTextBox.Size = new Size(370, 35);
            fullNameTextBox.TabIndex = 2;
            // 
            // mobileNumberLabel
            // 
            mobileNumberLabel.AutoSize = true;
            mobileNumberLabel.BackColor = Color.Transparent;
            mobileNumberLabel.Font = new Font("Arial", 9F);
            mobileNumberLabel.ForeColor = Color.Gray;
            mobileNumberLabel.Location = new Point(14, 122);
            mobileNumberLabel.Name = "mobileNumberLabel";
            mobileNumberLabel.Size = new Size(91, 15);
            mobileNumberLabel.TabIndex = 3;
            mobileNumberLabel.Text = "Mobile Number";
            // 
            // mobileNumberTextBox
            // 
            mobileNumberTextBox.BackColor = Color.Transparent;
            mobileNumberTextBox.BorderRadius = 5;
            mobileNumberTextBox.CustomizableEdges = customizableEdges8;
            mobileNumberTextBox.DefaultText = "";
            mobileNumberTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            mobileNumberTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            mobileNumberTextBox.DisabledState.ForeColor = Color.FromArgb(166, 166, 166);
            mobileNumberTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(166, 166, 166);
            mobileNumberTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            mobileNumberTextBox.Font = new Font("Segoe UI", 9F);
            mobileNumberTextBox.ForeColor = Color.Black;
            mobileNumberTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            mobileNumberTextBox.Location = new Point(14, 142);
            mobileNumberTextBox.Margin = new Padding(3, 4, 3, 4);
            mobileNumberTextBox.Name = "mobileNumberTextBox";
            mobileNumberTextBox.PlaceholderText = "0XXX XXX XXXX";
            mobileNumberTextBox.SelectedText = "";
            mobileNumberTextBox.ShadowDecoration.CustomizableEdges = customizableEdges9;
            mobileNumberTextBox.Size = new Size(370, 35);
            mobileNumberTextBox.TabIndex = 4;
            // 
            // requiredDocsLabel
            // 
            requiredDocsLabel.AutoSize = true;
            requiredDocsLabel.BackColor = Color.Transparent;
            requiredDocsLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            requiredDocsLabel.ForeColor = Color.Black;
            requiredDocsLabel.Location = new Point(14, 192);
            requiredDocsLabel.Name = "requiredDocsLabel";
            requiredDocsLabel.Size = new Size(215, 16);
            requiredDocsLabel.TabIndex = 5;
            requiredDocsLabel.Text = "Required Documents to Bring";
            // 
            // validIdCheckBox
            // 
            validIdCheckBox.AutoSize = true;
            validIdCheckBox.BackColor = Color.Transparent;
            validIdCheckBox.Font = new Font("Arial", 9F);
            validIdCheckBox.ForeColor = Color.Black;
            validIdCheckBox.Location = new Point(29, 217);
            validIdCheckBox.Name = "validIdCheckBox";
            validIdCheckBox.Size = new Size(137, 19);
            validIdCheckBox.TabIndex = 6;
            validIdCheckBox.Text = "Valid ID/Barangay ID";
            validIdCheckBox.UseVisualStyleBackColor = false;
            // 
            // proofAddressCheckBox
            // 
            proofAddressCheckBox.AutoSize = true;
            proofAddressCheckBox.BackColor = Color.Transparent;
            proofAddressCheckBox.Font = new Font("Arial", 9F);
            proofAddressCheckBox.ForeColor = Color.Black;
            proofAddressCheckBox.Location = new Point(29, 242);
            proofAddressCheckBox.Name = "proofAddressCheckBox";
            proofAddressCheckBox.Size = new Size(116, 19);
            proofAddressCheckBox.TabIndex = 7;
            proofAddressCheckBox.Text = "Proof of Address";
            proofAddressCheckBox.UseVisualStyleBackColor = false;
            // 
            // tinNumberCheckBox
            // 
            tinNumberCheckBox.AutoSize = true;
            tinNumberCheckBox.BackColor = Color.Transparent;
            tinNumberCheckBox.Font = new Font("Arial", 9F);
            tinNumberCheckBox.ForeColor = Color.Black;
            tinNumberCheckBox.Location = new Point(29, 267);
            tinNumberCheckBox.Name = "tinNumberCheckBox";
            tinNumberCheckBox.Size = new Size(93, 19);
            tinNumberCheckBox.TabIndex = 8;
            tinNumberCheckBox.Text = "TIN Number";
            tinNumberCheckBox.UseVisualStyleBackColor = false;
            // 
            // confirmButton
            // 
            confirmButton.BorderRadius = 6;
            confirmButton.CustomizableEdges = customizableEdges4;
            confirmButton.DisabledState.BorderColor = Color.DarkGray;
            confirmButton.DisabledState.CustomBorderColor = Color.DarkGray;
            confirmButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            confirmButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            confirmButton.FillColor = Color.FromArgb(22, 163, 74);
            confirmButton.Font = new Font("Franklin Gothic Demi", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            confirmButton.ForeColor = Color.White;
            confirmButton.Location = new Point(14, 317);
            confirmButton.Name = "confirmButton";
            confirmButton.ShadowDecoration.CustomizableEdges = customizableEdges5;
            confirmButton.Size = new Size(370, 45);
            confirmButton.TabIndex = 9;
            confirmButton.Text = "Confirm and Get Queue Ticket";
            confirmButton.Click += confirmButton_Click;
            // 
            // guna2ShadowPanel3
            // 
            guna2ShadowPanel3.BackColor = Color.Transparent;
            guna2ShadowPanel3.Controls.Add(guna2ShadowPanel2);
            guna2ShadowPanel3.Controls.Add(purchaseSummaryLabel);
            guna2ShadowPanel3.Controls.Add(productPanel);
            guna2ShadowPanel3.Controls.Add(unitPriceLabel);
            guna2ShadowPanel3.Controls.Add(totalDueValue);
            guna2ShadowPanel3.Controls.Add(unitPriceValue);
            guna2ShadowPanel3.Controls.Add(totalDueLabel);
            guna2ShadowPanel3.Controls.Add(processingFeeLabel);
            guna2ShadowPanel3.Controls.Add(processingFeeValue);
            guna2ShadowPanel3.FillColor = Color.White;
            guna2ShadowPanel3.Location = new Point(212, 120);
            guna2ShadowPanel3.Name = "guna2ShadowPanel3";
            guna2ShadowPanel3.Radius = 8;
            guna2ShadowPanel3.ShadowColor = Color.Black;
            guna2ShadowPanel3.ShadowDepth = 35;
            guna2ShadowPanel3.Size = new Size(400, 503);
            guna2ShadowPanel3.TabIndex = 8;
            // 
            // guna2ShadowPanel2
            // 
            guna2ShadowPanel2.BackColor = Color.Transparent;
            guna2ShadowPanel2.Controls.Add(instantOwnershipLabel);
            guna2ShadowPanel2.Controls.Add(instantOwnershipText);
            guna2ShadowPanel2.FillColor = Color.FromArgb(200, 235, 220);
            guna2ShadowPanel2.Location = new Point(18, 275);
            guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            guna2ShadowPanel2.Radius = 8;
            guna2ShadowPanel2.ShadowColor = Color.Black;
            guna2ShadowPanel2.ShadowDepth = 35;
            guna2ShadowPanel2.Size = new Size(359, 108);
            guna2ShadowPanel2.TabIndex = 10;
            // 
            // instantOwnershipLabel
            // 
            instantOwnershipLabel.AutoSize = true;
            instantOwnershipLabel.BackColor = Color.Transparent;
            instantOwnershipLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            instantOwnershipLabel.ForeColor = Color.Green;
            instantOwnershipLabel.Location = new Point(12, 16);
            instantOwnershipLabel.Name = "instantOwnershipLabel";
            instantOwnershipLabel.Size = new Size(148, 16);
            instantOwnershipLabel.TabIndex = 0;
            instantOwnershipLabel.Text = "✓ Instant Ownership";
            // 
            // instantOwnershipText
            // 
            instantOwnershipText.AutoSize = true;
            instantOwnershipText.BackColor = Color.Transparent;
            instantOwnershipText.Font = new Font("Arial", 8.5F);
            instantOwnershipText.ForeColor = Color.Gray;
            instantOwnershipText.Location = new Point(12, 36);
            instantOwnershipText.MaximumSize = new Size(340, 70);
            instantOwnershipText.Name = "instantOwnershipText";
            instantOwnershipText.Size = new Size(338, 15);
            instantOwnershipText.TabIndex = 1;
            instantOwnershipText.Text = "Drive home your motorcycle today upon payment completion.";
            // 
            // purchaseSummaryLabel
            // 
            purchaseSummaryLabel.AutoSize = true;
            purchaseSummaryLabel.BackColor = Color.Transparent;
            purchaseSummaryLabel.Font = new Font("Arial", 11F, FontStyle.Bold);
            purchaseSummaryLabel.ForeColor = Color.Black;
            purchaseSummaryLabel.Location = new Point(15, 25);
            purchaseSummaryLabel.Name = "purchaseSummaryLabel";
            purchaseSummaryLabel.Size = new Size(143, 18);
            purchaseSummaryLabel.TabIndex = 0;
            purchaseSummaryLabel.Text = "Purchase Summary";
            // 
            // productPanel
            // 
            productPanel.BackColor = Color.WhiteSmoke;
            productPanel.Controls.Add(productImage);
            productPanel.Controls.Add(productNameLabel);
            productPanel.Controls.Add(productDetailsLabel);
            productPanel.CustomizableEdges = customizableEdges10;
            productPanel.Location = new Point(15, 50);
            productPanel.Name = "productPanel";
            productPanel.ShadowDecoration.CustomizableEdges = customizableEdges11;
            productPanel.Size = new Size(370, 120);
            productPanel.TabIndex = 1;
            // 
            // productImage
            // 
            productImage.BackColor = Color.White;
            productImage.Location = new Point(15, 15);
            productImage.Name = "productImage";
            productImage.Size = new Size(70, 90);
            productImage.SizeMode = PictureBoxSizeMode.StretchImage;
            productImage.TabIndex = 0;
            productImage.TabStop = false;
            // 
            // productNameLabel
            // 
            productNameLabel.AutoSize = true;
            productNameLabel.BackColor = Color.Transparent;
            productNameLabel.Font = new Font("Arial", 11F, FontStyle.Bold);
            productNameLabel.ForeColor = Color.Black;
            productNameLabel.Location = new Point(95, 15);
            productNameLabel.Name = "productNameLabel";
            productNameLabel.Size = new Size(114, 18);
            productNameLabel.TabIndex = 1;
            productNameLabel.Text = "Honda ADV 160";
            // 
            // productDetailsLabel
            // 
            productDetailsLabel.AutoSize = true;
            productDetailsLabel.BackColor = Color.Transparent;
            productDetailsLabel.Font = new Font("Arial", 9F);
            productDetailsLabel.ForeColor = Color.Gray;
            productDetailsLabel.Location = new Point(95, 35);
            productDetailsLabel.Name = "productDetailsLabel";
            productDetailsLabel.Size = new Size(76, 15);
            productDetailsLabel.TabIndex = 2;
            productDetailsLabel.Text = "More Details";
            // 
            // unitPriceLabel
            // 
            unitPriceLabel.AutoSize = true;
            unitPriceLabel.BackColor = Color.Transparent;
            unitPriceLabel.Font = new Font("Arial", 9F);
            unitPriceLabel.ForeColor = Color.Gray;
            unitPriceLabel.Location = new Point(15, 180);
            unitPriceLabel.Name = "unitPriceLabel";
            unitPriceLabel.Size = new Size(60, 15);
            unitPriceLabel.TabIndex = 2;
            unitPriceLabel.Text = "Unit Price";
            // 
            // totalDueValue
            // 
            totalDueValue.AutoSize = true;
            totalDueValue.BackColor = Color.Transparent;
            totalDueValue.Font = new Font("Arial", 14F, FontStyle.Bold);
            totalDueValue.ForeColor = Color.Green;
            totalDueValue.Location = new Point(287, 237);
            totalDueValue.Name = "totalDueValue";
            totalDueValue.Size = new Size(94, 22);
            totalDueValue.TabIndex = 7;
            totalDueValue.Text = "₱167,900";
            totalDueValue.TextAlign = ContentAlignment.TopRight;
            // 
            // unitPriceValue
            // 
            unitPriceValue.AutoSize = true;
            unitPriceValue.BackColor = Color.Transparent;
            unitPriceValue.Font = new Font("Arial", 9F, FontStyle.Bold);
            unitPriceValue.ForeColor = Color.Black;
            unitPriceValue.Location = new Point(321, 180);
            unitPriceValue.Name = "unitPriceValue";
            unitPriceValue.Size = new Size(60, 15);
            unitPriceValue.TabIndex = 3;
            unitPriceValue.Text = "₱165,900";
            unitPriceValue.TextAlign = ContentAlignment.TopRight;
            // 
            // totalDueLabel
            // 
            totalDueLabel.AutoSize = true;
            totalDueLabel.BackColor = Color.Transparent;
            totalDueLabel.Font = new Font("Arial", 11F, FontStyle.Bold);
            totalDueLabel.ForeColor = Color.Black;
            totalDueLabel.Location = new Point(15, 240);
            totalDueLabel.Name = "totalDueLabel";
            totalDueLabel.Size = new Size(76, 18);
            totalDueLabel.TabIndex = 6;
            totalDueLabel.Text = "Total Due";
            // 
            // processingFeeLabel
            // 
            processingFeeLabel.AutoSize = true;
            processingFeeLabel.BackColor = Color.Transparent;
            processingFeeLabel.Font = new Font("Arial", 9F);
            processingFeeLabel.ForeColor = Color.Gray;
            processingFeeLabel.Location = new Point(15, 205);
            processingFeeLabel.Name = "processingFeeLabel";
            processingFeeLabel.Size = new Size(94, 15);
            processingFeeLabel.TabIndex = 4;
            processingFeeLabel.Text = "Processing Fee";
            // 
            // processingFeeValue
            // 
            processingFeeValue.AutoSize = true;
            processingFeeValue.BackColor = Color.Transparent;
            processingFeeValue.Font = new Font("Arial", 9F, FontStyle.Bold);
            processingFeeValue.ForeColor = Color.Black;
            processingFeeValue.Location = new Point(335, 205);
            processingFeeValue.Name = "processingFeeValue";
            processingFeeValue.Size = new Size(46, 15);
            processingFeeValue.TabIndex = 5;
            processingFeeValue.Text = "₱1,000";
            processingFeeValue.TextAlign = ContentAlignment.TopRight;
            // 
            // guna2ShadowPanel1
            // 
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(customerInfoLabel);
            guna2ShadowPanel1.Controls.Add(fullNameLabel);
            guna2ShadowPanel1.Controls.Add(confirmButton);
            guna2ShadowPanel1.Controls.Add(fullNameTextBox);
            guna2ShadowPanel1.Controls.Add(tinNumberCheckBox);
            guna2ShadowPanel1.Controls.Add(mobileNumberLabel);
            guna2ShadowPanel1.Controls.Add(proofAddressCheckBox);
            guna2ShadowPanel1.Controls.Add(mobileNumberTextBox);
            guna2ShadowPanel1.Controls.Add(validIdCheckBox);
            guna2ShadowPanel1.Controls.Add(requiredDocsLabel);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(696, 120);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 8;
            guna2ShadowPanel1.ShadowColor = Color.Black;
            guna2ShadowPanel1.ShadowDepth = 35;
            guna2ShadowPanel1.Size = new Size(400, 503);
            guna2ShadowPanel1.TabIndex = 9;
            // 
            // ck_confirm_payment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1278, 660);
            Controls.Add(guna2ShadowPanel1);
            Controls.Add(guna2ShadowPanel3);
            Controls.Add(kiosk_prod_panel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ck_confirm_payment";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ck_confirm_payment";
            kiosk_prod_panel.ResumeLayout(false);
            kiosk_prod_panel.PerformLayout();
            guna2ShadowPanel3.ResumeLayout(false);
            guna2ShadowPanel3.PerformLayout();
            guna2ShadowPanel2.ResumeLayout(false);
            guna2ShadowPanel2.PerformLayout();
            productPanel.ResumeLayout(false);
            productPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productImage).EndInit();
            guna2ShadowPanel1.ResumeLayout(false);
            guna2ShadowPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel kiosk_prod_panel;
        private Guna.UI2.WinForms.Guna2BorderlessForm payment_form;
        private Label label10;
        private Label customerInfoLabel;
        private Label fullNameLabel;
        private Guna.UI2.WinForms.Guna2TextBox fullNameTextBox;
        private Label mobileNumberLabel;
        private Guna.UI2.WinForms.Guna2TextBox mobileNumberTextBox;
        private Label requiredDocsLabel;
        private CheckBox validIdCheckBox;
        private CheckBox proofAddressCheckBox;
        private CheckBox tinNumberCheckBox;
        private Guna.UI2.WinForms.Guna2Button confirmButton;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel3;
        private Label purchaseSummaryLabel;
        private Guna.UI2.WinForms.Guna2Panel productPanel;
        private PictureBox productImage;
        private Label productNameLabel;
        private Label productDetailsLabel;
        private Label unitPriceLabel;
        private Label totalDueValue;
        private Label unitPriceValue;
        private Label totalDueLabel;
        private Label processingFeeLabel;
        private Label processingFeeValue;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private Label instantOwnershipLabel;
        private Label instantOwnershipText;
        private Guna.UI2.WinForms.Guna2ImageButton ckg_back;
    }
}