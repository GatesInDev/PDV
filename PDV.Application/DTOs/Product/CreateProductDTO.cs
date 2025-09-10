using PDV.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Product
{
    public class CreateProductDTO
    {

        [Required]
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

        [Required]
        public int CategoryId { get; set; } // FK

    }
}
