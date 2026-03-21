using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pl_Project_Combined.Assessor_Eddion
{
    public partial class LoginUiForm : Form
    {
        
        private const float BASE_W = 1366f;
        private const float BASE_H = 768f;

        
        private const int PAN_W = 567, PAN_H = 610;

        private const int LOGO_X = 189, LOGO_Y = 34, LOGO_W = 209, LOGO_H = 177;
        private const int LBL2_X = 163, LBL2_Y = 228;
        private const int TB1_X = 33, TB1_Y = 290, TB1_W = 507, TB1_H = 71;
        private const int TB2_X = 33, TB2_Y = 365, TB2_W = 507, TB2_H = 71;
        private const int TOG_X = 33, TOG_Y = 443, TOG_W = 61, TOG_H = 31;
        private const int LBL1_X = 100, LBL1_Y = 443;
        private const int BTN_X = 33, BTN_Y = 496, BTN_W = 507, BTN_H = 74;

        public LoginUiForm()
        {
            InitializeComponent();
        }

        private void CenterLayout()
        {
            int sw = this.ClientSize.Width;
            int sh = this.ClientSize.Height;
            if (sw < 100 || sh < 100) return;

            float sx = sw / BASE_W;
            float sy = sh / BASE_H;
            float s = Math.Min(sx, sy);

            // Background image
            guna2PictureBox1.Location = new Point(-30, 0);
            guna2PictureBox1.Size = new Size((int)(sw * 0.55f), sh);

            // Panel
            int panW = (int)(PAN_W * s);
            int panH = (int)(PAN_H * s);
            int rightStart = guna2PictureBox1.Width;
            int rightWidth = sw - rightStart;

            guna2Panel1.Size = new Size(panW, panH);
            guna2Panel1.Location = new Point(
                rightStart + (rightWidth - panW) / 2,
                             (sh - panH) / 2);

            // Motorcycle logo
            picLogo.Size = new Size((int)(LOGO_W * s), (int)(LOGO_H * s));
            picLogo.Location = new Point((panW - (int)(LOGO_W * s)) / 2, (int)(LOGO_Y * s));

            // USER LOGIN label
            lblTitle.Font = new Font("Segoe UI", Math.Max(9f, 24f * s), FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(panW, (int)(60 * s));
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Location = new Point(0, (int)(LBL2_Y * s));

            // Username textbox
            txtUsername.Size = new Size((int)(TB1_W * s), (int)(TB1_H * s));
            txtUsername.Location = new Point((panW - (int)(TB1_W * s)) / 2, (int)(TB1_Y * s));
            txtUsername.IconLeftSize = new Size((int)(35 * s), (int)(35 * s));
            txtUsername.Font = new Font("Segoe UI", Math.Max(12f, 12f * s));

            // Password textbox
            txtPassword.Size = new Size((int)(TB2_W * s), (int)(TB2_H * s));
            txtPassword.Location = new Point((panW - (int)(TB2_W * s)) / 2, (int)(TB2_Y * s));
            txtPassword.IconLeftSize = new Size((int)(35 * s), (int)(35 * s));
            txtPassword.Font = new Font("Segoe UI", Math.Max(12f, 12f * s));

            // Toggle switch
            int tbLeft = (panW - (int)(TB1_W * s)) / 2;
            togShowPassword.Size = new Size((int)(TOG_W * s), (int)(TOG_H * s));
            togShowPassword.Location = new Point(tbLeft, (int)(TOG_Y * s));

            // Show Password label
            lblShowPassword.Font = new Font("Segoe UI", Math.Max(7f, 12f * s), FontStyle.Bold);
            lblShowPassword.AutoSize = true;
            lblShowPassword.Location = new Point(
                tbLeft + (int)(TOG_W * s) + (int)(8 * s),
                (int)(LBL1_Y * s) + ((int)(TOG_H * s) - lblShowPassword.PreferredHeight) / 2);

            // LOGIN button
            btnLogin.Size = new Size((int)(BTN_W * s), (int)(BTN_H * s));
            btnLogin.Location = new Point((panW - (int)(BTN_W * s)) / 2, (int)(BTN_Y * s));
            btnLogin.Font = new Font("Segoe UI Black", Math.Max(7f, 9f * s), FontStyle.Bold);
        }

        private void LoginUiForm_Load(object sender, EventArgs e)
        {
            guna2Elipse1.BorderRadius = 0;
            this.BeginInvoke(new Action(CenterLayout));
        }

        private void LoginUiForm_Resize(object sender, EventArgs e)
        {
            CenterLayout(); // don't touch this is important shit
        }

        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }

        private void lblShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !togShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e) { }
        private void picLogo_Click(object sender, EventArgs e) { }

        private void togShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !togShowPassword.Checked;
        }
    }
}