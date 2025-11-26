using PDV.Application.DTOs.Customer;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        /// <summary>
        /// Retorna um cliente pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do Cliente.</param>
        /// <returns>Objeto com os dados do cliente.</returns>
        public async Task<List<CustomerDTO>> GetByNameAsync(string name)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByNameAsync(name);

                if (customer == null)
                    throw new Exception("Cliente não encontrado.");

                var map = _mapper.Map<List<CustomerDTO>>(customer);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter cliente: {ex.Message}");
            }
        }
    }
}
