namespace PDV.Application.DTOs.Product
{
    public class CreateProductDTO
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string MetricUnit { get; set; }

        public int CategoryId { get; set; }
    }
}
