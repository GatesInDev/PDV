using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.User;

namespace PDV.API.Controllers
{
    public partial class AuthController
    {
        /// <summary>
        /// Retorna um usuário pelo seu ID. Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">Identificador do usuário a ser encontrado.</param>
        /// <returns>Usuário encontrado e seus dados completos retornados.</returns>
        /// <response code="200">Usuário recuperado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _authService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}