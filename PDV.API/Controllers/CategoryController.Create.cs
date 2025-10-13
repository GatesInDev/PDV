using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Category;

namespace PDV.API.Controllers
{
    public partial class CategoryController
    {
        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="dto">Objeto com os dados da categoria a ser criada.</param>
        /// <returns>Categoria criada com os detalhes completos.</returns>
        /// <response code="201">Categoria criada com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDetailsDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                var id = await _categoryService.Create(dto);
                var category = await _categoryService.GetById(id);

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
