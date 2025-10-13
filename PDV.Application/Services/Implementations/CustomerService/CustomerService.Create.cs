using PDV.Application.DTOs.Customer;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="createCustomerDto">Objeto com os dados que serão criados.</param>
        /// <returns>Identificador do Cliente.</returns>
        public async Task<Guid> Create(CreateCustomerDTO createCustomerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(createCustomerDto);

                customer.Id = Guid.NewGuid();
                customer.CreatedAt = DateTime.UtcNow;

                if (char.IsLower(customer.Name[0]))
                    throw new Exception("O nome do cliente deve começar com letra maiúscula.");

                if (string.IsNullOrWhiteSpace(customer.Name) || customer.Name.Length < 3)
                    throw new Exception("O nome do cliente deve ter pelo menos 3 caracteres.");

                await _customerRepository.CreateCustomerAsync(customer);

                return customer.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar cliente: {ex.Message}");
            }
        }
    }
}
