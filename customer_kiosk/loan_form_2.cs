using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class loan_form_2 : Form
    {
        public loan_form_2()
        {
            InitializeComponent();
            // ensure validation labels are hidden at runtime initially
            try { firstNameErrorLabel.Visible = false; } catch { }
            try { lastNameErrorLabel.Visible = false; } catch { }
            try { mobileErrorLabel.Visible = false; } catch { }
            try { emailErrorLabel.Visible = false; } catch { }
            try { dobErrorLabel.Visible = false; } catch { }
            try { addressErrorLabel.Visible = false; } catch { }
            try { employedErrorLabel.Visible = false; } catch { }
            try { companyErrorLabel.Visible = false; } catch { }
            try { positionErrorLabel.Visible = false; } catch { }
            try { yearsErrorLabel.Visible = false; } catch { }
            // wire simple input helpers
            try { mobileNumberTextBox.KeyPress += MobileNumberTextBox_KeyPress; } catch { }
            try { mobileNumberTextBox.TextChanged += MobileNumberTextBox_TextChanged; } catch { }
            // hide error labels when user edits fields
            try { firstNameTextBox.TextChanged += (s, e) => firstNameErrorLabel.Visible = false; } catch { }
            try { lastNameTextBox.TextChanged += (s, e) => lastNameErrorLabel.Visible = false; } catch { }
            try { mobileNumberTextBox.TextChanged += (s, e) => mobileErrorLabel.Visible = false; } catch { }
            try { emailTextBox.TextChanged += (s, e) => emailErrorLabel.Visible = false; } catch { }
            try { dobPicker.ValueChanged += (s, e) => dobErrorLabel.Visible = false; } catch { }
            try { completeAddressTextBox.TextChanged += (s, e) => addressErrorLabel.Visible = false; } catch { }
            try { employedComboBox.SelectedIndexChanged += (s, e) => employedErrorLabel.Visible = false; } catch { }
            try { companyNameTextBox.TextChanged += (s, e) => companyErrorLabel.Visible = false; } catch { }
            try { positionComboBox.SelectedIndexChanged += (s, e) => positionErrorLabel.Visible = false; } catch { }
            try { yearsEmployedTextBox.TextChanged += (s, e) => yearsErrorLabel.Visible = false; } catch { }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            // inline validation: hide all error labels first
            try { firstNameErrorLabel.Visible = false; } catch { }
            try { lastNameErrorLabel.Visible = false; } catch { }
            try { mobileErrorLabel.Visible = false; } catch { }
            try { emailErrorLabel.Visible = false; } catch { }
            try { dobErrorLabel.Visible = false; } catch { }
            try { addressErrorLabel.Visible = false; } catch { }
            try { employedErrorLabel.Visible = false; } catch { }
            try { companyErrorLabel.Visible = false; } catch { }
            try { positionErrorLabel.Visible = false; } catch { }
            try { yearsErrorLabel.Visible = false; } catch { }

            bool hasError = false;

            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
            {
                try { firstNameErrorLabel.Text = "Please enter first name."; firstNameErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(lastNameTextBox.Text))
            {
                try { lastNameErrorLabel.Text = "Please enter last name."; lastNameErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            var mobile = mobileNumberTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(mobile))
            {
                try { mobileErrorLabel.Text = "Please enter mobile number."; mobileErrorLabel.Visible = true; } catch { }
                hasError = true;
            }
            else
            {
                var digitsOnly = mobile.Replace(" ", "");
                if (digitsOnly.StartsWith("+"))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(digitsOnly, "^\\+63\\d{10}$"))
                    {
                        try { mobileErrorLabel.Text = "Mobile must be 09xxxxxxxxx or +639xxxxxxxxx."; mobileErrorLabel.Visible = true; } catch { }
                        hasError = true;
                    }
                }
                else
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(digitsOnly, "^09\\d{9}$"))
                    {
                        try { mobileErrorLabel.Text = "Mobile must be 11 digits starting with 09."; mobileErrorLabel.Visible = true; } catch { }
                        hasError = true;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(emailTextBox.Text) || !emailTextBox.Text.Contains("@") || !emailTextBox.Text.Contains("."))
            {
                try { emailErrorLabel.Text = "Please enter a valid email address."; emailErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            try
            {
                if (dobPicker != null)
                {
                    var dob = dobPicker.Value.Date;
                    if (dob >= DateTime.Today)
                    {
                        try { dobErrorLabel.Text = "Birth date must be in the past."; dobErrorLabel.Visible = true; } catch { }
                        hasError = true;
                    }
                }
            }
            catch
            {
                try { dobErrorLabel.Text = "Please select birth date."; dobErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(completeAddressTextBox.Text))
            {
                try { addressErrorLabel.Text = "Please enter complete address."; addressErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (employedComboBox == null || employedComboBox.SelectedItem == null)
            {
                try { employedErrorLabel.Text = "Please select employment status."; employedErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(companyNameTextBox.Text))
            {
                try { companyErrorLabel.Text = "Please enter company name."; companyErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (positionComboBox == null || positionComboBox.SelectedItem == null)
            {
                try { positionErrorLabel.Text = "Please select position."; positionErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(yearsEmployedTextBox.Text) || !int.TryParse(yearsEmployedTextBox.Text.Trim(), out var yrs) || yrs < 0)
            {
                try { yearsErrorLabel.Text = "Please enter valid years employed."; yearsErrorLabel.Visible = true; } catch { }
                hasError = true;
            }

            if (hasError) return;

            var loan3 = new loan_form_3();
            loan3.Show();
            this.Close();
        }

        private void button_loan_back_2_Click(object sender, EventArgs e)
        {
            var product = new kiosk_product_detail();
            product.Show();
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var back1 = new loan_form_1();
            back1.Show();
            this.Close();
        }

        private void MobileNumberTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // allow digits, control keys and leading +
            if (char.IsControl(e.KeyChar)) return;
            if (e.KeyChar == '+' && (mobileNumberTextBox.SelectionStart == 0) && !mobileNumberTextBox.Text.Contains('+')) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
            // enforce max length of 13 for +63xxxxxxxxxx or 11 for local; we'll trim in validation
        }

        private void MobileNumberTextBox_TextChanged(object? sender, EventArgs e)
        {
            var txt = mobileNumberTextBox.Text ?? string.Empty;
            // remove spaces
            var cleaned = txt.Replace(" ", string.Empty);
            if (cleaned.StartsWith("+63") && cleaned.Length > 13) // +63 + 10 digits = 13
            {
                mobileNumberTextBox.Text = cleaned.Substring(0, 13);
                mobileNumberTextBox.SelectionStart = mobileNumberTextBox.Text.Length;
            }
            else if (!cleaned.StartsWith("+63") && cleaned.Length > 11)
            {
                mobileNumberTextBox.Text = cleaned.Substring(0, 11);
                mobileNumberTextBox.SelectionStart = mobileNumberTextBox.Text.Length;
            }
        }
    }
}
