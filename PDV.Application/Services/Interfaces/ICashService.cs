using PDV.Application.DTOs.Cash;
using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de caixa.
    /// </summary>
    public interface ICashService
    {
        /// <summary>
        /// Abre uma nova sessão de caixa.
        /// </summary>
        /// <param name="dto">Objeto com os dados iniciais de abertura.</param>
        /// <returns>Identificador da sessão de caixa.</returns>
        Task<Guid> OpenCash(OpenCashSessionDTO dto);

        /// <summary>
        /// Fecha a sessão de caixa atual.
        /// </summary>
        /// <param name="dto">Objeto com os dados de finalização de sessão de caixa.</param>
        /// <returns>Sem retorno.</returns>
        Task CloseCash(CloseCashSessionDTO dto);

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da sessão de caixa a ser retornada.</param>
        /// <returns>Objeto com os dados da sessão de caixa/Null</returns>
        Task<CashSession?> GetCashById(Guid id);
    }
}