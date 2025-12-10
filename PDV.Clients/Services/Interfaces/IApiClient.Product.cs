using PDV.Application.DTOs.Product;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        Task<List<ProductDTO>> GetAllProductsAsync();

        /// <summary>
        /// Obtém produtos por nome.
        /// </summary>
        Task<List<ProductDTO>> GetProductsByNameAsync(string name);

        /// <summary>
        /// Obtém detalhes completos de um produto.
        /// </summary>
        Task<ProductDetailsDTO> GetProductDetailsAsync(Guid id);

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        Task CreateProductAsync(CreateProductDTO dto);

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        Task<Guid> UpdateProductAsync(Guid id, UpdateProductDTO dto);

        /// <summary>
        /// Deleta um produto.
        /// </summary>
        Task<bool> DeleteProductAsync(Guid id);
    }
}