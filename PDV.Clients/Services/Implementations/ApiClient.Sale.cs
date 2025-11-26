using PDV.Application.DTOs.Metrics;
using PDV.Application.DTOs.Sales;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading; // Necessário para CancellationToken
using System.Threading.Tasks;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<bool> PostSaleAsync(CreateSalesDTO dto, CancellationToken cancellationToken)
        {
            try
            {

                var response = await _httpClient.PostAsJsonAsync("/api/Sales", dto, cancellationToken);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}