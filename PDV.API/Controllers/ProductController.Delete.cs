using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class ProductController
    {
        /// <summary>
        /// Desabilita um produto.
        /// </summary>
        /// <param name="id">Identificador do produto a ser desabilitado.</param>
        /// <returns>Sucesso ao desabilitar um produto.</returns>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _productService.Delete(id);
                if (result)
                    return Ok(await GetById(id));
                else
                    return BadRequest("Erro ao excluir");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
