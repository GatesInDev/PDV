using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.StockTransaction;

namespace PDV.API.Controllers
{
    public partial class StockTransactionController
    {
        /// <summary>
        /// Registra uma nova transação de estoque.
        /// </summary>
        /// <param name="stock">Objeto com os dados para criar a transação</param>
        /// <returns>Sucesso com os dados criados.</returns>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockTransactionDTO stock)
        {
            try
            {
                var id = await _stockTransactionService.Create(stock);
                var transaction = await _stockTransactionService.GetById(id);
                return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
