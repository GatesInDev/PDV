using PDV.Core.Entities;
using System.Text.Json.Serialization;

namespace PDV.Application.DTOs.Customer
{
    public class CustomersAndSalesDTO
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
        /// Lista de produtos incluídos na venda.
        /// </summary>
        public List<SaleProduct> SaleProducts { get; set; } = new();


        /// <summary>
        /// Identificador da sessão de caixa.
        /// </summary>
        public Guid CashSessionId { get; set; }

        /// <summary>
        /// Sessão de caixa associada à venda.
        /// </summary>
        [JsonIgnore]
        public CashSession CashSession { get; set; }

    }
}
