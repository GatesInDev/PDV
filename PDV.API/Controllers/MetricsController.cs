using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar as métricas do sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class MetricsController : ControllerBase
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
    }
}
