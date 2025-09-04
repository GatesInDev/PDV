using PDV.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Product
{
    public class UpdateProductDTO
    {
        public int Sku { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string MetricUnit { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; } 

    }
}
