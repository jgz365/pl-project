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

        public loan_form_4()
        {
            InitializeComponent();
            SetupLoanTermButtons();
        }

        private void SetupLoanTermButtons()
        {
            // Setup loan term button clicks
            loanTerm12Button.Click += (s, e) => SelectLoanTerm(loanTerm12Button);
            loanTerm24Button.Click += (s, e) => SelectLoanTerm(loanTerm24Button);
            loanTerm36Button.Click += (s, e) => SelectLoanTerm(loanTerm36Button);
        }

        private void SelectLoanTerm(Guna.UI2.WinForms.Guna2Button button)
        {
            // If clicking the already selected button, deselect it
            if (selectedLoanTermButton == button)
            {
                selectedLoanTermButton.FillColor = Color.LightGray;
                selectedLoanTermButton.ForeColor = Color.DarkGray;
                selectedLoanTermButton = null;
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
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            loan_form_5 form_5 = new loan_form_5();
            form_5.ShowDialog();
            this.Show();
        }

        private void button_loan_back_4_Click(object sender, EventArgs e)
        {
            this.Hide();
            kiosk_product_detail product_detail = new kiosk_product_detail();
            product_detail.ShowDialog();
            this.Show();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
