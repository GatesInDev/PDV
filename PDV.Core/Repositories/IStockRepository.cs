using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IStockRepository
    {
        Task<Stock> GetByProductIdAsync(Guid productId);

        Task UpdateAsync(Stock stock);

        Task CreateAsync(Stock stock);

        Task<bool> StockExistsAsync(Guid productId);
    }
}
