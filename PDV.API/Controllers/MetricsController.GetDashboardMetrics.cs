using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class MetricsController
    {
        /// <summary>
        /// Requisição que retorna dados para dashboard.
        /// </summary>
        /// <returns>Dados em formato JSON para dashboard.</returns>
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardMetrics()
        {
            return Ok(await _metricsService.GetDashboardMetrics());
        }
    }
}
