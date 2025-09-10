using System.ComponentModel.DataAnnotations;

namespace PDV.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Sku { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public int CategoryId { get; set; } // FK

        public Category Category { get; set; } // Navigation Property

    }
}
