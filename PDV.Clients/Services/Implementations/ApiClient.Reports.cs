using PDV.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<IEnumerable<SaleResultDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            string startStr = startDate.ToString("yyyy-MM-dd");
            string endStr = endDate.ToString("yyyy-MM-dd");

            string requestUrl = $"api/Sales?startDate={startStr}&endDate={endStr}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<SaleResultDto>>(requestUrl);

                return response ?? new List<SaleResultDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao buscar relatório de vendas: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado no cliente de API: {ex.Message}", ex);
            }
        }
    }
}