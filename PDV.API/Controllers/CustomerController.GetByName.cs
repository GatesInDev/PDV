using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CustomerController
    {
        /// <summary>
        /// Obtém clientes pelo nome.
        /// </summary>
        /// <param name="name">Nome do cliente a ser pesquisado.</param>
        /// <returns>Lista de clientes que correspondem ao nome fornecido.</returns>
        [HttpGet("GetByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            var customers = await _customerService.GetByNameAsync(name);
            if (customers == null || !customers.Any())
            {
                return NotFound();
            }
            return Ok(customers);
        }
    }
}
