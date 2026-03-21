using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    partial class UC_ConfirmDelete
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlCard = new Guna2Panel();
            pnlIconOuter = new Panel();
            lblIconGlyph = new Label();
            lblHeader = new Label();
            lblSubText = new Guna2HtmlLabel();
            btnCancel = new Guna2Button();
            btnConfirm = new Guna2Button();

            pnlCard.SuspendLayout();
            pnlIconOuter.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            ClientSize = new System.Drawing.Size(460, 340);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "UC_ConfirmDelete";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Permanently Delete?";
            Controls.Add(pnlCard);

            // ── pnlCard — white rounded card ──────────────────────────────────
            pnlCard.BackColor = System.Drawing.Color.Transparent;
            pnlCard.FillColor = System.Drawing.Color.White;
            pnlCard.BorderRadius = 20;
            pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlCard.Location = new System.Drawing.Point(0, 0);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new System.Windows.Forms.Padding(0);
            pnlCard.Size = new System.Drawing.Size(460, 340);
            pnlCard.TabIndex = 0;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Depth = 20;
            pnlCard.ShadowDecoration.Color = System.Drawing.Color.FromArgb(60, 0, 0, 0);
            pnlCard.Controls.Add(pnlIconOuter);
            pnlCard.Controls.Add(lblHeader);
            pnlCard.Controls.Add(lblSubText);
            pnlCard.Controls.Add(btnCancel);
            pnlCard.Controls.Add(btnConfirm);

            // ── Icon circle — red-100 bg, 100×100 so ellipse = perfect circle ─
            pnlIconOuter.BackColor = System.Drawing.Color.FromArgb(254, 226, 226); // red-100
            pnlIconOuter.Location = new System.Drawing.Point(180, 34);
            pnlIconOuter.Name = "pnlIconOuter";
            pnlIconOuter.Size = new System.Drawing.Size(100, 100); // square → ellipse = circle
            pnlIconOuter.TabIndex = 0;
            pnlIconOuter.Paint += pnlIconOuter_Paint;
            pnlIconOuter.Controls.Add(lblIconGlyph);

            lblIconGlyph.BackColor = System.Drawing.Color.Transparent;
            lblIconGlyph.Font = new System.Drawing.Font("Segoe UI Emoji", 26f);
            lblIconGlyph.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38); // red-600
            lblIconGlyph.Dock = System.Windows.Forms.DockStyle.Fill;
            lblIconGlyph.Name = "lblIconGlyph";
            lblIconGlyph.TabIndex = 0;
            lblIconGlyph.Text = "🗑";
            lblIconGlyph.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── Header ────────────────────────────────────────────────────────
            lblHeader.BackColor = System.Drawing.Color.Transparent;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            lblHeader.Location = new System.Drawing.Point(0, 150);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(460, 32);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Permanently Delete?";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── Sub-text (HTML for bold username) ─────────────────────────────
            lblSubText.BackColor = System.Drawing.Color.Transparent;
            lblSubText.Font = new System.Drawing.Font("Segoe UI", 9f);
            lblSubText.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblSubText.Location = new System.Drawing.Point(40, 190);
            lblSubText.Name = "lblSubText";
            lblSubText.Size = new System.Drawing.Size(380, 52);
            lblSubText.TabIndex = 2;
            lblSubText.Text = "This action cannot be undone.";
            lblSubText.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            // ── Cancel button — light gray ghost ──────────────────────────────
            btnCancel.BorderRadius = 10;
            btnCancel.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnCancel.BorderThickness = 1;
            btnCancel.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            btnCancel.Location = new System.Drawing.Point(32, 268);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(182, 48);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            // ── Delete button — red fill ──────────────────────────────────────
            btnConfirm.BorderRadius = 10;
            btnConfirm.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);  // red-600
            btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnConfirm.ForeColor = System.Drawing.Color.White;
            btnConfirm.HoverState.FillColor = System.Drawing.Color.FromArgb(185, 28, 28); // red-700
            btnConfirm.Location = new System.Drawing.Point(246, 268);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new System.Drawing.Size(182, 48);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Delete";
            btnConfirm.Click += btnConfirm_Click;

            // ── Resume ────────────────────────────────────────────────────────
            pnlIconOuter.ResumeLayout(false);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        // Named Paint handler — draws pnlIconOuter as a perfect circle
        private void pnlIconOuter_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(pnlCard.FillColor); // erase square corners
            using var br = new System.Drawing.SolidBrush(pnlIconOuter.BackColor);
            e.Graphics.FillEllipse(br, 0, 0, pnlIconOuter.Width - 1, pnlIconOuter.Height - 1);
        }

        #endregion

        private Guna2Panel pnlCard;
        private System.Windows.Forms.Panel pnlIconOuter;
        private System.Windows.Forms.Label lblIconGlyph;
        private System.Windows.Forms.Label lblHeader;
        private Guna2HtmlLabel lblSubText;
        private Guna2Button btnCancel;
        private Guna2Button btnConfirm;
    }
}