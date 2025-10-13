using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class MetricsController
    {
        /// <summary>
        /// Requisição para retornar os estoques que estão no fim.
        /// </summary>
        /// <param name="stockQuantity">O limite de estoque.</param>
        /// <returns>Os estoques com quantidade menor igual ao limite.</returns>
        [HttpGet("stockbelow/{stockQuantity}")]
        public async Task<IActionResult> GetBelowStock(int stockQuantity)
        {
            return Ok(await _metricsService.GetBelowStock(stockQuantity));
        }
    }
}