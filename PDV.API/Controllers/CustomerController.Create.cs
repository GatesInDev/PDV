using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Customer;

namespace PDV.API.Controllers
{
    public partial class CustomerController
    {
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="createCustomerDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do Cliente criado.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO createCustomerDto)
        {
            try
            {
                var customer = await _customerService.Create(createCustomerDto);
                return CreatedAtAction(nameof(GetById), new { id = customer }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
