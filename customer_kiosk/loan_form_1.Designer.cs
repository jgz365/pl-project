namespace customer_kiosk
{
    partial class loan_form_1
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loan_form_1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            loan_1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            ckg_back = new Guna.UI2.WinForms.Guna2ImageButton();
            headerLabel = new Label();
            step1Label = new Label();
            step1Number = new Label();
            step2Label = new Label();
            step2Number = new Label();
            step3Label = new Label();
            step3Number = new Label();
            step4Label = new Label();
            step4Number = new Label();
            step5Label = new Label();
            step5Number = new Label();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            continueButton = new Guna.UI2.WinForms.Guna2Button();
            colorValueLabel = new Label();
            colorLabel = new Label();
            priceLabel = new Label();
            productNameLabel = new Label();
            productPanel = new Guna.UI2.WinForms.Guna2Panel();
            productImage = new PictureBox();
            confirmSelectionLabel = new Label();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            productPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productImage).BeginInit();
            SuspendLayout();
            // 
            // loan_1
            // 
            loan_1.BorderRadius = 18;
            loan_1.ContainerControl = this;
            loan_1.DockIndicatorTransparencyValue = 0.6D;
            loan_1.DragForm = false;
            loan_1.TransparentWhileDrag = true;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(ckg_back);
            headerPanel.Controls.Add(headerLabel);
            headerPanel.CustomizableEdges = customizableEdges4;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges5;
            headerPanel.Size = new Size(1279, 80);
            headerPanel.TabIndex = 4;
            // 
            // ckg_back
            // 
            ckg_back.CheckedState.ImageSize = new Size(64, 64);
            ckg_back.HoverState.ImageSize = new Size(64, 64);
            ckg_back.Image = (Image)resources.GetObject("ckg_back.Image");
            ckg_back.ImageOffset = new Point(0, 0);
            ckg_back.ImageRotate = 0F;
            ckg_back.ImageSize = new Size(48, 48);
            ckg_back.Location = new Point(3, 3);
            ckg_back.Name = "ckg_back";
            ckg_back.PressedState.ImageSize = new Size(64, 64);
            ckg_back.ShadowDecoration.CustomizableEdges = customizableEdges3;
            ckg_back.Size = new Size(78, 74);
            ckg_back.TabIndex = 7;
            ckg_back.Click += ckg_back_Click;
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.BackColor = Color.Transparent;
            headerLabel.Font = new Font("Franklin Gothic Demi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            headerLabel.ForeColor = Color.Black;
            headerLabel.Location = new Point(87, 23);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(234, 37);
            headerLabel.TabIndex = 23;
            headerLabel.Text = "Loan Application";
            // 
            // step1Label
            // 
            step1Label.AutoSize = true;
            step1Label.BackColor = Color.Transparent;
            step1Label.Font = new Font("Arial", 9F);
            step1Label.ForeColor = Color.DodgerBlue;
            step1Label.Location = new Point(20, 38);
            step1Label.Name = "step1Label";
            step1Label.Size = new Size(60, 15);
            step1Label.TabIndex = 1;
            step1Label.Text = "Motorcycle";
            step1Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // step1Number
            // 
            step1Number.AutoSize = true;
            step1Number.BackColor = Color.RoyalBlue;
            step1Number.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            step1Number.ForeColor = Color.White;
            step1Number.Location = new Point(28, 5);
            step1Number.Name = "step1Number";
            step1Number.Size = new Size(30, 30);
            step1Number.TabIndex = 0;
            step1Number.Text = "1";
            step1Number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // step2Label
            // 
            step2Label.AutoSize = true;
            step2Label.BackColor = Color.Transparent;
            step2Label.Font = new Font("Arial", 9F);
            step2Label.ForeColor = Color.Gray;
            step2Label.Location = new Point(35, 38);
            step2Label.Name = "step2Label";
            step2Label.Size = new Size(50, 15);
            step2Label.TabIndex = 1;
            step2Label.Text = "Personal";
            // 
            // step2Number
            // 
            step2Number.AutoSize = true;
            step2Number.BackColor = Color.Transparent;
            step2Number.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            step2Number.ForeColor = Color.Gray;
            step2Number.Location = new Point(28, 5);
            step2Number.Name = "step2Number";
            step2Number.Size = new Size(30, 30);
            step2Number.TabIndex = 0;
            step2Number.Text = "2";
            step2Number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // step3Label
            // 
            step3Label.AutoSize = true;
            step3Label.BackColor = Color.Transparent;
            step3Label.Font = new Font("Arial", 9F);
            step3Label.ForeColor = Color.Gray;
            step3Label.Location = new Point(35, 38);
            step3Label.Name = "step3Label";
            step3Label.Size = new Size(55, 15);
            step3Label.TabIndex = 1;
            step3Label.Text = "Financial";
            // 
            // step3Number
            // 
            step3Number.AutoSize = true;
            step3Number.BackColor = Color.Transparent;
            step3Number.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            step3Number.ForeColor = Color.Gray;
            step3Number.Location = new Point(28, 5);
            step3Number.Name = "step3Number";
            step3Number.Size = new Size(30, 30);
            step3Number.TabIndex = 0;
            step3Number.Text = "3";
            step3Number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // step4Label
            // 
            step4Label.AutoSize = true;
            step4Label.BackColor = Color.Transparent;
            step4Label.Font = new Font("Arial", 9F);
            step4Label.ForeColor = Color.Gray;
            step4Label.Location = new Point(25, 38);
            step4Label.Name = "step4Label";
            step4Label.Size = new Size(73, 15);
            step4Label.TabIndex = 1;
            step4Label.Text = "Loan Config";
            // 
            // step4Number
            // 
            step4Number.AutoSize = true;
            step4Number.BackColor = Color.Transparent;
            step4Number.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            step4Number.ForeColor = Color.Gray;
            step4Number.Location = new Point(28, 5);
            step4Number.Name = "step4Number";
            step4Number.Size = new Size(30, 30);
            step4Number.TabIndex = 0;
            step4Number.Text = "4";
            step4Number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // step5Label
            // 
            step5Label.AutoSize = true;
            step5Label.BackColor = Color.Transparent;
            step5Label.Font = new Font("Arial", 9F);
            step5Label.ForeColor = Color.Gray;
            step5Label.Location = new Point(18, 38);
            step5Label.Name = "step5Label";
            step5Label.Size = new Size(72, 15);
            step5Label.TabIndex = 1;
            step5Label.Text = "Documents";
            // 
            // step5Number
            // 
            step5Number.AutoSize = true;
            step5Number.BackColor = Color.Transparent;
            step5Number.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            step5Number.ForeColor = Color.Gray;
            step5Number.Location = new Point(28, 5);
            step5Number.Name = "step5Number";
            step5Number.Size = new Size(30, 30);
            step5Number.TabIndex = 0;
            step5Number.Text = "5";
            step5Number.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.White;
            contentPanel.BorderColor = Color.LightGray;
            contentPanel.BorderThickness = 1;
            contentPanel.Controls.Add(continueButton);
            contentPanel.Controls.Add(colorValueLabel);
            contentPanel.Controls.Add(colorLabel);
            contentPanel.Controls.Add(priceLabel);
            contentPanel.Controls.Add(productNameLabel);
            contentPanel.Controls.Add(productPanel);
            contentPanel.Controls.Add(confirmSelectionLabel);
            contentPanel.CustomizableEdges = customizableEdges1;
            contentPanel.Location = new Point(300, 200);
            contentPanel.Name = "contentPanel";
            contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            contentPanel.Size = new Size(680, 350);
            contentPanel.TabIndex = 6;
            // 
            // continueButton
            // 
            continueButton.BorderRadius = 6;
            continueButton.CustomizableEdges = customizableEdges1;
            continueButton.DisabledState.BorderColor = Color.DarkGray;
            continueButton.DisabledState.CustomBorderColor = Color.DarkGray;
            continueButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            continueButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            continueButton.FillColor = Color.RoyalBlue;
            continueButton.Font = new Font("Arial", 10F, FontStyle.Bold);
            continueButton.ForeColor = Color.White;
            continueButton.Location = new Point(530, 290);
            continueButton.Name = "continueButton";
            continueButton.ShadowDecoration.CustomizableEdges = customizableEdges1;
            continueButton.Size = new Size(120, 45);
            continueButton.TabIndex = 6;
            continueButton.Text = "Continue >";
            continueButton.Click += continueButton_Click;
            // 
            // colorValueLabel
            // 
            colorValueLabel.AutoSize = true;
            colorValueLabel.BackColor = Color.Transparent;
            colorValueLabel.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            colorValueLabel.ForeColor = Color.Black;
            colorValueLabel.Location = new Point(192, 128);
            colorValueLabel.Name = "colorValueLabel";
            colorValueLabel.Size = new Size(74, 15);
            colorValueLabel.TabIndex = 5;
            colorValueLabel.Text = "Matte Black";
            // 
            // colorLabel
            // 
            colorLabel.AutoSize = true;
            colorLabel.BackColor = Color.Transparent;
            colorLabel.Font = new Font("Arial", 9F);
            colorLabel.ForeColor = Color.Gray;
            colorLabel.Location = new Point(150, 128);
            colorLabel.Name = "colorLabel";
            colorLabel.Size = new Size(40, 15);
            colorLabel.TabIndex = 4;
            colorLabel.Text = "Color:";
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.BackColor = Color.Transparent;
            priceLabel.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            priceLabel.ForeColor = Color.ForestGreen;
            priceLabel.Location = new Point(150, 95);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(94, 22);
            priceLabel.TabIndex = 3;
            priceLabel.Text = "₱670,000";
            // 
            // productNameLabel
            // 
            productNameLabel.AutoSize = true;
            productNameLabel.BackColor = Color.Transparent;
            productNameLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            productNameLabel.ForeColor = Color.Black;
            productNameLabel.Location = new Point(150, 70);
            productNameLabel.Name = "productNameLabel";
            productNameLabel.Size = new Size(127, 19);
            productNameLabel.TabIndex = 2;
            productNameLabel.Text = "Sample Sample";
            // 
            // productPanel
            // 
            productPanel.BackColor = Color.FromArgb(245, 245, 245);
            productPanel.BorderColor = Color.LightGray;
            productPanel.BorderThickness = 1;
            productPanel.Controls.Add(productImage);
            productPanel.CustomizableEdges = customizableEdges1;
            productPanel.Location = new Point(30, 60);
            productPanel.Name = "productPanel";
            productPanel.ShadowDecoration.CustomizableEdges = customizableEdges1;
            productPanel.Size = new Size(100, 100);
            productPanel.TabIndex = 1;
            // 
            // productImage
            // 
            productImage.BackColor = Color.White;
            productImage.Dock = DockStyle.Fill;
            productImage.Location = new Point(0, 0);
            productImage.Name = "productImage";
            productImage.Size = new Size(100, 100);
            productImage.SizeMode = PictureBoxSizeMode.CenterImage;
            productImage.TabIndex = 0;
            productImage.TabStop = false;
            // 
            // confirmSelectionLabel
            // 
            confirmSelectionLabel.AutoSize = true;
            confirmSelectionLabel.BackColor = Color.Transparent;
            confirmSelectionLabel.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            confirmSelectionLabel.ForeColor = Color.Black;
            confirmSelectionLabel.Location = new Point(30, 25);
            confirmSelectionLabel.Name = "confirmSelectionLabel";
            confirmSelectionLabel.Size = new Size(175, 22);
            confirmSelectionLabel.TabIndex = 0;
            confirmSelectionLabel.Text = "Confirm Selection";
            // 
            // loan_form_1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1278, 660);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "loan_form_1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "loan_form_1";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            productPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)productImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm loan_1;
        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Label headerLabel;
        private Label step1Number;
        private Label step1Label;
        private Label step2Number;
        private Label step2Label;
        private Label step3Number;
        private Label step3Label;
        private Label step4Number;
        private Label step4Label;
        private Label step5Number;
        private Label step5Label;

        // Content Panel Controls
        private Guna.UI2.WinForms.Guna2Panel contentPanel;
        private Label confirmSelectionLabel;
        private Guna.UI2.WinForms.Guna2Panel productPanel;
        private PictureBox productImage;
        private Label productNameLabel;
        private Label priceLabel;
        private Label colorLabel;
        private Label colorValueLabel;
        private Guna.UI2.WinForms.Guna2Button continueButton;
        private Guna.UI2.WinForms.Guna2ImageButton ckg_back;
    }
}