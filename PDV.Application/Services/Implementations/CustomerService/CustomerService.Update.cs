using PDV.Application.DTOs.Customer;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        public async Task Update(Guid id, UpdateCustomerDTO dto)
        {
            try
            {
                var entity = await _customerRepository.GetCustomerAsync(id);
                
                if (entity == null)
                    throw new Exception("Cliente não encontrado.");

                // ✅ Atualiza apenas os campos permitidos
                entity.Name = dto.Name;
                entity.Email = dto.Email;
                entity.Age = dto.Age;
                entity.Address = dto.Address;

                await _customerRepository.UpdateCustomerAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o cliente: " + ex.Message);
            }
        }
    }
}
