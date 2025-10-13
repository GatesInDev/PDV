using PDV.Core.Entities;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class StockTransactionService
    {
        /// <summary>
        /// Retorna uma transação de estoque pelo ID.
        /// </summary>
        /// <param name="id">Identificador da Transação.</param>
        /// <returns>Objeto com os dados da transação especifica.</returns>
        /// <exception cref="Exception">Transação não encontrada.</exception>
        public async Task<StockTransaction> GetById(Guid id)
        {
            try
            {
                var stock = await _stockTransactionRepository.GetById(id);

                if (stock == null)
                {
                    throw new StockTransactionNotFoundException(id);
                }

                return _mapper.Map<StockTransaction>(stock);
            }
            catch (StockTransactionNotFoundException)
            {
                throw new StockTransactionNotFoundException(id);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao obter a transação de estoque do banco de dados.", ex);
            }
        }
    }
}
