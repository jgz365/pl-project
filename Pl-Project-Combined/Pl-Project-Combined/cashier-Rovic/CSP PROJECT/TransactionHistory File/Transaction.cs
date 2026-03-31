// Transaction.cs — data model shared across POS forms
namespace POSCashierSystem
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string QueueNumber { get; set; }
        public System.DateTime DateTime { get; set; }
        public string PaymentType { get; set; }   // "Down Payment", "Full Cash", etc.
        public string CustomerName { get; set; }
        public string UnitModel { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }   // "Paid" | "Pending"
    }
}