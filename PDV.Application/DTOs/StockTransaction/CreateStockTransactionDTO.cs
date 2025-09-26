using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.StockTransaction
{
    /// <summary>
    /// DTO para criação de uma nova transação de estoque.
    /// </summary>
    public class CreateStockTransactionDTO
    {
        /// <summary>
        /// Identificador único do produto associado à transação de estoque.
        /// </summary>
        [Required(ErrorMessage = "Identificador do produto é obrigatório.")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade alterada no estoque.
        /// </summary>
        [Required(ErrorMessage = "Quantidade alterada é obrigatória.")]
        public int QuantityChanged { get; set; }

        /// <summary>
        /// Definir o tipo de transação (e.g., "addition", "removal").
        /// </summary>
        [Required(ErrorMessage = "Tipo de transação é obrigatório.")]
        public string Type { get; set; }

        /// <summary>
        /// Definir a razão (opcional)  
        /// </summary>
        public string? Reason { get; set; }
    }
}
