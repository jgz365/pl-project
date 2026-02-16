namespace customer_kiosk
{
    partial class loan_form_3
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loan_form_3));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            loan_3 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            button_loan_back_3 = new Guna.UI2.WinForms.Guna2ImageButton();
            label1 = new Label();
            progressPanel = new Guna.UI2.WinForms.Guna2Panel();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            backButton = new Guna.UI2.WinForms.Guna2Button();
            financialDetailsLabel = new Label();
            continueButton = new Guna.UI2.WinForms.Guna2Button();
            monthlyGrossIncomeLabel = new Label();
            monthlyGrossIncomeTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            existingLoanObligationLabel = new Label();
            existingLoanObligationTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // loan_3
            // 
            loan_3.BorderRadius = 18;
            loan_3.ContainerControl = this;
            loan_3.DockIndicatorTransparencyValue = 0.6D;
            loan_3.DragForm = false;
            loan_3.ResizeForm = false;
            loan_3.TransparentWhileDrag = true;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(button_loan_back_3);
            headerPanel.Controls.Add(label1);
            headerPanel.CustomizableEdges = customizableEdges8;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges9;
            headerPanel.Size = new Size(1279, 80);
            headerPanel.TabIndex = 4;
            // 
            // button_loan_back_3
            // 
            button_loan_back_3.CheckedState.ImageSize = new Size(64, 64);
            button_loan_back_3.HoverState.ImageSize = new Size(64, 64);
            button_loan_back_3.Image = (Image)resources.GetObject("button_loan_back_3.Image");
            button_loan_back_3.ImageOffset = new Point(0, 0);
            button_loan_back_3.ImageRotate = 0F;
            button_loan_back_3.ImageSize = new Size(48, 48);
            button_loan_back_3.Location = new Point(3, 3);
            button_loan_back_3.Name = "button_loan_back_3";
            button_loan_back_3.PressedState.ImageSize = new Size(64, 64);
            button_loan_back_3.ShadowDecoration.CustomizableEdges = customizableEdges7;
            button_loan_back_3.Size = new Size(78, 74);
            button_loan_back_3.TabIndex = 25;
            button_loan_back_3.Click += button_loan_back_3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Franklin Gothic Demi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(87, 21);
            label1.Name = "label1";
            label1.Size = new Size(234, 37);
            label1.TabIndex = 26;
            label1.Text = "Loan Application";
            // 
            // progressPanel
            // 
            progressPanel.BackColor = Color.Transparent;
            progressPanel.CustomizableEdges = customizableEdges1;
            progressPanel.Location = new Point(250, 95);
            progressPanel.Name = "progressPanel";
            progressPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressPanel.Size = new Size(780, 60);
            progressPanel.TabIndex = 5;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.White;
            contentPanel.BorderColor = Color.LightGray;
            contentPanel.BorderThickness = 1;
            contentPanel.Controls.Add(backButton);
            contentPanel.Controls.Add(financialDetailsLabel);
            contentPanel.Controls.Add(continueButton);
            contentPanel.Controls.Add(monthlyGrossIncomeLabel);
            contentPanel.Controls.Add(monthlyGrossIncomeTextBox);
            contentPanel.Controls.Add(existingLoanObligationLabel);
            contentPanel.Controls.Add(existingLoanObligationTextBox);
            contentPanel.CustomizableEdges = customizableEdges5;
            contentPanel.Location = new Point(250, 170);
            contentPanel.Name = "contentPanel";
            contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            contentPanel.Size = new Size(780, 498);
            contentPanel.TabIndex = 6;
            // 
            // backButton
            // 
            backButton.BorderColor = Color.LightGray;
            backButton.BorderRadius = 6;
            backButton.BorderThickness = 1;
            backButton.CustomizableEdges = customizableEdges3;
            backButton.DisabledState.BorderColor = Color.DarkGray;
            backButton.DisabledState.CustomBorderColor = Color.DarkGray;
            backButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            backButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            backButton.FillColor = Color.White;
            backButton.Font = new Font("Arial", 10F, FontStyle.Bold);
            backButton.ForeColor = Color.Black;
            backButton.Location = new Point(25, 440);
            backButton.Name = "backButton";
            backButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            backButton.Size = new Size(100, 40);
            backButton.TabIndex = 5;
            backButton.Text = "Back";
            backButton.Click += backButton_Click;
            // 
            // financialDetailsLabel
            // 
            financialDetailsLabel.AutoSize = true;
            financialDetailsLabel.BackColor = Color.Transparent;
            financialDetailsLabel.Font = new Font("Arial", 13F, FontStyle.Bold);
            financialDetailsLabel.ForeColor = Color.Black;
            financialDetailsLabel.Location = new Point(25, 25);
            financialDetailsLabel.Name = "financialDetailsLabel";
            financialDetailsLabel.Size = new Size(153, 21);
            financialDetailsLabel.TabIndex = 0;
            financialDetailsLabel.Text = "Financial Details";
            // 
            // continueButton
            // 
            continueButton.BorderRadius = 6;
            continueButton.CustomizableEdges = customizableEdges3;
            continueButton.DisabledState.BorderColor = Color.DarkGray;
            continueButton.DisabledState.CustomBorderColor = Color.DarkGray;
            continueButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            continueButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            continueButton.FillColor = Color.RoyalBlue;
            continueButton.Font = new Font("Arial", 10F, FontStyle.Bold);
            continueButton.ForeColor = Color.White;
            continueButton.Location = new Point(635, 440);
            continueButton.Name = "continueButton";
            continueButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            continueButton.Size = new Size(120, 40);
            continueButton.TabIndex = 6;
            continueButton.Text = "Continue >";
            continueButton.Click += continueButton_Click;
            // 
            // monthlyGrossIncomeLabel
            // 
            monthlyGrossIncomeLabel.AutoSize = true;
            monthlyGrossIncomeLabel.BackColor = Color.Transparent;
            monthlyGrossIncomeLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            monthlyGrossIncomeLabel.ForeColor = Color.Black;
            monthlyGrossIncomeLabel.Location = new Point(25, 70);
            monthlyGrossIncomeLabel.Name = "monthlyGrossIncomeLabel";
            monthlyGrossIncomeLabel.Size = new Size(164, 16);
            monthlyGrossIncomeLabel.TabIndex = 1;
            monthlyGrossIncomeLabel.Text = "Monthly Gross Income";
            // 
            // monthlyGrossIncomeTextBox
            // 
            monthlyGrossIncomeTextBox.BackColor = Color.Transparent;
            monthlyGrossIncomeTextBox.BorderColor = Color.LightGray;
            monthlyGrossIncomeTextBox.BorderRadius = 5;
            monthlyGrossIncomeTextBox.CustomizableEdges = customizableEdges4;
            monthlyGrossIncomeTextBox.DefaultText = "";
            monthlyGrossIncomeTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            monthlyGrossIncomeTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            monthlyGrossIncomeTextBox.DisabledState.ForeColor = Color.FromArgb(166, 166, 166);
            monthlyGrossIncomeTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(166, 166, 166);
            monthlyGrossIncomeTextBox.FocusedState.BorderColor = Color.RoyalBlue;
            monthlyGrossIncomeTextBox.Font = new Font("Segoe UI", 9F);
            monthlyGrossIncomeTextBox.ForeColor = Color.Black;
            monthlyGrossIncomeTextBox.HoverState.BorderColor = Color.RoyalBlue;
            monthlyGrossIncomeTextBox.Location = new Point(25, 95);
            monthlyGrossIncomeTextBox.Margin = new Padding(3, 4, 3, 4);
            monthlyGrossIncomeTextBox.Name = "monthlyGrossIncomeTextBox";
            monthlyGrossIncomeTextBox.PlaceholderText = "₱";
            monthlyGrossIncomeTextBox.SelectedText = "";
            monthlyGrossIncomeTextBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            monthlyGrossIncomeTextBox.Size = new Size(730, 40);
            monthlyGrossIncomeTextBox.TabIndex = 2;
            // 
            // existingLoanObligationLabel
            // 
            existingLoanObligationLabel.AutoSize = true;
            existingLoanObligationLabel.BackColor = Color.Transparent;
            existingLoanObligationLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            existingLoanObligationLabel.ForeColor = Color.Black;
            existingLoanObligationLabel.Location = new Point(25, 160);
            existingLoanObligationLabel.Name = "existingLoanObligationLabel";
            existingLoanObligationLabel.Size = new Size(245, 16);
            existingLoanObligationLabel.TabIndex = 3;
            existingLoanObligationLabel.Text = "Existing Monthly Loan Obligations";
            // 
            // existingLoanObligationTextBox
            // 
            existingLoanObligationTextBox.BackColor = Color.Transparent;
            existingLoanObligationTextBox.BorderColor = Color.LightGray;
            existingLoanObligationTextBox.BorderRadius = 5;
            existingLoanObligationTextBox.CustomizableEdges = customizableEdges4;
            existingLoanObligationTextBox.DefaultText = "";
            existingLoanObligationTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            existingLoanObligationTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            existingLoanObligationTextBox.DisabledState.ForeColor = Color.FromArgb(166, 166, 166);
            existingLoanObligationTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(166, 166, 166);
            existingLoanObligationTextBox.FocusedState.BorderColor = Color.RoyalBlue;
            existingLoanObligationTextBox.Font = new Font("Segoe UI", 9F);
            existingLoanObligationTextBox.ForeColor = Color.Black;
            existingLoanObligationTextBox.HoverState.BorderColor = Color.RoyalBlue;
            existingLoanObligationTextBox.Location = new Point(25, 185);
            existingLoanObligationTextBox.Margin = new Padding(3, 4, 3, 4);
            existingLoanObligationTextBox.Name = "existingLoanObligationTextBox";
            existingLoanObligationTextBox.PlaceholderText = "₱";
            existingLoanObligationTextBox.SelectedText = "";
            existingLoanObligationTextBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            existingLoanObligationTextBox.Size = new Size(730, 40);
            existingLoanObligationTextBox.TabIndex = 4;
            // 
            // loan_form_3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1278, 680);
            Controls.Add(progressPanel);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "loan_form_3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "loan_form_3";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm loan_3;
        private Guna.UI2.WinForms.Guna2Panel headerPanel;

        private Guna.UI2.WinForms.Guna2Panel progressPanel;

        private Guna.UI2.WinForms.Guna2Panel contentPanel;
        private Label financialDetailsLabel;
        private Label monthlyGrossIncomeLabel;
        private Guna.UI2.WinForms.Guna2TextBox monthlyGrossIncomeTextBox;
        private Label existingLoanObligationLabel;
        private Guna.UI2.WinForms.Guna2TextBox existingLoanObligationTextBox;
        private Guna.UI2.WinForms.Guna2Button backButton;
        private Guna.UI2.WinForms.Guna2Button continueButton;
        private Guna.UI2.WinForms.Guna2ImageButton button_loan_back_3;
        private Label label1;
    }
}