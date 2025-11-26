using PDV.Application.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace PDV.Application.DTOs.Sales
{
    /// <summary>
    /// DTO para criação de uma nova venda.
    /// </summary>
    public class CreateSalesDTO
    {
        /// <summary>
        /// Entrada do método de pagamento da venda.
        /// </summary>
        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O método de pagamento não pode exceder 50 caracteres.")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Entrada dos produtos incluídos na venda.
        /// </summary>
        [Required(ErrorMessage = "A lista de produtos é obrigatória.")]
        public List<CreateSaleProductDTO> Products { get; set; } = new();

        /// <summary>
        /// Identificador da sessão de caixa associada à venda.
        /// </summary>
        [Required(ErrorMessage = "O identificador da sessão de caixa é obrigatório.")]
        public Guid? CustomerId { get; set; }
    }
}
