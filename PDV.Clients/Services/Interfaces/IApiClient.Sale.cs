using PDV.Application.DTOs.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        public Task<bool> PostSaleAsync(CreateSalesDTO dto, CancellationToken cancellationToken);
    }
}
