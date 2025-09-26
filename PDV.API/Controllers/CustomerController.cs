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
    public class CustomerController : ControllerBase
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

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="createCustomerDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do Cliente criado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO createCustomerDto)
        {
            var customer = await _customerService.CreateCustomerAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer }, customer);
        }

        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Objeto com os dados do cliente.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Uma lista com todos os clientes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Retorna o histórico de compras de um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do Cliente.</param>
        /// <returns>Uma lista com todas suas compras.</returns>
        [HttpGet("{id}/sales")]
        public async Task<IActionResult> GetSalesHistoryById(Guid id)
        {
            var customers = await _customerService.GetSalesByCostumer(id);
            return Ok(customers);
        }
    }
}
