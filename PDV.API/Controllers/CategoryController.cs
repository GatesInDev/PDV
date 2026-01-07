using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces; 

namespace PDV.API.Controllers
{
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
