using PDV.Application.DTOs.StockTransaction;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class StockTransactionService
    {
        /// <summary>
        /// Retorna todas as transações de estoque.
        /// </summary>
        /// <returns>Uma lista com o estoque resumido.</returns>
        /// <exception cref="Exception">Não existem estoques.</exception>
        public async Task<List<StockTransactionDTO>> GetAll()
        {
            try
            {
                var stock = await _stockTransactionRepository.GetAllStockTransaction();

                if (!stock.Any())
                {
                    throw new NoStockTransactionsException();
                }

                return _mapper.Map<List<StockTransactionDTO>>(stock);
            }
            catch (NoStockTransactionsException)
            {
                throw new NoStockTransactionsException();
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao retornar dados.", ex);
            }
        }
    }
}
