using AutoMapper;
using PDV.Application.DTOs.Metrics;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para as métricas.
    /// </summary>
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository _metricsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor para o servico de métricas.
        /// </summary>
        /// <param name="metricsrepository">Repositório das métricas.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        public MetricsService(IMetricsRepository metricsrepository, IMapper mapper)
        {
            _metricsRepository = metricsrepository;
            _mapper = mapper;
        }

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

                var map = _mapper.Map<List<GetBestSellersMetricsDTO>>(list);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar os produtos.", ex);
            }
        }

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
                return stocks.Select(s => new GetBelowStockDTO
                {
                    Id = s.Id,
                    Quantity = s.Quantity,
                    MetricUnit = s.MetricUnit,
                    LastUpdated = s.LastUpdated,
                    ProductId = s.ProductId,
                    ProductName = s.Product.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar estoque.", ex);
            }
        }

        /// <summary>
        /// Serviço da Dashboard.
        /// </summary>
        /// <returns>Retorna um JSON com algumas metricas.</returns>
        /// <exception cref="Exception">Erro ao calcular métricas.</exception>
        public async Task<DataIndicatorsDTO> GetDashboardMetrics()
        {
            try
            {
                var map = new DataIndicatorsDTO();

                var salesDayTotal = await _metricsRepository.GetDailySales(DateTime.UtcNow);

                map.DailySales = salesDayTotal;

                var incomeDayTotal = await _metricsRepository.GetDailyIncome(DateTime.UtcNow);

                map.DailyIncome = incomeDayTotal;

                map.MediumTicket = incomeDayTotal / salesDayTotal;

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao calcular as métricas.", ex);
            }
        }
    }
}
