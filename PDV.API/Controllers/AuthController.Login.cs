using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs;

namespace PDV.API.Controllers
{
    public partial class AuthController
    {
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
                return Ok(await _authService.AuthenticateUser(login, _config["Jwt:Key"] ?? throw new Exception("Chave inválida.")));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
