namespace PDV.Application.DTOs.Stock
{
    public class CreateStockDTO
    {
        public Guid ProductId { get; set; } // FK
        public int Quantity { get; set; }

        public string MetricUnit { get; set; }
    }
}
