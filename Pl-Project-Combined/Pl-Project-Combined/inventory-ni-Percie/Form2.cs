// ═══════════════════════════════════════
// FILE: Form2.cs
// ═══════════════════════════════════════
using System;
using System.Drawing;
using System.Windows.Forms;

namespace inventory_ni_Percie
{
    /// <summary>
    /// Admin / Manager panel — identical to Form1 but WITHOUT the Users button.
    /// Admins can see: Dashboard, Inventory, Customers, Sales Reports,
    /// Financial Reports, Repossessions, Collections, Settings, Logout.
    /// </summary>
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // ── Universal page switcher (same pattern as Form1) ──────────────────
        public void DisplayPage(UserControl uc)
        {
            pnlMainContainer.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlMainContainer.Controls.Add(uc);
            uc.BringToFront();
        }

        // ── Sidebar navigation ───────────────────────────────────────────────
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.BackColor = System.Drawing.Color.White;
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            UC_Inventory uc = new UC_Inventory();
            uc.Dock = DockStyle.Fill;
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.Controls.Add(uc);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.BackColor = System.Drawing.Color.White;
        }

        private void btnSalesRep_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.BackColor = System.Drawing.Color.White;
        }

        private void btnFinanRepo_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.BackColor = System.Drawing.Color.White;
        }

        private void btnCollec_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.BackColor = System.Drawing.Color.White;
        }

        private void btnRepo_Click(object sender, EventArgs e)
        {
            // Load the Repossessions Overview screen
            DisplayPage(new MotoDealerShop.UC_Overview());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlMainContainer.Controls.Clear();
            pnlMainContainer.BackColor = System.Drawing.Color.White;
        }

        // ── Logout ───────────────────────────────────────────────────────────
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ── Session countdown timer ──────────────────────────────────────────
        private int secondsLeft = 1200; // 20 minutes

        private void tmrCountdown_Tick(object sender, EventArgs e)
        {
            if (secondsLeft > 0)
            {
                secondsLeft--;
                int minutes = secondsLeft / 60;
                int seconds = secondsLeft % 60;
                lblTimer.Text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Session Expired. Logging out...");
                PerformLogout();
            }
        }

        private void PerformLogout()
        {
            pnlMainContainer.Controls.Clear();
            Application.Exit();
        }

        private void guna2Panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
        private void guna2Panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
        private void logo_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel2_Click(object sender, EventArgs e) { }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e) { }
        private void lblTimer_Click(object sender, EventArgs e) { }

        private void logo_Click_1(object sender, EventArgs e)
        {

        }
    }
}