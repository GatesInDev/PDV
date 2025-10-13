using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CustomerController : ControllerBase
    {
        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Uma lista com todos os clientes.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _customerService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
