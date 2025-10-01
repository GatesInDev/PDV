using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas ao estoque.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        /// <summary>
        /// Controlador de estoque.
        /// </summary>
        /// <param name="stockService">Serviço de estoque.</param>
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// Retorna o estoque de um produto pelo ID do produto.
        /// </summary>
        /// <param name="id">Identificador do produto.</param>
        /// <returns>Estoque do produto em especifico retornado com sucesso.</returns>
        [Authorize(Roles = "Administrador,Operador,Estoquista")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockByProductId(Guid id)
        {
            var stock = await _stockService.GetStockByProductId(id);
            return Ok(stock);
        }
    }
}
