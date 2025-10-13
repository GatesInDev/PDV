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
    public partial class StockController : ControllerBase
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
    }
}
