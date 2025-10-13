using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Sales;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;


namespace PDV.API.Controllers
{
    /// <summary>
    /// Requisições relacionadas a vendas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class SalesController : ControllerBase
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
    }
}
