using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class MetricsController
    {
        /// <summary>
        /// Requisição para retornar os produtos mais vendidos. 
        /// </summary>
        /// <param name="items">Quantidade de items a serem retornados.</param>
        /// <returns>Lista descrecente de produtos baseado em sua venda.</returns>
        [HttpGet("bestsellers")]
        public async Task<IActionResult> GetBestSellers([FromQuery] int items)
        {

            return Ok(await _metricsService.GetBestSellers(items));
        }
    }
}
