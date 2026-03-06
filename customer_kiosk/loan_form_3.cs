<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_3 : Form
    {
        private Product currentProduct = null;
        private LoanApplicationSession loanSession = null;

        public loan_form_3()
        {
            InitializeComponent();
            WireSelectionPersistence();
        }

        public loan_form_3(Product product) : this()
        {
            currentProduct = product;
            loanSession = new LoanApplicationSession { SelectedProduct = product };
            PopulateSavedSelections();
        }

        public loan_form_3(Product product, LoanApplicationSession session) : this()
        {
            currentProduct = product;
            loanSession = session ?? new LoanApplicationSession();
            if (loanSession.SelectedProduct == null) loanSession.SelectedProduct = product;
            PopulateSavedSelections();
        }

        private void WireSelectionPersistence()
        {
            try
            {
                incomeCombobox.SelectedIndexChanged += (s, e) =>
                {
                    if (loanSession == null) return;
                    loanSession.SourceOfIncome = incomeCombobox?.SelectedItem?.ToString() ?? incomeCombobox?.Text;
                };
            }
            catch { }

            try
            {
                moneyCombobox.SelectedIndexChanged += (s, e) =>
                {
                    if (loanSession == null) return;
                    loanSession.MonthlyGrossIncomeRange = moneyCombobox?.SelectedItem?.ToString() ?? moneyCombobox?.Text;
                };
            }
            catch { }
        }

        private void PopulateSavedSelections()
        {
            if (loanSession == null) return;

            try
            {
                if (!string.IsNullOrWhiteSpace(loanSession.SourceOfIncome))
                {
                    incomeCombobox.SelectedItem = loanSession.SourceOfIncome;
                    if (incomeCombobox.SelectedIndex < 0) incomeCombobox.Text = loanSession.SourceOfIncome;
                }
            }
            catch { }

            try
            {
                if (!string.IsNullOrWhiteSpace(loanSession.MonthlyGrossIncomeRange))
                {
                    moneyCombobox.SelectedItem = loanSession.MonthlyGrossIncomeRange;
                    if (moneyCombobox.SelectedIndex < 0) moneyCombobox.Text = loanSession.MonthlyGrossIncomeRange;
                }
            }
            catch { }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            loanSession ??= new LoanApplicationSession();
            loanSession.SelectedProduct = currentProduct;
            loanSession.SourceOfIncome = incomeCombobox?.SelectedItem?.ToString() ?? incomeCombobox?.Text;
            loanSession.MonthlyGrossIncomeRange = moneyCombobox?.SelectedItem?.ToString() ?? moneyCombobox?.Text;

            var loan4 = new loan_form_4(currentProduct, loanSession);
            loan4.Show();
            this.Close();
        }

        private void button_loan_back_3_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail(currentProduct);
            product.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var back2 = new loan_form_2(currentProduct, loanSession);
            back2.Show();
            this.Close();
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_3 : Form
    {
        public loan_form_3()
        {
            InitializeComponent();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var loan4 = new loan_form_4();
            loan4.Show();
            this.Close();
        }

        private void button_loan_back_3_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var back2 = new loan_form_2();
            back2.Show();
            this.Close();
        }
    }
}
>>>>>>> 76e9872bf621d0cf86062814b6d214c8db3f7103
