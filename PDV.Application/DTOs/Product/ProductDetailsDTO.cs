namespace PDV.Application.DTOs.Product
{
    public class ProductDetailsDTO
    {
        public required int Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; }


    }
}
