namespace PDV.Application.DTOs.StockTransaction
{
    public class CreateStockTransactionDTO
    {
        public Guid ProductId { get; set; }
        public int QuantityChanged { get; set; }
        public string Type { get; set; }
        public string? Reason { get; set; }
    }
}
