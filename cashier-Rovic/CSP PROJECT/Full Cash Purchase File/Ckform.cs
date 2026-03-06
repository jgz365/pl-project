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
            // Fire CloseRequested so POSCashier can restore the main view,
            // then remove this control from its parent — same as AdvancePaymentForm
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }

        private void button_view_test_Click(object sender, EventArgs e)
        {
            // TODO: Hook up your CSP PROJECT product detail form here
        }
    }
}