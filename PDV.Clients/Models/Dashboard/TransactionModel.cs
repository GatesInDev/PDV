using System;

namespace PDV.Clients.Models.Dashboard
{
    public class TransactionModel
    {
        public Guid TransactionId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; }
    }
}