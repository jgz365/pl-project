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
