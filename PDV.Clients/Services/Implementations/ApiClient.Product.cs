using System.Net.Http;
using System.Net.Http.Json;
using PDV.Application.DTOs.Product;
using PDV.Clients.Services.Interfaces;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient : IApiClient
    {
        public async Task<List<ProductDTO>> GetProductsByNameAsync(string name)
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductDTO>>($"api/Product/GetByName/{name}");
                return products ?? new List<ProductDTO>();
            }
            catch (HttpRequestException)
            {
                return new List<ProductDTO>();
            }
            catch (Exception)
            {
                return new List<ProductDTO>();
            }
        }
    }
}
