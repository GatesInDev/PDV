using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Product;

namespace PDV.API.Controllers
{
    public partial class ProductController : ControllerBase
    {
        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">Identificador do produto a ser encontrado.</param>
        /// <param name="dto">Objeto com os dados para substituir na categoria existente.</param>
        /// <returns>Produto encontrado e seus dados substituidos.</returns>
        /// <response code="200">Produto atualizado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(ProductDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDTO dto)
        {
            try
            {
                var productId = await _productService.Update(id, dto);
                var product = await _productService.GetById(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
