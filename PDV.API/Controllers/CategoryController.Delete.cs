using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CategoryController
    {
        /// <summary>
        /// Deleta uma categoria desativando-a.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser deletada.</param>
        /// <returns>Sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Categoria deletada com sucesso.</response>
        /// <response code="400">Erro na requisição.</response>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
