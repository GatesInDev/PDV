using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Product;

namespace PDV.API.Controllers
{
    public partial class ProductController
    {
        /// <summary>
        /// Retorna todos os produtos resumidos.
        /// </summary>
        /// <returns>Lista de produtos encontrados e seus dados resumidos retornados.</returns>
        /// <response code="200">Produtos retornados com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Operador,Estoquista")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _productService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
