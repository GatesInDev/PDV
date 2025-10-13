using PDV.Application.DTOs.Customer;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        /// <returns>Uma lista com todos os clientes.</returns>
        public async Task<List<CustomerDTO>> GetAll()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomersAsync();

                if (customers == null)
                    throw new Exception("Nenhum cliente encontrado.");

                var map = _mapper.Map<List<CustomerDTO>>(customers);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter clientes: {ex.Message}");
            }
        }
    }
}
