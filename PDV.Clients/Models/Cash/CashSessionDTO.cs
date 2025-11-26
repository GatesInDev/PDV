using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDV.Core.Entities;

namespace PDV.Clients.Models.Cash
{
    public class CashSessionDTO
    {
        public Guid Id { get; set; }

        public string OperatorName { get; set; }

        public DateTime OpenedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        public decimal OpeningAmount { get; set; }

        public decimal? ClosingAmount { get; set; }



        public List<Sale> Sales { get; set; } = new();
    }
}
