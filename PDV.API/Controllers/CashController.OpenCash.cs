using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Cash;

namespace PDV.API.Controllers
{
    public partial class CashController
    {
        /// <summary>
        /// Abre uma nova sessão de caixa.
        /// </summary>
        /// <param name="dto">Dados iniciais do caixa a ser aberto.</param>
        /// <returns>Identificador da sessão de caixa.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpPost("open")]
        public async Task<IActionResult> OpenCash([FromBody] OpenCashSessionDTO dto)
        {
            try
            {
                return Ok(new { CashId = await _cashService.OpenCash(dto) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
