using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Customer;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a clientes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Construtor do controlador de clientes.
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
    }
}
