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
    public class ProductController : ControllerBase
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
                var product = await _productService.GetByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

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
                var productId = _productService.CreateAsync(dto);
                var product = await _productService.GetByIdAsync(await productId);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">Identificador do produto a ser encontrado.</param>
        /// <param name="dto">Objeto com os dados para substituir na categoria existente.</param>
        /// <returns>Produto encontrado e seus dados substituidos.</returns>
        /// <response code="200">Produto atualizado com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(ProductDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDTO dto)
        {
            try
            {
                var productId = await _productService.UpdateAsync(id, dto);
                var product = await _productService.GetByIdAsync(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

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
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna todos os produtos pertencentes aquela categoria.
        /// </summary>
        /// <returns>Lista de produtos encontrados e seus dados resumidos retornados.</returns>
        /// <response code="200">Produtos retornados com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Operador,Estoquista")]
        [HttpGet("Categories/{category}")]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFromCategory([FromRoute] string category)
        {
            try
            {
                var products = await _productService.GetByCategoryAsync(category);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Desabilita um produto.
        /// </summary>
        /// <param name="id">Identificador do produto a ser desabilitado.</param>
        /// <returns>Sucesso ao desabilitar um produto.</returns>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableProduct([FromRoute] Guid id)
        {
            try
            {
                var result = await _productService.DisableProductAsync(id);
                if (result)
                    return Ok(await GetById(id));
                else
                    return BadRequest("Erro ao excluir");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
