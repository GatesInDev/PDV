using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Sales;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Application.Services.Interfaces;


namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas a vendas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        /// <summary>
        /// Controlador de vendas.
        /// </summary>
        /// <param name="saleService">Serviço de vendas.</param>
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Cria uma venda, já subtraindo do estoque e criando a transação.
        /// </summary>
        /// <param name="dto">Objeto para a criação da venda.</param>
        /// <returns>Venda criada, subtraida do estoque, e registrada nas transações.</returns>
        /// <response code="200">Venda criada com sucesso.</response>
        /// <response code="400">Erro na requisição, dados inválidos.</response>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSalesDTO dto)
        {
            try
            {
                await _saleService.CreateSale(dto);
                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna uma venda pelo ID.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Os dados da venda.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            try
            {
                var sale = await _saleService.GetSaleById(id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retorna as vendas em um período.
        /// </summary>
        /// <param name="startDate">Data inicial de filtragem.</param>
        /// <param name="endDate">Data final de filtragem.</param>
        /// <returns>Uma lista com objetos dentro deste periodo.</returns>
        [HttpGet]
        public async Task<IActionResult> GetSaleByPeriod([FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            try
            {
                var sales = await _saleService.GetSalesByPeriod(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
