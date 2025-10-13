using PDV.Application.DTOs.Customer;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class CustomerService
    {
        /// <summary>
        /// Retorna todas as vendas de um cliente específico.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Uma lisat com todas as vendas do cliente.</returns>
        public async Task<List<CustomersAndSalesDTO>> GetSalesHistoryByCostumerId(Guid id)
        {
            try
            {
                var sales = await _customerRepository.GetSaleByCostumerAsync(id);

                if (sales == null || !sales.Any())
                    throw new Exception("Nenhuma venda encontrada para este cliente.");


                var map = _mapper.Map<List<CustomersAndSalesDTO>>(sales);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter histórico de compras: {ex.Message}");
            }
        }
    }
}
