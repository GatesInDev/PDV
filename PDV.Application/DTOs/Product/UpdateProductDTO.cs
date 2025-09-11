using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Product
{
    public class UpdateProductDTO
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
        [MaxLength(100)]
        public string CategoryId { get; set; }  //FK

    }
}
