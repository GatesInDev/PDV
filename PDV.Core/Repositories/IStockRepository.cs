using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IStockRepository
    {
        /// <summary>
        /// Retorna o estoque pelo ID do produto do banco de dados.
        /// </summary>
        /// <param name="productId">Identificador do produto cujo estoque será retornado.</param>
        /// <returns>Objeto com os dados do estoque.</returns>
        Task<Stock> GetByProductIdAsync(Guid productId);

        /// <summary>
        /// Atualiza o estoque no banco de dados.
        /// </summary>
        /// <param name="stock">Objeto com os dados a serem atualizados do estoque.</param>
        /// <returns>Sem retorno.</returns>
        Task UpdateAsync(Stock stock);

        /// <summary>
        /// Cria um novo estoque no banco de dados.
        /// </summary>
        /// <param name="stock">Dados do estoque a ser criado.</param>
        /// <returns>Sem retorno.</returns>
        Task CreateAsync(Stock stock);

        /// <summary>
        /// Verifica se o estoque existe para um determinado produto no banco de dados.
        /// </summary>
        /// <param name="productId">Identificador do produto a ser verificado.</param>
        /// <returns>True/False</returns>
        Task<bool> StockExistsAsync(Guid productId);
    }
}
