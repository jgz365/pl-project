namespace CSP_PROJECT
{
    public partial class CkForm : UserControl
    {
        // ── Same pattern as AdvancePaymentForm ────────────────────────────────
        public event EventHandler CloseRequested;

        public CkForm()
        {
            InitializeComponent();
        }

        private void ck_kiosk_clock_Tick(object sender, EventArgs e)
        {
            ck_clock_label.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void ckg_exit_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }

        // ─────────────────────────────────────────────────────────────────────
        // View Product — loads kiosk_product_detail as a UserControl
        // into the same parent container (pnlBackground in POSCashier)
        // ─────────────────────────────────────────────────────────────────────
        private void button_view_test_Click(object sender, EventArgs e)
        {
            if (this.Parent == null) return;

            var parent = this.Parent;

            // Remove any stale instance
            for (int i = parent.Controls.Count - 1; i >= 0; i--)
            {
                if (parent.Controls[i] is kiosk_product_detail)
                    parent.Controls.RemoveAt(i);
            }

            var productUC = new kiosk_product_detail
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Margin = new Padding(0)
            };

            // When the product detail wants to go back → restore this CkForm
            productUC.CloseRequested += (s, args) =>
            {
                parent.Controls.Remove(productUC);
                productUC.Dispose();
                this.Visible = true;
                this.BringToFront();
                this.Focus();
            };

            this.Visible = false;
            parent.Controls.Add(productUC);
            productUC.BringToFront();
            productUC.Focus();
        }
    }
}