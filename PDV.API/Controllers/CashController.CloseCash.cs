using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Cash;

namespace PDV.API.Controllers
{
    public partial class CashController
    {
        /// <summary>
        /// Fecha a sessão de caixa atual.
        /// </summary>
        /// <param name="dto">Objeto com o restante dos dados da sessão do caixa.</param>
        /// <returns>Mensagem de retorno sendo um sucesso.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpPost("close")]
        public async Task<IActionResult> CloseCash([FromBody] CloseCashSessionDTO dto)
        {
            try
            {
                await _cashService.CloseCash(dto);
                return Ok(GetCashById(dto.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
