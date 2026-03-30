using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using inventory_ni_Percie;

namespace Pl_Project_Combined.Databases
{
    internal sealed class KioskLoanApplicationRecord
    {
        public string QueueNumber { get; set; } = string.Empty;

        // Personal
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;

        // Employment
        public string EmploymentStatus { get; set; } = "Unspecified";
        public string CompanyOrBusinessName { get; set; } = string.Empty;
        public string PositionTitle { get; set; } = string.Empty;
        public int? YearsEmployed { get; set; }

        // Financial
        public decimal GrossIncome { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal TotalObligations { get; set; }
        public bool HasHomeLoan { get; set; }
        public bool HasCarLoan { get; set; }
        public bool HasPersonalLoan { get; set; }
        public bool HasCreditCard { get; set; }

        // Loan
        public string ProductName { get; set; } = string.Empty;
        public string ProductYear { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int DownPaymentPercent { get; set; }
        public decimal DownPaymentAmount { get; set; }
        public decimal FinancedAmount { get; set; }
        public int SelectedTermMonths { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal MonthlyAmortization { get; set; }

        // Docs / Terms
        public bool DocValidGovernmentId { get; set; }
        public bool DocProofOfAddress { get; set; }
        public bool DocEmploymentOrBusinessProof { get; set; }
        public bool DocPayslips { get; set; }
        public bool DocProofOfIncome { get; set; }
        public int SelectedDocumentCount { get; set; }
        public bool AgreedToTerms { get; set; }
    }

    internal sealed class KioskLoanAssessorItem
    {
        public long Id { get; set; }
        public string QueueNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string EmploymentStatus { get; set; } = string.Empty;
        public string CompanyOrBusinessName { get; set; } = string.Empty;
        public string PositionTitle { get; set; } = string.Empty;
        public int? YearsEmployed { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal TotalObligations { get; set; }
        public bool HasHomeLoan { get; set; }
        public bool HasCarLoan { get; set; }
        public bool HasPersonalLoan { get; set; }
        public bool HasCreditCard { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductYear { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int DownPaymentPercent { get; set; }
        public decimal DownPaymentAmount { get; set; }
        public decimal FinancedAmount { get; set; }
        public int SelectedTermMonths { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal MonthlyAmortization { get; set; }
        public bool DocValidGovernmentId { get; set; }
        public bool DocProofOfAddress { get; set; }
        public bool DocEmploymentOrBusiness { get; set; }
        public bool DocPayslips { get; set; }
        public bool DocProofOfIncome { get; set; }
        public int SelectedDocumentCount { get; set; }
        public bool AgreedToTerms { get; set; }
        public string ApplicationStatus { get; set; } = "Pending";
        public string ApprovedPaymentMode { get; set; } = string.Empty;
        public decimal? ApprovedDownPayment { get; set; }
        public decimal? ApprovedAdvancePayment { get; set; }
        public decimal? ApprovedMonthlyDue { get; set; }
        public int? ApprovedTermMonths { get; set; }
        public string AssessorNotes { get; set; } = string.Empty;
        public string AssessorBy { get; set; } = string.Empty;
        public DateTime? AssessorDecidedAt { get; set; }
        public string CashierStatus { get; set; } = "Waiting";
        public DateTime SubmittedAt { get; set; }
    }

    internal sealed class KioskAssessorDecision
    {
        public long ApplicationId { get; set; }
        public string ApprovedPaymentMode { get; set; } = string.Empty;
        public decimal? ApprovedDownPayment { get; set; }
        public decimal? ApprovedAdvancePayment { get; set; }
        public decimal? ApprovedMonthlyDue { get; set; }
        public int? ApprovedTermMonths { get; set; }
        public string AssessorNotes { get; set; } = string.Empty;
        public string AssessorBy { get; set; } = string.Empty;
    }

    internal static class KioskLoanApplicationDatabase
    {
        private static readonly object SyncRoot = new();
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized) return;

            lock (SyncRoot)
            {
                if (initialized) return;
                CreateTableIfMissing();
                EnsureAssessorFlowColumns();
                initialized = true;
            }
        }

        public static List<KioskLoanAssessorItem> GetApplications(string keyword = "")
        {
            Initialize();

            var items = new List<KioskLoanAssessorItem>();

            try
            {
                using var conn = DatabaseManager.GetConnection();

                const string sql = @"
SELECT
    id, queue_number,
    full_name, email, mobile, date_of_birth, address, city, province,
    employment_status, company_or_business_name, position_title, years_employed,
    gross_income, other_income, total_obligations, has_home_loan, has_car_loan, has_personal_loan, has_credit_card,
    product_name, product_year, product_price, down_payment_percent, down_payment_amount, financed_amount,
    selected_term_months, annual_interest_rate, interest_amount, total_payable, monthly_amortization,
    doc_valid_government_id, doc_proof_of_address, doc_employment_or_business, doc_payslips, doc_proof_of_income,
    selected_document_count, agreed_to_terms, application_status,
    approved_payment_mode, approved_down_payment, approved_advance_payment, approved_monthly_due, approved_term_months,
    assessor_notes, assessor_by, assessor_decided_at, cashier_status,
    submitted_at
FROM kiosk_loan_applications
WHERE (@keyword = '' OR full_name LIKE CONCAT('%', @keyword, '%') OR queue_number LIKE CONCAT('%', @keyword, '%'))
ORDER BY submitted_at DESC, id DESC;";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keyword", keyword.Trim());

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new KioskLoanAssessorItem
                    {
                        Id = reader.GetInt64("id"),
                        QueueNumber = reader.GetString("queue_number"),
                        FullName = reader.GetString("full_name"),
                        Email = reader.GetString("email"),
                        Mobile = reader.GetString("mobile"),
                        DateOfBirth = reader.GetDateTime("date_of_birth"),
                        Address = reader.GetString("address"),
                        City = reader.GetString("city"),
                        Province = reader.GetString("province"),
                        EmploymentStatus = reader.GetString("employment_status"),
                        CompanyOrBusinessName = reader.GetString("company_or_business_name"),
                        PositionTitle = reader.GetString("position_title"),
                        YearsEmployed = reader.IsDBNull(reader.GetOrdinal("years_employed")) ? null : reader.GetInt32("years_employed"),
                        GrossIncome = reader.GetDecimal("gross_income"),
                        OtherIncome = reader.GetDecimal("other_income"),
                        TotalObligations = reader.GetDecimal("total_obligations"),
                        HasHomeLoan = reader.GetBoolean("has_home_loan"),
                        HasCarLoan = reader.GetBoolean("has_car_loan"),
                        HasPersonalLoan = reader.GetBoolean("has_personal_loan"),
                        HasCreditCard = reader.GetBoolean("has_credit_card"),
                        ProductName = reader.GetString("product_name"),
                        ProductYear = reader.GetString("product_year"),
                        ProductPrice = reader.GetDecimal("product_price"),
                        DownPaymentPercent = reader.GetInt32("down_payment_percent"),
                        DownPaymentAmount = reader.GetDecimal("down_payment_amount"),
                        FinancedAmount = reader.GetDecimal("financed_amount"),
                        SelectedTermMonths = reader.GetInt32("selected_term_months"),
                        AnnualInterestRate = reader.GetDecimal("annual_interest_rate"),
                        InterestAmount = reader.GetDecimal("interest_amount"),
                        TotalPayable = reader.GetDecimal("total_payable"),
                        MonthlyAmortization = reader.GetDecimal("monthly_amortization"),
                        DocValidGovernmentId = reader.GetBoolean("doc_valid_government_id"),
                        DocProofOfAddress = reader.GetBoolean("doc_proof_of_address"),
                        DocEmploymentOrBusiness = reader.GetBoolean("doc_employment_or_business"),
                        DocPayslips = reader.GetBoolean("doc_payslips"),
                        DocProofOfIncome = reader.GetBoolean("doc_proof_of_income"),
                        SelectedDocumentCount = reader.GetInt32("selected_document_count"),
                        AgreedToTerms = reader.GetBoolean("agreed_to_terms"),
                        ApplicationStatus = reader.GetString("application_status"),
                        ApprovedPaymentMode = reader.IsDBNull(reader.GetOrdinal("approved_payment_mode")) ? string.Empty : reader.GetString("approved_payment_mode"),
                        ApprovedDownPayment = reader.IsDBNull(reader.GetOrdinal("approved_down_payment")) ? null : reader.GetDecimal("approved_down_payment"),
                        ApprovedAdvancePayment = reader.IsDBNull(reader.GetOrdinal("approved_advance_payment")) ? null : reader.GetDecimal("approved_advance_payment"),
                        ApprovedMonthlyDue = reader.IsDBNull(reader.GetOrdinal("approved_monthly_due")) ? null : reader.GetDecimal("approved_monthly_due"),
                        ApprovedTermMonths = reader.IsDBNull(reader.GetOrdinal("approved_term_months")) ? null : reader.GetInt32("approved_term_months"),
                        AssessorNotes = reader.IsDBNull(reader.GetOrdinal("assessor_notes")) ? string.Empty : reader.GetString("assessor_notes"),
                        AssessorBy = reader.IsDBNull(reader.GetOrdinal("assessor_by")) ? string.Empty : reader.GetString("assessor_by"),
                        AssessorDecidedAt = reader.IsDBNull(reader.GetOrdinal("assessor_decided_at")) ? null : reader.GetDateTime("assessor_decided_at"),
                        CashierStatus = reader.IsDBNull(reader.GetOrdinal("cashier_status")) ? "Waiting" : reader.GetString("cashier_status"),
                        SubmittedAt = reader.GetDateTime("submitted_at")
                    };

                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[KioskLoanApplicationDatabase] GetApplications error: {ex.Message}");
            }

            return items;
        }

        public static bool SaveAssessorDecision(KioskAssessorDecision decision)
        {
            Initialize();

            try
            {
                using var conn = DatabaseManager.GetConnection();

                const string sql = @"
UPDATE kiosk_loan_applications
SET
    approved_payment_mode = @mode,
    approved_down_payment = @approved_down_payment,
    approved_advance_payment = @approved_advance_payment,
    approved_monthly_due = @approved_monthly_due,
    approved_term_months = @approved_term_months,
    assessor_notes = @assessor_notes,
    assessor_by = @assessor_by,
    assessor_decided_at = NOW(),
    application_status = 'Approved',
    cashier_status = 'Waiting'
WHERE id = @id;";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", decision.ApplicationId);
                cmd.Parameters.AddWithValue("@mode", NormalizePaymentMode(decision.ApprovedPaymentMode));
                cmd.Parameters.AddWithValue("@approved_down_payment", decision.ApprovedDownPayment.HasValue ? decision.ApprovedDownPayment.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@approved_advance_payment", decision.ApprovedAdvancePayment.HasValue ? decision.ApprovedAdvancePayment.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@approved_monthly_due", decision.ApprovedMonthlyDue.HasValue ? decision.ApprovedMonthlyDue.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@approved_term_months", decision.ApprovedTermMonths.HasValue ? decision.ApprovedTermMonths.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@assessor_notes", decision.AssessorNotes ?? string.Empty);
                cmd.Parameters.AddWithValue("@assessor_by", decision.AssessorBy ?? string.Empty);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[KioskLoanApplicationDatabase] SaveAssessorDecision error: {ex.Message}");
                return false;
            }
        }

        public static List<KioskLoanAssessorItem> GetCashierReadyByMode(string paymentMode)
        {
            Initialize();

            var items = new List<KioskLoanAssessorItem>();

            try
            {
                using var conn = DatabaseManager.GetConnection();

                const string sql = @"
SELECT
    id, queue_number,
    full_name, email, mobile, date_of_birth, address, city, province,
    employment_status, company_or_business_name, position_title, years_employed,
    gross_income, other_income, total_obligations, has_home_loan, has_car_loan, has_personal_loan, has_credit_card,
    product_name, product_year, product_price, down_payment_percent, down_payment_amount, financed_amount,
    selected_term_months, annual_interest_rate, interest_amount, total_payable, monthly_amortization,
    doc_valid_government_id, doc_proof_of_address, doc_employment_or_business, doc_payslips, doc_proof_of_income,
    selected_document_count, agreed_to_terms, application_status,
    approved_payment_mode, approved_down_payment, approved_advance_payment, approved_monthly_due, approved_term_months,
    assessor_notes, assessor_by, assessor_decided_at, cashier_status,
    submitted_at
FROM kiosk_loan_applications
WHERE application_status = 'Approved'
  AND cashier_status = 'Waiting'
  AND approved_payment_mode = @mode
ORDER BY assessor_decided_at ASC, id ASC;";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mode", NormalizePaymentMode(paymentMode));

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new KioskLoanAssessorItem
                    {
                        Id = reader.GetInt64("id"),
                        QueueNumber = reader.GetString("queue_number"),
                        FullName = reader.GetString("full_name"),
                        Email = reader.GetString("email"),
                        Mobile = reader.GetString("mobile"),
                        DateOfBirth = reader.GetDateTime("date_of_birth"),
                        Address = reader.GetString("address"),
                        City = reader.GetString("city"),
                        Province = reader.GetString("province"),
                        EmploymentStatus = reader.GetString("employment_status"),
                        CompanyOrBusinessName = reader.GetString("company_or_business_name"),
                        PositionTitle = reader.GetString("position_title"),
                        YearsEmployed = reader.IsDBNull(reader.GetOrdinal("years_employed")) ? null : reader.GetInt32("years_employed"),
                        GrossIncome = reader.GetDecimal("gross_income"),
                        OtherIncome = reader.GetDecimal("other_income"),
                        TotalObligations = reader.GetDecimal("total_obligations"),
                        HasHomeLoan = reader.GetBoolean("has_home_loan"),
                        HasCarLoan = reader.GetBoolean("has_car_loan"),
                        HasPersonalLoan = reader.GetBoolean("has_personal_loan"),
                        HasCreditCard = reader.GetBoolean("has_credit_card"),
                        ProductName = reader.GetString("product_name"),
                        ProductYear = reader.GetString("product_year"),
                        ProductPrice = reader.GetDecimal("product_price"),
                        DownPaymentPercent = reader.GetInt32("down_payment_percent"),
                        DownPaymentAmount = reader.GetDecimal("down_payment_amount"),
                        FinancedAmount = reader.GetDecimal("financed_amount"),
                        SelectedTermMonths = reader.GetInt32("selected_term_months"),
                        AnnualInterestRate = reader.GetDecimal("annual_interest_rate"),
                        InterestAmount = reader.GetDecimal("interest_amount"),
                        TotalPayable = reader.GetDecimal("total_payable"),
                        MonthlyAmortization = reader.GetDecimal("monthly_amortization"),
                        DocValidGovernmentId = reader.GetBoolean("doc_valid_government_id"),
                        DocProofOfAddress = reader.GetBoolean("doc_proof_of_address"),
                        DocEmploymentOrBusiness = reader.GetBoolean("doc_employment_or_business"),
                        DocPayslips = reader.GetBoolean("doc_payslips"),
                        DocProofOfIncome = reader.GetBoolean("doc_proof_of_income"),
                        SelectedDocumentCount = reader.GetInt32("selected_document_count"),
                        AgreedToTerms = reader.GetBoolean("agreed_to_terms"),
                        ApplicationStatus = reader.GetString("application_status"),
                        ApprovedPaymentMode = reader.IsDBNull(reader.GetOrdinal("approved_payment_mode")) ? string.Empty : reader.GetString("approved_payment_mode"),
                        ApprovedDownPayment = reader.IsDBNull(reader.GetOrdinal("approved_down_payment")) ? null : reader.GetDecimal("approved_down_payment"),
                        ApprovedAdvancePayment = reader.IsDBNull(reader.GetOrdinal("approved_advance_payment")) ? null : reader.GetDecimal("approved_advance_payment"),
                        ApprovedMonthlyDue = reader.IsDBNull(reader.GetOrdinal("approved_monthly_due")) ? null : reader.GetDecimal("approved_monthly_due"),
                        ApprovedTermMonths = reader.IsDBNull(reader.GetOrdinal("approved_term_months")) ? null : reader.GetInt32("approved_term_months"),
                        AssessorNotes = reader.IsDBNull(reader.GetOrdinal("assessor_notes")) ? string.Empty : reader.GetString("assessor_notes"),
                        AssessorBy = reader.IsDBNull(reader.GetOrdinal("assessor_by")) ? string.Empty : reader.GetString("assessor_by"),
                        AssessorDecidedAt = reader.IsDBNull(reader.GetOrdinal("assessor_decided_at")) ? null : reader.GetDateTime("assessor_decided_at"),
                        CashierStatus = reader.IsDBNull(reader.GetOrdinal("cashier_status")) ? "Waiting" : reader.GetString("cashier_status"),
                        SubmittedAt = reader.GetDateTime("submitted_at")
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[KioskLoanApplicationDatabase] GetCashierReadyByMode error: {ex.Message}");
            }

            return items;
        }

        public static bool SaveApplication(KioskLoanApplicationRecord record)
        {
            Initialize();

            try
            {
                using var conn = DatabaseManager.GetConnection();

                const string sql = @"
INSERT INTO kiosk_loan_applications
(
    queue_number,
    full_name, email, mobile, date_of_birth, address, city, province,
    employment_status, company_or_business_name, position_title, years_employed,
    gross_income, other_income, total_obligations, has_home_loan, has_car_loan, has_personal_loan, has_credit_card,
    product_name, product_year, product_price, down_payment_percent, down_payment_amount, financed_amount,
    selected_term_months, annual_interest_rate, interest_amount, total_payable, monthly_amortization,
    doc_valid_government_id, doc_proof_of_address, doc_employment_or_business, doc_payslips, doc_proof_of_income,
    selected_document_count, agreed_to_terms
)
VALUES
(
    @queue_number,
    @full_name, @email, @mobile, @date_of_birth, @address, @city, @province,
    @employment_status, @company_or_business_name, @position_title, @years_employed,
    @gross_income, @other_income, @total_obligations, @has_home_loan, @has_car_loan, @has_personal_loan, @has_credit_card,
    @product_name, @product_year, @product_price, @down_payment_percent, @down_payment_amount, @financed_amount,
    @selected_term_months, @annual_interest_rate, @interest_amount, @total_payable, @monthly_amortization,
    @doc_valid_government_id, @doc_proof_of_address, @doc_employment_or_business, @doc_payslips, @doc_proof_of_income,
    @selected_document_count, @agreed_to_terms
);";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@queue_number", record.QueueNumber);

                cmd.Parameters.AddWithValue("@full_name", record.FullName);
                cmd.Parameters.AddWithValue("@email", record.Email);
                cmd.Parameters.AddWithValue("@mobile", record.Mobile);
                cmd.Parameters.AddWithValue("@date_of_birth", record.DateOfBirth.Date);
                cmd.Parameters.AddWithValue("@address", record.Address);
                cmd.Parameters.AddWithValue("@city", record.City);
                cmd.Parameters.AddWithValue("@province", record.Province);

                cmd.Parameters.AddWithValue("@employment_status", record.EmploymentStatus);
                cmd.Parameters.AddWithValue("@company_or_business_name", record.CompanyOrBusinessName);
                cmd.Parameters.AddWithValue("@position_title", record.PositionTitle);
                cmd.Parameters.AddWithValue("@years_employed", record.YearsEmployed.HasValue ? record.YearsEmployed.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@gross_income", record.GrossIncome);
                cmd.Parameters.AddWithValue("@other_income", record.OtherIncome);
                cmd.Parameters.AddWithValue("@total_obligations", record.TotalObligations);
                cmd.Parameters.AddWithValue("@has_home_loan", record.HasHomeLoan);
                cmd.Parameters.AddWithValue("@has_car_loan", record.HasCarLoan);
                cmd.Parameters.AddWithValue("@has_personal_loan", record.HasPersonalLoan);
                cmd.Parameters.AddWithValue("@has_credit_card", record.HasCreditCard);

                cmd.Parameters.AddWithValue("@product_name", record.ProductName);
                cmd.Parameters.AddWithValue("@product_year", record.ProductYear);
                cmd.Parameters.AddWithValue("@product_price", record.ProductPrice);
                cmd.Parameters.AddWithValue("@down_payment_percent", record.DownPaymentPercent);
                cmd.Parameters.AddWithValue("@down_payment_amount", record.DownPaymentAmount);
                cmd.Parameters.AddWithValue("@financed_amount", record.FinancedAmount);
                cmd.Parameters.AddWithValue("@selected_term_months", record.SelectedTermMonths);
                cmd.Parameters.AddWithValue("@annual_interest_rate", record.AnnualInterestRate);
                cmd.Parameters.AddWithValue("@interest_amount", record.InterestAmount);
                cmd.Parameters.AddWithValue("@total_payable", record.TotalPayable);
                cmd.Parameters.AddWithValue("@monthly_amortization", record.MonthlyAmortization);

                cmd.Parameters.AddWithValue("@doc_valid_government_id", record.DocValidGovernmentId);
                cmd.Parameters.AddWithValue("@doc_proof_of_address", record.DocProofOfAddress);
                cmd.Parameters.AddWithValue("@doc_employment_or_business", record.DocEmploymentOrBusinessProof);
                cmd.Parameters.AddWithValue("@doc_payslips", record.DocPayslips);
                cmd.Parameters.AddWithValue("@doc_proof_of_income", record.DocProofOfIncome);
                cmd.Parameters.AddWithValue("@selected_document_count", record.SelectedDocumentCount);
                cmd.Parameters.AddWithValue("@agreed_to_terms", record.AgreedToTerms);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[KioskLoanApplicationDatabase] SaveApplication error: {ex.Message}");
                return false;
            }
        }

        private static void CreateTableIfMissing()
        {
            using var conn = DatabaseManager.GetConnection();

            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = @db AND table_name = 'kiosk_loan_applications';",
                conn);
            checkCmd.Parameters.AddWithValue("@db", DatabaseManager.DATABASE_NAME);

            long exists = Convert.ToInt64(checkCmd.ExecuteScalar());
            if (exists > 0)
            {
                return;
            }

            const string createSql = @"
CREATE TABLE `kiosk_loan_applications` (
    `id`                             BIGINT AUTO_INCREMENT PRIMARY KEY,
    `queue_number`                   VARCHAR(20) NOT NULL,

    `full_name`                      VARCHAR(150) NOT NULL,
    `email`                          VARCHAR(150) NOT NULL,
    `mobile`                         VARCHAR(30) NOT NULL,
    `date_of_birth`                  DATE NOT NULL,
    `address`                        VARCHAR(255) NOT NULL,
    `city`                           VARCHAR(100) NOT NULL,
    `province`                       VARCHAR(100) NOT NULL,

    `employment_status`              VARCHAR(30) NOT NULL DEFAULT 'Unspecified',
    `company_or_business_name`       VARCHAR(150) NOT NULL,
    `position_title`                 VARCHAR(150) NOT NULL,
    `years_employed`                 INT NULL,

    `gross_income`                   DECIMAL(12,2) NOT NULL DEFAULT 0,
    `other_income`                   DECIMAL(12,2) NOT NULL DEFAULT 0,
    `total_obligations`              DECIMAL(12,2) NOT NULL DEFAULT 0,
    `has_home_loan`                  TINYINT(1) NOT NULL DEFAULT 0,
    `has_car_loan`                   TINYINT(1) NOT NULL DEFAULT 0,
    `has_personal_loan`              TINYINT(1) NOT NULL DEFAULT 0,
    `has_credit_card`                TINYINT(1) NOT NULL DEFAULT 0,

    `product_name`                   VARCHAR(200) NOT NULL,
    `product_year`                   VARCHAR(20) NOT NULL,
    `product_price`                  DECIMAL(12,2) NOT NULL DEFAULT 0,
    `down_payment_percent`           INT NOT NULL DEFAULT 0,
    `down_payment_amount`            DECIMAL(12,2) NOT NULL DEFAULT 0,
    `financed_amount`                DECIMAL(12,2) NOT NULL DEFAULT 0,
    `selected_term_months`           INT NOT NULL DEFAULT 0,
    `annual_interest_rate`           DECIMAL(8,4) NOT NULL DEFAULT 0,
    `interest_amount`                DECIMAL(12,2) NOT NULL DEFAULT 0,
    `total_payable`                  DECIMAL(12,2) NOT NULL DEFAULT 0,
    `monthly_amortization`           DECIMAL(12,2) NOT NULL DEFAULT 0,

    `doc_valid_government_id`        TINYINT(1) NOT NULL DEFAULT 0,
    `doc_proof_of_address`           TINYINT(1) NOT NULL DEFAULT 0,
    `doc_employment_or_business`     TINYINT(1) NOT NULL DEFAULT 0,
    `doc_payslips`                   TINYINT(1) NOT NULL DEFAULT 0,
    `doc_proof_of_income`            TINYINT(1) NOT NULL DEFAULT 0,
    `selected_document_count`        INT NOT NULL DEFAULT 0,
    `agreed_to_terms`                TINYINT(1) NOT NULL DEFAULT 0,

    `application_status`             ENUM('Pending','For Review','Approved','Rejected') NOT NULL DEFAULT 'Pending',
    `approved_payment_mode`          VARCHAR(30) NULL,
    `approved_down_payment`          DECIMAL(12,2) NULL,
    `approved_advance_payment`       DECIMAL(12,2) NULL,
    `approved_monthly_due`           DECIMAL(12,2) NULL,
    `approved_term_months`           INT NULL,
    `assessor_notes`                 TEXT NULL,
    `assessor_by`                    VARCHAR(80) NULL,
    `assessor_decided_at`            DATETIME NULL,
    `cashier_status`                 ENUM('Waiting','InProgress','Completed','Voided') NOT NULL DEFAULT 'Waiting',
    `submitted_at`                   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    INDEX `idx_kiosk_queue` (`queue_number`),
    INDEX `idx_kiosk_status` (`application_status`),
    INDEX `idx_kiosk_submitted_at` (`submitted_at`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using var createCmd = new MySqlCommand(createSql, conn);
            createCmd.ExecuteNonQuery();

            System.Diagnostics.Debug.WriteLine("[KioskLoanApplicationDatabase] Table `kiosk_loan_applications` created ✔");
        }

        private static void EnsureAssessorFlowColumns()
        {
            using var conn = DatabaseManager.GetConnection();

            EnsureColumn(conn, "approved_payment_mode", "ALTER TABLE kiosk_loan_applications ADD COLUMN approved_payment_mode VARCHAR(30) NULL AFTER application_status;");
            EnsureColumn(conn, "approved_down_payment", "ALTER TABLE kiosk_loan_applications ADD COLUMN approved_down_payment DECIMAL(12,2) NULL AFTER approved_payment_mode;");
            EnsureColumn(conn, "approved_advance_payment", "ALTER TABLE kiosk_loan_applications ADD COLUMN approved_advance_payment DECIMAL(12,2) NULL AFTER approved_down_payment;");
            EnsureColumn(conn, "approved_monthly_due", "ALTER TABLE kiosk_loan_applications ADD COLUMN approved_monthly_due DECIMAL(12,2) NULL AFTER approved_advance_payment;");
            EnsureColumn(conn, "approved_term_months", "ALTER TABLE kiosk_loan_applications ADD COLUMN approved_term_months INT NULL AFTER approved_monthly_due;");
            EnsureColumn(conn, "assessor_notes", "ALTER TABLE kiosk_loan_applications ADD COLUMN assessor_notes TEXT NULL AFTER approved_term_months;");
            EnsureColumn(conn, "assessor_by", "ALTER TABLE kiosk_loan_applications ADD COLUMN assessor_by VARCHAR(80) NULL AFTER assessor_notes;");
            EnsureColumn(conn, "assessor_decided_at", "ALTER TABLE kiosk_loan_applications ADD COLUMN assessor_decided_at DATETIME NULL AFTER assessor_by;");
            EnsureColumn(conn, "cashier_status", "ALTER TABLE kiosk_loan_applications ADD COLUMN cashier_status ENUM('Waiting','InProgress','Completed','Voided') NOT NULL DEFAULT 'Waiting' AFTER assessor_decided_at;");
        }

        private static void EnsureColumn(MySqlConnection conn, string columnName, string alterSql)
        {
            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = @db AND table_name = 'kiosk_loan_applications' AND column_name = @column;",
                conn);
            checkCmd.Parameters.AddWithValue("@db", DatabaseManager.DATABASE_NAME);
            checkCmd.Parameters.AddWithValue("@column", columnName);

            long exists = Convert.ToInt64(checkCmd.ExecuteScalar());
            if (exists > 0)
            {
                return;
            }

            using var alterCmd = new MySqlCommand(alterSql, conn);
            alterCmd.ExecuteNonQuery();
        }

        private static string NormalizePaymentMode(string mode)
        {
            if (string.IsNullOrWhiteSpace(mode))
            {
                return string.Empty;
            }

            string compact = mode.Replace(" ", string.Empty).Trim();
            if (compact.Equals("DownPayment", StringComparison.OrdinalIgnoreCase)) return "DownPayment";
            if (compact.Equals("AdvancePayment", StringComparison.OrdinalIgnoreCase)) return "AdvancePayment";
            if (compact.Equals("MonthlyPayment", StringComparison.OrdinalIgnoreCase)) return "MonthlyPayment";
            if (compact.Equals("FullCash", StringComparison.OrdinalIgnoreCase) || compact.Equals("FullCashPurchase", StringComparison.OrdinalIgnoreCase)) return "FullCash";
            if (compact.Equals("FullSettlement", StringComparison.OrdinalIgnoreCase)) return "FullSettlement";
            return compact;
        }
    }
}