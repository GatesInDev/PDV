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
    public partial class StockTransactionController : ControllerBase
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
    }
}
