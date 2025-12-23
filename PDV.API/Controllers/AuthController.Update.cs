using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.User;

namespace PDV.API.Controllers
{
    public partial class AuthController
    {
        /// <summary>
        /// Atualiza um usuário existente. Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="updateUserDto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Usuário atualizado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos ou usuário não encontrado.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDTO updateUserDto)
        {
            try
            {
                await _authService.UpdateAsync(id, updateUserDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}