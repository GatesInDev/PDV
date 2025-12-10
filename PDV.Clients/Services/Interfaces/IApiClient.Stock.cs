using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDV.Application.DTOs.Stock;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task<StockDTO> GetStockFromIdAsync(Guid id);
    }
}
