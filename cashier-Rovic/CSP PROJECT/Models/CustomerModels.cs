using System;

namespace POSCashierSystem
{
    // Shared model used by multiple POS modules
    public class CustomerSummary
    {
        public string? Name { get; set; }
        public string? QueueTicket { get; set; }
        public string? Location { get; set; }
        public string? TransactionType { get; set; }
        public UnitDetailsData? UnitDetails { get; set; }
        public FinancialStatusData? FinancialStatus { get; set; }
    }

    public class UnitDetailsData
    {
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? EngineNo { get; set; }
    }

    public class FinancialStatusData
    {
        // Monetary values
        public decimal DownPaymentDue { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal MonthlyAmortization { get; set; }

        // Original next due provided by backend/JSON (kept as string for compatibility with existing code)
        public string? NextDueDate { get; set; }
    }
}
