namespace PDV.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public string MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; } // FK

        public Category Category { get; set; } // Navigation Property

        public Stock Stock { get; set; }

    }
}
