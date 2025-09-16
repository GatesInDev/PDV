using System.ComponentModel.DataAnnotations; // Para as anotações de validação

namespace PDV.Application.DTOs.Product
{
    /// <summary>
    /// DTO para criação de um novo produto.
    /// </summary>
    public class CreateProductDTO
    {
        /// <summary>
        /// Definir o Stock Keep Unit do produto.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Sku { get; set; }

        /// <summary>
        /// Definir o nome do produto.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Definir o preço do produto.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Definir a quantidade do produto.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Definir a unidade de medida do produto(Un/Kg/Ml).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string MetricUnit { get; set; }

        /// <summary>
        /// Definir a FK para a categoria do produto.
        /// </summary>
        [Required]
        public int CategoryId { get; set; } // FK
    }
}
