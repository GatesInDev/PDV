using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class StockTransactionController
    {
        /// <summary>
        /// Retorna uma transação de estoque pelo ID.
        /// </summary>
        /// <param name="id">Identificador da transação.</param>
        /// <returns>Sucesso ao retornar a transação especificada.</returns>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _stockTransactionService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
