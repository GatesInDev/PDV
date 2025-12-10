using PDV.Application.DTOs.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<StockDTO> GetStockFromIdAsync(Guid id)
        {
            try
            {
                var stock = await _httpClient.GetFromJsonAsync<StockDTO>($"api/Stock/{id}");
                return stock ?? new StockDTO();
            }
            catch (Exception ex)
            {
                return new StockDTO();
            }
        }
    }
}
