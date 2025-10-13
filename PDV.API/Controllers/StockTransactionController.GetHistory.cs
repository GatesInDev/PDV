using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class StockTransactionController
    {
        /// <summary>
        /// Retorna todas as transações de estoque.
        /// </summary>
        /// <returns>Sucesso ao retonar todas as transações.</returns>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                return Ok(await _stockTransactionService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
