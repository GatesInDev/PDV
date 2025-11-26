using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Product
{
    /// <summary>
    /// DTO para adicionar um produto a uma venda.
    /// </summary>
    public class ProductFormSaleDTO
    {
        /// <summary>
        /// Parâmetro de entrada do identificador único do produto.
        /// </summary>
        [Required(ErrorMessage = "O Identificador do Produto é obrigatório.")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade do produto.
        /// </summary>
        [Required(ErrorMessage = "A Quantidade do Produto é obrigatória.")]
        public int Quantity { get; set; }
    }
}
