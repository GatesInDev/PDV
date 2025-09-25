using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Cash;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;

namespace PDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : ControllerBase
    {
        private readonly ICashService _cashService;

        public CashController(ICashService cashService)
        {
            _cashService = cashService;
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenCash([FromBody] OpenCashSessionDTO dto)
        {
            var id = await _cashService.OpenCash(dto);
            return Ok(new { CashId = id });
        }

        [HttpPost("close")]
        public async Task<IActionResult> CloseCash([FromBody] CloseCashSessionDTO dto)
        {
            await _cashService.CloseCash(dto);
            return Ok(new { Message = "Caixa fechado com sucesso." });
        }

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
