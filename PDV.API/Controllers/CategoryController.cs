using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // Para ter acesso aos atributos de Controller
using PDV.Application.DTOs.Category; // Para ter acesso as DTOs de Category
using PDV.Application.Services.Interfaces; // Para ter acesso a interface ICategoryService

namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas a categoria.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class CategoryController : ControllerBase
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
    }
}
