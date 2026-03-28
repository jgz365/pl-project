using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Newtonsoft.Json;

namespace POSCashierSystem
{
    public partial class MonthlyPaymentForm : UserControl
    {
        // ── Data ─────────────────────────────────────────────────────────────
        private List<CustomerSummary> customers = null!;
        private List<CustomerSummary> filteredCustomers = new();

        // ── Reference design geometry (from designer) ─────────────────────
        // Form: 1863 × 858
        // Outer padding (pnlMain → pnlContent): 53 H, 37 V
        // Inner padding (pnlContent edges):      40 H, 37 V
        // pnlHeader height:        62
        // pnlSearchContainer height: 74
        // Gap between sections: ~12-14
        private const float REF_W = 1863f;
        private const float REF_H = 858f;
        private const float REF_PAD_H = 53f;
        private const float REF_PAD_V = 37f;
        private const float REF_HDR_H = 62f;
        private const float REF_SRC_H = 74f;

        // ── Colors ────────────────────────────────────────────────────────
        private readonly Color cardDefaultBorderColor = Color.FromArgb(224, 224, 224);
        private readonly Color cardHoverBorderColor = Color.FromArgb(138, 171, 255);
        private readonly Color cardDefaultBackColor = Color.White;
        private readonly Color cardHoverBackColor = Color.FromArgb(250, 252, 253);

        private readonly string _customersJsonPath;

        // ─────────────────────────────────────────────────────────────────
        public MonthlyPaymentForm()
        {
            InitializeComponent();
            _customersJsonPath = Path.Combine(Application.StartupPath, "customers.json");
            LoadCustomerData();
            DisplayCustomers(customers ?? new());
        }

        // ═════════════════════════════════════════════════════════════════
        //  RESPONSIVE LAYOUT — fires on every Resize
        // ═════════════════════════════════════════════════════════════════
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated) ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            int w = Math.Max(400, Width);
            int h = Math.Max(200, Height);

            // ── pnlMain fills the entire UserControl ──────────────────────
            pnlMain.SetBounds(0, 0, w, h);

            // ── Outer padding (scales with size) ──────────────────────────
            int padH = Scale(REF_PAD_H, w, REF_W);   // left/right outer padding
            int padV = Scale(REF_PAD_V, h, REF_H);   // top/bottom outer padding

            // ── pnlContent ────────────────────────────────────────────────
            int cX = padH;
            int cY = padV;
            int cW = w - padH * 2;
            int cH = h - padV * 2;
            pnlContent.SetBounds(cX, cY, cW, cH);

            // ── Inner padding ─────────────────────────────────────────────
            int iPadH = Scale(40f, cW, REF_W - REF_PAD_H * 2);   // ~40/1760
            int iPadVT = Scale(REF_PAD_V, cH, REF_H - REF_PAD_V * 2); // top inner pad

            // ── pnlHeader ─────────────────────────────────────────────────
            int hdrH = Scale(REF_HDR_H, cH, REF_H - REF_PAD_V * 2);
            hdrH = Math.Max(44, hdrH);
            int hdrW = cW - iPadH * 2;
            pnlHeader.SetBounds(iPadH, iPadVT, hdrW, hdrH);

            // btnCloseForm right-aligned inside header
            btnCloseForm.Location = new Point(
                hdrW - btnCloseForm.Width - 17,
                (hdrH - btnCloseForm.Height) / 2);

            // ── pnlSearchContainer ────────────────────────────────────────
            int gapAfterHeader = Math.Max(8, Scale(14f, cH, REF_H - REF_PAD_V * 2));
            int srcY = iPadVT + hdrH + gapAfterHeader;
            int srcH = Scale(REF_SRC_H, cH, REF_H - REF_PAD_V * 2);
            srcH = Math.Max(40, srcH);
            int srcW = hdrW;
            pnlSearchContainer.SetBounds(iPadH, srcY, srcW, srcH);

            // txtSearch fills search container (minus 14px border margin)
            txtSearch.SetBounds(7, (srcH - txtSearch.Height) / 2,
                                srcW - 14, txtSearch.Height);

            // ── pnlCustomerList ───────────────────────────────────────────
            int gapAfterSearch = Math.Max(4, Scale(9f, cH, REF_H - REF_PAD_V * 2));
            int listY = srcY + srcH + gapAfterSearch;
            int listH = Math.Max(60, cH - listY - iPadVT);
            pnlCustomerList.SetBounds(iPadH, listY, srcW, listH);

            // ── Refresh dynamic card widths ───────────────────────────────
            RefreshCardWidths(srcW);
        }

        /// <summary>Proportionally scale a design value relative to the current size.</summary>
        private static int Scale(float designValue, float current, float reference)
            => (int)Math.Round(designValue * (current / reference));

        // ═════════════════════════════════════════════════════════════════
        //  CUSTOMER CARDS — dynamic width
        // ═════════════════════════════════════════════════════════════════
        private void RefreshCardWidths(int listPanelWidth)
        {
            int cardW = Math.Max(200, listPanelWidth - 20); // 10px margin each side
            foreach (Control ctrl in pnlCustomerList.Controls)
            {
                if (ctrl is not Guna2Panel card) continue;
                card.Width = cardW;

                // Reposition right-aligned amount labels
                foreach (Control inner in card.Controls)
                {
                    if (inner is Label lbl &&
                        (lbl.TextAlign == ContentAlignment.TopRight ||
                         lbl.Name == "lblDueLabel" || lbl.Name == "lblAmount"))
                    {
                        lbl.Location = new Point(cardW - 170, lbl.Location.Y);
                        lbl.Width = 150;
                    }
                }
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // Load
        // ─────────────────────────────────────────────────────────────────
        private void MonthlyPaymentForm_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
            ApplyResponsiveLayout();
        }

        // ═════════════════════════════════════════════════════════════════
        //  DATA
        // ═════════════════════════════════════════════════════════════════
        private void LoadCustomerData()
        {
            if (File.Exists(_customersJsonPath))
            {
                try
                {
                    string json = File.ReadAllText(_customersJsonPath);
                    customers = JsonConvert.DeserializeObject<List<CustomerSummary>>(json)
                                ?? new List<CustomerSummary>();
                }
                catch { customers = GetFallbackCustomers(); }
            }
            else
            {
                customers = GetFallbackCustomers();
            }
            filteredCustomers = new List<CustomerSummary>(customers);
        }

        private static List<CustomerSummary> GetFallbackCustomers() => new List<CustomerSummary>
        {
            new CustomerSummary {
                Name = "Juan Dela Cruz", QueueTicket = "LA-2026-0001",
                Location = "Quezon City", TransactionType = "MONTHLY",
                UnitDetails = new UnitDetailsData { Model = "Honda ADV 160", Color = "Matte Black", EngineNo = "K1Z-998877" },
                FinancialStatus = new FinancialStatusData {
                    DownPaymentDue = 53400, CurrentBalance = 134118.08m,
                    MonthlyAmortization = 5364.72m, NextDueDate = "Feb 1, 2026" }
            },
            new CustomerSummary {
                Name = "Maria Garcia", QueueTicket = "LA-2026-0002",
                Location = "Caloocan City", TransactionType = "MONTHLY",
                UnitDetails = new UnitDetailsData { Model = "Yamaha NMAX 155", Color = "Matte Blue", EngineNo = "YM2-447612" },
                FinancialStatus = new FinancialStatusData {
                    DownPaymentDue = 45570, CurrentBalance = 95400.00m,
                    MonthlyAmortization = 3797.50m, NextDueDate = "Feb 5, 2026" }
            },
            new CustomerSummary {
                Name = "Pedro Santos", QueueTicket = "LA-2026-0003",
                Location = "Marikina City", TransactionType = "MONTHLY",
                UnitDetails = new UnitDetailsData { Model = "Honda Click 160", Color = "Pearl White", EngineNo = "HC1-003341" },
                FinancialStatus = new FinancialStatusData {
                    DownPaymentDue = 38900, CurrentBalance = 77800.00m,
                    MonthlyAmortization = 3241.67m, NextDueDate = "Feb 10, 2026" }
            },
            new CustomerSummary {
                Name = "Ana Reyes", QueueTicket = "LA-2026-0004",
                Location = "Pasig City", TransactionType = "MONTHLY",
                UnitDetails = new UnitDetailsData { Model = "Suzuki Raider R150", Color = "Candy Red", EngineNo = "SR1-772209" },
                FinancialStatus = new FinancialStatusData {
                    DownPaymentDue = 42300, CurrentBalance = 84600.00m,
                    MonthlyAmortization = 3525.00m, NextDueDate = "Feb 12, 2026" }
            },
            new CustomerSummary {
                Name = "Carlos Mendoza", QueueTicket = "LA-2026-0005",
                Location = "Mandaluyong City", TransactionType = "MONTHLY",
                UnitDetails = new UnitDetailsData { Model = "Kawasaki Ninja 400", Color = "Lime Green", EngineNo = "KN4-118854" },
                FinancialStatus = new FinancialStatusData {
                    DownPaymentDue = 89500, CurrentBalance = 268500.00m,
                    MonthlyAmortization = 7458.33m, NextDueDate = "Feb 15, 2026" }
            }
        };

        // ═════════════════════════════════════════════════════════════════
        //  CARD RENDERING
        // ═════════════════════════════════════════════════════════════════
        private void DisplayCustomers(List<CustomerSummary> list)
        {
            pnlCustomerList.Controls.Clear();
            int y = 10;
            int cardW = Math.Max(200, pnlCustomerList.Width - 20);

            foreach (var c in list)
            {
                pnlCustomerList.Controls.Add(CreateCustomerCard(c, y, cardW));
                y += 90;
            }
        }

        private Guna2Panel CreateCustomerCard(CustomerSummary customer, int yPos, int cardW)
        {
            // ── Card shell ─────────────────────────────────────────────
            var card = new Guna2Panel
            {
                Size = new Size(cardW, 80),
                Location = new Point(10, yPos),
                BorderRadius = 12,
                BorderThickness = 2,
                BorderColor = cardDefaultBorderColor,
                FillColor = cardDefaultBackColor,
                Cursor = Cursors.Hand,
                Tag = customer
            };

            // ── Avatar ─────────────────────────────────────────────────
            var avatar = new Guna2Panel
            {
                Size = new Size(50, 50),
                Location = new Point(15, 15),
                BorderRadius = 25,
                FillColor = GetAvatarColor(customer.Name)
            };
            avatar.Controls.Add(new Label
            {
                Text = GetInitials(customer.Name),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            });

            // ── Name / info (left side) ────────────────────────────────
            var lblName = new Label
            {
                Text = customer.Name,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(75, 12),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            var lblInfo = new Label
            {
                Text = $"{customer.QueueTicket}  •  {customer.UnitDetails?.Model ?? ""}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(75, 35),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // ── Amount (right side — positioned relative to card width) ──
            int amtX = cardW - 170;
            var lblDueLabel = new Label
            {
                Name = "lblDueLabel",
                Text = "MONTHLY DUE",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(amtX, 18),
                Size = new Size(150, 15),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };
            var lblAmount = new Label
            {
                Name = "lblAmount",
                Text = $"₱{customer.FinancialStatus?.MonthlyAmortization:N2}",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(amtX, 35),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };

            card.Controls.Add(avatar);
            card.Controls.Add(lblName);
            card.Controls.Add(lblInfo);
            card.Controls.Add(lblDueLabel);
            card.Controls.Add(lblAmount);

            AttachHoverEffect(card);
            card.Click += (s, e) => OpenAccountSummary(customer);
            foreach (Control ctrl in card.Controls)
                ctrl.Click += (s, e) => OpenAccountSummary(customer);

            return card;
        }

        // ═════════════════════════════════════════════════════════════════
        //  NAVIGATION
        // ═════════════════════════════════════════════════════════════════
        private void OpenAccountSummary(CustomerSummary customer)
        {
            var host = this.Parent;
            if (host == null) return;

            var summaryScreen = new AccountSummaryForm2(customer)
            {
                Dock = DockStyle.Fill,
                Margin = Padding.Empty
            };

            summaryScreen.BackRequested += (s, e) =>
            {
                host.Controls.Remove(summaryScreen);
                summaryScreen.Dispose();
                host.Controls.Add(this);
                this.BringToFront();
                txtSearch.Clear();
                DisplayCustomers(customers);
            };

            summaryScreen.ContinueRequested += (s, data) => { };

            host.Controls.Add(summaryScreen);
            summaryScreen.BringToFront();
            host.Controls.Remove(this);
        }

        // ═════════════════════════════════════════════════════════════════
        //  HOVER EFFECTS
        // ═════════════════════════════════════════════════════════════════
        private void AttachHoverEffect(Guna2Panel card)
        {
            card.MouseEnter += (s, e) => SetHover(card, true);
            card.MouseLeave += (s, e) => SetHover(card, false);
            foreach (Control ctrl in card.Controls)
            {
                ctrl.MouseEnter += (s, e) => SetHover(card, true);
                ctrl.MouseLeave += (s, e) =>
                {
                    if (!card.ClientRectangle.Contains(card.PointToClient(Cursor.Position)))
                        SetHover(card, false);
                };
            }
        }

        private void SetHover(Guna2Panel card, bool on)
        {
            card.BorderColor = on ? cardHoverBorderColor : cardDefaultBorderColor;
            card.FillColor = on ? cardHoverBackColor : cardDefaultBackColor;
            card.ShadowDecoration.Enabled = on;
            if (on)
            {
                card.ShadowDecoration.Shadow = new Padding(0, 2, 8, 8);
                card.ShadowDecoration.Color = Color.FromArgb(30, cardHoverBorderColor);
            }
        }

        // ═════════════════════════════════════════════════════════════════
        //  SEARCH
        // ═════════════════════════════════════════════════════════════════
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string q = txtSearch.Text.ToLower().Trim();
            filteredCustomers = string.IsNullOrWhiteSpace(q)
                ? new List<CustomerSummary>(customers)
                : customers.Where(c =>
                      c.Name.ToLower().Contains(q) ||
                      c.QueueTicket.ToLower().Contains(q) ||
                      (c.UnitDetails?.Model?.ToLower().Contains(q) ?? false)
                  ).ToList();
            DisplayCustomers(filteredCustomers);
        }

        // ═════════════════════════════════════════════════════════════════
        //  EVENTS / RESET
        // ═════════════════════════════════════════════════════════════════
        public event EventHandler CloseRequested;

        private void BtnClose_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }

        public void ResetToInitial()
        {
            try
            {
                txtSearch.Clear();
                DisplayCustomers(customers ?? new());
                this.Visible = true;
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting form: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════
        //  AVATAR HELPERS
        // ═════════════════════════════════════════════════════════════════
        private static readonly Color[] AvatarColors =
        {
            Color.FromArgb(102, 217, 189),
            Color.FromArgb(138, 171, 255),
            Color.FromArgb(255, 186, 115),
            Color.FromArgb(188, 140, 255),
            Color.FromArgb(255, 138, 138)
        };

        private static Color GetAvatarColor(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return AvatarColors[0];
            return AvatarColors[char.ToUpper(name[0]) % AvatarColors.Length];
        }

        private static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";
            var parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length >= 2
                ? $"{parts[0][0]}{parts[1][0]}".ToUpper()
                : name.Substring(0, Math.Min(2, name.Length)).ToUpper();
        }
    }
}