using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CashController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOpenSession()
        {
            var openCashSession = await _cashService.GetOpenSessionAsync();
            return Ok(openCashSession);
        }
    }
}
