using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class AuthController
    {
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
