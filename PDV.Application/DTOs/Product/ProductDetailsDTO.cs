using PDV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Application.DTOs.Product
{
    public class ProductDetailsDTO
    {
        public required int Id { get; set; }

        public int Sku { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string MetricUnit { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; }


    }
}
