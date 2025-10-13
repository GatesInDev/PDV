using PDV.Application.DTOs.Customer;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do Cliente.</param>
        /// <returns>Objeto com os dados do cliente.</returns>
        public async Task<CustomerDTO> GetById(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerAsync(id);

                if (customer == null)
                    throw new Exception("Cliente não encontrado.");

                var map = _mapper.Map<CustomerDTO>(customer);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cliente: {ex.Message}");
            }
        }
    }
}
