using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _metricsService;

        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        [HttpGet("bestsellers/{items}")]
        public async Task<IActionResult> GetBestSellers([FromRoute] int items)
        {
            var list = await _metricsService.GetBestSellers(items);

            return Ok(list);
        }
    }
}
