namespace PDV.Core.Entities
{
    public class Product
    {
        public required int Id { get; set; }

        public int Sku { get; set; }

        public string Name { get; set; } = String.Empty;

        public bool IsActive { get; set; } = false;

        public int MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public int CategoryId { get; set; } // FK

        public Category Category { get; set; } = null!; // Navigation Property

    }
}
