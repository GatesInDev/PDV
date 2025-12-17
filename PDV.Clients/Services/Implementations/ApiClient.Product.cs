using System.Net.Http;
using PDV.Application.DTOs.Product;
using PDV.Clients.Services.Interfaces;
using System.Net.Http.Json;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient : IApiClient
    {
        private const string ProductEndpoint = "api/Product";

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductDTO>>(ProductEndpoint);
                return products ?? new List<ProductDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
            catch (Exception ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
        }

        public async Task<List<ProductDTO>> GetProductsByNameAsync(string name)
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductDTO>>(
                    $"{ProductEndpoint}/GetByName/{Uri.EscapeDataString(name)}"
                );
                return products.Where(p => p.IsActive == true).ToList() ?? new List<ProductDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
            catch (Exception ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
        }

        public async Task<ProductDetailsDTO> GetProductDetailsAsync(Guid id)
        {
            try
            {
                var product = await _httpClient.GetFromJsonAsync<ProductDetailsDTO>(
                    $"{ProductEndpoint}/{id}"
                );
                return product ?? throw new InvalidOperationException("Produto não encontrado.");
            }
            catch (HttpRequestException ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
            catch (Exception ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
        }

        public async Task CreateProductAsync(CreateProductDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ProductEndpoint, dto);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
            catch (Exception ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
        }

        public async Task<Guid> UpdateProductAsync(Guid id, UpdateProductDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{ProductEndpoint}/{id}", dto);
                response.EnsureSuccessStatusCode();

                var result = id;
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
            catch (Exception ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                await _httpClient.DeleteAsync($"{ProductEndpoint}/{id}");
                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
            catch (Exception ex)
            {
                throw new HttpIOException(HttpRequestError.Unknown);
            }
        }
    }
}
