using System.Text.Json.Serialization;
using PDV.Application.DTOs.Customer;
using PDV.Application.DTOs.Sales;
using PDV.Clients.Models.Cash;

namespace PDV.Core.Entities
{
    /// <summary>
    /// Entidade que representa uma venda realizada no sistema.
    /// </summary>
    public class Sale
    {
  
        public Guid Id { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime SaleDate { get; set; }

        public string PaymentMethod { get; set; }

        public List<SaleProductDTO> SaleProducts { get; set; } = new();


        public Guid CashSessionId { get; set; }


        public CashSessionDTO CashSession { get; set; }

        public Guid? CustomerId { get; set; }

        public CustomerDTO Customer { get; set; }

    }
}
