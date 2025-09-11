using PDV.Core.Entities;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid id);

    Task AddAsync(Product product);

    Task<bool> SkuExistsAsync(string sku);

    Task UpdateAsync(Product product);
}