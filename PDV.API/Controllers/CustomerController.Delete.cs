using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class CustomerController
    {
        /// <summary>
        /// Deletes a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerService.Delete(id);
            return NoContent();
        }
    }
}
