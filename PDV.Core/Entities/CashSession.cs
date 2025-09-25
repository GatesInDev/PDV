using System.Text.Json.Serialization;

namespace PDV.Core.Entities
{
    public class CashSession
    {
        public Guid Id { get; set; }

        public string OperatorName { get; set; } 
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; } 

        public decimal OpeningAmount { get; set; } 
        public decimal? ClosingAmount { get; set; }

        public List<Sale> Sales { get; set; } = new();
    }
}
