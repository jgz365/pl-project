// ═══════════════════════════════════════
// FILE: LoginUiForm.cs
// ═══════════════════════════════════════
using inventory_ni_Percie;
using System;
using System.Drawing;
using System.Windows.Forms;
using Assessor_Eddion;

namespace Pl_Project_Combined.Assessor_Eddion
{
    public partial class LoginUiForm : Form
    {
        private const float BASE_W = 1366f;
        private const float BASE_H = 768f;

        private const int PAN_W = 567, PAN_H = 610;
        private const int LOGO_X = 189, LOGO_Y = 34, LOGO_W = 209, LOGO_H = 177;
        private const int LBL2_Y = 228;
        private const int TB1_Y = 290, TB1_W = 507, TB1_H = 71;
        private const int TB2_Y = 365, TB2_W = 507, TB2_H = 71;
        private const int TOG_X = 33, TOG_Y = 443, TOG_W = 61, TOG_H = 31;
        private const int LBL1_Y = 443;
        private const int BTN_Y = 496, BTN_W = 507, BTN_H = 74;

        public LoginUiForm()
        {
            InitializeComponent();

            // ── Password masking — set in code to guarantee it works ──────
            txtPassword.PasswordChar = '*';
            txtPassword.UseSystemPasswordChar = false;

            // ── Enter key submits login from either field ─────────────────
            txtUsername.KeyDown += Field_KeyDown;
            txtPassword.KeyDown += Field_KeyDown;

            // ── Tab order ─────────────────────────────────────────────────
            txtUsername.TabIndex = 0;
            txtPassword.TabIndex = 1;
            btnLogin.TabIndex = 2;
        }

        // ════════════════════════════════════════════════════════════════════
        //  ENTER KEY — works on both text fields
        // ════════════════════════════════════════════════════════════════════
        private void Field_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;   // Prevents the Windows ding sound
                btnLogin.PerformClick();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  SHOW PASSWORD TOGGLE
        // ════════════════════════════════════════════════════════════════════
        private void togShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.PasswordChar = togShowPassword.Checked ? '\0' : '*';

            // Keep cursor at end after toggle
            txtPassword.SelectionStart = txtPassword.Text.Length;
            txtPassword.Focus();
        }

        private void lblShowPassword_Click(object sender, EventArgs e)
        {
            // Clicking the label also toggles the switch
            togShowPassword.Checked = !togShowPassword.Checked;
        }

        // ════════════════════════════════════════════════════════════════════
        //  LIVE VALIDATION — border turns red when field is empty
        // ════════════════════════════════════════════════════════════════════
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            txtUsername.FocusedState.BorderColor = string.IsNullOrWhiteSpace(txtUsername.Text)
                ? Color.FromArgb(220, 38, 38)
                : Color.FromArgb(94, 148, 255);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.FocusedState.BorderColor = string.IsNullOrWhiteSpace(txtPassword.Text)
                ? Color.FromArgb(220, 38, 38)
                : Color.FromArgb(94, 148, 255);
        }

        // ════════════════════════════════════════════════════════════════════
        //  LOGIN BUTTON
        // ════════════════════════════════════════════════════════════════════
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // ── Validations ───────────────────────────────────────────────
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                ShakeControl(btnLogin);
                MessageBox.Show("Please enter your username and password.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                ShakeControl(txtUsername);
                MessageBox.Show("Username is required.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShakeControl(txtPassword);
                MessageBox.Show("Password is required.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password.Length < 4)
            {
                ShakeControl(txtPassword);
                MessageBox.Show("Password must be at least 4 characters.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // ── Disable button while processing ───────────────────────────
            btnLogin.Enabled = false;
            btnLogin.Text = "Logging in...";

            try
            {
                UserModel? user = DatabaseManager.ValidateLogin(username, password);

                if (user == null)
                {
                    ShakeControl(btnLogin);
                    txtPassword.Text = "";      // Clear password on wrong attempt
                    txtPassword.Focus();
                    MessageBox.Show("Invalid username or password.\nPlease try again.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ── Block inactive / suspended accounts ───────────────────
                if (!user.Status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        $"Your account is {user.Status}.\nContact your administrator.",
                        "Account Restricted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DatabaseManager.UpdateLastLogin(user.Id);

                // ── Route to the correct form ─────────────────────────────
                Form? targetForm = user.Role switch
                {
                    "SuperAdmin" => new inventory_ni_Percie.Form1(),
                    "Admin" => new inventory_ni_Percie.Form2(),
                    "Assessor" => new Assessordeskeddion(),
                    "POSCashier" => new POSCashierSystem.POSCashier(),
                    "Inventory" => new inventory_ni_Percie.InventoryForm(),
                    _ => null
                };

                if (targetForm == null)
                {
                    MessageBox.Show(
                        $"Unknown role: '{user.Role}'.\nPlease contact your administrator.",
                        "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Hide();
                targetForm.FormClosed += (s, args) => Application.Exit();
                targetForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Cannot connect to database.\nMake sure XAMPP/MySQL is running.\n\nDetail: {ex.Message}",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "LOGIN";
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  SHAKE ANIMATION — visual error feedback
        // ════════════════════════════════════════════════════════════════════
        private static async void ShakeControl(Control ctrl)
        {
            Point orig = ctrl.Location;
            const int dist = 6;
            const int delay = 28;

            for (int i = 0; i < 3; i++)
            {
                ctrl.Location = new Point(orig.X - dist, orig.Y);
                await System.Threading.Tasks.Task.Delay(delay);
                ctrl.Location = new Point(orig.X + dist, orig.Y);
                await System.Threading.Tasks.Task.Delay(delay);
            }
            ctrl.Location = orig;
        }

        // ════════════════════════════════════════════════════════════════════
        //  RESPONSIVE LAYOUT  (don't touch — important)
        // ════════════════════════════════════════════════════════════════════
        private void CenterLayout()
        {
            int sw = this.ClientSize.Width;
            int sh = this.ClientSize.Height;
            if (sw < 100 || sh < 100) return;

            float s = Math.Min(sw / BASE_W, sh / BASE_H);

            guna2PictureBox1.Location = new Point(-30, 0);
            guna2PictureBox1.Size = new Size((int)(sw * 0.55f), sh);

            int panW = (int)(PAN_W * s);
            int panH = (int)(PAN_H * s);
            int rightStart = guna2PictureBox1.Width;
            int rightWidth = sw - rightStart;

            guna2Panel1.Size = new Size(panW, panH);
            guna2Panel1.Location = new Point(
                rightStart + (rightWidth - panW) / 2,
                (sh - panH) / 2);

            picLogo.Size = new Size((int)(LOGO_W * s), (int)(LOGO_H * s));
            picLogo.Location = new Point((panW - (int)(LOGO_W * s)) / 2, (int)(LOGO_Y * s));

            lblTitle.Font = new Font("Segoe UI", Math.Max(9f, 24f * s), FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(panW, (int)(60 * s));
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Location = new Point(0, (int)(LBL2_Y * s));

            int tbLeft = (panW - (int)(TB1_W * s)) / 2;

            txtUsername.Size = new Size((int)(TB1_W * s), (int)(TB1_H * s));
            txtUsername.Location = new Point(tbLeft, (int)(TB1_Y * s));
            txtUsername.IconLeftSize = new Size((int)(35 * s), (int)(35 * s));
            txtUsername.Font = new Font("Segoe UI", Math.Max(12f, 12f * s));

            txtPassword.Size = new Size((int)(TB2_W * s), (int)(TB2_H * s));
            txtPassword.Location = new Point(tbLeft, (int)(TB2_Y * s));
            txtPassword.IconLeftSize = new Size((int)(35 * s), (int)(35 * s));
            txtPassword.Font = new Font("Segoe UI", Math.Max(12f, 12f * s));

            togShowPassword.Size = new Size((int)(TOG_W * s), (int)(TOG_H * s));
            togShowPassword.Location = new Point(tbLeft, (int)(TOG_Y * s));

            lblShowPassword.Font = new Font("Segoe UI", Math.Max(7f, 12f * s), FontStyle.Bold);
            lblShowPassword.AutoSize = true;
            lblShowPassword.Location = new Point(
                tbLeft + (int)(TOG_W * s) + (int)(8 * s),
                (int)(LBL1_Y * s) + ((int)(TOG_H * s) - lblShowPassword.PreferredHeight) / 2);

            btnLogin.Size = new Size((int)(BTN_W * s), (int)(BTN_H * s));
            btnLogin.Location = new Point((panW - (int)(BTN_W * s)) / 2, (int)(BTN_Y * s));
            btnLogin.Font = new Font("Segoe UI Black", Math.Max(7f, 9f * s), FontStyle.Bold);
        }

        private void LoginUiForm_Load(object sender, EventArgs e)
        {
            guna2Elipse1.BorderRadius = 0;
            this.BeginInvoke(new Action(CenterLayout));
        }

        private void LoginUiForm_Resize(object sender, EventArgs e) => CenterLayout();

        private void picLogo_Click(object sender, EventArgs e) { }
    }
}