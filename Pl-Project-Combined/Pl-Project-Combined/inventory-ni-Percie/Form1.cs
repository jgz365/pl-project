namespace inventory_ni_Percie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ShowSpecsOverlay()
        {
            pnlSpecsContainer details = new pnlSpecsContainer();

            // 1. Force the size to match the main panel, not the whole form
            details.Size = pnlMainContainer.Size;
            details.Location = new Point(0, 0);

            // 2. ADD TO THE PANEL, not the Form (this is the most important part)
            pnlMainContainer.Controls.Add(details);

            // 3. Bring it to the top layer within that panel
            details.BringToFront();
        }
        // This is the universal 'Switcher' for your kiosk
        public void DisplayPage(UserControl uc)
        {
            // 1. Clear the main panel to remove the current view
            pnlMainContainer.Controls.Clear();

            // 2. Set the new UserControl to fill the space
            uc.Dock = DockStyle.Fill;

            // 3. Add and show it
            pnlMainContainer.Controls.Add(uc);
            uc.BringToFront();
        }
        private void logo_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            // 1. Create the control
            UC_Inventory uc = new UC_Inventory();

            // 2. Set it to fill the main display area
            uc.Dock = DockStyle.Fill;

            // 3. Clear the main panel and add the new one
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.Controls.Add(uc);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // 2. Ensure the panel itself is solid white
            pnlMainContainer.BackColor = Color.White;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // 2. Ensure the panel itself is solid white
            pnlMainContainer.BackColor = Color.White;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // 2. Ensure the panel itself is solid white
            pnlMainContainer.BackColor = Color.White;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // Uses your existing DisplayPage() switcher — no layout jump guaranteed
            DisplayPage(new UC_Users());
        }

        private void btnRepo_Click(object sender, EventArgs e)
        {
            // Load the Repossessions Overview screen
            DisplayPage(new MotoDealerShop.UC_Overview());
        }

        private void btnCollec_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // 2. Ensure the panel itself is solid white
            pnlMainContainer.BackColor = Color.White;
        }

        private void btnFinanRepo_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // 2. Ensure the panel itself is solid white
            pnlMainContainer.BackColor = Color.White;
        }

        private void btnSalesRep_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();

            // 2. Ensure the panel itself is solid white
            pnlMainContainer.BackColor = Color.White;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // 1. Confirmation Dialog
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                                                  "Logout Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                PerformLogout();
            }
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }
        // 1. Declare the remaining time at the top of your class
        // 1. Add this at the very top of your class (outside any methods)
        private int secondsLeft = 1200; // 20 minutes * 60 seconds

        private void tmrCountdown_Tick(object sender, EventArgs e)
        {
            if (secondsLeft > 0)
            {
                secondsLeft--;

                // Calculate and format the time
                int minutes = secondsLeft / 60;
                int seconds = secondsLeft % 60;
                lblTimer.Text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                // Fix for the red error line
                timer1.Stop();
                MessageBox.Show("Session Expired. Logging out...");
                PerformLogout();
            }
        }

        // 2. Create the missing method to resolve the error
        private void PerformLogout()
        {
            pnlMainContainer.Controls.Clear();

            var loginForm = new Pl_Project_Combined.Assessor_Eddion.LoginUiForm
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            loginForm.FormClosed += (_, __) =>
            {
                if (!IsDisposed)
                {
                    Close();
                }
            };

            Hide();
            loginForm.Show();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }

}