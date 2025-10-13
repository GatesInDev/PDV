using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para autenticação e autorização de usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuth _authService;

        /// <summary>
        /// Construtor do controlador de autenticação.
        /// </summary>
        /// <param name="config">DI para acesso ao appsettings.json</param>
        /// <param name="authService">Serviço de autenticação.</param>
        public AuthController(IConfiguration config, IAuth authService)
        {
            _config = config;
            _authService = authService;
        }
    }
}