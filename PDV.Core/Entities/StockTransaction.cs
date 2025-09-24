using System.Text.Json.Serialization;

namespace PDV.Core.Entities
{
    public class StockTransaction
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        public int QuantityChanged { get; set; }

        public string Type { get; set; }

        public DateTime LastUpdated { get; set; }

        public string? Reason { get; set; }
    
    }

}
