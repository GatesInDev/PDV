using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas a transações de estoque.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionController : ControllerBase
    {
        private readonly IStockTransactionService _stockTransactionService;

        /// <summary>
        /// Controlador de transações de estoque.
        /// </summary>
        /// <param name="stockTransactionService">Serviço de transação de estoque.</param>
        public StockTransactionController(IStockTransactionService stockTransactionService)
        {
            _stockTransactionService = stockTransactionService;
        }

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
                var stock = await _stockTransactionService.GetAllStockTransaction();
                return Ok(stock);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

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
                var stock = await _stockTransactionService.GetStockTransactionById(id);
                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Registra uma nova transação de estoque.
        /// </summary>
        /// <param name="stock">Objeto com os dados para criar a transação</param>
        /// <returns>Sucesso com os dados criados.</returns>
        [Authorize(Roles = "Administrador,Estoquista")]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateStockTransactionDTO stock)
        {
            try
            {
                var local = await _stockTransactionService.CreateTransaction(stock);
                return Created(String.Empty, await GetById(local));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
