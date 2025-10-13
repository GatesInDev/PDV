using PDV.Application.DTOs.Metrics;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class MetricsService
    {
        /// <summary>
        /// Retorna os produtos com mais vendas.
        /// </summary>
        /// <param name="items">Quantidade de produtos a retornar.</param>
        /// <returns>Uma lista com os produtos.</returns>
        /// <exception cref="Exception">Erro ao verificar os produtos.</exception>
        public async Task<List<GetBestSellersMetricsDTO>> GetBestSellers(int items)
        {
            try
            {
                var list = await _metricsRepository.GetBestSellersAsync(items);

                return _mapper.Map<List<GetBestSellersMetricsDTO>>(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar os produtos.", ex);
            }
        }
    }
}
