using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Clients.Models
{
    public class ProductsSuggestionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        private decimal Price { get; set; }
        public override string ToString() => Name;
    }
}
