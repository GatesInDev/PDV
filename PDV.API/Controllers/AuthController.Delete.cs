using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class AuthController
    {
        /// <summary>
        /// Deleta um usuário. Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">Identificador do usuário a ser deletado.</param>
        /// <returns>Resultado da operação.</returns>
        /// <response code="204">Usuário deletado com sucesso.</response>
        /// <response code="400">Erro na requisição ou usuário não encontrado.</response>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _authService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}