using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Domain
{
    public class SaleResultDto
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } 
        public string Status { get; set; }
        public string? Product { get; set; }
    }
}
