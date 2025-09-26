using System.ComponentModel.DataAnnotations; // Para as anotações de validação

namespace PDV.Application.DTOs.Product
{
    /// <summary>
    /// DTO para atualização de um produto existente.
    /// </summary>
    public class UpdateProductDTO
    {
        /// <summary>
        /// Definir o Stock Keep Unit do produto.
        /// </summary>
        [Required(ErrorMessage = "O SKU é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O tamanho maximo do SKU é 100 caracteres.")]
        public string Sku { get; set; }

        /// <summary>
        /// Definir o nome do produto.
        /// </summary>
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [MaxLength(256, ErrorMessage = "O tamanho maximo do Nome é 256 caracteres.")]
        public string Name { get; set; }

        /// <summary>
        /// Definir o preço do produto.
        /// </summary>
        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Definir a unidade de medida do produto(Un/Kg/Ml).
        /// </summary>
        [Required(ErrorMessage = "A unidade de medida é obrigatória.")]
        [MaxLength(50, ErrorMessage = "A unidade de medida não pode exceder 50 caracteres.")]
        public string MetricUnit { get; set; }

        /// <summary>
        /// Definir a FK para a categoria do produto.
        /// </summary>
        [Required(ErrorMessage = "O identificador da categoria é obrigatório.")]
        public int CategoryId { get; set; }  //FK
    }
}
