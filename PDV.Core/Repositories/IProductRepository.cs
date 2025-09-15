using PDV.Core.Entities; // Para acessar a entidade Product

namespace PDV.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);

        Task AddAsync(Product product);

        Task<bool> SkuExistsAsync(string sku);

        Task UpdateAsync(Product product);

        Task<List<Product>> GetAllAsync();

        Task<List<Product>> GetByCategory(string category);
    }
}