namespace PDV.Application.DTOs.Product
{
    public class UpdateProductDTO
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string MetricUnit { get; set; }

        public string CategoryName { get; set; } 

    }
}
