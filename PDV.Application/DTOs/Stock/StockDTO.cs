namespace PDV.Application.DTOs.Stock
{
    public class StockDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; } // FK
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string MetricUnit { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
