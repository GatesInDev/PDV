using PDV.Application.DTOs.Metrics;
using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de métricas.
    /// </summary>
    public interface IMetricsService
    {
        /// <summary>
        /// Retorna os produtos com mais vendas.
        /// </summary>
        /// <param name="items">Quantidade de produtos a retornar.</param>
        /// <returns>Uma lista com os produtos.</returns>
        public Task<List<GetBestSellersMetricsDTO>> GetBestSellers(int items);

        /// <summary>
        /// Servicço que retorna o estoque abaixo do limite.
        /// </summary>
        /// <param name="stockQuantity">Limite de estoque.</param>
        /// <returns>Lista com os produtos que tem o estoque abaixo do limite.</returns>
        public Task<List<GetBelowStockDTO>> GetBelowStock(int stockQuantity);

        /// <summary>
        /// Serviço da Dashboard.
        /// </summary>
        /// <returns>Retorna um JSON com algumas metricas.</returns>
        public Task<DataIndicatorsDTO> GetDashboardMetrics();
    }
}
