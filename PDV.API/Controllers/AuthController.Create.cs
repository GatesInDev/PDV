using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.User;

namespace PDV.API.Controllers
{
    public partial class AuthController
    {
        /// <summary>
        /// Cria um novo usuário. Apenas acessível por administradores.
        /// </summary>
        /// <param name="createUserDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos ou usuário já existe.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDto)
        {
            try
            {
                var userId = await _authService.CreateAsync(createUserDto);
                return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}