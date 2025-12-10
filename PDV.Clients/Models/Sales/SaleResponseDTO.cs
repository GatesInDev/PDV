using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDV.Clients.Models.Customer;

namespace PDV.Clients.Models.Sales
{
    public class SaleResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; } 
        public string PaymentMethod { get; set; } 

        public CustomerSimpleDTO? Customer { get; set; }
            
    }
}
