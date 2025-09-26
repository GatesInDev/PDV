using PDV.Core.Entities;

namespace PDV.Application.DTOs.Sales
{
    /// <summary>
    /// DTO para exibição dos detalhes de uma venda.
    /// </summary>
    public class SaleDetailsDTO
    {
        /// <summary>
        /// Identificador único da venda.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Preço total da venda.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Timestamp da data da venda.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Método de pagamento utilizado na venda (ex: Dinheiro, Cartão de Crédito, Pix).
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Operador de caixa responsável pela venda.
        /// </summary>
        public string CashOperator { get; set; }

        /// <summary>
        /// Nome do cliente associado à venda, se houver.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Lista de produtos incluídos na venda.
        /// </summary>
        public List<SaleProductDTO> Products { get; set; } = new();
    }
}
