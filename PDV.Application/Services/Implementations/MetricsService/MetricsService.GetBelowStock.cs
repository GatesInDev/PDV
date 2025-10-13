using PDV.Application.DTOs.Metrics;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class MetricsService
    {
        /// <summary>
        /// Servicço que retorna o estoque abaixo do limite.
        /// </summary>
        /// <param name="stockQuantity">Limite de estoque.</param>
        /// <returns>Lista com os produtos que tem o estoque abaixo do limite.</returns>
        /// <exception cref="Exception">Erro ao verificar o estoque.</exception>
        public async Task<List<GetBelowStockDTO>> GetBelowStock(int stockQuantity)
        {
            try
            {
                var stocks = await _metricsRepository.GetBelowStockAsync(stockQuantity);
                return _mapper.Map<List<GetBelowStockDTO>>(stocks);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar estoque.", ex);
            }
        }
    }
}
