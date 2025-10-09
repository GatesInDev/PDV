using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar as métricas do sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _metricsService;

        /// <summary>
        /// Construtor das métricas.
        /// </summary>
        /// <param name="metricsService">Serviço das métricas.</param>
        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        /// <summary>
        /// Requisição para retornar os produtos mais vendidos. 
        /// </summary>
        /// <param name="items">Quantidade de items a serem retornados.</param>
        /// <returns>Lista descrecente de produtos baseado em sua venda.</returns>
        [HttpGet("bestsellers")]
        public async Task<IActionResult> GetBestSellers([FromQuery] int items)
        {
            var list = await _metricsService.GetBestSellers(items);

            return Ok(list);
        }

        /// <summary>
        /// Requisição para retornar os estoques que estão no fim.
        /// </summary>
        /// <param name="stockQuantity">O limite de estoque.</param>
        /// <returns>Os estoques com quantidade menor igual ao limite.</returns>
        [HttpGet("stockbelow/{stockQuantity:int}")]
        public async Task<IActionResult> GetBelowStock(int stockQuantity)
        {
            var list = await _metricsService.GetBelowStock(stockQuantity);

            return Ok(list);
        }

        /// <summary>
        /// Requisição que retorna dados para dashboard.
        /// </summary>
        /// <returns>Dados em formato JSON para dashboard.</returns>
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardMetrics()
        {
            var list = await _metricsService.GetDashboardMetrics();

            return Ok(list);
        }
    }
}
