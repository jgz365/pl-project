namespace customer_kiosk
{
    public class LoanApplicationSession
    {
        public Product SelectedProduct { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime? DateOfBirth { get; set; }
        public string CompleteAddress { get; set; }
        public string EmploymentStatus { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public int? YearsEmployed { get; set; }

        public string SourceOfIncome { get; set; }
        public string MonthlyGrossIncomeRange { get; set; }

        public int? LoanTermMonths { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public string QueueTicketNumber { get; set; }

        public string ValidIdFileName { get; set; }
        public string ProofOfIncomeFileName { get; set; }
        public string ProofOfAddressFileName { get; set; }
    }
}
