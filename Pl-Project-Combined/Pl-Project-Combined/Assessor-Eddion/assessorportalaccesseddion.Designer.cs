using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Assessor_Eddion
{
    partial class assessorportalaccesseddion
    {
        private IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            cardPanel = new Panel();
            guna2Button2 = new Guna2Button();
            headerPanel = new Panel();
            titleLabel = new Label();
            btnClose = new Guna2Button();
            guna2HtmlLabel2 = new Guna2HtmlLabel();
            guna2TextBox1 = new Guna2TextBox();
            guna2HtmlLabel3 = new Guna2HtmlLabel();
            guna2TextBox2 = new Guna2TextBox();
            guna2ButtonLogin = new Guna2Button();
            cardPanel.SuspendLayout();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.DarkSlateBlue;
            cardPanel.Controls.Add(guna2Button2);
            cardPanel.Controls.Add(headerPanel);
            cardPanel.Controls.Add(guna2HtmlLabel2);
            cardPanel.Controls.Add(guna2TextBox1);
            cardPanel.Controls.Add(guna2HtmlLabel3);
            cardPanel.Controls.Add(guna2TextBox2);
            cardPanel.Controls.Add(guna2ButtonLogin);
            cardPanel.Dock = DockStyle.Fill;
            cardPanel.Location = new Point(0, 0);
            cardPanel.Name = "cardPanel";
            cardPanel.Size = new Size(420, 303);
            cardPanel.TabIndex = 0;
            // 
            // guna2Button2
            // 
            guna2Button2.BackColor = Color.DimGray;
            guna2Button2.BorderThickness = 1;
            guna2Button2.CustomizableEdges = customizableEdges1;
            guna2Button2.FillColor = Color.Transparent;
            guna2Button2.FocusedColor = Color.Transparent;
            guna2Button2.Font = new Font("Segoe UI", 9F);
            guna2Button2.ForeColor = Color.Black;
            guna2Button2.Location = new Point(352, 173);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button2.Size = new Size(38, 40);
            guna2Button2.TabIndex = 5;
            guna2Button2.Text = "👁";
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(20, 28, 37);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(btnClose);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(420, 56);
            headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(44, 18);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(168, 20);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Assessor Portal Access";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BorderRadius = 6;
            btnClose.CustomizableEdges = customizableEdges3;
            btnClose.FillColor = Color.FromArgb(20, 28, 37);
            btnClose.Font = new Font("Segoe UI", 9F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(504, 14);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnClose.Size = new Size(34, 28);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            btnClose.Click += BtnClose_Click;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2HtmlLabel2.ForeColor = Color.Black;
            guna2HtmlLabel2.Location = new Point(30, 82);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(60, 17);
            guna2HtmlLabel2.TabIndex = 1;
            guna2HtmlLabel2.Text = "Username";
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BorderColor = Color.Black;
            guna2TextBox1.BorderRadius = 8;
            guna2TextBox1.CustomizableEdges = customizableEdges5;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.FillColor = Color.FromArgb(64, 64, 64);
            guna2TextBox1.Font = new Font("Segoe UI", 9F);
            guna2TextBox1.ForeColor = Color.White;
            guna2TextBox1.Location = new Point(30, 103);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PlaceholderForeColor = Color.FromArgb(180, 180, 180);
            guna2TextBox1.PlaceholderText = "Enter username";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2TextBox1.Size = new Size(360, 40);
            guna2TextBox1.TabIndex = 2;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2HtmlLabel3.ForeColor = Color.Black;
            guna2HtmlLabel3.Location = new Point(30, 152);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(55, 17);
            guna2HtmlLabel3.TabIndex = 3;
            guna2HtmlLabel3.Text = "Password";
            // 
            // guna2TextBox2
            // 
            guna2TextBox2.BorderColor = Color.Black;
            guna2TextBox2.BorderRadius = 8;
            guna2TextBox2.CustomizableEdges = customizableEdges7;
            guna2TextBox2.DefaultText = "";
            guna2TextBox2.FillColor = Color.FromArgb(64, 64, 64);
            guna2TextBox2.Font = new Font("Segoe UI", 9F);
            guna2TextBox2.ForeColor = Color.White;
            guna2TextBox2.Location = new Point(30, 173);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PasswordChar = '●';
            guna2TextBox2.PlaceholderForeColor = Color.FromArgb(180, 180, 180);
            guna2TextBox2.PlaceholderText = "Enter password";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2TextBox2.Size = new Size(360, 40);
            guna2TextBox2.TabIndex = 4;
            // 
            // guna2ButtonLogin
            // 
            guna2ButtonLogin.BorderRadius = 12;
            guna2ButtonLogin.CustomizableEdges = customizableEdges9;
            guna2ButtonLogin.FillColor = Color.FromArgb(20, 28, 37);
            guna2ButtonLogin.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            guna2ButtonLogin.ForeColor = Color.White;
            guna2ButtonLogin.Location = new Point(30, 233);
            guna2ButtonLogin.Name = "guna2ButtonLogin";
            guna2ButtonLogin.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2ButtonLogin.Size = new Size(360, 44);
            guna2ButtonLogin.TabIndex = 6;
            guna2ButtonLogin.Text = "Login";
            // 
            // assessorportalaccesseddion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(420, 303);
            Controls.Add(cardPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "assessorportalaccesseddion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Assessor Portal Access";
            cardPanel.ResumeLayout(false);
            cardPanel.PerformLayout();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel cardPanel;
        private Panel headerPanel;
        private Label titleLabel;
        private Guna2Button btnClose;
        private Guna2HtmlLabel guna2HtmlLabel2;
        private Guna2TextBox guna2TextBox1;
        private Guna2HtmlLabel guna2HtmlLabel3;
        private Guna2TextBox guna2TextBox2;
        private Guna2Button guna2Button2;
        private Guna2Button guna2ButtonLogin;

      protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}