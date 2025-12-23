using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Category;

namespace PDV.API.Controllers
{
    public partial class CategoryController : ControllerBase
    {
        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser atualizada.</param>
        /// <param name="dto">Objeto com os dados para substituir na categoria existente.</param>
        /// <returns>Categoria atualizada com os detalhes completos.</returns>
        /// <response code="200">Categoria atualizada com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDTO dto)
        {
            try
            {
                var categoryId = await _categoryService.Update(id, dto);
                var category = await _categoryService.GetById(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
