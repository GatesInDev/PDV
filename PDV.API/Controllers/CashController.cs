using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para operações relacionadas ao caixa.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public partial class CashController : ControllerBase
    {
        private readonly ICashService _cashService;

        /// <summary>
        /// Construtor do controlador de caixa.
        /// </summary>
        /// <param name="cashService"></param>
        public CashController(ICashService cashService)
        {
            _cashService = cashService;
        }
    }
}
