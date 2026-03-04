using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace POSCashierSystem
{
    public partial class POSCashier : Form
    {
        // Original colors for cards
        private readonly Color cardDefaultBorderColor = Color.FromArgb(224, 224, 224);
        private readonly Color cardDefaultBackColor = Color.White;
        private readonly Color cardHoverBackColor = Color.FromArgb(250, 252, 253);

        // Accent colors for borders on hover
        private readonly Color downPaymentAccent = Color.FromArgb(102, 217, 189);
        private readonly Color monthlyPaymentAccent = Color.FromArgb(138, 171, 255);
        private readonly Color fullCashAccent = Color.FromArgb(188, 140, 255);
        private readonly Color otherServicesAccent = Color.FromArgb(255, 186, 115);
        private readonly Color advancePaymentAccent = Color.FromArgb(102, 217, 189);
        private readonly Color fullSettlementAccent = Color.FromArgb(255, 138, 138);

        public POSCashier()
        {
            InitializeComponent();
            InitializeCardHoverEffects();
        }

        private void InitializeCardHoverEffects()
        {
            AttachHoverEffect(pnlDownPayment, downPaymentAccent);
            AttachHoverEffect(pnlMonthlyPayment, monthlyPaymentAccent);
            AttachHoverEffect(pnlFullCash, fullCashAccent);
            AttachHoverEffect(pnlOtherServices, otherServicesAccent);
            AttachHoverEffect(pnlAdvancePayment, advancePaymentAccent);
            AttachHoverEffect(pnlFullSettlement, fullSettlementAccent);
        }

        private void AttachHoverEffect(Guna2Panel panel, Color accentColor)
        {
            panel.MouseEnter += (s, e) =>
            {
                panel.BorderColor = accentColor;
                panel.FillColor = cardHoverBackColor;
                panel.ShadowDecoration.Enabled = true;
                panel.ShadowDecoration.Shadow = new Padding(0, 2, 8, 8);
                panel.ShadowDecoration.Color = Color.FromArgb(50, accentColor);
                panel.Cursor = Cursors.Hand;
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BorderColor = cardDefaultBorderColor;
                panel.FillColor = cardDefaultBackColor;
                panel.ShadowDecoration.Enabled = false;
                panel.Cursor = Cursors.Default;
            };

            foreach (Control ctrl in panel.Controls)
            {
                ctrl.MouseEnter += (s, e) =>
                {
                    panel.BorderColor = accentColor;
                    panel.FillColor = cardHoverBackColor;
                    panel.ShadowDecoration.Enabled = true;
                    panel.ShadowDecoration.Shadow = new Padding(0, 2, 8, 8);
                    panel.ShadowDecoration.Color = Color.FromArgb(50, accentColor);
                };

                ctrl.MouseLeave += (s, e) =>
                {
                    if (!panel.ClientRectangle.Contains(panel.PointToClient(Cursor.Position)))
                    {
                        panel.BorderColor = cardDefaultBorderColor;
                        panel.FillColor = cardDefaultBackColor;
                        panel.ShadowDecoration.Enabled = false;
                    }
                };
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Down Payment click
        // ─────────────────────────────────────────────────────────────────────
        private void PnlDownPayment_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);

            for (int i = pnlBackground.Controls.Count - 1; i >= 0; i--)
            {
                if (pnlBackground.Controls[i] is DownPaymentForm)
                    pnlBackground.Controls.RemoveAt(i);
            }

            var dpControl = new DownPaymentForm
            {
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Dock = DockStyle.Fill
            };

            dpControl.CloseRequested += (s, args) => RestoreMainView();

            pnlBackground.Controls.Add(dpControl);
            dpControl.BringToFront();
            dpControl.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Monthly Payment click
        // ─────────────────────────────────────────────────────────────────────
        private void PnlMonthlyPayment_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);

            for (int i = pnlBackground.Controls.Count - 1; i >= 0; i--)
            {
                if (pnlBackground.Controls[i] is MonthlyPaymentForm)
                    pnlBackground.Controls.RemoveAt(i);
            }

            var mpControl = new MonthlyPaymentForm
            {
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Dock = DockStyle.Fill
            };

            mpControl.CloseRequested += (s, args) => RestoreMainView();

            pnlBackground.Controls.Add(mpControl);
            mpControl.BringToFront();
            mpControl.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Full Cash Purchase click  ←  NOW LOADS CkForm
        // ─────────────────────────────────────────────────────────────────────
        private void PnlFullCash_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);

            // Remove any existing CkForm instance first
            for (int i = pnlBackground.Controls.Count - 1; i >= 0; i--)
            {
                if (pnlBackground.Controls[i] is CSP_PROJECT.CkForm)
                    pnlBackground.Controls.RemoveAt(i);
            }

            var ckControl = new CSP_PROJECT.CkForm
            {
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Dock = DockStyle.Fill
            };

            ckControl.CloseRequested += (s, args) => RestoreMainView();

            pnlBackground.Controls.Add(ckControl);
            ckControl.BringToFront();
            ckControl.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Advance Payment click
        // ─────────────────────────────────────────────────────────────────────
        private void PnlAdvancePayment_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);

            for (int i = pnlBackground.Controls.Count - 1; i >= 0; i--)
            {
                if (pnlBackground.Controls[i] is AdvancePaymentForm)
                    pnlBackground.Controls.RemoveAt(i);
            }

            var apControl = new AdvancePaymentForm
            {
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Dock = DockStyle.Fill
            };

            apControl.CloseRequested += (s, args) => RestoreMainView();

            pnlBackground.Controls.Add(apControl);
            apControl.BringToFront();
            apControl.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Full Settlement click
        // ─────────────────────────────────────────────────────────────────────
        private void PnlFullSettlement_Click(object sender, EventArgs e)
        {
            SetMainViewVisible(false);

            for (int i = pnlBackground.Controls.Count - 1; i >= 0; i--)
            {
                if (pnlBackground.Controls[i] is FullSettlementForm)
                    pnlBackground.Controls.RemoveAt(i);
            }

            var fsControl = new FullSettlementForm
            {
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Dock = DockStyle.Fill
            };

            fsControl.CloseRequested += (s, args) => RestoreMainView();

            pnlBackground.Controls.Add(fsControl);
            fsControl.BringToFront();
            fsControl.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────
        // PUBLIC — called by ReceiptForm to restore the main POS view
        // ─────────────────────────────────────────────────────────────────────
        public void RestoreMainView()
        {
            var toRemove = new System.Collections.Generic.List<Control>();

            foreach (Control ctrl in pnlBackground.Controls)
            {
                if (ctrl is UserControl)
                    toRemove.Add(ctrl);
            }

            foreach (Control ctrl in toRemove)
            {
                pnlBackground.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            SetMainViewVisible(true);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Single helper — show/hide every main-view element together
        // ─────────────────────────────────────────────────────────────────────
        private void SetMainViewVisible(bool visible)
        {
            pnlDownPayment.Visible = visible;
            pnlMonthlyPayment.Visible = visible;
            pnlFullCash.Visible = visible;
            pnlOtherServices.Visible = visible;
            pnlAdvancePayment.Visible = visible;
            pnlFullSettlement.Visible = visible;

            pnlNoTransactions.Visible = visible;
            lblNewTransaction.Visible = visible;
            guna2CirclePictureBox3.Visible = visible;
            lblRecentTransactions.Visible = visible;
            lblViewAll.Visible = visible;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Other card clicks (stubbed)
        // ─────────────────────────────────────────────────────────────────────
        private void PnlOtherServices_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Other Services module selected.\n\nParts, Service, Registration Fees.",
                "Other Services", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCloseRegister_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to close the register?",
                "Close Register", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void LblViewAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "View All Transactions functionality will be implemented here.",
                "Recent Transactions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void POSCashier_Load(object sender, EventArgs e)
        {
            UpdateStatusDisplay();
        }

        private void UpdateStatusDisplay()
        {
            lblExpires.Text = $"Expires: {DateTime.Now.AddHours(2):HH:mm}";
        }
    }
}