using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Sales;

namespace PDV.API.Controllers
{
    public partial class SalesController
    {
        /// <summary>
        /// Cria uma venda, já subtraindo do estoque e criando a transação.
        /// </summary>
        /// <param name="dto">Objeto para a criação da venda.</param>
        /// <returns>Venda criada, subtraida do estoque, e registrada nas transações.</returns>
        /// <response code="200">Venda criada com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSalesDTO dto)
        {
            try
            {
                var sale = await _saleService.Create(dto);
                return Ok(await _saleService.GetById(sale));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
