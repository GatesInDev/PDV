using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PDV.Application.DTOs;
using PDV.Application.Services.Interfaces;
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
        /// <param name="config"></param>
        /// <param name="authService"></param>
        public AuthController(IConfiguration config, IAuth authService)
        {
            _config = config;
            _authService = authService;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT se as credenciais forem válidas.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();

            try
            {
                var user = _authService.AuthenticateUser(login);

                if (user != null)
                {
                    var tokenString = GenerateJwtToken(user.Username, user.Role);
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
        /// <returns></returns>
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

        private string GenerateJwtToken(string username, string role)
        {
            try
            {
                return _authService.GenerateToken(username, role, _config["Jwt:Key"]);   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
