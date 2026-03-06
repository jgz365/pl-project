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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loan_form_3));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            loan_3 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            button_loan_back_3 = new Guna.UI2.WinForms.Guna2ImageButton();
            label1 = new Label();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            backButton = new Guna.UI2.WinForms.Guna2Button();
            financialDetailsLabel = new Label();
            continueButton = new Guna.UI2.WinForms.Guna2Button();
            monthlyGrossIncomeLabel = new Label();
            existingLoanObligationLabel = new Label();
            moneyCombobox = new Guna.UI2.WinForms.Guna2ComboBox();
            incomeCombobox = new Guna.UI2.WinForms.Guna2ComboBox();
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
            headerPanel.CustomizableEdges = customizableEdges7;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
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
            button_loan_back_3.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            // contentPanel
            // 
            contentPanel.BackColor = Color.White;
            contentPanel.BorderColor = Color.LightGray;
            contentPanel.BorderThickness = 1;
            contentPanel.Controls.Add(incomeCombobox);
            contentPanel.Controls.Add(moneyCombobox);
            contentPanel.Controls.Add(backButton);
            contentPanel.Controls.Add(financialDetailsLabel);
            contentPanel.Controls.Add(continueButton);
            contentPanel.Controls.Add(monthlyGrossIncomeLabel);
            contentPanel.Controls.Add(existingLoanObligationLabel);
            contentPanel.CustomizableEdges = customizableEdges4;
            contentPanel.Location = new Point(250, 170);
            contentPanel.Name = "contentPanel";
            contentPanel.ShadowDecoration.CustomizableEdges = customizableEdges5;
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
            monthlyGrossIncomeLabel.Location = new Point(25, 136);
            monthlyGrossIncomeLabel.Name = "monthlyGrossIncomeLabel";
            monthlyGrossIncomeLabel.Size = new Size(164, 16);
            monthlyGrossIncomeLabel.TabIndex = 1;
            monthlyGrossIncomeLabel.Text = "Monthly Gross Income";
            // 
            // existingLoanObligationLabel
            // 
            existingLoanObligationLabel.AutoSize = true;
            existingLoanObligationLabel.BackColor = Color.Transparent;
            existingLoanObligationLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            existingLoanObligationLabel.ForeColor = Color.Black;
            existingLoanObligationLabel.Location = new Point(25, 63);
            existingLoanObligationLabel.Name = "existingLoanObligationLabel";
            existingLoanObligationLabel.Size = new Size(134, 16);
            existingLoanObligationLabel.TabIndex = 3;
            existingLoanObligationLabel.Text = "Source of Income:";
            // 
            // moneyCombobox
            // 
            moneyCombobox.BackColor = Color.Transparent;
            moneyCombobox.BorderColor = Color.LightGray;
            moneyCombobox.BorderRadius = 5;
            moneyCombobox.CustomizableEdges = customizableEdges2;
            moneyCombobox.DrawMode = DrawMode.OwnerDrawFixed;
            moneyCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            moneyCombobox.FocusedColor = Color.RoyalBlue;
            moneyCombobox.FocusedState.BorderColor = Color.RoyalBlue;
            moneyCombobox.Font = new Font("Segoe UI", 9F);
            moneyCombobox.ForeColor = Color.Gray;
            moneyCombobox.ItemHeight = 30;
            moneyCombobox.Items.AddRange(new object[] { "₱15,000–₱30,000", "₱35,000–₱60,000", "₱70,000–₱150,000", "₱150,000–₱300,000", "₱300,000–₱500,000", "₱500,000+" });
            moneyCombobox.Location = new Point(25, 155);
            moneyCombobox.Name = "moneyCombobox";
            moneyCombobox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            moneyCombobox.Size = new Size(281, 36);
            moneyCombobox.TabIndex = 9;
            // 
            // incomeCombobox
            // 
            incomeCombobox.BackColor = Color.Transparent;
            incomeCombobox.BorderColor = Color.LightGray;
            incomeCombobox.BorderRadius = 5;
            incomeCombobox.CustomizableEdges = customizableEdges1;
            incomeCombobox.DrawMode = DrawMode.OwnerDrawFixed;
            incomeCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            incomeCombobox.FocusedColor = Color.RoyalBlue;
            incomeCombobox.FocusedState.BorderColor = Color.RoyalBlue;
            incomeCombobox.Font = new Font("Segoe UI", 9F);
            incomeCombobox.ForeColor = Color.Gray;
            incomeCombobox.ItemHeight = 30;
            incomeCombobox.Items.AddRange(new object[] { "Freelancing", "Allowance", "Side Business", "Remittance", "Salary" });
            incomeCombobox.Location = new Point(25, 82);
            incomeCombobox.Name = "incomeCombobox";
            incomeCombobox.ShadowDecoration.CustomizableEdges = customizableEdges1;
            incomeCombobox.Size = new Size(281, 36);
            incomeCombobox.TabIndex = 10;
            // 
            // loan_form_3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1278, 680);
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

        private Guna.UI2.WinForms.Guna2Panel contentPanel;
        private Label financialDetailsLabel;
        private Label monthlyGrossIncomeLabel;
        private Label existingLoanObligationLabel;
        private Guna.UI2.WinForms.Guna2Button backButton;
        private Guna.UI2.WinForms.Guna2Button continueButton;
        private Guna.UI2.WinForms.Guna2ImageButton button_loan_back_3;
        private Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox moneyCombobox;
        private Guna.UI2.WinForms.Guna2ComboBox incomeCombobox;
    }
}