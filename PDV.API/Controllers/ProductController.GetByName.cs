using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class ProductController
    {
        /// <summary>
        /// Obtém uma lista de produtos pelo nome.
        /// </summary>
        /// <param name="name">Nome do produto a ser pesquisado.</param>
        /// <returns>Lista de produtos que correspondem ao nome fornecido.</returns>
        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var products = await _productService.GetProductsByNameAsync(name);
            return Ok(products);
        }
    }
}
