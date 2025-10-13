using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class StockController
    {
        /// <summary>
        /// Retorna o estoque de um produto pelo ID do produto.
        /// </summary>
        /// <param name="id">Identificador do produto.</param>
        /// <returns>Estoque do produto em especifico retornado com sucesso.</returns>
        [Authorize(Roles = "Administrador,Operador,Estoquista")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockByProductId(Guid id)
        {
            return Ok(await _stockService.GetByProductId(id));
        }
    }
}
