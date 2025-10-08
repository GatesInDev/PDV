using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IMetricsRepository
    {
        public Task<List<Product>> GetBestSellersAsync(int items);
    }
}
