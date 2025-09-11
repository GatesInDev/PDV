using PDV.Application.DTOs.Product;

public interface IProductService
{
    Task<ProductDetailsDTO> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(CreateProductDTO dto);

    Task<Guid> UpdateAsync(Guid id, UpdateProductDTO dto);
}
