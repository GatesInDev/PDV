using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.DTOs.Customer;

namespace PDV.API.Controllers
{
    public partial class CustomerController
    {
        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <param name="dto">Dados do cliente a serem atualizados.</param>
        /// <returns>Sem conteúdo se bem-sucedido.</returns>
        [Authorize(Roles = "Administrador,Operador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDTO dto)
        {
            try
            {
                await _customerService.Update(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
