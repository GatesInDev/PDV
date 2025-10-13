using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Product;

namespace PDV.API.Controllers
{
    public partial class ProductController
    {
        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="dto">Objeto com os dados completos do produto a ser criado.</param>
        /// <returns>Produto novo criado com todos seus dados completos.</returns>
        /// <response code="201">Produto criado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPost]
        [ProducesResponseType(typeof(ProductDetailsDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO dto)
        {
            try
            {
                var productId = await _productService.Create(dto);
                var product = await _productService.GetById(productId);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
