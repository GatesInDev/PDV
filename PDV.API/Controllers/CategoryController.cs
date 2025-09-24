using PDV.Application.DTOs.Category; // Para ter acesso as DTOs de Category
using PDV.Application.Services.Interfaces; // Para ter acesso a interface ICategoryService

using Microsoft.AspNetCore.Mvc; // Para ter acesso aos atributos de Controller

namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas a categoria.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Controlador de categorias.
        /// </summary>
        /// <param name="categoryService">Serviço de categorias.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retorna uma Categoria pelo ID.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser encontrada.</param>
        /// <returns>Categoria encontrada e seus dados completos retornados.</returns>
        /// <response code="200">Categoria recuperado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="dto">Objeto com os dados da categoria a ser criada.</param>
        /// <returns>Categoria criada com os detalhes completos.</returns>
        /// <response code="201">Categoria criada com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDetailsDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                var id = await _categoryService.CreateAsync(dto);
                var category = await _categoryService.GetByIdAsync(id);

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser atualizada.</param>
        /// <param name="dto">Objeto com os dados para substituir na categoria existente.</param>
        /// <returns>Categoria atualizada com os detalhes completos.</returns>
        /// <response code="200">Categoria atualizada com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(CategoryDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDTO dto)
        {
            try
            {
                var categoryId = await _categoryService.UpdateAsync(id, dto);
                var category = await _categoryService.GetByIdAsync(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna todas as categorias simplificadas.
        /// </summary>
        /// <returns>Categorias resumidas retornadas com sucesso.</returns>
        /// <response code="200">Categorias recuperadas com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
