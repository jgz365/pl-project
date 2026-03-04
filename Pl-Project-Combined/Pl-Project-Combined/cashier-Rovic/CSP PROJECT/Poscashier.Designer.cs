namespace POSCashierSystem
{
    partial class POSCashier
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2BorderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);

            // Background
            this.pnlBackground = new Guna.UI2.WinForms.Guna2Panel();

            // Top bar
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCloseRegister = new Guna.UI2.WinForms.Guna2Button();
            this.lblTotalCollected = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblSystemOnline = new System.Windows.Forms.Label();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lblExpires = new System.Windows.Forms.Label();
            this.lblStaffName = new System.Windows.Forms.Label();
            this.lblStationStatus = new System.Windows.Forms.Label();
            this.lblStationTitle = new System.Windows.Forms.Label();
            this.guna2CirclePictureBox2 = new Guna.UI2.WinForms.Guna2CirclePictureBox();

            // Independent labels
            this.lblRecentTransactions = new System.Windows.Forms.Label();
            this.lblViewAll = new System.Windows.Forms.Label();
            this.lblNewTransaction = new System.Windows.Forms.Label();
            this.guna2CirclePictureBox3 = new Guna.UI2.WinForms.Guna2CirclePictureBox();

            // Sidebar panel
            this.pnlNoTransactions = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNoTransactions = new System.Windows.Forms.Label();

            // All 6 payment cards as independent panels
            this.pnlDownPayment = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlDownPaymentIcon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDownPaymentIcon = new System.Windows.Forms.Label();
            this.lblDownPayment = new System.Windows.Forms.Label();
            this.lblDownPaymentDesc = new System.Windows.Forms.Label();

            this.pnlMonthlyPayment = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlMonthlyPaymentIcon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMonthlyPaymentIcon = new System.Windows.Forms.Label();
            this.lblMonthlyPayment = new System.Windows.Forms.Label();
            this.lblMonthlyPaymentDesc = new System.Windows.Forms.Label();

            this.pnlFullCash = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlFullCashIcon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFullCashIcon = new System.Windows.Forms.Label();
            this.lblFullCash = new System.Windows.Forms.Label();
            this.lblFullCashDesc = new System.Windows.Forms.Label();

            this.pnlOtherServices = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlOtherServicesIcon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblOtherServicesIcon = new System.Windows.Forms.Label();
            this.lblOtherServices = new System.Windows.Forms.Label();
            this.lblOtherServicesDesc = new System.Windows.Forms.Label();

            this.pnlAdvancePayment = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlAdvancePaymentIcon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAdvancePaymentIcon = new System.Windows.Forms.Label();
            this.lblAdvancePayment = new System.Windows.Forms.Label();
            this.lblAdvancePaymentDesc = new System.Windows.Forms.Label();

            this.pnlFullSettlement = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlFullSettlementIcon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFullSettlementIcon = new System.Windows.Forms.Label();
            this.lblFullSettlement = new System.Windows.Forms.Label();
            this.lblFullSettlementDesc = new System.Windows.Forms.Label();

            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox3)).BeginInit();
            this.pnlNoTransactions.SuspendLayout();
            this.pnlDownPayment.SuspendLayout();
            this.pnlDownPaymentIcon.SuspendLayout();
            this.pnlMonthlyPayment.SuspendLayout();
            this.pnlMonthlyPaymentIcon.SuspendLayout();
            this.pnlFullCash.SuspendLayout();
            this.pnlFullCashIcon.SuspendLayout();
            this.pnlOtherServices.SuspendLayout();
            this.pnlOtherServicesIcon.SuspendLayout();
            this.pnlAdvancePayment.SuspendLayout();
            this.pnlAdvancePaymentIcon.SuspendLayout();
            this.pnlFullSettlement.SuspendLayout();
            this.pnlFullSettlementIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm
            // 
            this.guna2BorderlessForm.AnimateWindow = true;
            this.guna2BorderlessForm.BorderRadius = 15;
            this.guna2BorderlessForm.ContainerControl = this;
            this.guna2BorderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm.DragForm = true;
            this.guna2BorderlessForm.HasFormShadow = true;
            this.guna2BorderlessForm.ResizeForm = false;
            this.guna2BorderlessForm.ShadowColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.guna2BorderlessForm.TransparentWhileDrag = true;
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(248, 250, 251);
            this.pnlBackground.Location = new System.Drawing.Point(0, 100);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(1400, 700);
            this.pnlBackground.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BorderThickness = 0;
            this.pnlTop.Controls.Add(this.btnCloseRegister);
            this.pnlTop.Controls.Add(this.lblTotalCollected);
            this.pnlTop.Controls.Add(this.lblTotalAmount);
            this.pnlTop.Controls.Add(this.lblSystemOnline);
            this.pnlTop.Controls.Add(this.guna2CirclePictureBox1);
            this.pnlTop.Controls.Add(this.lblExpires);
            this.pnlTop.Controls.Add(this.lblStaffName);
            this.pnlTop.Controls.Add(this.lblStationStatus);
            this.pnlTop.Controls.Add(this.lblStationTitle);
            this.pnlTop.Controls.Add(this.guna2CirclePictureBox2);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1400, 100);
            this.pnlTop.TabIndex = 1;
            // 
            // btnCloseRegister
            // 
            this.btnCloseRegister.BorderRadius = 8;
            this.btnCloseRegister.FillColor = System.Drawing.Color.FromArgb(255, 138, 138);
            this.btnCloseRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCloseRegister.ForeColor = System.Drawing.Color.White;
            this.btnCloseRegister.Location = new System.Drawing.Point(1245, 30);
            this.btnCloseRegister.Name = "btnCloseRegister";
            this.btnCloseRegister.Size = new System.Drawing.Size(140, 42);
            this.btnCloseRegister.TabIndex = 9;
            this.btnCloseRegister.Text = "Close Register";
            this.btnCloseRegister.Click += new System.EventHandler(this.BtnCloseRegister_Click);
            // 
            // lblTotalCollected
            // 
            this.lblTotalCollected.AutoSize = true;
            this.lblTotalCollected.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblTotalCollected.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblTotalCollected.Location = new System.Drawing.Point(1081, 60);
            this.lblTotalCollected.Name = "lblTotalCollected";
            this.lblTotalCollected.Size = new System.Drawing.Size(105, 13);
            this.lblTotalCollected.TabIndex = 8;
            this.lblTotalCollected.Text = "TOTAL COLLECTED";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTotalAmount.Location = new System.Drawing.Point(1078, 28);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(48, 32);
            this.lblTotalAmount.TabIndex = 7;
            this.lblTotalAmount.Text = "₱0";
            // 
            // lblSystemOnline
            // 
            this.lblSystemOnline.AutoSize = true;
            this.lblSystemOnline.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSystemOnline.ForeColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.lblSystemOnline.Location = new System.Drawing.Point(911, 33);
            this.lblSystemOnline.Name = "lblSystemOnline";
            this.lblSystemOnline.Size = new System.Drawing.Size(117, 17);
            this.lblSystemOnline.TabIndex = 6;
            this.lblSystemOnline.Text = "SYSTEM ONLINE";
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(891, 36);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(12, 12);
            this.guna2CirclePictureBox1.TabIndex = 5;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // lblExpires
            // 
            this.lblExpires.AutoSize = true;
            this.lblExpires.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblExpires.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblExpires.Location = new System.Drawing.Point(911, 52);
            this.lblExpires.Name = "lblExpires";
            this.lblExpires.Size = new System.Drawing.Size(73, 13);
            this.lblExpires.TabIndex = 4;
            this.lblExpires.Text = "Expires: 13:45";
            // 
            // lblStaffName
            // 
            this.lblStaffName.AutoSize = true;
            this.lblStaffName.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblStaffName.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblStaffName.Location = new System.Drawing.Point(85, 55);
            this.lblStaffName.Name = "lblStaffName";
            this.lblStaffName.Size = new System.Drawing.Size(76, 13);
            this.lblStaffName.TabIndex = 3;
            this.lblStaffName.Text = "Maria Santos";
            // 
            // lblStationStatus
            // 
            this.lblStationStatus.AutoSize = true;
            this.lblStationStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStationStatus.ForeColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.lblStationStatus.Location = new System.Drawing.Point(85, 38);
            this.lblStationStatus.Name = "lblStationStatus";
            this.lblStationStatus.Size = new System.Drawing.Size(39, 15);
            this.lblStationStatus.TabIndex = 2;
            this.lblStationStatus.Text = "OPEN";
            // 
            // lblStationTitle
            // 
            this.lblStationTitle.AutoSize = true;
            this.lblStationTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblStationTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblStationTitle.Location = new System.Drawing.Point(84, 20);
            this.lblStationTitle.Name = "lblStationTitle";
            this.lblStationTitle.Size = new System.Drawing.Size(88, 20);
            this.lblStationTitle.TabIndex = 1;
            this.lblStationTitle.Text = "POS Station";
            // 
            // guna2CirclePictureBox2
            // 
            this.guna2CirclePictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox2.FillColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.guna2CirclePictureBox2.ImageRotate = 0F;
            this.guna2CirclePictureBox2.Location = new System.Drawing.Point(33, 28);
            this.guna2CirclePictureBox2.Name = "guna2CirclePictureBox2";
            this.guna2CirclePictureBox2.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox2.Size = new System.Drawing.Size(40, 40);
            this.guna2CirclePictureBox2.TabIndex = 0;
            this.guna2CirclePictureBox2.TabStop = false;
            // 
            // lblRecentTransactions
            // 
            this.lblRecentTransactions.AutoSize = true;
            this.lblRecentTransactions.BackColor = System.Drawing.Color.FromArgb(248, 250, 251);
            this.lblRecentTransactions.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRecentTransactions.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblRecentTransactions.Location = new System.Drawing.Point(1035, 127);
            this.lblRecentTransactions.Name = "lblRecentTransactions";
            this.lblRecentTransactions.Size = new System.Drawing.Size(146, 20);
            this.lblRecentTransactions.TabIndex = 2;
            this.lblRecentTransactions.Text = "Recent Transactions";
            // 
            // lblViewAll
            // 
            this.lblViewAll.AutoSize = true;
            this.lblViewAll.BackColor = System.Drawing.Color.FromArgb(248, 250, 251);
            this.lblViewAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblViewAll.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblViewAll.ForeColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.lblViewAll.Location = new System.Drawing.Point(1312, 129);
            this.lblViewAll.Name = "lblViewAll";
            this.lblViewAll.Size = new System.Drawing.Size(53, 15);
            this.lblViewAll.TabIndex = 3;
            this.lblViewAll.Text = "View All";
            this.lblViewAll.Click += new System.EventHandler(this.LblViewAll_Click);
            // 
            // lblNewTransaction
            // 
            this.lblNewTransaction.AutoSize = true;
            this.lblNewTransaction.BackColor = System.Drawing.Color.FromArgb(248, 250, 251);
            this.lblNewTransaction.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblNewTransaction.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblNewTransaction.Location = new System.Drawing.Point(76, 127);
            this.lblNewTransaction.Name = "lblNewTransaction";
            this.lblNewTransaction.Size = new System.Drawing.Size(130, 20);
            this.lblNewTransaction.TabIndex = 4;
            this.lblNewTransaction.Text = "New Transaction";
            // 
            // guna2CirclePictureBox3
            // 
            this.guna2CirclePictureBox3.BackColor = System.Drawing.Color.FromArgb(248, 250, 251);
            this.guna2CirclePictureBox3.FillColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.guna2CirclePictureBox3.ImageRotate = 0F;
            this.guna2CirclePictureBox3.Location = new System.Drawing.Point(48, 126);
            this.guna2CirclePictureBox3.Name = "guna2CirclePictureBox3";
            this.guna2CirclePictureBox3.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox3.Size = new System.Drawing.Size(20, 20);
            this.guna2CirclePictureBox3.TabIndex = 5;
            this.guna2CirclePictureBox3.TabStop = false;
            // 
            // pnlNoTransactions
            // 
            this.pnlNoTransactions.BackColor = System.Drawing.Color.White;
            this.pnlNoTransactions.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlNoTransactions.BorderRadius = 15;
            this.pnlNoTransactions.BorderThickness = 1;
            this.pnlNoTransactions.Controls.Add(this.lblNoTransactions);
            this.pnlNoTransactions.Location = new System.Drawing.Point(1025, 160);
            this.pnlNoTransactions.Name = "pnlNoTransactions";
            this.pnlNoTransactions.ShadowDecoration.BorderRadius = 15;
            this.pnlNoTransactions.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlNoTransactions.ShadowDecoration.Depth = 10;
            this.pnlNoTransactions.ShadowDecoration.Enabled = true;
            this.pnlNoTransactions.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlNoTransactions.Size = new System.Drawing.Size(350, 620);
            this.pnlNoTransactions.TabIndex = 6;
            // 
            // lblNoTransactions
            // 
            this.lblNoTransactions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoTransactions.ForeColor = System.Drawing.Color.FromArgb(140, 140, 140);
            this.lblNoTransactions.Location = new System.Drawing.Point(15, 590);
            this.lblNoTransactions.Name = "lblNoTransactions";
            this.lblNoTransactions.Size = new System.Drawing.Size(320, 20);
            this.lblNoTransactions.TabIndex = 0;
            this.lblNoTransactions.Text = "No transactions yet today.";
            this.lblNoTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDownPayment
            // 
            this.pnlDownPayment.BackColor = System.Drawing.Color.White;
            this.pnlDownPayment.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlDownPayment.BorderRadius = 15;
            this.pnlDownPayment.BorderThickness = 1;
            this.pnlDownPayment.Controls.Add(this.pnlDownPaymentIcon);
            this.pnlDownPayment.Controls.Add(this.lblDownPayment);
            this.pnlDownPayment.Controls.Add(this.lblDownPaymentDesc);
            this.pnlDownPayment.FillColor = System.Drawing.Color.White;
            this.pnlDownPayment.Location = new System.Drawing.Point(60, 180);
            this.pnlDownPayment.Name = "pnlDownPayment";
            this.pnlDownPayment.ShadowDecoration.BorderRadius = 15;
            this.pnlDownPayment.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlDownPayment.ShadowDecoration.Depth = 10;
            this.pnlDownPayment.ShadowDecoration.Enabled = true;
            this.pnlDownPayment.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlDownPayment.Size = new System.Drawing.Size(350, 170);
            this.pnlDownPayment.TabIndex = 7;
            this.pnlDownPayment.Click += new System.EventHandler(this.PnlDownPayment_Click);
            // 
            // pnlDownPaymentIcon
            // 
            this.pnlDownPaymentIcon.BackColor = System.Drawing.Color.FromArgb(230, 245, 240);
            this.pnlDownPaymentIcon.BorderRadius = 10;
            this.pnlDownPaymentIcon.Controls.Add(this.lblDownPaymentIcon);
            this.pnlDownPaymentIcon.Location = new System.Drawing.Point(20, 20);
            this.pnlDownPaymentIcon.Name = "pnlDownPaymentIcon";
            this.pnlDownPaymentIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlDownPaymentIcon.TabIndex = 0;
            this.pnlDownPaymentIcon.Click += new System.EventHandler(this.PnlDownPayment_Click);
            // 
            // lblDownPaymentIcon
            // 
            this.lblDownPaymentIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDownPaymentIcon.ForeColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.lblDownPaymentIcon.Location = new System.Drawing.Point(0, 0);
            this.lblDownPaymentIcon.Name = "lblDownPaymentIcon";
            this.lblDownPaymentIcon.Size = new System.Drawing.Size(40, 40);
            this.lblDownPaymentIcon.TabIndex = 0;
            this.lblDownPaymentIcon.Text = "$";
            this.lblDownPaymentIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDownPaymentIcon.Click += new System.EventHandler(this.PnlDownPayment_Click);
            // 
            // lblDownPayment
            // 
            this.lblDownPayment.AutoSize = true;
            this.lblDownPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblDownPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblDownPayment.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblDownPayment.Location = new System.Drawing.Point(20, 80);
            this.lblDownPayment.Name = "lblDownPayment";
            this.lblDownPayment.Size = new System.Drawing.Size(135, 25);
            this.lblDownPayment.TabIndex = 1;
            this.lblDownPayment.Text = "Down Payment";
            this.lblDownPayment.Click += new System.EventHandler(this.PnlDownPayment_Click);
            // 
            // lblDownPaymentDesc
            // 
            this.lblDownPaymentDesc.AutoSize = true;
            this.lblDownPaymentDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDownPaymentDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblDownPaymentDesc.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblDownPaymentDesc.Location = new System.Drawing.Point(20, 108);
            this.lblDownPaymentDesc.Name = "lblDownPaymentDesc";
            this.lblDownPaymentDesc.Size = new System.Drawing.Size(197, 15);
            this.lblDownPaymentDesc.TabIndex = 2;
            this.lblDownPaymentDesc.Text = "Initial payment for new approved loans.";
            this.lblDownPaymentDesc.Click += new System.EventHandler(this.PnlDownPayment_Click);
            // 
            // pnlMonthlyPayment
            // 
            this.pnlMonthlyPayment.BackColor = System.Drawing.Color.White;
            this.pnlMonthlyPayment.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlMonthlyPayment.BorderRadius = 15;
            this.pnlMonthlyPayment.BorderThickness = 1;
            this.pnlMonthlyPayment.Controls.Add(this.pnlMonthlyPaymentIcon);
            this.pnlMonthlyPayment.Controls.Add(this.lblMonthlyPayment);
            this.pnlMonthlyPayment.Controls.Add(this.lblMonthlyPaymentDesc);
            this.pnlMonthlyPayment.FillColor = System.Drawing.Color.White;
            this.pnlMonthlyPayment.Location = new System.Drawing.Point(445, 180);
            this.pnlMonthlyPayment.Name = "pnlMonthlyPayment";
            this.pnlMonthlyPayment.ShadowDecoration.BorderRadius = 15;
            this.pnlMonthlyPayment.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlMonthlyPayment.ShadowDecoration.Depth = 10;
            this.pnlMonthlyPayment.ShadowDecoration.Enabled = true;
            this.pnlMonthlyPayment.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlMonthlyPayment.Size = new System.Drawing.Size(350, 170);
            this.pnlMonthlyPayment.TabIndex = 8;
            this.pnlMonthlyPayment.Click += new System.EventHandler(this.PnlMonthlyPayment_Click);
            // 
            // pnlMonthlyPaymentIcon
            // 
            this.pnlMonthlyPaymentIcon.BackColor = System.Drawing.Color.FromArgb(235, 240, 255);
            this.pnlMonthlyPaymentIcon.BorderRadius = 10;
            this.pnlMonthlyPaymentIcon.Controls.Add(this.lblMonthlyPaymentIcon);
            this.pnlMonthlyPaymentIcon.Location = new System.Drawing.Point(20, 20);
            this.pnlMonthlyPaymentIcon.Name = "pnlMonthlyPaymentIcon";
            this.pnlMonthlyPaymentIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlMonthlyPaymentIcon.TabIndex = 0;
            this.pnlMonthlyPaymentIcon.Click += new System.EventHandler(this.PnlMonthlyPayment_Click);
            // 
            // lblMonthlyPaymentIcon
            // 
            this.lblMonthlyPaymentIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMonthlyPaymentIcon.ForeColor = System.Drawing.Color.FromArgb(138, 171, 255);
            this.lblMonthlyPaymentIcon.Location = new System.Drawing.Point(0, 0);
            this.lblMonthlyPaymentIcon.Name = "lblMonthlyPaymentIcon";
            this.lblMonthlyPaymentIcon.Size = new System.Drawing.Size(40, 40);
            this.lblMonthlyPaymentIcon.TabIndex = 0;
            this.lblMonthlyPaymentIcon.Text = "📅";
            this.lblMonthlyPaymentIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMonthlyPaymentIcon.Click += new System.EventHandler(this.PnlMonthlyPayment_Click);
            // 
            // lblMonthlyPayment
            // 
            this.lblMonthlyPayment.AutoSize = true;
            this.lblMonthlyPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblMonthlyPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblMonthlyPayment.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblMonthlyPayment.Location = new System.Drawing.Point(20, 80);
            this.lblMonthlyPayment.Name = "lblMonthlyPayment";
            this.lblMonthlyPayment.Size = new System.Drawing.Size(166, 25);
            this.lblMonthlyPayment.TabIndex = 1;
            this.lblMonthlyPayment.Text = "Monthly Payment";
            this.lblMonthlyPayment.Click += new System.EventHandler(this.PnlMonthlyPayment_Click);
            // 
            // lblMonthlyPaymentDesc
            // 
            this.lblMonthlyPaymentDesc.AutoSize = true;
            this.lblMonthlyPaymentDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblMonthlyPaymentDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblMonthlyPaymentDesc.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblMonthlyPaymentDesc.Location = new System.Drawing.Point(20, 108);
            this.lblMonthlyPaymentDesc.Name = "lblMonthlyPaymentDesc";
            this.lblMonthlyPaymentDesc.Size = new System.Drawing.Size(166, 15);
            this.lblMonthlyPaymentDesc.TabIndex = 2;
            this.lblMonthlyPaymentDesc.Text = "Regular installment collection.";
            this.lblMonthlyPaymentDesc.Click += new System.EventHandler(this.PnlMonthlyPayment_Click);
            // 
            // pnlFullCash
            // 
            this.pnlFullCash.BackColor = System.Drawing.Color.White;
            this.pnlFullCash.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlFullCash.BorderRadius = 15;
            this.pnlFullCash.BorderThickness = 1;
            this.pnlFullCash.Controls.Add(this.pnlFullCashIcon);
            this.pnlFullCash.Controls.Add(this.lblFullCash);
            this.pnlFullCash.Controls.Add(this.lblFullCashDesc);
            this.pnlFullCash.FillColor = System.Drawing.Color.White;
            this.pnlFullCash.Location = new System.Drawing.Point(60, 365);
            this.pnlFullCash.Name = "pnlFullCash";
            this.pnlFullCash.ShadowDecoration.BorderRadius = 15;
            this.pnlFullCash.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlFullCash.ShadowDecoration.Depth = 10;
            this.pnlFullCash.ShadowDecoration.Enabled = true;
            this.pnlFullCash.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlFullCash.Size = new System.Drawing.Size(350, 170);
            this.pnlFullCash.TabIndex = 9;
            this.pnlFullCash.Click += new System.EventHandler(this.PnlFullCash_Click);
            // 
            // pnlFullCashIcon
            // 
            this.pnlFullCashIcon.BackColor = System.Drawing.Color.FromArgb(245, 235, 255);
            this.pnlFullCashIcon.BorderRadius = 10;
            this.pnlFullCashIcon.Controls.Add(this.lblFullCashIcon);
            this.pnlFullCashIcon.Location = new System.Drawing.Point(20, 20);
            this.pnlFullCashIcon.Name = "pnlFullCashIcon";
            this.pnlFullCashIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlFullCashIcon.TabIndex = 0;
            this.pnlFullCashIcon.Click += new System.EventHandler(this.PnlFullCash_Click);
            // 
            // lblFullCashIcon
            // 
            this.lblFullCashIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblFullCashIcon.ForeColor = System.Drawing.Color.FromArgb(188, 140, 255);
            this.lblFullCashIcon.Location = new System.Drawing.Point(0, 0);
            this.lblFullCashIcon.Name = "lblFullCashIcon";
            this.lblFullCashIcon.Size = new System.Drawing.Size(40, 40);
            this.lblFullCashIcon.TabIndex = 0;
            this.lblFullCashIcon.Text = "💳";
            this.lblFullCashIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFullCashIcon.Click += new System.EventHandler(this.PnlFullCash_Click);
            // 
            // lblFullCash
            // 
            this.lblFullCash.AutoSize = true;
            this.lblFullCash.BackColor = System.Drawing.Color.Transparent;
            this.lblFullCash.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblFullCash.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblFullCash.Location = new System.Drawing.Point(20, 80);
            this.lblFullCash.Name = "lblFullCash";
            this.lblFullCash.Size = new System.Drawing.Size(176, 25);
            this.lblFullCash.TabIndex = 1;
            this.lblFullCash.Text = "Full Cash Purchase";
            this.lblFullCash.Click += new System.EventHandler(this.PnlFullCash_Click);
            // 
            // lblFullCashDesc
            // 
            this.lblFullCashDesc.AutoSize = true;
            this.lblFullCashDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblFullCashDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblFullCashDesc.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblFullCashDesc.Location = new System.Drawing.Point(20, 108);
            this.lblFullCashDesc.Name = "lblFullCashDesc";
            this.lblFullCashDesc.Size = new System.Drawing.Size(192, 15);
            this.lblFullCashDesc.TabIndex = 2;
            this.lblFullCashDesc.Text = "One-time payment for unit purchase.";
            this.lblFullCashDesc.Click += new System.EventHandler(this.PnlFullCash_Click);
            // 
            // pnlOtherServices
            // 
            this.pnlOtherServices.BackColor = System.Drawing.Color.White;
            this.pnlOtherServices.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlOtherServices.BorderRadius = 15;
            this.pnlOtherServices.BorderThickness = 1;
            this.pnlOtherServices.Controls.Add(this.pnlOtherServicesIcon);
            this.pnlOtherServices.Controls.Add(this.lblOtherServices);
            this.pnlOtherServices.Controls.Add(this.lblOtherServicesDesc);
            this.pnlOtherServices.FillColor = System.Drawing.Color.White;
            this.pnlOtherServices.Location = new System.Drawing.Point(445, 365);
            this.pnlOtherServices.Name = "pnlOtherServices";
            this.pnlOtherServices.ShadowDecoration.BorderRadius = 15;
            this.pnlOtherServices.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlOtherServices.ShadowDecoration.Depth = 10;
            this.pnlOtherServices.ShadowDecoration.Enabled = true;
            this.pnlOtherServices.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlOtherServices.Size = new System.Drawing.Size(350, 170);
            this.pnlOtherServices.TabIndex = 10;
            this.pnlOtherServices.Click += new System.EventHandler(this.PnlOtherServices_Click);
            // 
            // pnlOtherServicesIcon
            // 
            this.pnlOtherServicesIcon.BackColor = System.Drawing.Color.FromArgb(255, 245, 235);
            this.pnlOtherServicesIcon.BorderRadius = 10;
            this.pnlOtherServicesIcon.Controls.Add(this.lblOtherServicesIcon);
            this.pnlOtherServicesIcon.Location = new System.Drawing.Point(20, 20);
            this.pnlOtherServicesIcon.Name = "pnlOtherServicesIcon";
            this.pnlOtherServicesIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlOtherServicesIcon.TabIndex = 0;
            this.pnlOtherServicesIcon.Click += new System.EventHandler(this.PnlOtherServices_Click);
            // 
            // lblOtherServicesIcon
            // 
            this.lblOtherServicesIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblOtherServicesIcon.ForeColor = System.Drawing.Color.FromArgb(255, 186, 115);
            this.lblOtherServicesIcon.Location = new System.Drawing.Point(0, 0);
            this.lblOtherServicesIcon.Name = "lblOtherServicesIcon";
            this.lblOtherServicesIcon.Size = new System.Drawing.Size(40, 40);
            this.lblOtherServicesIcon.TabIndex = 0;
            this.lblOtherServicesIcon.Text = "⚙";
            this.lblOtherServicesIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOtherServicesIcon.Click += new System.EventHandler(this.PnlOtherServices_Click);
            // 
            // lblOtherServices
            // 
            this.lblOtherServices.AutoSize = true;
            this.lblOtherServices.BackColor = System.Drawing.Color.Transparent;
            this.lblOtherServices.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblOtherServices.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblOtherServices.Location = new System.Drawing.Point(20, 80);
            this.lblOtherServices.Name = "lblOtherServices";
            this.lblOtherServices.Size = new System.Drawing.Size(138, 25);
            this.lblOtherServices.TabIndex = 1;
            this.lblOtherServices.Text = "Other Services";
            this.lblOtherServices.Click += new System.EventHandler(this.PnlOtherServices_Click);
            // 
            // lblOtherServicesDesc
            // 
            this.lblOtherServicesDesc.AutoSize = true;
            this.lblOtherServicesDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblOtherServicesDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblOtherServicesDesc.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblOtherServicesDesc.Location = new System.Drawing.Point(20, 108);
            this.lblOtherServicesDesc.Name = "lblOtherServicesDesc";
            this.lblOtherServicesDesc.Size = new System.Drawing.Size(185, 15);
            this.lblOtherServicesDesc.TabIndex = 2;
            this.lblOtherServicesDesc.Text = "Parts, Service, Registration Fees.";
            this.lblOtherServicesDesc.Click += new System.EventHandler(this.PnlOtherServices_Click);
            // 
            // pnlAdvancePayment
            // 
            this.pnlAdvancePayment.BackColor = System.Drawing.Color.White;
            this.pnlAdvancePayment.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlAdvancePayment.BorderRadius = 15;
            this.pnlAdvancePayment.BorderThickness = 1;
            this.pnlAdvancePayment.Controls.Add(this.pnlAdvancePaymentIcon);
            this.pnlAdvancePayment.Controls.Add(this.lblAdvancePayment);
            this.pnlAdvancePayment.Controls.Add(this.lblAdvancePaymentDesc);
            this.pnlAdvancePayment.FillColor = System.Drawing.Color.White;
            this.pnlAdvancePayment.Location = new System.Drawing.Point(60, 550);
            this.pnlAdvancePayment.Name = "pnlAdvancePayment";
            this.pnlAdvancePayment.ShadowDecoration.BorderRadius = 15;
            this.pnlAdvancePayment.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlAdvancePayment.ShadowDecoration.Depth = 10;
            this.pnlAdvancePayment.ShadowDecoration.Enabled = true;
            this.pnlAdvancePayment.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlAdvancePayment.Size = new System.Drawing.Size(350, 170);
            this.pnlAdvancePayment.TabIndex = 11;
            this.pnlAdvancePayment.Click += new System.EventHandler(this.PnlAdvancePayment_Click);
            // 
            // pnlAdvancePaymentIcon
            // 
            this.pnlAdvancePaymentIcon.BackColor = System.Drawing.Color.FromArgb(230, 245, 240);
            this.pnlAdvancePaymentIcon.BorderRadius = 10;
            this.pnlAdvancePaymentIcon.Controls.Add(this.lblAdvancePaymentIcon);
            this.pnlAdvancePaymentIcon.Location = new System.Drawing.Point(20, 20);
            this.pnlAdvancePaymentIcon.Name = "pnlAdvancePaymentIcon";
            this.pnlAdvancePaymentIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlAdvancePaymentIcon.TabIndex = 0;
            this.pnlAdvancePaymentIcon.Click += new System.EventHandler(this.PnlAdvancePayment_Click);
            // 
            // lblAdvancePaymentIcon
            // 
            this.lblAdvancePaymentIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAdvancePaymentIcon.ForeColor = System.Drawing.Color.FromArgb(102, 217, 189);
            this.lblAdvancePaymentIcon.Location = new System.Drawing.Point(0, 0);
            this.lblAdvancePaymentIcon.Name = "lblAdvancePaymentIcon";
            this.lblAdvancePaymentIcon.Size = new System.Drawing.Size(40, 40);
            this.lblAdvancePaymentIcon.TabIndex = 0;
            this.lblAdvancePaymentIcon.Text = "📆";
            this.lblAdvancePaymentIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAdvancePaymentIcon.Click += new System.EventHandler(this.PnlAdvancePayment_Click);
            // 
            // lblAdvancePayment
            // 
            this.lblAdvancePayment.AutoSize = true;
            this.lblAdvancePayment.BackColor = System.Drawing.Color.Transparent;
            this.lblAdvancePayment.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblAdvancePayment.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblAdvancePayment.Location = new System.Drawing.Point(20, 80);
            this.lblAdvancePayment.Name = "lblAdvancePayment";
            this.lblAdvancePayment.Size = new System.Drawing.Size(171, 25);
            this.lblAdvancePayment.TabIndex = 1;
            this.lblAdvancePayment.Text = "Advance Payment";
            this.lblAdvancePayment.Click += new System.EventHandler(this.PnlAdvancePayment_Click);
            // 
            // lblAdvancePaymentDesc
            // 
            this.lblAdvancePaymentDesc.AutoSize = true;
            this.lblAdvancePaymentDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblAdvancePaymentDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblAdvancePaymentDesc.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblAdvancePaymentDesc.Location = new System.Drawing.Point(20, 108);
            this.lblAdvancePaymentDesc.Name = "lblAdvancePaymentDesc";
            this.lblAdvancePaymentDesc.Size = new System.Drawing.Size(232, 15);
            this.lblAdvancePaymentDesc.TabIndex = 2;
            this.lblAdvancePaymentDesc.Text = "Pay multiple months ahead with discount.";
            this.lblAdvancePaymentDesc.Click += new System.EventHandler(this.PnlAdvancePayment_Click);
            // 
            // pnlFullSettlement
            // 
            this.pnlFullSettlement.BackColor = System.Drawing.Color.FromArgb(255, 241, 241);
            this.pnlFullSettlement.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlFullSettlement.BorderRadius = 15;
            this.pnlFullSettlement.BorderThickness = 1;
            this.pnlFullSettlement.Controls.Add(this.pnlFullSettlementIcon);
            this.pnlFullSettlement.Controls.Add(this.lblFullSettlement);
            this.pnlFullSettlement.Controls.Add(this.lblFullSettlementDesc);
            this.pnlFullSettlement.FillColor = System.Drawing.Color.FromArgb(255, 241, 241);
            this.pnlFullSettlement.Location = new System.Drawing.Point(445, 550);
            this.pnlFullSettlement.Name = "pnlFullSettlement";
            this.pnlFullSettlement.ShadowDecoration.BorderRadius = 15;
            this.pnlFullSettlement.ShadowDecoration.Color = System.Drawing.Color.FromArgb(200, 200, 200);
            this.pnlFullSettlement.ShadowDecoration.Depth = 10;
            this.pnlFullSettlement.ShadowDecoration.Enabled = true;
            this.pnlFullSettlement.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.pnlFullSettlement.Size = new System.Drawing.Size(350, 170);
            this.pnlFullSettlement.TabIndex = 12;
            this.pnlFullSettlement.Click += new System.EventHandler(this.PnlFullSettlement_Click);
            // 
            // pnlFullSettlementIcon
            // 
            this.pnlFullSettlementIcon.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            this.pnlFullSettlementIcon.BorderRadius = 10;
            this.pnlFullSettlementIcon.Controls.Add(this.lblFullSettlementIcon);
            this.pnlFullSettlementIcon.Location = new System.Drawing.Point(20, 20);
            this.pnlFullSettlementIcon.Name = "pnlFullSettlementIcon";
            this.pnlFullSettlementIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlFullSettlementIcon.TabIndex = 0;
            this.pnlFullSettlementIcon.Click += new System.EventHandler(this.PnlFullSettlement_Click);
            // 
            // lblFullSettlementIcon
            // 
            this.lblFullSettlementIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblFullSettlementIcon.ForeColor = System.Drawing.Color.FromArgb(255, 138, 138);
            this.lblFullSettlementIcon.Location = new System.Drawing.Point(0, 0);
            this.lblFullSettlementIcon.Name = "lblFullSettlementIcon";
            this.lblFullSettlementIcon.Size = new System.Drawing.Size(40, 40);
            this.lblFullSettlementIcon.TabIndex = 0;
            this.lblFullSettlementIcon.Text = "🛡";
            this.lblFullSettlementIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFullSettlementIcon.Click += new System.EventHandler(this.PnlFullSettlement_Click);
            // 
            // lblFullSettlement
            // 
            this.lblFullSettlement.AutoSize = true;
            this.lblFullSettlement.BackColor = System.Drawing.Color.Transparent;
            this.lblFullSettlement.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblFullSettlement.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblFullSettlement.Location = new System.Drawing.Point(20, 80);
            this.lblFullSettlement.Name = "lblFullSettlement";
            this.lblFullSettlement.Size = new System.Drawing.Size(150, 25);
            this.lblFullSettlement.TabIndex = 1;
            this.lblFullSettlement.Text = "Full Settlement";
            this.lblFullSettlement.Click += new System.EventHandler(this.PnlFullSettlement_Click);
            // 
            // lblFullSettlementDesc
            // 
            this.lblFullSettlementDesc.AutoSize = true;
            this.lblFullSettlementDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblFullSettlementDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblFullSettlementDesc.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.lblFullSettlementDesc.Location = new System.Drawing.Point(20, 108);
            this.lblFullSettlementDesc.Name = "lblFullSettlementDesc";
            this.lblFullSettlementDesc.Size = new System.Drawing.Size(171, 15);
            this.lblFullSettlementDesc.TabIndex = 2;
            this.lblFullSettlementDesc.Text = "Pay off entire loan with rebate.";
            this.lblFullSettlementDesc.Click += new System.EventHandler(this.PnlFullSettlement_Click);
            // 
            // POSCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 251);
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.pnlFullSettlement);
            this.Controls.Add(this.pnlAdvancePayment);
            this.Controls.Add(this.pnlOtherServices);
            this.Controls.Add(this.pnlFullCash);
            this.Controls.Add(this.pnlMonthlyPayment);
            this.Controls.Add(this.pnlDownPayment);
            this.Controls.Add(this.pnlNoTransactions);
            this.Controls.Add(this.guna2CirclePictureBox3);
            this.Controls.Add(this.lblNewTransaction);
            this.Controls.Add(this.lblViewAll);
            this.Controls.Add(this.lblRecentTransactions);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POSCashier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Cashier Station";
            this.Load += new System.EventHandler(this.POSCashier_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox3)).EndInit();
            this.pnlNoTransactions.ResumeLayout(false);
            this.pnlDownPayment.ResumeLayout(false);
            this.pnlDownPayment.PerformLayout();
            this.pnlDownPaymentIcon.ResumeLayout(false);
            this.pnlMonthlyPayment.ResumeLayout(false);
            this.pnlMonthlyPayment.PerformLayout();
            this.pnlMonthlyPaymentIcon.ResumeLayout(false);
            this.pnlFullCash.ResumeLayout(false);
            this.pnlFullCash.PerformLayout();
            this.pnlFullCashIcon.ResumeLayout(false);
            this.pnlOtherServices.ResumeLayout(false);
            this.pnlOtherServices.PerformLayout();
            this.pnlOtherServicesIcon.ResumeLayout(false);
            this.pnlAdvancePayment.ResumeLayout(false);
            this.pnlAdvancePayment.PerformLayout();
            this.pnlAdvancePaymentIcon.ResumeLayout(false);
            this.pnlFullSettlement.ResumeLayout(false);
            this.pnlFullSettlement.PerformLayout();
            this.pnlFullSettlementIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm;
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2Button btnCloseRegister;
        private System.Windows.Forms.Label lblTotalCollected;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblSystemOnline;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private System.Windows.Forms.Label lblExpires;
        private System.Windows.Forms.Label lblStaffName;
        private System.Windows.Forms.Label lblStationStatus;
        private System.Windows.Forms.Label lblStationTitle;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox2;
        private System.Windows.Forms.Label lblRecentTransactions;
        private System.Windows.Forms.Label lblViewAll;
        private System.Windows.Forms.Label lblNewTransaction;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox3;
        private Guna.UI2.WinForms.Guna2Panel pnlNoTransactions;
        private System.Windows.Forms.Label lblNoTransactions;
        private Guna.UI2.WinForms.Guna2Panel pnlDownPayment;
        private Guna.UI2.WinForms.Guna2Panel pnlDownPaymentIcon;
        private System.Windows.Forms.Label lblDownPaymentIcon;
        private System.Windows.Forms.Label lblDownPayment;
        private System.Windows.Forms.Label lblDownPaymentDesc;
        private Guna.UI2.WinForms.Guna2Panel pnlMonthlyPayment;
        private Guna.UI2.WinForms.Guna2Panel pnlMonthlyPaymentIcon;
        private System.Windows.Forms.Label lblMonthlyPaymentIcon;
        private System.Windows.Forms.Label lblMonthlyPayment;
        private System.Windows.Forms.Label lblMonthlyPaymentDesc;
        private Guna.UI2.WinForms.Guna2Panel pnlFullCash;
        private Guna.UI2.WinForms.Guna2Panel pnlFullCashIcon;
        private System.Windows.Forms.Label lblFullCashIcon;
        private System.Windows.Forms.Label lblFullCash;
        private System.Windows.Forms.Label lblFullCashDesc;
        private Guna.UI2.WinForms.Guna2Panel pnlOtherServices;
        private Guna.UI2.WinForms.Guna2Panel pnlOtherServicesIcon;
        private System.Windows.Forms.Label lblOtherServicesIcon;
        private System.Windows.Forms.Label lblOtherServices;
        private System.Windows.Forms.Label lblOtherServicesDesc;
        private Guna.UI2.WinForms.Guna2Panel pnlAdvancePayment;
        private Guna.UI2.WinForms.Guna2Panel pnlAdvancePaymentIcon;
        private System.Windows.Forms.Label lblAdvancePaymentIcon;
        private System.Windows.Forms.Label lblAdvancePayment;
        private System.Windows.Forms.Label lblAdvancePaymentDesc;
        private Guna.UI2.WinForms.Guna2Panel pnlFullSettlement;
        private Guna.UI2.WinForms.Guna2Panel pnlFullSettlementIcon;
        private System.Windows.Forms.Label lblFullSettlementIcon;
        private System.Windows.Forms.Label lblFullSettlement;
        private System.Windows.Forms.Label lblFullSettlementDesc;
    }
}