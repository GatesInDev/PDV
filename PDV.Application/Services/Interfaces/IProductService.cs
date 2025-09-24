using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailsDTO> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(CreateProductDTO dto);

        Task<Guid> UpdateAsync(Guid id, UpdateProductDTO dto);

        Task<List<ProductDTO>> GetAllAsync();

        Task<List<ProductDTO>> GetByCategoryAsync(string category);

        Task<bool> DisableProductAsync(Guid id);
    }
}
