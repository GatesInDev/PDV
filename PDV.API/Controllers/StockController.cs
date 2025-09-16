using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockByProductId(Guid id)
        {
            var stock = await _stockService.GetStockByProductId(id);
            return Ok(stock);
        }

    }
}
