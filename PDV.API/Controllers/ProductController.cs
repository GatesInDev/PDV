using PDV.Application.Services.Interfaces; // Para ter acesso a interface IProductService
using PDV.Application.DTOs.Product; // Para ter acesso as DTOs de Product

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas a produtos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Controlador de produtos.
        /// </summary>
        /// <param name="productService">Serviço de produtos.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    }
}
