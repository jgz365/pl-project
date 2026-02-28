using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Assessor_Eddion
{
    public partial class staffportaleddion : Form
    {
        // store original positions to revert after hover
        private Point _btn1Original;
        private Point _btn2Original;
        private Point _btn3Original;

        // labels are reparented into their buttons so they move with the button


        // accents used for hover
        private readonly Color _accent1 = Color.FromArgb(142, 68, 173); // purple
        private readonly Color _accent2 = Color.FromArgb(0, 153, 102);  // green
        private readonly Color _accent3 = Color.FromArgb(230, 74, 25);  // red
        private readonly Color _defaultBorder = Color.FromArgb(230, 230, 230);

        public staffportaleddion()
        {
            InitializeComponent();

            // capture original positions once controls are created
            _btn1Original = guna2Button1.Location;
            _btn2Original = guna2Button2.Location;
            _btn3Original = guna2Button3.Location;
            // reparent labels into their corresponding buttons so they move together
            // Adjust label locations to be relative to the button after reparenting
            ReparentLabelIntoButton(guna2HtmlLabel3, guna2Button1, new Point(28, 104));
            ReparentLabelIntoButton(guna2HtmlLabel5, guna2Button1, new Point(28, 142));
            ReparentLabelIntoButton(guna2HtmlLabel6, guna2Button1, new Point(28, 222));
            guna2HtmlLabel3.Enabled = false; guna2HtmlLabel5.Enabled = false; guna2HtmlLabel6.Enabled = false;
            ReparentControlIntoButton(guna2PictureBox1, guna2Button1, new Point(20, 40)); ReparentControlIntoButton(guna2PictureBox2, guna2Button1, new Point(80, 40));

            ReparentLabelIntoButton(guna2HtmlLabel10, guna2Button2, new Point(28, 104));
            ReparentLabelIntoButton(guna2HtmlLabel8, guna2Button2, new Point(28, 142));
            ReparentLabelIntoButton(guna2HtmlLabel7, guna2Button2, new Point(28, 222));
            guna2HtmlLabel10.Enabled = false; guna2HtmlLabel8.Enabled = false; guna2HtmlLabel7.Enabled = false;
            ReparentControlIntoButton(guna2PictureBox5, guna2Button2, new Point(20, 40)); ReparentControlIntoButton(guna2PictureBox3, guna2Button2, new Point(80, 40));

            ReparentLabelIntoButton(guna2HtmlLabel14, guna2Button3, new Point(28, 104));
            ReparentLabelIntoButton(guna2HtmlLabel12, guna2Button3, new Point(28, 142));
            ReparentLabelIntoButton(guna2HtmlLabel11, guna2Button3, new Point(28, 222));
            guna2HtmlLabel10.Enabled = false; guna2HtmlLabel8.Enabled = false; guna2HtmlLabel7.Enabled = false;
            ReparentControlIntoButton(guna2PictureBox6, guna2Button3, new Point(20, 40)); ReparentControlIntoButton(guna2PictureBox4, guna2Button3, new Point(80, 40));
            // open assessor access dialog when card 1 is clicked
            try { guna2Button1.Click += guna2Button1_Click; } catch { }
        }

        private void guna2Button1_Click(object? sender, EventArgs e)
        {
            ShowAssessorPortalDialog();
        }

        private void ShowAssessorPortalDialog()
        {
            // create a semi-transparent overlay to dim the background
            var overlay = new Form();
            overlay.FormBorderStyle = FormBorderStyle.None;
            overlay.StartPosition = FormStartPosition.Manual;
            overlay.ShowInTaskbar = false;
            overlay.BackColor = Color.Black;
            overlay.Opacity = 0.55;
            overlay.Size = this.ClientSize;
            // position overlay relative to parent window
            overlay.Location = this.PointToScreen(new Point(0, 0));
            // owner is this form so overlay stays on top
            overlay.Owner = this;
            overlay.Show(this);

            try
            {
                using var dlg = new assessorportalaccesseddion();
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ShowDialog(overlay);
            }
            finally
            {
                try { overlay.Close(); } catch { }
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        // BUTTON 1 HOVER
        private void guna2Button1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                guna2Button1.BorderThickness = 2;
                guna2Button1.BorderColor = _accent1;
                guna2Button1.Location = new Point(_btn1Original.X, _btn1Original.Y - 8);
                // keep button behind PictureBox
                guna2Button1.SendToBack();
            }
            catch { }
        }

        private void guna2Button1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                guna2Button1.BorderThickness = 1;
                guna2Button1.BorderColor = _defaultBorder;
                guna2Button1.Location = _btn1Original;
            }
            catch { }
        }

        // BUTTON 2 HOVER
        private void guna2Button2_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                guna2Button2.BorderThickness = 2;
                guna2Button2.BorderColor = _accent2;
                guna2Button2.Location = new Point(_btn2Original.X, _btn2Original.Y - 8);
                guna2Button2.SendToBack();
            }
            catch { }
        }

        private void guna2Button2_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                guna2Button2.BorderThickness = 1;
                guna2Button2.BorderColor = _defaultBorder;
                guna2Button2.Location = _btn2Original;
            }
            catch { }
        }

        // BUTTON 3 HOVER
        private void guna2Button3_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                guna2Button3.BorderThickness = 2;
                guna2Button3.BorderColor = _accent3;
                guna2Button3.Location = new Point(_btn3Original.X, _btn3Original.Y - 8);
                guna2Button3.SendToBack();
            }
            catch { }
        }

        private void guna2Button3_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                guna2Button3.BorderThickness = 1;
                guna2Button3.BorderColor = _defaultBorder;
                guna2Button3.Location = _btn3Original;
            }
            catch { }
        }


        // Reparents a label control into a button and sets its local position
        private void ReparentLabelIntoButton(Control label, Control button, Point localPosition)
        {
            // remove from current parent
            var oldParent = label.Parent;
            if (oldParent != null)
            {
                oldParent.Controls.Remove(label);
            }

            // add to button and set local location
            button.Controls.Add(label);
            label.Location = localPosition;
            // make label transparent background so button border is visible behind it
            label.BackColor = Color.Transparent;
        }
        // Reparents any control (label, picturebox, etc.) into a button
        private void ReparentControlIntoButton(Control child, Control button, Point localPosition)
        {
            var oldParent = child.Parent;
            if (oldParent != null)
            {
                oldParent.Controls.Remove(child);
            }

            button.Controls.Add(child);
            child.Location = localPosition;
            child.BackColor = Color.Transparent; // so button visuals show behind
            child.Enabled = false; // prevents intercepting mouse events
        }


        private Bitmap CreateLockBitmap(int w, int h, Color bg)
        {
            var bmp = new Bitmap(w, h);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);

            using var brush = new SolidBrush(bg);
            // draw small rounded rect background
            g.FillRectangle(brush, 0, 0, w, h);

            using var pen = new Pen(Color.FromArgb(120, 120, 120), 1.5f);
            // draw lock body
            g.FillRectangle(Brushes.White, w / 4, h / 3, w / 2, h / 3);
            g.DrawRectangle(pen, w / 4, h / 3, w / 2, h / 3);
            // draw shackle
            g.DrawArc(pen, w / 4, h / 6, w / 2, h / 2, 200, 140);

            return bmp;
        }


        private Bitmap CreateCardIcon(int width, int height, Color bg, int type)
        {
            var bmp = new Bitmap(width, height);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);

            // rounded bg
            using var bgBrush = new SolidBrush(bg);
            var rect = new Rectangle(0, 0, width, height);
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            int r = 10;
            path.AddArc(rect.Left, rect.Top, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Top, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            g.FillPath(bgBrush, path);

            // draw simple white glyphs depending on type
            using var glyphBrush = new SolidBrush(Color.White);
            using var glyphPen = new Pen(Color.FromArgb(200, 200, 200), 2f);
            if (type == 1)
            {
                // clipboard-like: draw rounded paper
                var paper = new Rectangle(width / 4, height / 6, width / 2, height * 2 / 3);
                g.FillRectangle(glyphBrush, paper);
                g.DrawRectangle(glyphPen, paper);
                // top notch
                g.FillRectangle(glyphBrush, new Rectangle(width / 2 - 6, height / 12, 12, 6));
            }
            else if (type == 2)
            {
                // card-like: rounded rect and stripe
                var card = new Rectangle(width / 6, height / 4, width * 2 / 3, height / 2);
                g.FillRectangle(glyphBrush, card);
                g.FillRectangle(new SolidBrush(Color.FromArgb(220, 220, 220)), new Rectangle(card.Left + 6, card.Top + 6, card.Width - 12, 8));
            }
            else
            {
                // gear-like: circle with small teeth
                var cx = width / 2; var cy = height / 2; var rad = Math.Min(width, height) / 4;
                g.FillEllipse(glyphBrush, cx - rad, cy - rad, rad * 2, rad * 2);
                for (int i = 0; i < 8; i++)
                {
                    double ang = i * Math.PI * 2 / 8;
                    int tx = cx + (int)((rad + 6) * Math.Cos(ang));
                    int ty = cy + (int)((rad + 6) * Math.Sin(ang));
                    g.FillRectangle(glyphBrush, tx - 3, ty - 3, 6, 6);
                }
            }

            return bmp;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }
    }


}