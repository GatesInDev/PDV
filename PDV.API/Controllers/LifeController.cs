using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para autenticação e autorização de usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LifeController : ControllerBase
    {
        private readonly ILife _lifeService;

        /// <summary>
        /// Construtor do controlador de vida.
        /// </summary>
        /// <param name="config">DI para acesso ao appsettings.json</param>
        /// <param name="lifeService">Serviço de vida.</param>
        public LifeController(ILife lifeService)
        {
            _lifeService = lifeService;
        }

        [HttpGet("life")]
        public async Task<IActionResult> GetLife()
        {
            var modelo = new LifeModel()
            {
                Status = "Conectado",
                CpuLoad = await _lifeService.CpuLoad()
            };

            return Ok(modelo);
        }
    }
}