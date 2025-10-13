using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Category;

namespace PDV.API.Controllers
{
    public partial class CategoryController
    {
        /// <summary>
        /// Retorna uma Categoria pelo ID.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser encontrada.</param>
        /// <returns>Categoria encontrada e seus dados completos retornados.</returns>
        /// <response code="200">Categoria recuperado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Operador,Estoquista")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
