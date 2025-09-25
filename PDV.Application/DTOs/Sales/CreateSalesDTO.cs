using PDV.Application.DTOs.Product;

namespace PDV.Application.DTOs.Sales
{
    public class CreateSalesDTO
    {
        public string PaymentMethod { get; set; }
        public List<CreateSaleProductDTO> Products { get; set; } = new();
    }
}
