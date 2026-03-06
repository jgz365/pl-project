using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_4 : Form
    {
        private Guna.UI2.WinForms.Guna2Button selectedLoanTermButton = null;
        private Product currentProduct = null;
        private LoanApplicationSession loanSession = null;

        public loan_form_4()
        {
            InitializeComponent();
            SetupLoanTermButtons();
        }

        public loan_form_4(Product product) : this()
        {
            currentProduct = product;
            loanSession = new LoanApplicationSession { SelectedProduct = product };
            UpdateLoanTermButtons();
            try { monthlyPaymentValueLabel.Text = "₱0.00"; } catch { }
        }

        public loan_form_4(Product product, LoanApplicationSession session) : this()
        {
            currentProduct = product;
            loanSession = session ?? new LoanApplicationSession();
            if (loanSession.SelectedProduct == null) loanSession.SelectedProduct = product;
            UpdateLoanTermButtons();
            try { monthlyPaymentValueLabel.Text = "₱0.00"; } catch { }
            RestoreSelectedLoanTerm();
        }

        private void SetupLoanTermButtons()
        {
            // Setup loan term button clicks
            loanTermB1.Click += (s, e) => SelectLoanTerm(loanTermB1);
            loanTermB2.Click += (s, e) => SelectLoanTerm(loanTermB2);
            loanTermB3.Click += (s, e) => SelectLoanTerm(loanTermB3);

            // ensure continue is disabled until a selection is made
            try { continueButton.Enabled = false; } catch { }
        }

        private int GetLoanTermMonths(Guna.UI2.WinForms.Guna2Button button)
        {
            if (button == loanTermB1) return 6;
            if (button == loanTermB2) return 12;
            if (button == loanTermB3) return 24;
            return 0;
        }

        private decimal CalculateMonthlyPayment(int termMonths)
        {
            if (currentProduct == null || termMonths <= 0) return 0;

            decimal loanAmount = currentProduct.Price * 0.9m; // 10% down payment
            return Math.Round(loanAmount / termMonths, 2, MidpointRounding.AwayFromZero);
        }

        private void UpdateLoanTermButtons()
        {
            UpdateLoanTermButtonText(loanTermB1, 6);
            UpdateLoanTermButtonText(loanTermB2, 12);
            UpdateLoanTermButtonText(loanTermB3, 24);
        }

        private void UpdateLoanTermButtonText(Guna.UI2.WinForms.Guna2Button button, int termMonths)
        {
            decimal monthly = CalculateMonthlyPayment(termMonths);
            try { button.Text = $"₱{monthly:N2}/monthly for {termMonths} months"; } catch { }
        }

        private void RestoreSelectedLoanTerm()
        {
            if (!loanSession?.LoanTermMonths.HasValue ?? true) return;

            if (loanSession.LoanTermMonths.Value == 6) SelectLoanTerm(loanTermB1);
            else if (loanSession.LoanTermMonths.Value == 12) SelectLoanTerm(loanTermB2);
            else if (loanSession.LoanTermMonths.Value == 24) SelectLoanTerm(loanTermB3);
        }

        private void SelectLoanTerm(Guna.UI2.WinForms.Guna2Button button)
        {
            // If clicking the already selected button, deselect it
            if (selectedLoanTermButton == button)
            {
                selectedLoanTermButton.FillColor = Color.LightGray;
                selectedLoanTermButton.ForeColor = Color.DarkGray;
                selectedLoanTermButton = null;
                try { continueButton.Enabled = false; } catch { }
                try { monthlyPaymentValueLabel.Text = "₱0"; } catch { }
                if (loanSession != null)
                {
                    loanSession.LoanTermMonths = null;
                    loanSession.MonthlyPayment = null;
                }
                return;
            }

            // Deselect previous button
            if (selectedLoanTermButton != null)
            {
                selectedLoanTermButton.FillColor = Color.LightGray;
                selectedLoanTermButton.ForeColor = Color.DarkGray;
            }

            // Select new button
            selectedLoanTermButton = button;
            selectedLoanTermButton.FillColor = Color.RoyalBlue;
            selectedLoanTermButton.ForeColor = Color.White;
            try { continueButton.Enabled = true; } catch { }

            // Calculate monthly payment
            int term = GetLoanTermMonths(button);
            if (term > 0)
            {
                decimal monthly = CalculateMonthlyPayment(term);
                try { monthlyPaymentValueLabel.Text = $"₱{monthly:N2}"; } catch { }
                if (loanSession != null)
                {
                    loanSession.LoanTermMonths = term;
                    loanSession.MonthlyPayment = monthly;
                }
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var loan5 = new loan_form_5(currentProduct, loanSession);
            loan5.Show();
            this.Close();
        }

        private void button_loan_back_4_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail(currentProduct);
            product.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var back3 = new loan_form_3(currentProduct, loanSession);
            back3.Show();
            this.Close();
        }
    }
}
