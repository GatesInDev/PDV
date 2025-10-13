using PDV.Application.DTOs.StockTransaction;
using PDV.Core.Entities;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class StockTransactionService
    {
        /// <summary>
        /// Cria uma nova transação de estoque.
        /// </summary>
        /// <param name="stock">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="ArgumentNullException">Objeto invalido.</exception>
        /// <exception cref="Exception">Erro genêrico.</exception>
        public async Task<Guid> Create(CreateStockTransactionDTO stock)
        {
            try
            {
                if (stock == null)
                {
                    throw new ArgumentNullException(nameof(stock));
                }

                var stockTransaction = _mapper.Map<StockTransaction>(stock);
                stockTransaction.Id = Guid.NewGuid();
                stockTransaction.LastUpdated = DateTime.UtcNow;

                await _stockTransactionRepository.CreateTransaction(stockTransaction);
                return stockTransaction.Id;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Dados inválidos fornecidos para criar a transação de estoque.", ex);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao criar a transação de estoque.", ex);
            }
        }
    }
}
