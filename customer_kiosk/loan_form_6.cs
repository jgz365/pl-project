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
    public partial class loan_form_6 : Form
    {
        private Product currentProduct = null;
        private LoanApplicationSession loanSession = null;

        public loan_form_6()
        {
            InitializeComponent();
            SetQueueTicketNumber();
        }

        public loan_form_6(Product product) : this()
        {
            currentProduct = product;
            loanSession = new LoanApplicationSession { SelectedProduct = product };
            SetQueueTicketNumber();
        }

        public loan_form_6(Product product, LoanApplicationSession session) : this()
        {
            currentProduct = product;
            loanSession = session ?? new LoanApplicationSession();
            if (loanSession.SelectedProduct == null) loanSession.SelectedProduct = product;
            SetQueueTicketNumber();
        }

        private void SetQueueTicketNumber()
        {
            var ticket = loanSession?.QueueTicketNumber;

            if (string.IsNullOrWhiteSpace(ticket))
            {
                ticket = GenerateQueueTicketNumber();
                if (loanSession != null) loanSession.QueueTicketNumber = ticket;
            }

            try { queueNumber.Text = ticket; } catch { }
        }

        private static string GenerateQueueTicketNumber()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char a = letters[Random.Shared.Next(letters.Length)];
            char b = letters[Random.Shared.Next(letters.Length)];
            char c = letters[Random.Shared.Next(letters.Length)];
            char d = letters[Random.Shared.Next(letters.Length)];
            int firstPair = Random.Shared.Next(10, 100);
            int secondPair = Random.Shared.Next(10, 100);

            return $"{a}{b}-{firstPair}-{c}{d}-{secondPair}";
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var main = Application.OpenForms["ck"] as ck;
            if (main != null)
            {
                main.Show();
            }
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
    public partial class loan_form_6 : Form
    {
        public loan_form_6()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var main = Application.OpenForms["ck"] as ck;
            if (main != null)
            {
                main.Show();
            }
            this.Close();
        }

    }
}
>>>>>>> 76e9872bf621d0cf86062814b6d214c8db3f7103
