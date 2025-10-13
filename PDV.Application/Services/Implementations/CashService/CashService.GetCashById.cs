using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CashService
    {
        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do caixa.</param>
        /// <returns>Objeto com os dados do caixa.</returns>
        public async Task<CashSession?> GetCashById(Guid id)
        {
            try
            {
                var session = await _repository.GetByIdAsync(id);

                if (session == null)
                    throw new Exception("Caixa não encontrado.");

                return session;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o caixa: " + ex.Message);
            }
        }
    }
}
