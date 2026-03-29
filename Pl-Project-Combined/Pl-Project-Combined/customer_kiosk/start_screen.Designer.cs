namespace customer_kiosk
{
    partial class start_screen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            gkStartBackground = new Guna.UI2.WinForms.Guna2GradientPanel();
            pictureBox4 = new PictureBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            gkTouchToStart = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pictureBox2 = new PictureBox();
            gkStartBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // gkStartBackground
            // 
            gkStartBackground.Controls.Add(pictureBox4);
            gkStartBackground.Controls.Add(guna2HtmlLabel2);
            gkStartBackground.Controls.Add(guna2ShadowPanel1);
            gkStartBackground.CustomizableEdges = customizableEdges1;
            gkStartBackground.Dock = DockStyle.Fill;
            gkStartBackground.FillColor = Color.Gainsboro;
            gkStartBackground.FillColor2 = Color.Blue;
            gkStartBackground.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            gkStartBackground.Location = new Point(0, 0);
            gkStartBackground.Name = "gkStartBackground";
            gkStartBackground.ShadowDecoration.CustomizableEdges = customizableEdges2;
            gkStartBackground.Size = new Size(1920, 1080);
            gkStartBackground.TabIndex = 0;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.ImageLocation = "C:\\Users\\Jonathan\\source\\repos\\pl-project\\Pl-Project-Combined\\Pl-Project-Combined\\customer_kiosk\\images\\guy.png";
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(1920, 740);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Crete Round", 66.74999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = Color.White;
            guna2HtmlLabel2.Location = new Point(679, 733);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(506, 115);
            guna2HtmlLabel2.TabIndex = 8;
            guna2HtmlLabel2.Text = "AsensoMoto";
            // 
            // guna2ShadowPanel1
            // 
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(gkTouchToStart);
            guna2ShadowPanel1.Controls.Add(pictureBox2);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(-9, 854);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 26;
            guna2ShadowPanel1.ShadowColor = Color.Black;
            guna2ShadowPanel1.ShadowDepth = 200;
            guna2ShadowPanel1.Size = new Size(1939, 273);
            guna2ShadowPanel1.TabIndex = 5;
            // 
            // gkTouchToStart
            // 
            gkTouchToStart.BackColor = Color.Transparent;
            gkTouchToStart.Font = new Font("Crete Round", 71.99999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gkTouchToStart.ForeColor = Color.Black;
            gkTouchToStart.Location = new Point(587, 53);
            gkTouchToStart.Name = "gkTouchToStart";
            gkTouchToStart.Size = new Size(1001, 124);
            gkTouchToStart.TabIndex = 1;
            gkTouchToStart.Text = "Tap anywhere to begin";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.ImageLocation = "C:\\Users\\Jonathan\\source\\repos\\pl-project\\Pl-Project-Combined\\Pl-Project-Combined\\customer_kiosk\\images\\tap.png";
            pictureBox2.Location = new Point(379, 21);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(191, 187);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // start_screen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(gkStartBackground);
            DoubleBuffered = true;
            Name = "start_screen";
            Size = new Size(1920, 1080);
            gkStartBackground.ResumeLayout(false);
            gkStartBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            guna2ShadowPanel1.ResumeLayout(false);
            guna2ShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel gkStartBackground;
        private Guna.UI2.WinForms.Guna2HtmlLabel gkTouchToStart;
        private PictureBox pictureBox2;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private PictureBox pictureBox4;
    }
}
