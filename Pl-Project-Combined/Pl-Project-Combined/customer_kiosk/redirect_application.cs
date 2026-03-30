using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Pl_Project_Combined.Databases;

namespace customer_kiosk
{
    public partial class redirect_application : UserControl
    {
        private const int RequiredDocumentCount = 3;
        private const string QueueCharacters = "0123456789";
        private static readonly HttpClient ProductImageClient = new();

        private enum WizardStep
        {
            Personal = 0,
            Employment = 1,
            Financial = 2,
            Loan = 3
        }

        private readonly OnScreenKeyboard onScreenKeyboard;
        private WizardStep currentStep = WizardStep.Personal;

        private readonly Guna2Panel employmentStatusPanel = new();
        private readonly Guna2RadioButton rbEmployed = new();
        private readonly Guna2RadioButton rbSelfEmployed = new();
        private readonly Guna2RadioButton rbBusinessOwner = new();
        private readonly Guna2Panel financialLoansPanel = new();
        private readonly Guna2CheckBox chkHomeLoan = new();
        private readonly Guna2CheckBox chkCarLoan = new();
        private readonly Guna2CheckBox chkPersonalLoan = new();
        private readonly Guna2CheckBox chkCreditCard = new();
        private readonly Guna2Panel loanChecklistPanel = new();
        private readonly Guna2Panel loanTermsPanel = new();
        private readonly Guna2HtmlLabel loanChecklistTitle = new();
        private readonly Guna2HtmlLabel loanChecklistSubtitle = new();
        private readonly Guna2CheckBox chkDocValidId = new();
        private readonly Guna2CheckBox chkDocProofAddress = new();
        private readonly Guna2CheckBox chkDocEmploymentCert = new();
        private readonly Guna2CheckBox chkDocPayslips = new();
        private readonly Guna2CheckBox chkDocProofIncome = new();
        private readonly Guna2ProgressBar docRequiredProgress = new();
        private readonly Guna2HtmlLabel docRequiredCount = new();
        private readonly Guna2CheckBox chkAgreeTerms = new();
        private readonly Guna2HtmlLabel termsSubtitle = new();
        private readonly Guna2DateTimePicker dtpDob = new();
        private readonly Guna2ComboBox cbCity = new();
        private readonly Guna2HtmlLabel validationMessageLabel = new();

        private string personalFullName = string.Empty;
        private string personalEmail = string.Empty;
        private string personalMobile = string.Empty;
        private string personalAddress = string.Empty;
        private DateTime personalDob = new DateTime(2000, 1, 1);
        private int personalCityIndex;
        private int personalProvinceIndex;

        private string employmentCompanyName = string.Empty;
        private string employmentPositionTitle = string.Empty;
        private string employmentYears = string.Empty;

        private string financialGrossIncome = string.Empty;
        private string financialOtherIncome = string.Empty;
        private string financialTotalObligations = string.Empty;
        private string currentProductImageUrl = string.Empty;
        private Image? currentProductImage;

        private ResponsiveScaler? responsiveScaler;
        private const float BaseWidth = 1920f;
        private const float BaseHeight = 1080f;

        private const decimal DefaultBasePrice = 145000m;
        private int selectedTermMonths;
        private readonly Dictionary<int, decimal> annualRateByTerm = new()
        {
            { 6, 0.15m },
            { 12, 0.18m },
            { 24, 0.20m },
            { 36, 0.22m }
        };

        public redirect_application()
        {
            InitializeComponent();


            onScreenKeyboard = new OnScreenKeyboard
            {
                Size = new Size(1880, 410),
                Location = new Point(20, 650),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Visible = false
            };

            onScreenKeyboard.CloseRequested += OnScreenKeyboard_CloseRequested;
            mainContainer.Controls.Add(onScreenKeyboard);
            UpdateOnScreenKeyboardLayout();

            RegisterTextBoxHandlers(this);
            RegisterNumericInputGuards(this);
            InitializeWizard();
            InitializeLoanCalculator();
            RegisterOutsideClickHandlers(this);
            btnNext.Click += BtnNext_Click;
            btnBack.Click += BtnBack_Click;
            Disposed += RedirectApplication_Disposed;

            try
            {
                KioskLoanApplicationDatabase.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kiosk database initialization failed.\n\n{ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void SetSelectedProduct(string? title, string? sub, string? price, string? imageUrl)
        {
            productName.Text = string.IsNullOrWhiteSpace(title) ? "Selected Motorcycle" : title;
            productYear.Text = ExtractProductYear(sub);

            decimal parsedPrice = ParsePeso(price ?? string.Empty);
            if (parsedPrice > 0)
            {
                productPrice.Text = FormatPeso(parsedPrice);
            }
            else if (!string.IsNullOrWhiteSpace(price))
            {
                productPrice.Text = price;
            }

            currentProductImageUrl = imageUrl ?? string.Empty;
            _ = LoadSelectedProductImageAsync(currentProductImageUrl);
            ApplyLoanCalculations();
        }

        private static string ExtractProductYear(string? sub)
        {
            if (string.IsNullOrWhiteSpace(sub))
            {
                return DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            }

            string[] parts = sub.Split('•', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0] : sub;
        }

        private async Task LoadSelectedProductImageAsync(string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return;
            }

            try
            {
                byte[] bytes = await ProductImageClient.GetByteArrayAsync(imageUrl);
                using var memoryStream = new MemoryStream(bytes);
                using var sourceImage = Image.FromStream(memoryStream);
                var imageCopy = new Bitmap(sourceImage);

                if (IsDisposed)
                {
                    imageCopy.Dispose();
                    return;
                }

                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed)
                    {
                        imageCopy.Dispose();
                        return;
                    }

                    var oldImage = productImagePanel.BackgroundImage;
                    productImagePanel.BackgroundImage = imageCopy;
                    productImagePanel.BackgroundImageLayout = ImageLayout.Zoom;

                    if (currentProductImage != null && !ReferenceEquals(currentProductImage, imageCopy))
                    {
                        currentProductImage.Dispose();
                    }

                    currentProductImage = imageCopy;

                    if (oldImage != null && !ReferenceEquals(oldImage, currentProductImage))
                    {
                        oldImage.Dispose();
                    }
                }));
            }
            catch
            {
                // Keep existing placeholder if image URL cannot be loaded.
            }
        }

        private void InitializeLoanCalculator()
        {
            trkDownPayment.Minimum = 20;
            trkDownPayment.Maximum = 50;
            trkDownPayment.Value = Math.Max(trkDownPayment.Minimum, Math.Min(trkDownPayment.Maximum, trkDownPayment.Value));

            AttachTermCardHandlers(termCard1, 6);
            AttachTermCardHandlers(termCard2, 12);
            AttachTermCardHandlers(termCard3, 24);
            AttachTermCardHandlers(termCard4, 36);

            trkDownPayment.ValueChanged += (s, e) => ApplyLoanCalculations();
            txtFullName.TextChanged += (s, e) => ApplyLoanCalculations();
            txtEmail.TextChanged += (s, e) => ApplyLoanCalculations();
            txtDob.TextChanged += (s, e) => ApplyLoanCalculations();

            HighlightSelectedTermCard();
            ApplyLoanCalculations();
        }

        private void AttachTermCardHandlers(Guna2Panel card, int months)
        {
            void OnTermClick(object? sender, EventArgs e) => SelectLoanTerm(months);

            card.Click += OnTermClick;
            foreach (Control control in card.Controls)
            {
                control.Click += OnTermClick;
            }
        }

        private void SelectLoanTerm(int months)
        {
            selectedTermMonths = months;
            HighlightSelectedTermCard();
            ApplyLoanCalculations();
        }

        private void HighlightSelectedTermCard()
        {
            SetTermCardStyle(termCard1, selectedTermMonths == 6);
            SetTermCardStyle(termCard2, selectedTermMonths == 12);
            SetTermCardStyle(termCard3, selectedTermMonths == 24);
            SetTermCardStyle(termCard4, selectedTermMonths == 36);
        }

        private static void SetTermCardStyle(Guna2Panel card, bool isSelected)
        {
            card.BorderColor = isSelected
                ? Color.FromArgb(51, 65, 85)
                : Color.FromArgb(226, 232, 240);
            card.BorderThickness = isSelected ? 2 : 1;
        }

        private void ApplyLoanCalculations()
        {
            decimal basePrice = ParsePeso(productPrice.Text);
            if (basePrice <= 0)
            {
                basePrice = DefaultBasePrice;
            }

            int downPercent = trkDownPayment.Value;
            decimal downPayment = basePrice * downPercent / 100m;
            decimal financed = basePrice - downPayment;

            decimal monthly6 = ComputeMonthlyPayment(financed, 6, annualRateByTerm[6]);
            decimal monthly12 = ComputeMonthlyPayment(financed, 12, annualRateByTerm[12]);
            decimal monthly24 = ComputeMonthlyPayment(financed, 24, annualRateByTerm[24]);
            decimal monthly36 = ComputeMonthlyPayment(financed, 36, annualRateByTerm[36]);

            term1Amount.Text = FormatPeso(monthly6) + "/mo";
            term2Amount.Text = FormatPeso(monthly12) + "/mo";
            term3Amount.Text = FormatPeso(monthly24) + "/mo";
            term4Amount.Text = FormatPeso(monthly36) + "/mo";

            lblDpPercent.Text = downPercent + "%";
            lblDpAmount.Text = "Down Payment: " + FormatPeso(downPayment);
            lblLoanAmount.Text = "Loan Amount: " + FormatPeso(financed);

            bool hasSelectedTerm = annualRateByTerm.TryGetValue(selectedTermMonths, out decimal annualRate);
            decimal interest = hasSelectedTerm
                ? financed * annualRate * (selectedTermMonths / 12m)
                : 0m;
            decimal totalPayable = financed + interest;
            decimal monthlySelected = hasSelectedTerm && selectedTermMonths > 0
                ? totalPayable / selectedTermMonths
                : 0m;

            summaryBaseValue.Text = FormatPeso(basePrice);
            summaryDpText.Text = $"Down Payment ({downPercent}%):";
            summaryDpValue.Text = "-" + FormatPeso(downPayment);
            summaryFinancedValue.Text = FormatPeso(financed);
            summaryInterestText.Text = hasSelectedTerm
                ? $"Interest ({annualRate * 100m:0}% p.a.):"
                : "Interest (Select loan term):";
            summaryInterestValue.Text = hasSelectedTerm ? "+" + FormatPeso(interest) : "--";
            summaryTotalValue.Text = hasSelectedTerm ? FormatPeso(totalPayable) : "--";
            summaryMonthlyValue.Text = hasSelectedTerm ? FormatPeso(monthlySelected) : "--";

            UpdateIncomeVerification(monthlySelected);
        }

        private void UpdateIncomeVerification(decimal monthlyPayment)
        {
            decimal grossIncome = ParseNumeric(txtFullName.Text);
            decimal otherIncome = ParseNumeric(txtEmail.Text);
            decimal totalIncome = grossIncome + otherIncome;

            if (totalIncome <= 0)
            {
                incomeText.Text = "Your Monthly Income: ₱0.00";
                incomeRatio.Text = "Ratio: --";
                incomeStatus.Text = "Status: NEEDS DATA";
                incomePanel.BackColor = Color.FromArgb(255, 251, 235);
                incomePanel.BorderColor = Color.FromArgb(251, 191, 36);
                incomeTitle.ForeColor = Color.FromArgb(161, 98, 7);
                incomeStatus.ForeColor = Color.FromArgb(161, 98, 7);
                return;
            }

            decimal ratio = monthlyPayment / totalIncome * 100m;
            string status;
            Color panelColor;
            Color borderColor;
            Color statusColor;

            if (ratio <= 35m)
            {
                status = "APPROVED";
                panelColor = Color.FromArgb(240, 253, 244);
                borderColor = Color.FromArgb(74, 222, 128);
                statusColor = Color.FromArgb(21, 128, 61);
            }
            else if (ratio <= 45m)
            {
                status = "FOR REVIEW";
                panelColor = Color.FromArgb(255, 251, 235);
                borderColor = Color.FromArgb(251, 191, 36);
                statusColor = Color.FromArgb(161, 98, 7);
            }
            else
            {
                status = "HIGH RISK";
                panelColor = Color.FromArgb(254, 242, 242);
                borderColor = Color.FromArgb(248, 113, 113);
                statusColor = Color.FromArgb(185, 28, 28);
            }

            incomePanel.BackColor = panelColor;
            incomePanel.BorderColor = borderColor;
            incomeTitle.ForeColor = statusColor;
            incomeStatus.ForeColor = statusColor;

            incomeText.Text = "Your Monthly Income: " + FormatPeso(totalIncome);
            incomeRatio.Text = $"Ratio: {ratio:0.00}%";
            incomeStatus.Text = "Status: " + status;
        }

        private decimal ComputeMonthlyPayment(decimal financed, int months, decimal annualRate)
        {
            decimal interest = financed * annualRate * (months / 12m);
            decimal totalPayable = financed + interest;
            return totalPayable / months;
        }

        private static string FormatPeso(decimal amount)
        {
            return "₱" + amount.ToString("N2", CultureInfo.InvariantCulture);
        }

        private static decimal ParsePeso(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0m;
            }

            var cleaned = new string(value.Where(ch => char.IsDigit(ch) || ch == '.' || ch == '-').ToArray());
            return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsed)
                ? parsed
                : 0m;
        }

        private static decimal ParseNumeric(string value)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsed)
                ? parsed
                : 0m;
        }

        private void RegisterOutsideClickHandlers(Control root)
        {
            foreach (Control control in root.Controls)
            {
                control.MouseDown -= OutsideControl_MouseDown;
                control.MouseDown += OutsideControl_MouseDown;

                if (control.HasChildren)
                {
                    RegisterOutsideClickHandlers(control);
                }
            }
        }

        private void OutsideControl_MouseDown(object? sender, MouseEventArgs e)
        {
            if (!onScreenKeyboard.Visible || sender is not Control control)
            {
                return;
            }

            if (IsWithinOnScreenKeyboard(control) || control is Guna2TextBox)
            {
                return;
            }

            onScreenKeyboard.Visible = false;
            onScreenKeyboard.TargetTextBox = null;
        }

        private bool IsWithinOnScreenKeyboard(Control control)
        {
            Control? current = control;
            while (current != null)
            {
                if (ReferenceEquals(current, onScreenKeyboard))
                {
                    return true;
                }

                current = current.Parent;
            }

            return false;
        }

        private void RegisterNumericInputGuards(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is Guna2TextBox textBox)
                {
                    textBox.KeyPress -= NumericOnly_KeyPress;
                    textBox.KeyPress += NumericOnly_KeyPress;
                }

                if (control.HasChildren)
                {
                    RegisterNumericInputGuards(control);
                }
            }
        }

        private void NumericOnly_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (sender is not Guna2TextBox textBox)
            {
                return;
            }

            if (textBox.Tag is not string tag ||
                !string.Equals(tag, "numeric", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void InitializeWizard()
        {
            InitializePersonalInputControls();

            employmentStatusPanel.BackColor = Color.Transparent;
            employmentStatusPanel.Location = new Point(16, 103);
            employmentStatusPanel.Size = new Size(864, 36);
            employmentStatusPanel.Visible = false;

            ConfigureEmploymentRadio(rbEmployed, "Employed", new Point(0, 6));
            ConfigureEmploymentRadio(rbSelfEmployed, "Self-Employed", new Point(220, 6));
            ConfigureEmploymentRadio(rbBusinessOwner, "Business Owner", new Point(470, 6));
            rbEmployed.Checked = true;

            employmentStatusPanel.Controls.Add(rbEmployed);
            employmentStatusPanel.Controls.Add(rbSelfEmployed);
            employmentStatusPanel.Controls.Add(rbBusinessOwner);
            leftFormPanel.Controls.Add(employmentStatusPanel);
            employmentStatusPanel.BringToFront();

            financialLoansPanel.BackColor = Color.Transparent;
            financialLoansPanel.Location = new Point(16, 267);
            financialLoansPanel.Size = new Size(864, 72);
            financialLoansPanel.Visible = false;

            ConfigureLoanCheckBox(chkHomeLoan, "Home Loan", new Point(0, 4));
            ConfigureLoanCheckBox(chkCarLoan, "Car Loan", new Point(430, 4));
            ConfigureLoanCheckBox(chkPersonalLoan, "Personal Loan", new Point(0, 36));
            ConfigureLoanCheckBox(chkCreditCard, "Credit Card", new Point(430, 36));

            financialLoansPanel.Controls.Add(chkHomeLoan);
            financialLoansPanel.Controls.Add(chkCarLoan);
            financialLoansPanel.Controls.Add(chkPersonalLoan);
            financialLoansPanel.Controls.Add(chkCreditCard);
            leftFormPanel.Controls.Add(financialLoansPanel);
            financialLoansPanel.BringToFront();

            loanChecklistPanel.BackColor = Color.White;
            loanChecklistPanel.BorderColor = Color.FromArgb(226, 232, 240);
            loanChecklistPanel.BorderRadius = 12;
            loanChecklistPanel.BorderThickness = 1;
            loanChecklistPanel.Location = new Point(16, 16);
            loanChecklistPanel.Size = new Size(864, 370);
            loanChecklistPanel.Visible = false;

            loanChecklistTitle.BackColor = Color.Transparent;
            loanChecklistTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            loanChecklistTitle.ForeColor = Color.FromArgb(15, 23, 42);
            loanChecklistTitle.Location = new Point(22, 16);
            loanChecklistTitle.Text = "Document Checklist";

            loanChecklistSubtitle.BackColor = Color.Transparent;
            loanChecklistSubtitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            loanChecklistSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            loanChecklistSubtitle.Location = new Point(22, 60);
            loanChecklistSubtitle.Text = "Select the documents you will provide (minimum 3 required):";

            ConfigureDocumentCheckBox(chkDocValidId, "Valid Government ID (2 copies)", new Point(22, 96));
            ConfigureDocumentCheckBox(chkDocProofAddress, "Proof of Address (utility bill/bank statement)", new Point(22, 146));
            ConfigureDocumentCheckBox(chkDocEmploymentCert, "Certificate of Employment / Business Permit", new Point(22, 196));
            ConfigureDocumentCheckBox(chkDocPayslips, "Latest 3 Consecutive Payslips", new Point(22, 246));
            ConfigureDocumentCheckBox(chkDocProofIncome, "Proof of Income (ITR / Bank Statements)", new Point(22, 296));

            docRequiredProgress.Location = new Point(22, 340);
            docRequiredProgress.Size = new Size(700, 8);
            docRequiredProgress.Maximum = RequiredDocumentCount;
            docRequiredProgress.Value = 0;
            docRequiredProgress.FillColor = Color.FromArgb(226, 232, 240);
            docRequiredProgress.ProgressColor = Color.FromArgb(15, 23, 42);
            docRequiredProgress.ProgressColor2 = Color.FromArgb(15, 23, 42);

            docRequiredCount.BackColor = Color.Transparent;
            docRequiredCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            docRequiredCount.ForeColor = Color.FromArgb(15, 23, 42);
            docRequiredCount.Location = new Point(728, 329);
            docRequiredCount.Text = $"0/{RequiredDocumentCount} required";

            loanChecklistPanel.Controls.Add(loanChecklistTitle);
            loanChecklistPanel.Controls.Add(loanChecklistSubtitle);
            loanChecklistPanel.Controls.Add(chkDocValidId);
            loanChecklistPanel.Controls.Add(chkDocProofAddress);
            loanChecklistPanel.Controls.Add(chkDocEmploymentCert);
            loanChecklistPanel.Controls.Add(chkDocPayslips);
            loanChecklistPanel.Controls.Add(chkDocProofIncome);
            loanChecklistPanel.Controls.Add(docRequiredProgress);
            loanChecklistPanel.Controls.Add(docRequiredCount);

            loanTermsPanel.BackColor = Color.White;
            loanTermsPanel.BorderColor = Color.FromArgb(226, 232, 240);
            loanTermsPanel.BorderRadius = 12;
            loanTermsPanel.BorderThickness = 1;
            loanTermsPanel.Location = new Point(16, 396);
            loanTermsPanel.Size = new Size(864, 78);
            loanTermsPanel.Visible = false;

            chkAgreeTerms.Text = "I agree to the Terms and Conditions *";
            chkAgreeTerms.Location = new Point(22, 12);
            chkAgreeTerms.AutoSize = true;
            chkAgreeTerms.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            chkAgreeTerms.ForeColor = Color.FromArgb(15, 23, 42);
            chkAgreeTerms.CheckedState.FillColor = Color.FromArgb(15, 23, 42);
            chkAgreeTerms.CheckedState.BorderColor = Color.FromArgb(15, 23, 42);
            chkAgreeTerms.CheckedState.BorderThickness = 2;
            chkAgreeTerms.UncheckedState.BorderColor = Color.FromArgb(71, 85, 105);
            chkAgreeTerms.UncheckedState.FillColor = Color.White;
            chkAgreeTerms.UncheckedState.BorderThickness = 2;

            termsSubtitle.BackColor = Color.Transparent;
            termsSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            termsSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            termsSubtitle.Location = new Point(50, 42);
            termsSubtitle.Text = "By checking this box, you agree to our loan terms, privacy policy, and consent to credit verification.";

            loanTermsPanel.Controls.Add(chkAgreeTerms);
            loanTermsPanel.Controls.Add(termsSubtitle);
            chkAgreeTerms.BringToFront();

            leftFormPanel.Controls.Add(loanChecklistPanel);
            leftFormPanel.Controls.Add(loanTermsPanel);
            loanChecklistPanel.BringToFront();
            loanTermsPanel.BringToFront();

            chkDocValidId.CheckedChanged += DocumentCheckChanged;
            chkDocProofAddress.CheckedChanged += DocumentCheckChanged;
            chkDocEmploymentCert.CheckedChanged += DocumentCheckChanged;
            chkDocPayslips.CheckedChanged += DocumentCheckChanged;
            chkDocProofIncome.CheckedChanged += DocumentCheckChanged;

            UpdateDocumentProgress();
            ClearDesignerDefaultInputText();
            InitializeValidationMessageArea();

            SetStep(WizardStep.Personal);
        }

        private void InitializeValidationMessageArea()
        {
            validationMessageLabel.BackColor = Color.Transparent;
            validationMessageLabel.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            validationMessageLabel.ForeColor = Color.FromArgb(220, 38, 38);
            validationMessageLabel.Location = new Point(16, 515);
            validationMessageLabel.Size = new Size(864, 30);
            validationMessageLabel.Visible = false;

            leftFormPanel.Controls.Add(validationMessageLabel);
            validationMessageLabel.BringToFront();
        }

        private void ShowValidationMessage(string message)
        {
            validationMessageLabel.Text = message;
            validationMessageLabel.Visible = true;
            validationMessageLabel.BringToFront();
        }

        private void ClearValidationMessage()
        {
            validationMessageLabel.Text = string.Empty;
            validationMessageLabel.Visible = false;
        }

        private void InitializePersonalInputControls()
        {
            dtpDob.BorderRadius = 8;
            dtpDob.Checked = true;
            dtpDob.FillColor = Color.FromArgb(248, 250, 252);
            dtpDob.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dtpDob.ForeColor = Color.FromArgb(30, 41, 59);
            dtpDob.Format = DateTimePickerFormat.Long;
            dtpDob.Location = new Point(16, 267);
            dtpDob.MaxDate = DateTime.Today;
            dtpDob.MinDate = new DateTime(1950, 1, 1);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(864, 36);
            dtpDob.Value = new DateTime(2000, 1, 1);
            dtpDob.Visible = false;

            cbCity.BackColor = Color.Transparent;
            cbCity.BorderColor = Color.FromArgb(226, 232, 240);
            cbCity.BorderRadius = 8;
            cbCity.DrawMode = DrawMode.OwnerDrawFixed;
            cbCity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCity.FillColor = Color.FromArgb(248, 250, 252);
            cbCity.FocusedColor = Color.FromArgb(94, 148, 255);
            cbCity.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbCity.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            cbCity.ForeColor = Color.FromArgb(15, 23, 42);
            cbCity.ItemHeight = 30;
            cbCity.Location = new Point(16, 431);
            cbCity.Name = "cbCity";
            cbCity.Size = new Size(430, 36);
            cbCity.Visible = false;

            cbCity.Items.Clear();
            cbCity.Items.Add("Select city");
            cbCity.Items.AddRange(new object[]
            {
                "Manila", "Quezon City", "Caloocan", "Taguig", "Makati", "Pasig",
                "Cebu City", "Davao City", "Iloilo City", "Baguio", "Cagayan de Oro",
                "General Santos", "Bacolod", "Zamboanga City", "Antipolo", "Dasmariñas"
            });
            cbCity.StartIndex = 0;

            cbProvince.Items.Clear();
            cbProvince.Items.Add("Select province");
            cbProvince.Items.AddRange(new object[]
            {
                "Metro Manila", "Cavite", "Laguna", "Batangas", "Rizal", "Bulacan",
                "Pampanga", "Cebu", "Davao del Sur", "Iloilo", "Negros Occidental",
                "Misamis Oriental", "South Cotabato", "Benguet", "Zamboanga del Sur"
            });
            cbProvince.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            cbProvince.ForeColor = Color.FromArgb(15, 23, 42);
            cbProvince.StartIndex = 0;

            leftFormPanel.Controls.Add(dtpDob);
            leftFormPanel.Controls.Add(cbCity);
            dtpDob.BringToFront();
            cbCity.BringToFront();
        }

        private void ClearDesignerDefaultInputText()
        {
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtDob.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
        }

        private static void ConfigureEmploymentRadio(Guna2RadioButton radio, string text, Point location)
        {
            radio.Text = text;
            radio.Location = location;
            radio.AutoSize = true;
            radio.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            radio.ForeColor = Color.FromArgb(15, 23, 42);
            radio.CheckedState.FillColor = Color.FromArgb(15, 23, 42);
            radio.UncheckedState.BorderColor = Color.FromArgb(100, 116, 139);
            radio.UncheckedState.FillColor = Color.White;
        }

        private static void ConfigureLoanCheckBox(Guna2CheckBox checkBox, string text, Point location)
        {
            checkBox.Text = text;
            checkBox.Location = location;
            checkBox.AutoSize = true;
            checkBox.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            checkBox.ForeColor = Color.FromArgb(15, 23, 42);
            checkBox.CheckedState.FillColor = Color.FromArgb(15, 23, 42);
            checkBox.CheckedState.BorderColor = Color.FromArgb(15, 23, 42);
            checkBox.UncheckedState.BorderColor = Color.FromArgb(71, 85, 105);
            checkBox.UncheckedState.FillColor = Color.White;
            checkBox.UncheckedState.BorderThickness = 1;
        }

        private static void ConfigureDocumentCheckBox(Guna2CheckBox checkBox, string text, Point location)
        {
            checkBox.Text = text;
            checkBox.Location = location;
            checkBox.Size = new Size(820, 40);
            checkBox.AutoSize = false;
            checkBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            checkBox.ForeColor = Color.FromArgb(15, 23, 42);
            checkBox.CheckedState.FillColor = Color.FromArgb(15, 23, 42);
            checkBox.CheckedState.BorderColor = Color.FromArgb(15, 23, 42);
            checkBox.UncheckedState.BorderColor = Color.FromArgb(203, 213, 225);
            checkBox.UncheckedState.BorderThickness = 1;
            checkBox.UncheckedState.FillColor = Color.White;
        }

        private void DocumentCheckChanged(object? sender, EventArgs e)
        {
            UpdateDocumentProgress();
        }

        private void UpdateDocumentProgress()
        {
            int selected = CountSelectedDocuments();
            int progressValue = Math.Min(selected, RequiredDocumentCount);

            docRequiredProgress.Value = progressValue;
            docRequiredCount.Text = $"{selected}/{RequiredDocumentCount} required";
        }

        private int CountSelectedDocuments()
        {
            int count = 0;
            if (chkDocValidId.Checked) count++;
            if (chkDocProofAddress.Checked) count++;
            if (chkDocEmploymentCert.Checked) count++;
            if (chkDocPayslips.Checked) count++;
            if (chkDocProofIncome.Checked) count++;
            return count;
        }

        private void BtnNext_Click(object? sender, EventArgs e)
        {
            if (!ValidateCurrentStep(out string message))
            {
                ShowValidationMessage(message);
                return;
            }

            ClearValidationMessage();
            SaveCurrentStepInput();

            if (currentStep == WizardStep.Loan)
            {
                string queueNumber = GenerateQueueNumber();

                if (!SaveLoanApplication(queueNumber, out string saveError))
                {
                    ShowValidationMessage(saveError);
                    return;
                }

                ShowLoanReceipt(queueNumber);
                return;
            }

            SetStep((WizardStep)((int)currentStep + 1));
        }

        private void ShowLoanReceipt(string queueNumber)
        {
            onScreenKeyboard.Visible = false;

            var host = Parent;
            if (host == null)
            {
                return;
            }

            var receipt = new user_receipt_loan(queueNumber)
            {
                Dock = DockStyle.Fill
            };

            host.Controls.Add(receipt);
            receipt.BringToFront();

            host.Controls.Remove(this);
            Dispose();
        }

        private void RegisterTextBoxHandlers(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is Guna2TextBox textBox)
                {
                    textBox.Enter += TextBox_Enter;
                    textBox.Click += TextBox_Enter;
                }

                if (control.HasChildren)
                {
                    RegisterTextBoxHandlers(control);
                }
            }
        }

        private void TextBox_Enter(object? sender, EventArgs e)
        {
            if (sender is not Guna2TextBox textBox)
            {
                return;
            }

            onScreenKeyboard.TargetTextBox = textBox;
            onScreenKeyboard.Visible = true;
            onScreenKeyboard.BringToFront();
        }

        private void OnScreenKeyboard_CloseRequested(object? sender, EventArgs e)
        {
            onScreenKeyboard.Visible = false;
            onScreenKeyboard.TargetTextBox = null;
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            onScreenKeyboard.Visible = false;

            if (currentStep != WizardStep.Personal)
            {
                SaveCurrentStepInput();
                SetStep((WizardStep)((int)currentStep - 1));
                return;
            }

            var host = Parent;
            if (host == null)
            {
                return;
            }

            host.Controls.Remove(this);
            Dispose();
        }

        private bool ValidateCurrentStep(out string message)
        {
            message = string.Empty;

            if (currentStep == WizardStep.Employment &&
                !rbEmployed.Checked && !rbSelfEmployed.Checked && !rbBusinessOwner.Checked)
            {
                message = "Please select employment status.";
                return false;
            }

            if (currentStep == WizardStep.Personal)
            {
                if (cbCity.SelectedIndex <= 0)
                {
                    message = "Please select your city.";
                    return false;
                }

                if (cbProvince.SelectedIndex <= 0)
                {
                    message = "Please select your province.";
                    return false;
                }
            }

            if (currentStep == WizardStep.Financial)
            {
                if (!decimal.TryParse(txtFullName.Text, out decimal grossIncome) || grossIncome < 15000m)
                {
                    message = "Monthly Gross Income must be at least ₱15,000.";
                    return false;
                }
            }

            if (currentStep == WizardStep.Loan)
            {
                if (CountSelectedDocuments() < RequiredDocumentCount)
                {
                    message = $"Please select at least {RequiredDocumentCount} documents in the checklist.";
                    return false;
                }

                if (!chkAgreeTerms.Checked)
                {
                    message = "Please agree to the Terms and Conditions before submitting.";
                    return false;
                }
            }

            foreach (Control control in leftFormPanel.Controls)
            {
                if (!control.Visible)
                {
                    continue;
                }

                if (control is Guna2TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    message = "Please complete all required fields before proceeding.";
                    return false;
                }
            }

            return true;
        }

        private void SaveCurrentStepInput()
        {
            switch (currentStep)
            {
                case WizardStep.Personal:
                    personalFullName = txtFullName.Text.Trim();
                    personalEmail = txtEmail.Text.Trim();
                    personalMobile = txtMobile.Text.Trim();
                    personalAddress = txtAddress.Text.Trim();
                    personalDob = dtpDob.Value.Date;
                    personalCityIndex = cbCity.SelectedIndex;
                    personalProvinceIndex = cbProvince.SelectedIndex;
                    break;

                case WizardStep.Employment:
                    employmentCompanyName = txtEmail.Text.Trim();
                    employmentPositionTitle = txtMobile.Text.Trim();
                    employmentYears = txtDob.Text.Trim();
                    break;

                case WizardStep.Financial:
                    financialGrossIncome = txtFullName.Text.Trim();
                    financialOtherIncome = txtEmail.Text.Trim();
                    financialTotalObligations = txtDob.Text.Trim();
                    break;
            }
        }

        private void SetStep(WizardStep step)
        {
            currentStep = step;
            ClearValidationMessage();

            HideFormFields();

            switch (currentStep)
            {
                case WizardStep.Personal:
                    ApplyPersonalStep();
                    break;
                case WizardStep.Employment:
                    ApplyEmploymentStep();
                    break;
                case WizardStep.Financial:
                    ApplyFinancialStep();
                    break;
                case WizardStep.Loan:
                    ApplyLoanStep();
                    break;
            }

            UpdateStepHeader();
            UpdateNavigationButtons();
            ApplyCurrentStepResponsiveLayout();
        }

        private void HideFormFields()
        {
            var controlsToHide = new List<Control>
            {
                lblFullName, txtFullName,
                lblEmail, txtEmail,
                lblMobile, txtMobile,
                lblDob, txtDob,
                lblAddress, txtAddress,
                lblCity, txtCity,
                lblProvince, cbProvince,
                dtpDob,
                cbCity,
                employmentStatusPanel,
                financialLoansPanel,
                loanChecklistPanel,
                loanTermsPanel
            };

            foreach (var control in controlsToHide)
            {
                control.Visible = false;
            }
        }

        private void ApplyPersonalStep()
        {
            lblPersonalInfo.Visible = true;
            lblPersonalInfo.Text = "Personal Information";

            SetField(lblFullName, txtFullName, "Full Name *", 77, "Juan Dela Cruz", true);
            txtFullName.Tag = "text";
            SetField(lblEmail, txtEmail, "Email *", 159, "juan@email.com", true);
            txtEmail.Tag = "text";
            lblEmail.Location = new Point(16, 159);
            txtEmail.Location = new Point(16, 185);
            txtEmail.Size = new Size(430, 36);

            SetField(lblMobile, txtMobile, "Mobile Number *", 159, "09171234567", false);
            txtMobile.Tag = "numeric";

            lblDob.Visible = true;
            lblDob.Text = "Date of Birth *";
            lblDob.Location = new Point(16, 241);
            dtpDob.Visible = true;
            dtpDob.Location = new Point(16, 267);
            dtpDob.Size = new Size(864, 36);

            SetField(lblAddress, txtAddress, "Address *", 323, "123 Main Street, Barangay", true);
            txtAddress.Tag = "text";

            lblCity.Visible = true;
            lblCity.Text = "City *";
            lblCity.Location = new Point(16, 405);
            cbCity.Visible = true;
            cbCity.Location = new Point(16, 431);
            cbCity.Size = new Size(430, 36);

            lblProvince.Visible = true;
            cbProvince.Visible = true;
            lblProvince.Text = "Province *";
            lblProvince.Location = new Point(466, 405);
            cbProvince.Location = new Point(466, 431);
            cbProvince.Size = new Size(414, 36);

            txtFullName.Text = personalFullName;
            txtEmail.Text = personalEmail;
            txtMobile.Text = personalMobile;
            txtAddress.Text = personalAddress;
            dtpDob.Value = personalDob;
            cbCity.SelectedIndex = Math.Max(0, Math.Min(personalCityIndex, cbCity.Items.Count - 1));
            cbProvince.SelectedIndex = Math.Max(0, Math.Min(personalProvinceIndex, cbProvince.Items.Count - 1));
        }

        private void ApplyEmploymentStep()
        {
            lblPersonalInfo.Visible = true;
            lblPersonalInfo.Text = "Employment Details";

            lblFullName.Visible = true;
            lblFullName.Text = "Employment Status *";
            lblFullName.Location = new Point(16, 77);
            employmentStatusPanel.Visible = true;

            SetField(lblEmail, txtEmail, "Company / Business Name *", 159, "ABC Corporation", true);
            txtEmail.Tag = "text";
            txtEmail.Location = new Point(16, 185);
            txtEmail.Size = new Size(864, 36);

            SetField(lblMobile, txtMobile, "Position / Title", 241, "Senior Manager", true);
            txtMobile.Tag = "text";
            txtMobile.Location = new Point(16, 267);
            txtMobile.Size = new Size(864, 36);

            SetField(lblDob, txtDob, "Years Employed *", 323, "1", true);
            txtDob.Tag = "numeric";
            txtDob.Location = new Point(16, 349);
            txtDob.Size = new Size(864, 36);

            txtEmail.Text = employmentCompanyName;
            txtMobile.Text = employmentPositionTitle;
            txtDob.Text = employmentYears;
        }

        private void ApplyFinancialStep()
        {
            lblPersonalInfo.Visible = true;
            lblPersonalInfo.Text = "Financial Information";

            SetField(lblFullName, txtFullName, "Monthly Gross Income * (min ₱15,000)", 77, "25000", true);
            txtFullName.Tag = "numeric";
            SetField(lblEmail, txtEmail, "Other Monthly Income", 159, "0", true);
            txtEmail.Tag = "numeric";

            lblMobile.Visible = true;
            lblMobile.Text = "Existing Loans (check all that apply)";
            lblMobile.Location = new Point(16, 241);

            financialLoansPanel.Visible = true;

            SetField(lblDob, txtDob, "Total Monthly Obligations", 349, "0", true);
            txtDob.Tag = "numeric";

            txtFullName.Text = financialGrossIncome;
            txtEmail.Text = financialOtherIncome;
            txtDob.Text = financialTotalObligations;

            ApplyLoanCalculations();
        }

        private void ApplyLoanStep()
        {
            lblPersonalInfo.Visible = false;
            loanChecklistPanel.Visible = true;
            loanTermsPanel.Visible = true;
            UpdateDocumentProgress();
            ApplyLoanCalculations();
        }

        private void SetField(
            Guna2HtmlLabel label,
            Guna2TextBox textBox,
            string labelText,
            int top,
            string placeholder,
            bool fullWidth)
        {
            label.Visible = true;
            textBox.Visible = true;

            label.Text = labelText;
            label.Location = new Point(fullWidth ? 16 : 466, top);

            textBox.Location = new Point(fullWidth ? 16 : 466, top + 26);
            textBox.Size = fullWidth ? new Size(864, 36) : new Size(414, 36);
            textBox.PlaceholderText = placeholder;
        }

        private void UpdateStepHeader()
        {
            int index = (int)currentStep;
            stepCircle.Text = (index + 1).ToString();
            stepCircle.Location = currentStep switch
            {
                WizardStep.Personal => new Point(0, 3),
                WizardStep.Employment => new Point(123, 3),
                WizardStep.Financial => new Point(258, 3),
                _ => new Point(378, 3)
            };

            Color active = Color.FromArgb(15, 23, 42);
            Color inactive = Color.FromArgb(100, 116, 139);

            stepPersonal.ForeColor = currentStep == WizardStep.Personal ? active : inactive;
            stepEmployment.ForeColor = currentStep == WizardStep.Employment ? active : inactive;
            stepFinancial.ForeColor = currentStep == WizardStep.Financial ? active : inactive;
            stepLoan.ForeColor = currentStep == WizardStep.Loan ? active : inactive;
        }

        private void UpdateNavigationButtons()
        {
            btnBack.Text = currentStep == WizardStep.Personal ? "Back to Inventory" : "Previous";
            btnNext.Text = currentStep == WizardStep.Loan ? "Submit" : "Next";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyResolutionScaling();
            ApplyCurrentStepResponsiveLayout();
            UpdateOnScreenKeyboardLayout();
            EnsureProductImageVisible();
        }

        private void ApplyResolutionScaling()
        {
            responsiveScaler ??= new ResponsiveScaler(this, new Size((int)BaseWidth, (int)BaseHeight));
            responsiveScaler.Apply(this.ClientSize);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (!IsResponsiveLayoutReady())
            {
                return;
            }

            ApplyResolutionScaling();
            ApplyCurrentStepResponsiveLayout();
            UpdateOnScreenKeyboardLayout();
            EnsureProductImageVisible();
        }

        private void EnsureProductImageVisible()
        {
            if (productImagePanel == null)
            {
                return;
            }

            productImagePanel.BackgroundImageLayout = ImageLayout.Zoom;

            if (productImagePanel.BackgroundImage == null)
            {
                if (currentProductImage != null)
                {
                    productImagePanel.BackgroundImage = currentProductImage;
                }
                else if (!string.IsNullOrWhiteSpace(currentProductImageUrl))
                {
                    _ = LoadSelectedProductImageAsync(currentProductImageUrl);
                }
            }
        }

        private void RedirectApplication_Disposed(object? sender, EventArgs e)
        {
            currentProductImage?.Dispose();
            currentProductImage = null;
        }

        private bool IsResponsiveLayoutReady()
        {
            return !IsDisposed
                && mainContainer != null
                && leftFormPanel != null
                && validationMessageLabel != null
                && onScreenKeyboard != null;
        }

        private void ApplyCurrentStepResponsiveLayout()
        {
            if (!IsResponsiveLayoutReady() || leftFormPanel.ClientSize.Width <= 0)
            {
                return;
            }

            int panelWidth = Math.Max(320, leftFormPanel.ClientSize.Width);
            int margin = 16;
            int gap = 20;
            int fullWidth = Math.Max(220, panelWidth - (margin * 2));
            int halfWidth = Math.Max(120, (fullWidth - gap) / 2);
            int secondColumnLeft = margin + halfWidth + gap;

            switch (currentStep)
            {
                case WizardStep.Personal:
                    txtEmail.Location = new Point(margin, txtEmail.Top);
                    txtEmail.Width = halfWidth;
                    lblEmail.Location = new Point(margin, lblEmail.Top);

                    txtMobile.Location = new Point(secondColumnLeft, txtMobile.Top);
                    txtMobile.Width = halfWidth;
                    lblMobile.Location = new Point(secondColumnLeft, lblMobile.Top);

                    cbCity.Location = new Point(margin, cbCity.Top);
                    cbCity.Width = halfWidth;
                    lblCity.Location = new Point(margin, lblCity.Top);

                    cbProvince.Location = new Point(secondColumnLeft, cbProvince.Top);
                    cbProvince.Width = halfWidth;
                    lblProvince.Location = new Point(secondColumnLeft, lblProvince.Top);

                    txtFullName.Width = fullWidth;
                    txtAddress.Width = fullWidth;
                    dtpDob.Width = fullWidth;
                    break;

                case WizardStep.Employment:
                    txtEmail.Location = new Point(margin, txtEmail.Top);
                    txtEmail.Width = fullWidth;
                    txtMobile.Location = new Point(margin, txtMobile.Top);
                    txtMobile.Width = fullWidth;
                    txtDob.Location = new Point(margin, txtDob.Top);
                    txtDob.Width = fullWidth;
                    employmentStatusPanel.Location = new Point(margin, employmentStatusPanel.Top);
                    employmentStatusPanel.Width = fullWidth;
                    break;

                case WizardStep.Financial:
                    txtFullName.Location = new Point(margin, txtFullName.Top);
                    txtFullName.Width = fullWidth;
                    txtEmail.Location = new Point(margin, txtEmail.Top);
                    txtEmail.Width = fullWidth;
                    txtDob.Location = new Point(margin, txtDob.Top);
                    txtDob.Width = fullWidth;
                    financialLoansPanel.Location = new Point(margin, financialLoansPanel.Top);
                    financialLoansPanel.Width = fullWidth;
                    break;

                case WizardStep.Loan:
                    loanChecklistPanel.Location = new Point(margin, loanChecklistPanel.Top);
                    loanChecklistPanel.Width = fullWidth;
                    loanTermsPanel.Location = new Point(margin, loanTermsPanel.Top);
                    loanTermsPanel.Width = fullWidth;

                    int checklistInnerWidth = Math.Max(220, loanChecklistPanel.ClientSize.Width - 44);
                    chkDocValidId.Width = checklistInnerWidth;
                    chkDocProofAddress.Width = checklistInnerWidth;
                    chkDocEmploymentCert.Width = checklistInnerWidth;
                    chkDocPayslips.Width = checklistInnerWidth;
                    chkDocProofIncome.Width = checklistInnerWidth;

                    docRequiredProgress.Width = Math.Max(180, loanChecklistPanel.ClientSize.Width - 190);
                    docRequiredCount.Location = new Point(Math.Max(22, loanChecklistPanel.ClientSize.Width - 145), docRequiredCount.Top);

                    termsSubtitle.Location = new Point(50, termsSubtitle.Top);
                    termsSubtitle.MaximumSize = new Size(Math.Max(200, loanTermsPanel.ClientSize.Width - 72), 0);
                    break;
            }

            validationMessageLabel.Width = fullWidth;
        }

        private void UpdateOnScreenKeyboardLayout()
        {
            if (!IsResponsiveLayoutReady())
            {
                return;
            }

            int width = Math.Max(320, mainContainer.ClientSize.Width - 40);
            int height = Math.Max(220, Math.Min(420, (int)Math.Round(mainContainer.ClientSize.Height * 0.32f)));
            int left = Math.Max(10, (mainContainer.ClientSize.Width - width) / 2);
            int top = Math.Max(10, mainContainer.ClientSize.Height - height - 16);

            onScreenKeyboard.Bounds = new Rectangle(left, top, width, height);
            if (onScreenKeyboard.Visible)
            {
                onScreenKeyboard.BringToFront();
            }
        }

        private static string GenerateQueueNumber()
        {
            Span<char> code = stackalloc char[4];
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = QueueCharacters[Random.Shared.Next(QueueCharacters.Length)];
            }

            return new string(code);
        }

        private bool SaveLoanApplication(string queueNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                decimal basePrice = ParsePeso(productPrice.Text);
                if (basePrice <= 0)
                {
                    basePrice = DefaultBasePrice;
                }

                int downPercent = trkDownPayment.Value;
                decimal downPayment = basePrice * downPercent / 100m;
                decimal financed = basePrice - downPayment;

                bool hasSelectedTerm = annualRateByTerm.TryGetValue(selectedTermMonths, out decimal annualRate);
                decimal interest = hasSelectedTerm ? financed * annualRate * (selectedTermMonths / 12m) : 0m;
                decimal totalPayable = financed + interest;
                decimal monthlySelected = hasSelectedTerm && selectedTermMonths > 0 ? totalPayable / selectedTermMonths : 0m;

                int? years = int.TryParse(employmentYears, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsedYears)
                    ? parsedYears
                    : null;

                string city = personalCityIndex > 0 && personalCityIndex < cbCity.Items.Count
                    ? Convert.ToString(cbCity.Items[personalCityIndex]) ?? string.Empty
                    : string.Empty;

                string province = personalProvinceIndex > 0 && personalProvinceIndex < cbProvince.Items.Count
                    ? Convert.ToString(cbProvince.Items[personalProvinceIndex]) ?? string.Empty
                    : string.Empty;

                var record = new KioskLoanApplicationRecord
                {
                    QueueNumber = queueNumber,

                    FullName = personalFullName,
                    Email = personalEmail,
                    Mobile = personalMobile,
                    DateOfBirth = personalDob,
                    Address = personalAddress,
                    City = city,
                    Province = province,

                    EmploymentStatus = rbEmployed.Checked ? "Employed"
                        : rbSelfEmployed.Checked ? "Self-Employed"
                        : rbBusinessOwner.Checked ? "Business Owner"
                        : "Unspecified",
                    CompanyOrBusinessName = employmentCompanyName,
                    PositionTitle = employmentPositionTitle,
                    YearsEmployed = years,

                    GrossIncome = ParseNumeric(financialGrossIncome),
                    OtherIncome = ParseNumeric(financialOtherIncome),
                    TotalObligations = ParseNumeric(financialTotalObligations),
                    HasHomeLoan = chkHomeLoan.Checked,
                    HasCarLoan = chkCarLoan.Checked,
                    HasPersonalLoan = chkPersonalLoan.Checked,
                    HasCreditCard = chkCreditCard.Checked,

                    ProductName = productName.Text.Trim(),
                    ProductYear = productYear.Text.Trim(),
                    ProductPrice = basePrice,
                    DownPaymentPercent = downPercent,
                    DownPaymentAmount = downPayment,
                    FinancedAmount = financed,
                    SelectedTermMonths = selectedTermMonths,
                    AnnualInterestRate = hasSelectedTerm ? annualRate : 0m,
                    InterestAmount = interest,
                    TotalPayable = totalPayable,
                    MonthlyAmortization = monthlySelected,

                    DocValidGovernmentId = chkDocValidId.Checked,
                    DocProofOfAddress = chkDocProofAddress.Checked,
                    DocEmploymentOrBusinessProof = chkDocEmploymentCert.Checked,
                    DocPayslips = chkDocPayslips.Checked,
                    DocProofOfIncome = chkDocProofIncome.Checked,
                    SelectedDocumentCount = CountSelectedDocuments(),
                    AgreedToTerms = chkAgreeTerms.Checked
                };

                if (!KioskLoanApplicationDatabase.SaveApplication(record))
                {
                    errorMessage = "Unable to save application. Please try again.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Unable to save application: {ex.Message}";
                return false;
            }
        }
    }
}
