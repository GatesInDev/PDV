using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Cash;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para operações relacionadas ao caixa.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : ControllerBase
    {
        private readonly ICashService _cashService;

        /// <summary>
        /// Construtor do controlador de caixa.
        /// </summary>
        /// <param name="cashService"></param>
        public CashController(ICashService cashService)
        {
            _cashService = cashService;
        }

        /// <summary>
        /// Abre uma nova sessão de caixa.
        /// </summary>
        /// <param name="dto">Dados iniciais do caixa a ser aberto.</param>
        /// <returns>Identificador da sessão de caixa.</returns>
        [HttpPost("open")]
        public async Task<IActionResult> OpenCash([FromBody] OpenCashSessionDTO dto)
        {
            var id = await _cashService.OpenCash(dto);
            return Ok(new { CashId = id });
        }

        /// <summary>
        /// Fecha a sessão de caixa atual.
        /// </summary>
        /// <param name="dto">Objeto com o restante dos dados da sessão do caixa.</param>
        /// <returns>Mensagem de retorno sendo um sucesso.</returns>
        [HttpPost("close")]
        public async Task<IActionResult> CloseCash([FromBody] CloseCashSessionDTO dto)
        {
            await _cashService.CloseCash(dto);
            return Ok(new { Message = "Caixa fechado com sucesso." });
        }

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da sessão de caixa.</param>
        /// <returns>Objeto com os dados dessa sessão.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCashById(Guid id)
        {
            var session = await _cashService.GetCashById(id);
            if (session == null)
                return NotFound();

            return Ok(session);
        }
    }
}
