using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CSP_PROJECT
{
    /// <summary>
    /// UserControl — Cash Purchase Confirmation (modernized).
    /// New features:
    ///   • Recommended Add-ons (LTO Registration + TPL Insurance) with live price update
    ///   • Transaction Note panel
    ///   • On Confirm → opens CollectionForm5 (cashier payment screen) via panel-swap,
    ///     passing customer name, charges breakdown, and total.
    /// </summary>
    public partial class ck_confirm_payment : UserControl
    {
        // ── Navigation event (back to kiosk_product_detail) ──────────────────
        public event EventHandler CloseRequested;

        // ── Add-on prices (easily configurable) ──────────────────────────────
        private const decimal BASE_UNIT_PRICE = 151_900m;
        private const decimal DOC_STAMP_TAX = 600m;
        private const decimal LTO_REG_PRICE = 1_500m;
        private const decimal TPL_INSURANCE_PRICE = 950m;

        private bool _ltoChecked = true;
        private bool _tplChecked = true;

        public ck_confirm_payment()
        {
            InitializeComponent();
            WireAddOnEvents();
            RefreshTotal();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Add-on toggle logic
        // ─────────────────────────────────────────────────────────────────────
        private void WireAddOnEvents()
        {
            // Clicking anywhere on the LTO card toggles the checkbox
            ltoPanel.Click += (s, e) => ToggleLTO();
            ltoCheck.Click += (s, e) => ToggleLTO();
            ltoNameLabel.Click += (s, e) => ToggleLTO();
            ltoDescLabel.Click += (s, e) => ToggleLTO();
            ltoPriceLabel.Click += (s, e) => ToggleLTO();

            tplPanel.Click += (s, e) => ToggleTPL();
            tplCheck.Click += (s, e) => ToggleTPL();
            tplNameLabel.Click += (s, e) => ToggleTPL();
            tplDescLabel.Click += (s, e) => ToggleTPL();
            tplPriceLabel.Click += (s, e) => ToggleTPL();
        }

        private void ToggleLTO()
        {
            _ltoChecked = !_ltoChecked;
            ltoCheck.Checked = _ltoChecked;
            ApplyAddonStyle(ltoPanel, ltoCheck, _ltoChecked);
            RefreshTotal();
        }

        private void ToggleTPL()
        {
            _tplChecked = !_tplChecked;
            tplCheck.Checked = _tplChecked;
            ApplyAddonStyle(tplPanel, tplCheck, _tplChecked);
            RefreshTotal();
        }

        private static void ApplyAddonStyle(Panel panel, CheckBox check, bool active)
        {
            panel.BackColor = active
                ? Color.FromArgb(236, 253, 245)   // light green
                : Color.FromArgb(248, 250, 252);  // cool gray
            check.ForeColor = active ? Color.FromArgb(5, 150, 105) : Color.Gray;
        }

        private void RefreshTotal()
        {
            decimal total = BASE_UNIT_PRICE + DOC_STAMP_TAX;
            if (_ltoChecked) total += LTO_REG_PRICE;
            if (_tplChecked) total += TPL_INSURANCE_PRICE;

            // Update breakdown rows visibility
            ltoBreakdownRow.Visible = _ltoChecked;
            tplBreakdownRow.Visible = _tplChecked;
            ltoBreakdownAmt.Visible = _ltoChecked;
            tplBreakdownAmt.Visible = _tplChecked;

            totalAmountValue.Text = $"₱{total:N0}";
        }

        private decimal ComputeTotal()
        {
            decimal total = BASE_UNIT_PRICE + DOC_STAMP_TAX;
            if (_ltoChecked) total += LTO_REG_PRICE;
            if (_tplChecked) total += TPL_INSURANCE_PRICE;
            return total;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Confirm → open CollectionForm5 (cashier screen)
        // ─────────────────────────────────────────────────────────────────────
        private void confirmButton_Click(object sender, EventArgs e)
        {
            string customerName = fullNameTextBox.Text.Trim();
            string mobileNumber = mobileNumberTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Please enter the customer's full name.", "Required Field",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fullNameTextBox.Focus();
                return;
            }

            // Build the charges list to pass to CollectionForm5
            var charges = new List<(string Label, decimal Amount)>
            {
                ($"Unit: {productNameLabel.Text}", BASE_UNIT_PRICE),
                ("Documentary Stamp Tax", DOC_STAMP_TAX),
            };
            if (_ltoChecked) charges.Add(("LTO Registration (3 Yrs)", LTO_REG_PRICE));
            if (_tplChecked) charges.Add(("TPL Insurance (1 Yr)", TPL_INSURANCE_PRICE));

            decimal totalDue = ComputeTotal();

            if (this.Parent == null) return;
            var parent = this.Parent;

            // Remove any existing CollectionForm5
            for (int i = parent.Controls.Count - 1; i >= 0; i--)
                if (parent.Controls[i] is CollectionForm5)
                    parent.Controls.RemoveAt(i);

            var col5 = new CollectionForm5();
            col5.SetData(customerName, mobileNumber, charges, totalDue);
            col5.Dock = DockStyle.Fill;
            col5.Location = new Point(0, 0);
            col5.Margin = new Padding(0);

            col5.BackRequested += (s, args) =>
            {
                parent.Controls.Remove(col5);
                col5.Dispose();
                this.Visible = true;
                this.BringToFront();
                this.Focus();
            };

            this.Visible = false;
            parent.Controls.Add(col5);
            col5.BringToFront();
            col5.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Back button
        // ─────────────────────────────────────────────────────────────────────
        private void ckg_back_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }
    }
}