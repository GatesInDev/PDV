using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailsDTO> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(CreateProductDTO dto);

        Task<Guid> UpdateAsync(Guid id, UpdateProductDTO dto);

        //Task<IEnumerable<ProductDetailsDTO>> GetAllAsync();

        //Task<bool> DeleteAsync(Guid id);
    }
}
