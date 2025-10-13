using PDV.Application.DTOs.Metrics;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class MetricsService
    {
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
