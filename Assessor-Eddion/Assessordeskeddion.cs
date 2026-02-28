using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Assessor_Eddion
{
    public partial class Assessordeskeddion : Form
    {
        public Assessordeskeddion()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2ButtonExit_Click(object sender, EventArgs e)
        {
            // Closes the entire application
            this.Close();
        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel19_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {

        }

        private void guna2ButtonTrigger_Click(object sender, EventArgs e)
        {
            // If panel is hidden, show it and hide the picturebox/label
            if (!guna2Panel1.Visible)
            {
                guna2PictureBox7.Visible = false;
                guna2HtmlLabel19.Visible = false;

                guna2Panel1.Visible = true;
                guna2Button11.Visible = true;
                guna2Button12.Visible = true;
                guna2Button13.Visible = true;
                guna2Button14.Visible = true;
                guna2Button15.Visible = true;
                guna2Button16.Visible = true;
                guna2PictureBox8.Visible = true;
                guna2PictureBox9.Visible = true;
                guna2HtmlLabel20.Visible = true;
                guna2Button11.Checked = true;
                guna2Panel4.Visible = true;
                guna2Panel5.Visible = true;
            }
            else
            {
                // If panel is already visible, reverse it
                guna2Panel1.Visible = false;
                guna2Button11.Visible = false;
                guna2Button12.Visible = false;
                guna2Button13.Visible = false;
                guna2Button14.Visible = false;
                guna2Button15.Visible = false;
                guna2Button16.Visible = false;
                guna2PictureBox8.Visible = false;
                guna2PictureBox9.Visible = false;
                guna2HtmlLabel20.Visible = false;
                guna2Button11.Checked = false;
                guna2Panel4.Visible = false;
                guna2Panel5.Visible = false;

                guna2PictureBox7.Visible = true;
                guna2HtmlLabel19.Visible = true;
            }
        }

        private void guna2HtmlLabel21_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel22_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel29_Click(object sender, EventArgs e)
        {

        }
        private void guna2panel4(object sender, EventArgs e)
        {
            guna2Panel4.BorderThickness = 5;
            guna2Panel4.BorderRadius = 5;
        }

    }
}
