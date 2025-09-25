namespace PDV.Application.DTOs.Sales
{
    public class SaleProductDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal PriceAtSaleTime { get; set; }
        public int Quantity { get; set; }
    }
}
