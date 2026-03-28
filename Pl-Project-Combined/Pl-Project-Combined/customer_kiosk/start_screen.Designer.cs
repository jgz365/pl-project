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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            label2 = new Label();
            label1 = new Label();
            guna2GradientPanel1.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.Controls.Add(guna2Panel1);
            guna2GradientPanel1.Controls.Add(label1);
            guna2GradientPanel1.CustomizableEdges = customizableEdges3;
            guna2GradientPanel1.FillColor = Color.FromArgb(64, 64, 64);
            guna2GradientPanel1.FillColor2 = Color.FromArgb(128, 128, 255);
            guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2GradientPanel1.Size = new Size(1920, 1080);
            guna2GradientPanel1.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(label2);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Location = new Point(0, 719);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(1920, 361);
            guna2Panel1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Poppins SemiBold", 75F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(544, 162);
            label2.Name = "label2";
            label2.Size = new Size(787, 177);
            label2.TabIndex = 2;
            label2.Text = "Touch to Start";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaption;
            label1.Font = new Font("Poppins SemiBold", 48F, FontStyle.Bold);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(3, 603);
            label1.Name = "label1";
            label1.Size = new Size(879, 113);
            label1.TabIndex = 0;
            label1.Text = "Welcome to AsensoMoto!";
            // 
            // start_screen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2GradientPanel1);
            DoubleBuffered = true;
            Name = "start_screen";
            Size = new Size(1920, 1080);
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel1.PerformLayout();
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label label2;
        private Label label1;
    }
}
