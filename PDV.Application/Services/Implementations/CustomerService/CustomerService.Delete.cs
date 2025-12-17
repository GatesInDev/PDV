using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        public async Task Delete(Guid id)
        {
            try
            {
                var entity = await _customerRepository.GetCustomerAsync(id);
                if (entity == null)
                    throw new Exception("Cliente não encontrado para exclusão.");
                entity.IsActive = false;
                await _customerRepository.UpdateCustomerAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o cliente: {ex.Message}");
            }
        }
    }
}
