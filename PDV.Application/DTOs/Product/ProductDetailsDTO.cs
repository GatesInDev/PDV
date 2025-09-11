namespace PDV.Application.DTOs.Product
{
    public class ProductDetailsDTO
    {
        public required Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public string MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string CategoryName { get; set; }


    }
}
