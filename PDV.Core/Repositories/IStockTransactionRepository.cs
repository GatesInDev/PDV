using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface para o repositório de transações de estoque.
    /// </summary>
    public interface IStockTransactionRepository
    {
        /// <summary>
        /// Retorna todas as transações de estoque do banco de dados.
        /// </summary>
        /// <returns>Lista com todas as transações.</returns>
        Task<List<StockTransaction>> GetAllStockTransaction();

        /// <summary>
        /// Retorna uma transação de estoque pelo ID do banco de dados.
        /// </summary>
        /// <param name="id">Identificador da transação a ser encontrada</param>
        /// <returns>Objeto com os dados da transação.</returns>
        Task<StockTransaction> GetById(Guid id);

        /// <summary>
        /// Cria uma nova transação de estoque no banco de dados.
        /// </summary>
        /// <param name="stockTransaction">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        Task CreateTransaction(StockTransaction stockTransaction);
    }
}
