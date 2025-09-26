using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface para o repositório de sessões de caixa.
    /// </summary>
    public interface ICashSessionRepository
    {
        /// <summary>
        /// Adiciona uma nova sessão de caixa ao repositório.
        /// </summary>
        /// <param name="session">Objeto com as informações de abertura da sessão.</param>
        /// <returns>Sem retorno.</returns>
        Task AddAsync(CashSession session);

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id do repositório.
        /// </summary>
        /// <param name="id">Identificador da sessão a retornar.</param>
        /// <returns>Dados da sessão.</returns>
        Task<CashSession?> GetByIdAsync(Guid id);

        /// <summary>
        /// Verifica se existe uma sessão de caixa aberta.
        /// </summary>
        /// <returns>Objeto com os dados da sessão aberta.</returns>
        Task<CashSession?> GetOpenSessionAsync();

        /// <summary>
        /// Atualiza os dados de uma sessão de caixa no repositório.
        /// </summary>
        /// <param name="session">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        Task UpdateAsync(CashSession session);
    }
}
