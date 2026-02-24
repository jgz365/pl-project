namespace POSCashierSystem
{
    partial class FullSettlementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2BorderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCustomerList = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlSearchContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCloseForm = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlSearchContainer.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm
            // 
            this.guna2BorderlessForm.AnimateWindow = true;
            this.guna2BorderlessForm.BorderRadius = 15;
            this.guna2BorderlessForm.ContainerControl = this;
            this.guna2BorderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm.ResizeForm = false;
            this.guna2BorderlessForm.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.guna2BorderlessForm.TransparentWhileDrag = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(53, 37, 53, 37);
            this.pnlMain.Size = new System.Drawing.Size(1863, 858);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.BorderRadius = 15;
            this.pnlContent.Controls.Add(this.pnlCustomerList);
            this.pnlContent.Controls.Add(this.pnlSearchContainer);
            this.pnlContent.Controls.Add(this.pnlHeader);
            this.pnlContent.Location = new System.Drawing.Point(53, 37);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(40, 37, 40, 37);
            this.pnlContent.ShadowDecoration.BorderRadius = 15;
            this.pnlContent.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnlContent.ShadowDecoration.Depth = 15;
            this.pnlContent.ShadowDecoration.Enabled = true;
            this.pnlContent.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 3, 10, 10);
            this.pnlContent.Size = new System.Drawing.Size(1760, 784);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.AutoScroll = true;
            this.pnlCustomerList.BackColor = System.Drawing.Color.Transparent;
            this.pnlCustomerList.Location = new System.Drawing.Point(40, 209);
            this.pnlCustomerList.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(1680, 542);
            this.pnlCustomerList.TabIndex = 2;
            // 
            // pnlSearchContainer
            // 
            this.pnlSearchContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchContainer.Controls.Add(this.txtSearch);
            this.pnlSearchContainer.Location = new System.Drawing.Point(40, 123);
            this.pnlSearchContainer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearchContainer.Name = "pnlSearchContainer";
            this.pnlSearchContainer.Size = new System.Drawing.Size(1680, 74);
            this.pnlSearchContainer.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtSearch.BorderRadius = 12;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.IconLeftOffset = new System.Drawing.Point(15, 0);
            this.txtSearch.Location = new System.Drawing.Point(7, 12);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.txtSearch.PlaceholderText = "Enter Account Number or Name...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(1666, 55);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextOffset = new System.Drawing.Point(10, 0);
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnCloseForm);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Location = new System.Drawing.Point(40, 37);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1680, 62);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCloseForm.BorderRadius = 8;
            this.btnCloseForm.BorderThickness = 1;
            this.btnCloseForm.FillColor = System.Drawing.Color.Transparent;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnCloseForm.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.btnCloseForm.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnCloseForm.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.btnCloseForm.Location = new System.Drawing.Point(1543, 14);
            this.btnCloseForm.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(120, 44);
            this.btnCloseForm.TabIndex = 2;
            this.btnCloseForm.Text = "✕  ESC";
            this.btnCloseForm.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 6);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Full Settlement";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(3, 39);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(216, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Step 1: Verify Customer Identity";
            // 
            // FullSettlementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FullSettlementForm";
            this.Size = new System.Drawing.Size(1863, 858);
            this.Load += new System.EventHandler(this.FullSettlementForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlSearchContainer.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnCloseForm;
        private Guna.UI2.WinForms.Guna2Panel pnlSearchContainer;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2Panel pnlCustomerList;
    }
}