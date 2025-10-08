using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    public interface IMetricsService
    {
        public Task<List<Product>> GetBestSellers(int items);
    }
}
