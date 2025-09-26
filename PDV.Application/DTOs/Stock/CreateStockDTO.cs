using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Stock
{
    /// <summary>
    /// DTO para criação de um novo estoque.
    /// </summary>
    public class CreateStockDTO
    {
        /// <summary>
        /// Define o ID do produto ao qual este estoque pertence.
        /// </summary>
        [Required(ErrorMessage = "Identificador do produto é obrigatório.")]
        public Guid ProductId { get; set; } // FK

        /// <summary>
        /// Define a quantidade inicial de itens em estoque para o produto.
        /// </summary>
        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Define a unidade de medida do estoque (Un/Kg/Ml).
        /// </summary>
        [Required(ErrorMessage = "Unidade de medida é obrigatória.")]
        [MaxLength(50, ErrorMessage = "A unidade de medida não pode exceder 50 caracteres.")]
        public string MetricUnit { get; set; }
    }
}
