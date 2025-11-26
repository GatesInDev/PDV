using PDV.Application.DTOs.Product;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task<List<ProductDTO>> GetProductsByNameAsync(string name);
    }
}
