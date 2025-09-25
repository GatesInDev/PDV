using PDV.Application.DTOs.StockTransaction;
using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    public interface IStockTransactionService
    {
        /// <summary>
        /// Retorna todas as transações.
        /// </summary>
        /// <returns>Uma lista resumida com todas as transações.</returns>
        Task<List<StockTransactionDTO>> GetAllStockTransaction();

        /// <summary>
        /// Retorna uma transação pelo ID.
        /// </summary>
        /// <param name="id">Identificador da transação.</param>
        /// <returns></returns>
        Task<StockTransaction> GetStockTransactionById(Guid id);

        /// <summary>
        /// Cria uma nova transação.
        /// </summary>
        /// <param name="stockTransaction">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador da Transação Criada.</returns>
        Task<Guid> CreateTransaction(CreateStockTransactionDTO stockTransaction);
    }
}
