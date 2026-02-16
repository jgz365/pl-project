namespace customer_kiosk
{
    partial class loan_form_4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loan_form_4));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            loan_4 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            button_loan_back_4 = new Guna.UI2.WinForms.Guna2ImageButton();
            label1 = new Label();
            progressPanel = new Guna.UI2.WinForms.Guna2Panel();
            contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            backButton = new Guna.UI2.WinForms.Guna2Button();
            configureLoanLabel = new Label();
            continueButton = new Guna.UI2.WinForms.Guna2Button();
            loanTermLabel = new Label();
            loanTerm12Button = new Guna.UI2.WinForms.Guna2Button();
            loanTerm24Button = new Guna.UI2.WinForms.Guna2Button();
            loanTerm36Button = new Guna.UI2.WinForms.Guna2Button();
            downPaymentLabel = new Label();
            downPaymentMinLabel = new Label();
            downPaymentSlider = new Guna.UI2.WinForms.Guna2TrackBar();
            downPaymentMaxLabel = new Label();
            monthlyPaymentLabel = new Label();
            monthlyPaymentValueLabel = new Label();
            preApprovedPanel = new Guna.UI2.WinForms.Guna2Panel();
            preApprovedCheckLabel = new Label();
            preApprovedTextLabel = new Label();
            label2 = new Label();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            preApprovedPanel.SuspendLayout();
            SuspendLayout();
            // 
            // loan_4
            // 
            loan_4.BorderRadius = 18;
            loan_4.ContainerControl = this;
            loan_4.DockIndicatorTransparencyValue = 0.6D;
            loan_4.DragForm = false;
            loan_4.ResizeForm = false;
            loan_4.TransparentWhileDrag = true;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(button_loan_back_4);
            headerPanel.Controls.Add(label1);
            headerPanel.CustomizableEdges = customizableEdges8;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges9;
            headerPanel.Size = new Size(1279, 80);
            headerPanel.TabIndex = 4;
            // 
            // button_loan_back_4
            // 
            button_loan_back_4.CheckedState.ImageSize = new Size(64, 64);
            button_loan_back_4.HoverState.ImageSize = new Size(64, 64);
            button_loan_back_4.Image = (Image)resources.GetObject("button_loan_back_4.Image");
            button_loan_back_4.ImageOffset = new Point(0, 0);
            button_loan_back_4.ImageRotate = 0F;
            button_loan_back_4.ImageSize = new Size(48, 48);
            button_loan_back_4.Location = new Point(3, 3);
            button_loan_back_4.Name = "button_loan_back_4";
            button_loan_back_4.PressedState.ImageSize = new Size(64, 64);
            button_loan_back_4.ShadowDecoration.CustomizableEdges = customizableEdges7;
            button_loan_back_4.Size = new Size(78, 74);
            button_loan_back_4.TabIndex = 25;
            button_loan_back_4.Click += button_loan_back_4_Click;
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
            contentPanel.Controls.Add(label2);
            contentPanel.Controls.Add(backButton);
            contentPanel.Controls.Add(configureLoanLabel);
            contentPanel.Controls.Add(continueButton);
            contentPanel.Controls.Add(loanTermLabel);
            contentPanel.Controls.Add(loanTerm12Button);
            contentPanel.Controls.Add(loanTerm24Button);
            contentPanel.Controls.Add(loanTerm36Button);
            contentPanel.Controls.Add(downPaymentLabel);
            contentPanel.Controls.Add(downPaymentMinLabel);
            contentPanel.Controls.Add(downPaymentSlider);
            contentPanel.Controls.Add(downPaymentMaxLabel);
            contentPanel.Controls.Add(monthlyPaymentLabel);
            contentPanel.Controls.Add(monthlyPaymentValueLabel);
            contentPanel.Controls.Add(preApprovedPanel);
            contentPanel.CustomizableEdges = customizableEdges6;
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
            backButton.Location = new Point(25, 442);
            backButton.Name = "backButton";
            backButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            backButton.Size = new Size(100, 40);
            backButton.TabIndex = 5;
            backButton.Text = "Back";
            backButton.Click += backButton_Click;
            // 
            // configureLoanLabel
            // 
            configureLoanLabel.AutoSize = true;
            configureLoanLabel.BackColor = Color.Transparent;
            configureLoanLabel.Font = new Font("Arial", 13F, FontStyle.Bold);
            configureLoanLabel.ForeColor = Color.Black;
            configureLoanLabel.Location = new Point(25, 25);
            configureLoanLabel.Name = "configureLoanLabel";
            configureLoanLabel.Size = new Size(144, 21);
            configureLoanLabel.TabIndex = 0;
            configureLoanLabel.Text = "Configure Loan";
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
            continueButton.Location = new Point(635, 442);
            continueButton.Name = "continueButton";
            continueButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            continueButton.Size = new Size(120, 40);
            continueButton.TabIndex = 6;
            continueButton.Text = "Continue >";
            continueButton.Click += continueButton_Click;
            // 
            // loanTermLabel
            // 
            loanTermLabel.AutoSize = true;
            loanTermLabel.BackColor = Color.Transparent;
            loanTermLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            loanTermLabel.ForeColor = Color.DarkGray;
            loanTermLabel.Location = new Point(25, 70);
            loanTermLabel.Name = "loanTermLabel";
            loanTermLabel.Size = new Size(89, 16);
            loanTermLabel.TabIndex = 1;
            loanTermLabel.Text = "LOAN TERM";
            // 
            // loanTerm12Button
            // 
            loanTerm12Button.BorderRadius = 6;
            loanTerm12Button.CustomizableEdges = customizableEdges4;
            loanTerm12Button.DisabledState.BorderColor = Color.DarkGray;
            loanTerm12Button.DisabledState.CustomBorderColor = Color.DarkGray;
            loanTerm12Button.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            loanTerm12Button.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            loanTerm12Button.FillColor = Color.RoyalBlue;
            loanTerm12Button.Font = new Font("Arial", 10F, FontStyle.Bold);
            loanTerm12Button.ForeColor = Color.White;
            loanTerm12Button.Location = new Point(25, 95);
            loanTerm12Button.Name = "loanTerm12Button";
            loanTerm12Button.ShadowDecoration.CustomizableEdges = customizableEdges4;
            loanTerm12Button.Size = new Size(100, 35);
            loanTerm12Button.TabIndex = 2;
            loanTerm12Button.Text = "12 mos";
            // 
            // loanTerm24Button
            // 
            loanTerm24Button.BorderRadius = 6;
            loanTerm24Button.CustomizableEdges = customizableEdges4;
            loanTerm24Button.DisabledState.BorderColor = Color.DarkGray;
            loanTerm24Button.DisabledState.CustomBorderColor = Color.DarkGray;
            loanTerm24Button.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            loanTerm24Button.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            loanTerm24Button.FillColor = Color.LightGray;
            loanTerm24Button.Font = new Font("Arial", 10F, FontStyle.Bold);
            loanTerm24Button.ForeColor = Color.DarkGray;
            loanTerm24Button.Location = new Point(140, 95);
            loanTerm24Button.Name = "loanTerm24Button";
            loanTerm24Button.ShadowDecoration.CustomizableEdges = customizableEdges4;
            loanTerm24Button.Size = new Size(100, 35);
            loanTerm24Button.TabIndex = 3;
            loanTerm24Button.Text = "24 mos";
            // 
            // loanTerm36Button
            // 
            loanTerm36Button.BorderRadius = 6;
            loanTerm36Button.CustomizableEdges = customizableEdges4;
            loanTerm36Button.DisabledState.BorderColor = Color.DarkGray;
            loanTerm36Button.DisabledState.CustomBorderColor = Color.DarkGray;
            loanTerm36Button.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            loanTerm36Button.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            loanTerm36Button.FillColor = Color.LightGray;
            loanTerm36Button.Font = new Font("Arial", 10F, FontStyle.Bold);
            loanTerm36Button.ForeColor = Color.DarkGray;
            loanTerm36Button.Location = new Point(255, 95);
            loanTerm36Button.Name = "loanTerm36Button";
            loanTerm36Button.ShadowDecoration.CustomizableEdges = customizableEdges4;
            loanTerm36Button.Size = new Size(100, 35);
            loanTerm36Button.TabIndex = 4;
            loanTerm36Button.Text = "36 mos";
            // 
            // downPaymentLabel
            // 
            downPaymentLabel.AutoSize = true;
            downPaymentLabel.BackColor = Color.Transparent;
            downPaymentLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            downPaymentLabel.ForeColor = Color.DarkGray;
            downPaymentLabel.Location = new Point(25, 160);
            downPaymentLabel.Name = "downPaymentLabel";
            downPaymentLabel.Size = new Size(121, 16);
            downPaymentLabel.TabIndex = 5;
            downPaymentLabel.Text = "DOWN PAYMENT";
            // 
            // downPaymentMinLabel
            // 
            downPaymentMinLabel.AutoSize = true;
            downPaymentMinLabel.BackColor = Color.Transparent;
            downPaymentMinLabel.Font = new Font("Arial", 9F);
            downPaymentMinLabel.ForeColor = Color.Gray;
            downPaymentMinLabel.Location = new Point(25, 185);
            downPaymentMinLabel.Name = "downPaymentMinLabel";
            downPaymentMinLabel.Size = new Size(32, 15);
            downPaymentMinLabel.TabIndex = 6;
            downPaymentMinLabel.Text = "10%";
            // 
            // downPaymentSlider
            // 
            downPaymentSlider.BackColor = Color.Transparent;
            downPaymentSlider.Cursor = Cursors.Hand;
            downPaymentSlider.Location = new Point(60, 180);
            downPaymentSlider.Name = "downPaymentSlider";
            downPaymentSlider.Size = new Size(379, 25);
            downPaymentSlider.TabIndex = 7;
            downPaymentSlider.ThumbColor = Color.RoyalBlue;
            // 
            // downPaymentMaxLabel
            // 
            downPaymentMaxLabel.AutoSize = true;
            downPaymentMaxLabel.BackColor = Color.Transparent;
            downPaymentMaxLabel.Font = new Font("Arial", 9F);
            downPaymentMaxLabel.ForeColor = Color.Gray;
            downPaymentMaxLabel.Location = new Point(695, 185);
            downPaymentMaxLabel.Name = "downPaymentMaxLabel";
            downPaymentMaxLabel.Size = new Size(32, 15);
            downPaymentMaxLabel.TabIndex = 9;
            downPaymentMaxLabel.Text = "90%";
            // 
            // monthlyPaymentLabel
            // 
            monthlyPaymentLabel.AutoSize = true;
            monthlyPaymentLabel.BackColor = Color.Transparent;
            monthlyPaymentLabel.Font = new Font("Arial", 10F, FontStyle.Bold);
            monthlyPaymentLabel.ForeColor = Color.DarkGray;
            monthlyPaymentLabel.Location = new Point(500, 100);
            monthlyPaymentLabel.Name = "monthlyPaymentLabel";
            monthlyPaymentLabel.Size = new Size(146, 16);
            monthlyPaymentLabel.TabIndex = 10;
            monthlyPaymentLabel.Text = "MONTHLY PAYMENT";
            // 
            // monthlyPaymentValueLabel
            // 
            monthlyPaymentValueLabel.AutoSize = true;
            monthlyPaymentValueLabel.BackColor = Color.Transparent;
            monthlyPaymentValueLabel.Font = new Font("Arial", 20F, FontStyle.Bold);
            monthlyPaymentValueLabel.ForeColor = Color.ForestGreen;
            monthlyPaymentValueLabel.Location = new Point(500, 125);
            monthlyPaymentValueLabel.Name = "monthlyPaymentValueLabel";
            monthlyPaymentValueLabel.Size = new Size(115, 32);
            monthlyPaymentValueLabel.TabIndex = 11;
            monthlyPaymentValueLabel.Text = "₱13,575";
            // 
            // preApprovedPanel
            // 
            preApprovedPanel.BackColor = Color.FromArgb(220, 245, 235);
            preApprovedPanel.BorderColor = Color.FromArgb(144, 238, 144);
            preApprovedPanel.BorderThickness = 1;
            preApprovedPanel.Controls.Add(preApprovedCheckLabel);
            preApprovedPanel.Controls.Add(preApprovedTextLabel);
            preApprovedPanel.CustomizableEdges = customizableEdges5;
            preApprovedPanel.Location = new Point(500, 180);
            preApprovedPanel.Name = "preApprovedPanel";
            preApprovedPanel.ShadowDecoration.CustomizableEdges = customizableEdges5;
            preApprovedPanel.Size = new Size(255, 70);
            preApprovedPanel.TabIndex = 12;
            // 
            // preApprovedCheckLabel
            // 
            preApprovedCheckLabel.AutoSize = true;
            preApprovedCheckLabel.BackColor = Color.Transparent;
            preApprovedCheckLabel.Font = new Font("Arial", 11F, FontStyle.Bold);
            preApprovedCheckLabel.ForeColor = Color.ForestGreen;
            preApprovedCheckLabel.Location = new Point(10, 10);
            preApprovedCheckLabel.Name = "preApprovedCheckLabel";
            preApprovedCheckLabel.Size = new Size(120, 18);
            preApprovedCheckLabel.TabIndex = 0;
            preApprovedCheckLabel.Text = "✓ Pre-Approved";
            // 
            // preApprovedTextLabel
            // 
            preApprovedTextLabel.AutoSize = true;
            preApprovedTextLabel.BackColor = Color.Transparent;
            preApprovedTextLabel.Font = new Font("Arial", 9F);
            preApprovedTextLabel.ForeColor = Color.Gray;
            preApprovedTextLabel.Location = new Point(10, 35);
            preApprovedTextLabel.Name = "preApprovedTextLabel";
            preApprovedTextLabel.Size = new Size(153, 15);
            preApprovedTextLabel.TabIndex = 1;
            preApprovedTextLabel.Text = "Based on reported income";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 9F);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(445, 185);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 13;
            label2.Text = "50%";
            // 
            // loan_form_4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1278, 680);
            Controls.Add(progressPanel);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "loan_form_4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "loan_form_4";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            preApprovedPanel.ResumeLayout(false);
            preApprovedPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm loan_4;
        private Guna.UI2.WinForms.Guna2Panel headerPanel;

        private Guna.UI2.WinForms.Guna2Panel progressPanel;

        private Guna.UI2.WinForms.Guna2Panel contentPanel;
        private Label configureLoanLabel;
        private Label loanTermLabel;
        private Guna.UI2.WinForms.Guna2Button loanTerm12Button;
        private Guna.UI2.WinForms.Guna2Button loanTerm24Button;
        private Guna.UI2.WinForms.Guna2Button loanTerm36Button;
        private Label downPaymentLabel;
        private Label downPaymentMinLabel;
        private Guna.UI2.WinForms.Guna2TrackBar downPaymentSlider;
        private Label downPaymentMaxLabel;
        private Label monthlyPaymentLabel;
        private Label monthlyPaymentValueLabel;
        private Guna.UI2.WinForms.Guna2Panel preApprovedPanel;
        private Label preApprovedCheckLabel;
        private Label preApprovedTextLabel;
        private Guna.UI2.WinForms.Guna2Button backButton;
        private Guna.UI2.WinForms.Guna2Button continueButton;
        private Guna.UI2.WinForms.Guna2ImageButton button_loan_back_4;
        private Label label1;
        private Label label2;
    }
}