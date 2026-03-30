using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using customer_kiosk;
using Guna.UI2.WinForms;
using Pl_Project_Combined.Databases;

namespace Assessor_Eddion
{
    public partial class Assessordeskeddion : Form
    {
        private readonly List<KioskLoanAssessorItem> applications = new();
        private KioskLoanAssessorItem? selectedApplication;
        private Guna2Panel? selectedQueueCard;
        private string currentFilter = "ALL";
        private Guna2ComboBox? cmbPaymentMode;
        private Guna2TextBox? txtApprovedDownPayment;
        private Guna2TextBox? txtApprovedAdvancePayment;
        private Guna2TextBox? txtApprovedMonthlyDue;
        private Guna2TextBox? txtApprovedTermMonths;
        private Guna2TextBox? txtAssessorNotes;

        public Assessordeskeddion()
        {
            InitializeComponent();

            PreparePanels();

            CustomerProfileButton.Click -= CustomerProfileButton_Click;
            CustomerProfileButton.Click += CustomerProfileButton_Click;

            FinancialAnalysisButton.Click -= FinancialAnalysisButton_Click;
            FinancialAnalysisButton.Click += FinancialAnalysisButton_Click;

            UnitDetailsButton.Click -= UnitDetailsButton_Click;
            UnitDetailsButton.Click += UnitDetailsButton_Click;

            VerificationButton.Click -= VerificationButton_Click;
            VerificationButton.Click += VerificationButton_Click;

            AssessmentNotesButton.Click -= AssessmentNotesButton_Click;
            AssessmentNotesButton.Click += AssessmentNotesButton_Click;

            AuditTrailButton.Click -= AuditTrailButton_Click;
            AuditTrailButton.Click += AuditTrailButton_Click;

            InitializeAssessorDataUi();
        }

        private void InitializeAssessorDataUi()
        {
            guna2TextBox1.PlaceholderText = "Search name or queue no...";
            guna2TextBox1.TextChanged -= SearchTextBox_TextChanged;
            guna2TextBox1.TextChanged += SearchTextBox_TextChanged;

            guna2Button2.Click += (_, _) => ApplyFilter("ALL");
            guna2Button3.Click += (_, _) => ApplyFilter("Pending");
            guna2Button4.Click += (_, _) => ApplyFilter("For Review");
            guna2Button5.Click += (_, _) => ApplyFilter("Approved");
            guna2Button6.Click += (_, _) => ApplyFilter("Rejected");

            try
            {
                KioskLoanApplicationDatabase.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to initialize assessor pipeline.\n\n{ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            ShowOnlyPanel(CustomerProfilePanel);
            RefreshAssessorView();
        }

        private void SearchTextBox_TextChanged(object? sender, EventArgs e)
        {
            RefreshAssessorView();
        }

        private void ApplyFilter(string filter)
        {
            currentFilter = filter;
            RefreshAssessorView();
        }

        private void RefreshAssessorView()
        {
            applications.Clear();
            applications.AddRange(KioskLoanApplicationDatabase.GetApplications(guna2TextBox1.Text));

            UpdateQueueCounters();
            RenderQueueCards();

            if (applications.Count == 0)
            {
                selectedApplication = null;
                RenderEmptyPanels("No applications available.");
                return;
            }

            if (selectedApplication == null || applications.All(a => a.Id != selectedApplication.Id))
            {
                SelectApplication(applications[0], null);
            }
            else
            {
                BindSelectedApplication();
            }
        }

        private IEnumerable<KioskLoanAssessorItem> GetFilteredApplications()
        {
            return currentFilter == "ALL"
                ? applications
                : applications.Where(a => string.Equals(a.ApplicationStatus, currentFilter, StringComparison.OrdinalIgnoreCase));
        }

        private void UpdateQueueCounters()
        {
            int all = applications.Count;
            int pending = applications.Count(a => IsStatus(a.ApplicationStatus, "Pending"));
            int review = applications.Count(a => IsStatus(a.ApplicationStatus, "For Review"));
            int approved = applications.Count(a => IsStatus(a.ApplicationStatus, "Approved"));
            int rejected = applications.Count(a => IsStatus(a.ApplicationStatus, "Rejected"));

            guna2Button2.Text = $"ALL\n{all}";
            guna2Button3.Text = $"NEW\n{pending}";
            guna2Button4.Text = $"HOLD\n{review}";
            guna2Button5.Text = $"DONE\n{approved}";
            guna2Button6.Text = $"NO\n{rejected}";
        }

        private static bool IsStatus(string current, string expected)
            => string.Equals(current, expected, StringComparison.OrdinalIgnoreCase);

        private void RenderQueueCards()
        {
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel3.Controls.Clear();

            foreach (var item in GetFilteredApplications())
            {
                var card = CreateQueueCard(item);
                flowLayoutPanel3.Controls.Add(card);
            }

            flowLayoutPanel3.ResumeLayout();
        }

        private Guna2Panel CreateQueueCard(KioskLoanAssessorItem item)
        {
            var card = new Guna2Panel
            {
                Width = Math.Max(320, flowLayoutPanel3.ClientSize.Width - 26),
                Height = 98,
                Margin = new Padding(6, 6, 6, 0),
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderThickness = 1,
                FillColor = Color.White,
                CustomBorderThickness = new Padding(4, 0, 0, 0),
                CustomBorderColor = GetStatusAccent(item.ApplicationStatus),
                BorderRadius = 8,
                Cursor = Cursors.Hand,
                Tag = item
            };

            var nameLabel = new Label
            {
                AutoSize = false,
                Location = new Point(14, 10),
                Size = new Size(card.Width - 30, 24),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Text = item.FullName
            };

            var metaLabel = new Label
            {
                AutoSize = false,
                Location = new Point(14, 35),
                Size = new Size(card.Width - 30, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Text = $"Q#{item.QueueNumber} • {item.ProductName}"
            };

            var statusLabel = new Label
            {
                AutoSize = false,
                Location = new Point(14, 62),
                Size = new Size(110, 24),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = item.ApplicationStatus.ToUpperInvariant(),
                BackColor = GetStatusBackground(item.ApplicationStatus),
                ForeColor = GetStatusText(item.ApplicationStatus)
            };

            var timeLabel = new Label
            {
                AutoSize = false,
                Location = new Point(card.Width - 116, 64),
                Size = new Size(102, 18),
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleRight,
                Text = item.SubmittedAt.ToString("hh:mm tt")
            };

            card.Controls.Add(nameLabel);
            card.Controls.Add(metaLabel);
            card.Controls.Add(statusLabel);
            card.Controls.Add(timeLabel);

            WireClick(card, () => SelectApplication(item, card));

            if (selectedApplication != null && selectedApplication.Id == item.Id)
            {
                ApplyCardSelectionStyle(card, true);
                selectedQueueCard = card;
            }

            return card;
        }

        private static void WireClick(Control root, Action callback)
        {
            root.Click += (_, _) => callback();
            foreach (Control child in root.Controls)
            {
                WireClick(child, callback);
            }
        }

        private void SelectApplication(KioskLoanAssessorItem item, Guna2Panel? card)
        {
            selectedApplication = item;

            if (selectedQueueCard != null && !selectedQueueCard.IsDisposed)
            {
                ApplyCardSelectionStyle(selectedQueueCard, false);
            }

            if (card != null)
            {
                selectedQueueCard = card;
                ApplyCardSelectionStyle(card, true);
            }
            else
            {
                var matched = flowLayoutPanel3.Controls
                    .OfType<Guna2Panel>()
                    .FirstOrDefault(p => p.Tag is KioskLoanAssessorItem row && row.Id == item.Id);

                if (matched != null)
                {
                    selectedQueueCard = matched;
                    ApplyCardSelectionStyle(matched, true);
                }
            }

            BindSelectedApplication();
            ShowOnlyPanel(CustomerProfilePanel);
        }

        private static void ApplyCardSelectionStyle(Guna2Panel card, bool selected)
        {
            card.BorderThickness = selected ? 2 : 1;
            card.BorderColor = selected ? Color.FromArgb(37, 99, 235) : Color.FromArgb(226, 232, 240);
            card.FillColor = selected ? Color.FromArgb(248, 250, 252) : Color.White;
        }

        private void BindSelectedApplication()
        {
            if (selectedApplication == null)
            {
                RenderEmptyPanels("Select an application to view details.");
                return;
            }

            var item = selectedApplication;
            guna2HtmlLabel5.Text = item.FullName;
            guna2HtmlLabel7.Text = $"Q#{item.QueueNumber}";
            guna2HtmlLabel9.Text = item.SubmittedAt.ToString("MMM dd, yyyy");
            guna2HtmlLabel10.Text = $"{item.ApplicationStatus.ToUpperInvariant()} • CASHIER: {item.CashierStatus.ToUpperInvariant()}";
            guna2HtmlLabel10.ForeColor = GetStatusText(item.ApplicationStatus);
            guna2HtmlLabel12.Text = item.SubmittedAt.ToString("hh:mm tt");

            int risk = CalculateRiskScore(item);
            guna2HtmlLabel14.Text = risk.ToString();
            guna2Panel9.FillColor = risk >= 70 ? Color.FromArgb(254, 243, 199) : Color.FromArgb(220, 252, 231);
            guna2HtmlLabel14.ForeColor = risk >= 70 ? Color.FromArgb(146, 64, 14) : Color.FromArgb(22, 101, 52);

            RenderCustomerProfilePanel(item);
            RenderFinancialAnalysisPanel(item);
            RenderUnitDetailsPanel(item);
            RenderVerificationPanel(item);
            RenderAssessmentNotesPanel(item);
            RenderAuditTrailPanel(item);
        }

        private static int CalculateRiskScore(KioskLoanAssessorItem item)
        {
            decimal totalIncome = item.GrossIncome + item.OtherIncome;
            decimal ratio = totalIncome <= 0m ? 100m : (item.MonthlyAmortization / totalIncome) * 100m;
            decimal baseScore = Math.Min(100m, Math.Max(10m, ratio * 2m));
            return (int)Math.Round(baseScore, MidpointRounding.AwayFromZero);
        }

        private static Color GetStatusAccent(string status)
        {
            if (IsStatus(status, "Approved")) return Color.FromArgb(34, 197, 94);
            if (IsStatus(status, "For Review")) return Color.FromArgb(245, 158, 11);
            if (IsStatus(status, "Rejected")) return Color.FromArgb(239, 68, 68);
            return Color.FromArgb(59, 130, 246);
        }

        private static Color GetStatusBackground(string status)
        {
            if (IsStatus(status, "Approved")) return Color.FromArgb(220, 252, 231);
            if (IsStatus(status, "For Review")) return Color.FromArgb(254, 243, 199);
            if (IsStatus(status, "Rejected")) return Color.FromArgb(254, 226, 226);
            return Color.FromArgb(219, 234, 254);
        }

        private static Color GetStatusText(string status)
        {
            if (IsStatus(status, "Approved")) return Color.FromArgb(22, 101, 52);
            if (IsStatus(status, "For Review")) return Color.FromArgb(146, 64, 14);
            if (IsStatus(status, "Rejected")) return Color.FromArgb(153, 27, 27);
            return Color.FromArgb(30, 64, 175);
        }

        private void RenderEmptyPanels(string message)
        {
            RenderPanelMessage(CustomerProfilePanel, message);
            RenderPanelMessage(FinancialAnalysisPanel, message);
            RenderPanelMessage(UnitDetailsPanel, message);
            RenderPanelMessage(VerificationPanel, message);
            RenderPanelMessage(AssessmentNotesPanel, message);
            RenderPanelMessage(AuditTrailPanel, message);
            guna2HtmlLabel5.Text = "Applicant Name";
            guna2HtmlLabel7.Text = "Queue Number";
            guna2HtmlLabel9.Text = "Date";
            guna2HtmlLabel10.Text = "•";
            guna2HtmlLabel12.Text = "Time";
            guna2HtmlLabel14.Text = "--";
        }

        private static void RenderPanelMessage(Control panel, string message)
        {
            panel.Controls.Clear();

            var label = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                Text = message
            };

            panel.Controls.Add(label);
        }

        private void RenderCustomerProfilePanel(KioskLoanAssessorItem item)
        {
            RenderCards(CustomerProfilePanel,
                CreateDetailCard("PERSONAL DETAILS", new[]
                {
                    ("Full Name", item.FullName),
                    ("Date of Birth", item.DateOfBirth.ToString("MMMM dd, yyyy")),
                    ("Email", item.Email),
                    ("Mobile", item.Mobile),
                    ("Address", $"{item.Address}, {item.City}, {item.Province}")
                }),
                CreateDetailCard("EMPLOYMENT", new[]
                {
                    ("Status", item.EmploymentStatus),
                    ("Company", item.CompanyOrBusinessName),
                    ("Position", item.PositionTitle),
                    ("Years", item.YearsEmployed?.ToString() ?? "N/A")
                }));
        }

        private void RenderFinancialAnalysisPanel(KioskLoanAssessorItem item)
        {
            decimal totalIncome = item.GrossIncome + item.OtherIncome;
            decimal ratio = totalIncome <= 0m ? 0m : (item.MonthlyAmortization / totalIncome) * 100m;

            RenderCards(FinancialAnalysisPanel,
                CreateDetailCard("INCOME SUMMARY", new[]
                {
                    ("Gross Income", FormatPeso(item.GrossIncome)),
                    ("Other Income", FormatPeso(item.OtherIncome)),
                    ("Total Income", FormatPeso(totalIncome)),
                    ("Total Obligations", FormatPeso(item.TotalObligations))
                }),
                CreateDetailCard("AFFORDABILITY", new[]
                {
                    ("Monthly Amortization", FormatPeso(item.MonthlyAmortization)),
                    ("Debt-to-Income Ratio", $"{ratio:0.00}%"),
                    ("Existing Loans", BuildExistingLoansText(item)),
                    ("Risk Score", CalculateRiskScore(item).ToString())
                }));
        }

        private void RenderUnitDetailsPanel(KioskLoanAssessorItem item)
        {
            RenderCards(UnitDetailsPanel,
                CreateDetailCard("UNIT DETAILS", new[]
                {
                    ("Product", item.ProductName),
                    ("Model Year", item.ProductYear),
                    ("Price", FormatPeso(item.ProductPrice)),
                    ("Term", item.SelectedTermMonths > 0 ? $"{item.SelectedTermMonths} months" : "N/A")
                }),
                CreateDetailCard("LOAN BREAKDOWN", new[]
                {
                    ("Down Payment", $"{item.DownPaymentPercent}% ({FormatPeso(item.DownPaymentAmount)})"),
                    ("Financed Amount", FormatPeso(item.FinancedAmount)),
                    ("Interest", FormatPeso(item.InterestAmount)),
                    ("Total Payable", FormatPeso(item.TotalPayable))
                }));
        }

        private void RenderVerificationPanel(KioskLoanAssessorItem item)
        {
            RenderCards(VerificationPanel,
                CreateDetailCard("DOCUMENT CHECKLIST", new[]
                {
                    ("Valid Government ID", ToYesNo(item.DocValidGovernmentId)),
                    ("Proof of Address", ToYesNo(item.DocProofOfAddress)),
                    ("Employment/Business Proof", ToYesNo(item.DocEmploymentOrBusiness)),
                    ("Payslips", ToYesNo(item.DocPayslips)),
                    ("Proof of Income", ToYesNo(item.DocProofOfIncome))
                }),
                CreateDetailCard("VERIFICATION STATUS", new[]
                {
                    ("Selected Documents", item.SelectedDocumentCount.ToString()),
                    ("Terms Accepted", ToYesNo(item.AgreedToTerms)),
                    ("Queue Number", item.QueueNumber),
                    ("Application Status", item.ApplicationStatus)
                }));
        }

        private void RenderAssessmentNotesPanel(KioskLoanAssessorItem item)
        {
            AssessmentNotesPanel.Controls.Clear();

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White,
                ColumnCount = 2,
                RowCount = 1
            };
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));

            var summaryCard = CreateDetailCard("ASSESSOR NOTES", new[]
            {
                ("Suggested Decision", BuildSuggestedDecision(item)),
                ("Primary Concern", BuildPrimaryConcern(item)),
                ("Next Action", "Finalize payment mode and forward to cashier queue."),
                ("Current Assessor", string.IsNullOrWhiteSpace(item.AssessorBy) ? "Staff #042" : item.AssessorBy)
            });

            var decisionCard = BuildDecisionCard(item);

            summaryCard.Dock = DockStyle.Fill;
            decisionCard.Dock = DockStyle.Fill;

            container.Controls.Add(summaryCard, 0, 0);
            container.Controls.Add(decisionCard, 1, 0);

            AssessmentNotesPanel.Controls.Add(container);
        }

        private void RenderAuditTrailPanel(KioskLoanAssessorItem item)
        {
            RenderCards(AuditTrailPanel,
                CreateDetailCard("AUDIT TRAIL", new[]
                {
                    ("Created", item.SubmittedAt.ToString("MMM dd, yyyy hh:mm tt")),
                    ("Source", "Customer Kiosk"),
                    ("Current Status", item.ApplicationStatus),
                    ("Record ID", item.Id.ToString())
                }));
        }

        private void RenderCards(Control panel, params Control[] cards)
        {
            panel.Controls.Clear();

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White,
                ColumnCount = cards.Length > 1 ? 2 : 1,
                RowCount = cards.Length > 1 ? (int)Math.Ceiling(cards.Length / 2d) : cards.Length,
                AutoScroll = true
            };

            if (cards.Length > 1)
            {
                container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }
            else
            {
                container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            }

            for (int i = 0; i < container.RowCount; i++)
            {
                container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            for (int i = 0; i < cards.Length; i++)
            {
                int row = cards.Length > 1 ? i / 2 : i;
                int col = cards.Length > 1 ? i % 2 : 0;
                cards[i].Dock = DockStyle.Fill;
                container.Controls.Add(cards[i], col, row);
            }

            panel.Controls.Add(container);
        }

        private static Guna2Panel CreateDetailCard(string title, IEnumerable<(string Label, string Value)> rows)
        {
            var card = new Guna2Panel
            {
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 12,
                BorderThickness = 1,
                FillColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(14),
                Height = 220
            };

            var titleLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Text = title
            };

            var body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                AutoScroll = true
            };

            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            foreach (var (label, value) in rows)
            {
                var rowPanel = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 44,
                    Padding = new Padding(0, 2, 0, 2)
                };

                var key = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 16,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Text = label.ToUpperInvariant()
                };

                var val = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 22,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    Text = value
                };

                rowPanel.Controls.Add(val);
                rowPanel.Controls.Add(key);
                body.Controls.Add(rowPanel);
            }

            card.Controls.Add(body);
            card.Controls.Add(titleLabel);
            return card;
        }

        private Guna2Panel BuildDecisionCard(KioskLoanAssessorItem item)
        {
            var card = new Guna2Panel
            {
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 12,
                BorderThickness = 1,
                FillColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(14)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            for (int i = 0; i < 7; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 6 ? 50 : 42));
            }

            var title = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Text = "APPROVAL HANDOFF"
            };

            cmbPaymentMode = new Guna2ComboBox
            {
                Dock = DockStyle.Fill,
                BorderRadius = 8,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                ItemHeight = 30,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
            };
            cmbPaymentMode.Items.Clear();
            cmbPaymentMode.Items.AddRange(new object[]
            {
                "DownPayment",
                "AdvancePayment",
                "MonthlyPayment",
                "FullCash",
                "FullSettlement"
            });
            cmbPaymentMode.SelectedIndex = GetModeIndex(item.ApprovedPaymentMode);
            cmbPaymentMode.SelectedIndexChanged += (_, _) => ApplyModeFieldState();

            txtApprovedDownPayment = CreateDecisionTextBox(item.ApprovedDownPayment ?? item.DownPaymentAmount);
            txtApprovedAdvancePayment = CreateDecisionTextBox(item.ApprovedAdvancePayment ?? item.MonthlyAmortization);
            txtApprovedMonthlyDue = CreateDecisionTextBox(item.ApprovedMonthlyDue ?? item.MonthlyAmortization);
            txtApprovedTermMonths = CreateDecisionTextBox((item.ApprovedTermMonths ?? item.SelectedTermMonths).ToString());
            txtAssessorNotes = CreateDecisionTextBox(string.IsNullOrWhiteSpace(item.AssessorNotes)
                ? "Validated documents and approved for cashier processing."
                : item.AssessorNotes);

            var btnForward = new Guna2Button
            {
                Dock = DockStyle.Fill,
                Text = "Approve & Send to Cashier",
                BorderRadius = 10,
                FillColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold)
            };
            btnForward.Click += (_, _) => ApproveAndForwardToCashier();

            AddDecisionRow(layout, 0, "Payment Mode", cmbPaymentMode);
            AddDecisionRow(layout, 1, "Down Payment", txtApprovedDownPayment);
            AddDecisionRow(layout, 2, "Advance Payment", txtApprovedAdvancePayment);
            AddDecisionRow(layout, 3, "Monthly Due", txtApprovedMonthlyDue);
            AddDecisionRow(layout, 4, "Term (months)", txtApprovedTermMonths);
            AddDecisionRow(layout, 5, "Assessor Notes", txtAssessorNotes);
            layout.Controls.Add(btnForward, 0, 6);
            layout.SetColumnSpan(btnForward, 2);

            card.Controls.Add(layout);
            card.Controls.Add(title);

            ApplyModeFieldState();
            return card;
        }

        private void AddDecisionRow(TableLayoutPanel layout, int row, string label, Control editor)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105)
            };

            layout.Controls.Add(lbl, 0, row);
            layout.Controls.Add(editor, 1, row);
        }

        private static Guna2TextBox CreateDecisionTextBox(decimal value)
            => CreateDecisionTextBox(value.ToString("0.##"));

        private static Guna2TextBox CreateDecisionTextBox(string value)
        {
            return new Guna2TextBox
            {
                Dock = DockStyle.Fill,
                BorderRadius = 8,
                Text = value,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
            };
        }

        private int GetModeIndex(string approvedMode)
        {
            if (string.IsNullOrWhiteSpace(approvedMode))
            {
                return 0;
            }

            string mode = approvedMode.Replace(" ", string.Empty).Trim();
            int itemCount = cmbPaymentMode?.Items.Count ?? 0;
            for (int i = 0; i < itemCount; i++)
            {
                if (string.Equals(cmbPaymentMode.Items[i]?.ToString()?.Replace(" ", string.Empty), mode, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return 0;
        }

        private void ApplyModeFieldState()
        {
            string mode = cmbPaymentMode?.SelectedItem?.ToString() ?? "DownPayment";

            if (txtApprovedDownPayment != null) txtApprovedDownPayment.Enabled = mode == "DownPayment";
            if (txtApprovedAdvancePayment != null) txtApprovedAdvancePayment.Enabled = mode == "AdvancePayment";
            if (txtApprovedMonthlyDue != null) txtApprovedMonthlyDue.Enabled = mode == "MonthlyPayment";
            if (txtApprovedTermMonths != null) txtApprovedTermMonths.Enabled = mode == "DownPayment" || mode == "MonthlyPayment";
        }

        private void ApproveAndForwardToCashier()
        {
            if (selectedApplication == null || cmbPaymentMode == null)
            {
                return;
            }

            string mode = cmbPaymentMode.SelectedItem?.ToString() ?? "DownPayment";
            decimal? approvedDp = ParseNullableDecimal(txtApprovedDownPayment?.Text);
            decimal? approvedAdvance = ParseNullableDecimal(txtApprovedAdvancePayment?.Text);
            decimal? approvedMonthly = ParseNullableDecimal(txtApprovedMonthlyDue?.Text);
            int? approvedTerm = ParseNullableInt(txtApprovedTermMonths?.Text);

            if (mode == "DownPayment" && (!approvedDp.HasValue || approvedDp.Value <= 0m))
            {
                MessageBox.Show("Please enter a valid approved down payment amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mode == "AdvancePayment" && (!approvedAdvance.HasValue || approvedAdvance.Value <= 0m))
            {
                MessageBox.Show("Please enter a valid approved advance payment amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mode == "MonthlyPayment" && (!approvedMonthly.HasValue || approvedMonthly.Value <= 0m))
            {
                MessageBox.Show("Please enter a valid approved monthly due amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var decision = new KioskAssessorDecision
            {
                ApplicationId = selectedApplication.Id,
                ApprovedPaymentMode = mode,
                ApprovedDownPayment = approvedDp,
                ApprovedAdvancePayment = approvedAdvance,
                ApprovedMonthlyDue = approvedMonthly,
                ApprovedTermMonths = approvedTerm,
                AssessorNotes = txtAssessorNotes?.Text.Trim() ?? string.Empty,
                AssessorBy = "Staff #042"
            };

            if (!KioskLoanApplicationDatabase.SaveAssessorDecision(decision))
            {
                MessageBox.Show("Unable to approve and forward to cashier.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Application approved and forwarded to cashier queue.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshAssessorView();
        }

        private static decimal? ParseNullableDecimal(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return decimal.TryParse(value, out decimal parsed) ? parsed : null;
        }

        private static int? ParseNullableInt(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return int.TryParse(value, out int parsed) ? parsed : null;
        }

        private static string BuildExistingLoansText(KioskLoanAssessorItem item)
        {
            var loans = new List<string>();
            if (item.HasHomeLoan) loans.Add("Home Loan");
            if (item.HasCarLoan) loans.Add("Car Loan");
            if (item.HasPersonalLoan) loans.Add("Personal Loan");
            if (item.HasCreditCard) loans.Add("Credit Card");
            return loans.Count == 0 ? "None" : string.Join(", ", loans);
        }

        private static string BuildSuggestedDecision(KioskLoanAssessorItem item)
        {
            decimal totalIncome = item.GrossIncome + item.OtherIncome;
            if (totalIncome <= 0m) return "For Review";

            decimal ratio = (item.MonthlyAmortization / totalIncome) * 100m;
            if (ratio <= 35m) return "Approve (subject to document validation)";
            if (ratio <= 45m) return "Manual Review Required";
            return "High Risk - Further Verification Needed";
        }

        private static string BuildPrimaryConcern(KioskLoanAssessorItem item)
        {
            if (!item.AgreedToTerms) return "Terms not accepted";
            if (item.SelectedDocumentCount < 3) return "Insufficient submitted documents";
            if (item.GrossIncome < 15000m) return "Income below threshold";
            return "No immediate blocker detected";
        }

        private static string ToYesNo(bool value) => value ? "Yes" : "No";

        private static string FormatPeso(decimal value) => $"₱{value:N2}";

        // ════════════════════════════════════════════════════════════════════
        //  LOGOUT — redirect to start_screen (hosted by main_menu)
        // ════════════════════════════════════════════════════════════════════
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            RedirectToStartScreen();
        }

        private void RedirectToStartScreen()
        {
            var kioskMainMenu = Application.OpenForms.OfType<main_menu>().FirstOrDefault();

            if (kioskMainMenu == null)
            {
                kioskMainMenu = new main_menu
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
            }

            kioskMainMenu.Show();
            kioskMainMenu.BringToFront();
            Hide();
        }

        private void CustomerProfileButton_Click(object sender, EventArgs e)
        {
            bool willShow = !CustomerProfilePanel.Visible;
            ShowOnlyPanel(willShow ? CustomerProfilePanel : null);
            SetChecked(CustomerProfileButton, willShow);
        }

        private void FinancialAnalysisButton_Click(object sender, EventArgs e)
        {
            bool willShow = !FinancialAnalysisPanel.Visible;
            ShowOnlyPanel(willShow ? FinancialAnalysisPanel : null);
            SetChecked(FinancialAnalysisButton, willShow);
        }

        private void UnitDetailsButton_Click(object sender, EventArgs e)
        {
            bool willShow = !UnitDetailsPanel.Visible;
            ShowOnlyPanel(willShow ? UnitDetailsPanel : null);
            SetChecked(UnitDetailsButton, willShow);
        }

        private void VerificationButton_Click(object sender, EventArgs e)
        {
            bool willShow = !VerificationPanel.Visible;
            ShowOnlyPanel(willShow ? VerificationPanel : null);
            SetChecked(VerificationButton, willShow);
        }

        private void AssessmentNotesButton_Click(object sender, EventArgs e)
        {
            bool willShow = !AssessmentNotesPanel.Visible;
            ShowOnlyPanel(willShow ? AssessmentNotesPanel : null);
            SetChecked(AssessmentNotesButton, willShow);
        }

        private void AuditTrailButton_Click(object sender, EventArgs e)
        {
            bool willShow = !AuditTrailPanel.Visible;
            ShowOnlyPanel(willShow ? AuditTrailPanel : null);
            SetChecked(AuditTrailButton, willShow);
        }

        private void PreparePanels()
        {
            var panels = new Control[]
            {
                CustomerProfilePanel,
                FinancialAnalysisPanel,
                UnitDetailsPanel,
                VerificationPanel,
                AssessmentNotesPanel,
                AuditTrailPanel
            };

            if (PanelSpace == null) throw new InvalidOperationException("PanelSpace is null. Check Designer.");

            foreach (var p in panels)
            {
                if (p == null) continue;

                if (p.Parent != PanelSpace)
                {
                    p.Parent?.Controls.Remove(p);
                    PanelSpace.Controls.Add(p);
                }

                p.Dock = DockStyle.Fill;
                p.Visible = false;
            }

            if (PanelSpace.Dock == DockStyle.None)
                PanelSpace.Dock = DockStyle.Fill;
        }

        private void ShowOnlyPanel(Control? panelToShow)
        {
            CustomerProfilePanel.Visible = false;
            FinancialAnalysisPanel.Visible = false;
            UnitDetailsPanel.Visible = false;
            VerificationPanel.Visible = false;
            AssessmentNotesPanel.Visible = false;
            AuditTrailPanel.Visible = false;

            if (panelToShow == null)
            {
                SetChecked(CustomerProfileButton, false);
                SetChecked(FinancialAnalysisButton, false);
                SetChecked(UnitDetailsButton, false);
                SetChecked(VerificationButton, false);
                SetChecked(AssessmentNotesButton, false);
                SetChecked(AuditTrailButton, false);
                return;
            }

            if (panelToShow.Parent != PanelSpace)
            {
                panelToShow.Parent?.Controls.Remove(panelToShow);
                PanelSpace.Controls.Add(panelToShow);
            }

            panelToShow.Dock = DockStyle.Fill;
            panelToShow.Visible = true;
            panelToShow.BringToFront();

            SetChecked(CustomerProfileButton, panelToShow == CustomerProfilePanel);
            SetChecked(FinancialAnalysisButton, panelToShow == FinancialAnalysisPanel);
            SetChecked(UnitDetailsButton, panelToShow == UnitDetailsPanel);
            SetChecked(VerificationButton, panelToShow == VerificationPanel);
            SetChecked(AssessmentNotesButton, panelToShow == AssessmentNotesPanel);
            SetChecked(AuditTrailButton, panelToShow == AuditTrailPanel);
        }

        private void SetChecked(Control btn, bool value)
        {
            if (btn == null) return;
            var prop = btn.GetType().GetProperty("Checked", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null || prop.PropertyType != typeof(bool) || !prop.CanWrite) return;
            try { prop.SetValue(btn, value); }
            catch { }
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e) { }
        private void guna2HtmlLabel18_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel59_Click(object sender, EventArgs e) { }
        private void tableLayoutPanel54_Paint(object sender, PaintEventArgs e) { }
    }
}