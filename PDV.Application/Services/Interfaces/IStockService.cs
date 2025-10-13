using PDV.Application.DTOs.Stock;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de estoque.
    /// </summary>
    public interface IStockService
    {
        /// <summary>
        /// Retorna o estoque pelo ID do produto.
        /// </summary>
        /// <param name="productId">Identificador do produto para encontrar seu estoque.</param>
        /// <returns>Objeto com os dados do estoque.</returns>
        Task<StockDTO> GetByProductId(Guid productId);

        /// <summary>
        /// Atualiza o estoque.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        Task Update(UpdateStockDTO dto);

        /// <summary>
        /// Cria um novo estoque.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        Task Create(CreateStockDTO dto);

        /// <summary>
        /// Verifica se o estoque existe para um determinado produto.
        /// </summary>
        /// <param name="productId">Identificador do produto que sera válidado a existencia de estoque.</param>
        /// <returns>True/False</returns>
        Task<bool> StockExists(Guid productId);
    }
}
