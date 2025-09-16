public class StockTransaction
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int QuantityChanged { get; set; }
    public string Type { get; set; } 
    public DateTime LastUpdated { get; set; }
    public string? Reason { get; set; } 
}
