using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace customer_kiosk
{
    public partial class payment_confirmed_window : Form
    {
        public payment_confirmed_window()
        {
            InitializeComponent();
            SetQueueTicketNumber();
        }

        private void SetQueueTicketNumber()
        {
            try { queueNumber.Text = GenerateQueueTicketNumber(); } catch { }
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
