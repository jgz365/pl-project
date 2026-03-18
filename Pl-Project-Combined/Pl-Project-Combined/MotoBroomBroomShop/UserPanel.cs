using customer_kiosk;

namespace MotoDealerShop
{
    public partial class UserPanel : Form
    {
        public UserPanel()
        {
            InitializeComponent();
        }

        private void CustomerKioskButton_Click(object sender, EventArgs e)
        {
            // 1. Create the new screen (from ck.cs)
            ck customerKiosk = new ck();

            // 2. Show the new screen
            customerKiosk.Show();

            // 3. Hide this UserPanel screen
            this.Hide();    
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {

        }

        private void MotoDealerShopLogo_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MyAccountButton_Click(object sender, EventArgs e)
        {
            UserPanel2 userPanel2 = new UserPanel2();
            userPanel2.Show();
            this.Hide();
        }
    }
}
