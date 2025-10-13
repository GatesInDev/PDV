using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class SalesController
    {
        /// <summary>
        /// Retorna as vendas em um período.
        /// </summary>
        /// <param name="startDate">Data inicial de filtragem.</param>
        /// <param name="endDate">Data final de filtragem.</param>
        /// <returns>Uma lista com objetos dentro deste periodo.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpGet]
        public async Task<IActionResult> GetByPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                return Ok(await _saleService.GetByPeriod(startDate, endDate));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
