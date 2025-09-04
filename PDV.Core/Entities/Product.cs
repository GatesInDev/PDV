using System.ComponentModel.DataAnnotations;

namespace PDV.Core.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Sku { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public int CategoryId { get; set; } // FK

        public Category Category { get; set; } // Navigation Property

    }
}
