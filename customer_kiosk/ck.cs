namespace customer_kiosk
{
    public partial class ck : Form
    {
        public ck()
        {
            InitializeComponent();
        }

        private void ck_kiosk_clock_Tick(object sender, EventArgs e)
        {
            ck_clock_label.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void ckg_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_view_test_Click(object sender, EventArgs e)
        {
            this.Hide();
            kiosk_product_detail product_Detail = new kiosk_product_detail();
            product_Detail.ShowDialog();
            this.Show();
        }
    }
}
