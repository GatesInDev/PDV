using PDV.Core.Entities;
using System.Text.Json.Serialization;

public class SaleProduct
{
    public Guid SaleId { get; set; }

    [JsonIgnore]
    public Sale Sale { get; set; }

    public Guid ProductId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public decimal PriceAtSaleTime { get; set; }
}
