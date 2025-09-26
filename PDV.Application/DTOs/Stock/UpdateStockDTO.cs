using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Stock
{
    /// <summary>
    /// DTO para atualização de um estoque existente.
    /// </summary>
    public class UpdateStockDTO
    {
        /// <summary>
        /// Identificador único do produto associado ao estoque.
        /// </summary>
        [Required(ErrorMessage = "O campo ProductId é obrigatório.")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Definir a quantidade do produto no estoque.
        /// </summary>
        [Required(ErrorMessage = "O campo Quantity é obrigatório.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Definir a unidade de medida do produto (Un/Kg/Ml).
        /// </summary>
        [Required(ErrorMessage = "O campo MetricUnit é obrigatório.")]
        [MaxLength(50, ErrorMessage = "A unidade de medida não pode exceder 50 caracteres.")]
        public string MetricUnit { get; set; }
    }
}
