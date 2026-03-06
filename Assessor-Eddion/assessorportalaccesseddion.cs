using System;
using System.Windows.Forms;

namespace Assessor_Eddion
{
    public partial class assessorportalaccesseddion : Form
    {
        public assessorportalaccesseddion()
        {
            InitializeComponent();

            // Wire up events
            this.guna2Button2.Click += guna2Button2_Click;         // password toggle
            this.guna2ButtonLogin.Click += guna2ButtonLogin_Click; // login button
            this.guna2TextBox1.KeyDown += guna2TextBox1_KeyDown;   // tab focus

            // Enable form to capture key presses
            this.KeyPreview = true;
            this.KeyDown += assessorportalaccesseddion_KeyDown;
        }

        // Password toggle button
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox2.PasswordChar = guna2TextBox2.PasswordChar == '\0' ? '●' : '\0';
        }

        // Shared login logic
        private void DoLogin()
        {
            string username = guna2TextBox1.Text; // username textbox
            string password = guna2TextBox2.Text; // password textbox

            // Replace with real DB validation later
            if (username == "admin" && password == "1234")
            {
                this.Hide(); // hide login form

                // open Assessor Desk
                var desk = new Assessordeskeddion();
                desk.ShowDialog();

                this.Close(); // close login after desk closes
            }
            else
            {
                MessageBox.Show("Invalid username or password.",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // Login button click
        private void guna2ButtonLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        // Enter key press (anywhere on form)
        private void assessorportalaccesseddion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoLogin();
                e.Handled = true;
            }
        }

        // Tab key press (inside Username textbox)
        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                guna2TextBox2.Focus(); // move cursor to Password textbox
                e.Handled = true;
            }
        }
    }
}
