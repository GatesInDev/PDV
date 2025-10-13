using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Product;

namespace PDV.API.Controllers
{
    public partial class ProductController
    {
        /// <summary>
        /// Retorna um produto pelo ID.
        /// </summary>
        /// <param name="id">Identificador do produto a ser encontrado.</param>
        /// <returns>Produto encontrado e seus dados completos retornados.</returns>
        /// <response code="200">Produto recuperado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _productService.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
