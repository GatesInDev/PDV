using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CashController
    {
        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da sessão de caixa.</param>
        /// <returns>Objeto com os dados dessa sessão.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCashById(Guid id)
        {
            try
            {
                return Ok(await _cashService.GetCashById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
