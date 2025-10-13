using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class SalesController
    {
        /// <summary>
        /// Retorna uma venda pelo ID.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Os dados da venda.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _saleService.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
