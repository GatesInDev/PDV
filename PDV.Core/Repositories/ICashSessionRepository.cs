using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface responsável pelas operações de persistência relacionadas às sessões de caixa.
    /// </summary>
    public interface ICashSessionRepository
    {
        /// <summary>
        /// Insere uma nova sessão de caixa no repositório.
        /// </summary>
        /// <param name="session">Objeto <see cref="CashSession"/> contendo os dados da sessão a ser adicionada.</param>
        /// <returns>Uma tarefa assíncrona que representa a operação.</returns>
        Task AddAsync(CashSession session);

        /// <summary>
        /// Busca uma sessão de caixa pelo seu identificador único.
        /// </summary>
        /// <param name="id">Identificador <see cref="Guid"/> da sessão.</param>
        /// <returns>Objeto <see cref="CashSession"/> correspondente, ou <c>null</c> se não encontrado.</returns>
        Task<CashSession?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtém a sessão de caixa atualmente aberta, se houver.
        /// </summary>
        /// <returns>Objeto <see cref="CashSession"/> da sessão aberta, ou <c>null</c> se nenhuma estiver aberta.</returns>
        Task<CashSession?> GetOpenSessionAsync();

        /// <summary>
        /// Atualiza os dados de uma sessão de caixa existente.
        /// </summary>
        /// <param name="session">Objeto <see cref="CashSession"/> com os dados atualizados.</param>
        /// <returns>Uma tarefa assíncrona que representa a operação.</returns>
        Task UpdateAsync(CashSession session);

        /// <summary>
        /// Calcula o valor total movimentado em uma sessão de caixa específica.
        /// </summary>
        /// <param name="id">Identificador <see cref="Guid"/> da sessão.</param>
        /// <returns>Valor total movimentado na sessão.</returns>
        Task<decimal> SumOfCashSession(Guid id);
    }
}
