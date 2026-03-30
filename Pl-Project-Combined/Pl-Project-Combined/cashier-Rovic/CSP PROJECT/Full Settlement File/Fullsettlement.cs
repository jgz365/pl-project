using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Pl_Project_Combined.Databases;

namespace POSCashierSystem
{
    public partial class FullSettlementForm : UserControl
    {
        // ── Customer list (loaded from JSON) ──────────────────────────────────
        private List<CustomerSummary> customers = null!;
        private List<CustomerSummary> filteredCustomers = new();

        // ── Hover colors ──────────────────────────────────────────────────────
        private readonly Color cardDefaultBorderColor = Color.FromArgb(224, 224, 224);
        private readonly Color cardHoverBorderColor = Color.FromArgb(255, 138, 138);   // red accent for Full Settlement
        private readonly Color cardDefaultBackColor = Color.White;
        private readonly Color cardHoverBackColor = Color.FromArgb(250, 252, 253);

        // ─────────────────────────────────────────────────────────────────────
        public FullSettlementForm()
        {
            InitializeComponent();
            LoadCustomerData();
            DisplayCustomers(customers ?? new());
        }

        // ── Load ALL customer data from customers.json ─────────────────────────
        private void LoadCustomerData()
        {
            customers = new List<CustomerSummary>();
            filteredCustomers = new List<CustomerSummary>();

            try
            {
                KioskLoanApplicationDatabase.Initialize();
                var cashierReady = KioskLoanApplicationDatabase.GetCashierReadyByMode("FullSettlement");
                customers = cashierReady.Select(MapCashierReadyToSummary).ToList();
                filteredCustomers = new List<CustomerSummary>(customers);
            }
            catch
            {
                customers = new List<CustomerSummary>();
                filteredCustomers = new List<CustomerSummary>();
            }
        }

        private static CustomerSummary MapCashierReadyToSummary(KioskLoanAssessorItem row)
        {
            decimal settlementBalance = row.TotalPayable > 0m ? row.TotalPayable : row.FinancedAmount;

            return new CustomerSummary
            {
                Name = row.FullName,
                QueueTicket = row.QueueNumber,
                Location = $"{row.City}, {row.Province}",
                TransactionType = string.IsNullOrWhiteSpace(row.ApprovedPaymentMode) ? "FullSettlement" : row.ApprovedPaymentMode,
                UnitDetails = new UnitDetailsData
                {
                    Model = row.ProductName,
                    Color = string.Empty,
                    EngineNo = string.Empty
                },
                FinancialStatus = new FinancialStatusData
                {
                    DownPaymentDue = row.ApprovedDownPayment ?? row.DownPaymentAmount,
                    CurrentBalance = settlementBalance,
                    MonthlyAmortization = row.ApprovedMonthlyDue ?? row.MonthlyAmortization,
                    NextDueDate = row.AssessorDecidedAt?.ToString("MMM dd, yyyy") ?? row.SubmittedAt.ToString("MMM dd, yyyy")
                }
            };
        }

        // ─────────────────────────────────────────────────────────────────────
        // Card display
        // ─────────────────────────────────────────────────────────────────────
        private void DisplayCustomers(List<CustomerSummary> list)
        {
            pnlCustomerList.Controls.Clear();
            int y = 10;
            foreach (var c in list)
            {
                pnlCustomerList.Controls.Add(CreateCustomerCard(c, y));
                y += 90;
            }
        }

        private Guna2Panel CreateCustomerCard(CustomerSummary customer, int yPosition)
        {
            var card = new Guna2Panel
            {
                Size = new Size(660, 80),
                Location = new Point(10, yPosition),
                BorderRadius = 12,
                BorderThickness = 2,
                BorderColor = cardDefaultBorderColor,
                FillColor = cardDefaultBackColor,
                Cursor = Cursors.Hand,
                Tag = customer
            };

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
            var lblDueLabel = new Label
            {
                Text = "SETTLEMENT BALANCE",
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(480, 18),
                Size = new Size(160, 15),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };
            var lblAmount = new Label
            {
                Text = $"₱{customer.FinancialStatus?.CurrentBalance:N2}",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),   // red to highlight full balance
                Location = new Point(480, 35),
                Size = new Size(160, 25),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };

            card.Controls.Add(avatar);
            card.Controls.Add(lblName);
            card.Controls.Add(lblInfo);
            card.Controls.Add(lblDueLabel);
            card.Controls.Add(lblAmount);

            AttachHoverEffect(card);

            // ── CLICK: open AccountSummaryForm4 (Full Settlement version) ─────
            card.Click += (s, e) => OpenAccountSummary(customer);
            foreach (Control ctrl in card.Controls)
                ctrl.Click += (s, e) => OpenAccountSummary(customer);

            return card;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Navigation: open AccountSummaryForm4
        // ─────────────────────────────────────────────────────────────────────
        private void OpenAccountSummary(CustomerSummary customer)
        {
            var host = this.Parent;
            if (host == null) return;

            var summaryScreen = new AccountSummaryForm4(customer)   // ← Full Settlement version
            {
                Size = this.Size,
                Dock = DockStyle.Fill
            };

            summaryScreen.BackRequested += (s, e) =>
            {
                host.Controls.Remove(summaryScreen);
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

        // ─────────────────────────────────────────────────────────────────────
        // Hover effects
        // ─────────────────────────────────────────────────────────────────────
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

        private void SetHover(Guna2Panel card, bool hovered)
        {
            card.BorderColor = hovered ? cardHoverBorderColor : cardDefaultBorderColor;
            card.FillColor = hovered ? cardHoverBackColor : cardDefaultBackColor;
            card.ShadowDecoration.Enabled = hovered;
            if (hovered)
            {
                card.ShadowDecoration.Shadow = new Padding(0, 2, 8, 8);
                card.ShadowDecoration.Color = Color.FromArgb(30, cardHoverBorderColor);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Search
        // ─────────────────────────────────────────────────────────────────────
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

        // ─────────────────────────────────────────────────────────────────────
        // Close / Load / Reset
        // ─────────────────────────────────────────────────────────────────────
        public event EventHandler CloseRequested;

        private void BtnClose_Click(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }

        private void FullSettlementForm_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
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

        // ─────────────────────────────────────────────────────────────────────
        // Avatar helpers
        // ─────────────────────────────────────────────────────────────────────
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