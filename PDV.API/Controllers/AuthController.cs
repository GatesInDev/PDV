using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PDV.Application.DTOs;
using PDV.Application.Services.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para autenticação e autorização de usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
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

        /// <summary>
        /// Autentica um usuário e retorna um token JWT se as credenciais forem válidas.
        /// </summary>
        /// <param name="login">Objeto com usuario e senha.</param>
        /// <returns>Token Bearer JWT</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();

            try
            {
                var user = await _authService.AuthenticateUser(login);

                if (user != null)
                {
                    var tokenString = _authService.GenerateToken(user.Username, user.Role, _config["Jwt:Key"]);
                    response = Ok(new { token = tokenString });
                }
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna todos os usuários do sistema. Apenas acessível por administradores.
        /// </summary>
        /// <returns>Uma lista com todos os usuarios.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _authService.GetAllUserAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
