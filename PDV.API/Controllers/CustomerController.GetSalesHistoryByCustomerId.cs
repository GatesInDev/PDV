using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CustomerController
    {
        /// <summary>
        /// Retorna o histórico de compras de um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do Cliente.</param>
        /// <returns>Uma lista com todas suas compras.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpGet("{id}/sales")]
        public async Task<IActionResult> GetSalesHistoryByCustomerId(Guid id)
        {
            try
            {
                return Ok(await _customerService.GetSalesHistoryByCostumerId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
