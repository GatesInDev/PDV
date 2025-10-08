using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository _metricsRepository;

        public MetricsService(IMetricsRepository metricsrepository)
        {
            _metricsRepository = metricsrepository;
        }

        public async Task<List<Product>> GetBestSellers(int items)
        {
           return await _metricsRepository.GetBestSellersAsync(items);
        }
    }
}
