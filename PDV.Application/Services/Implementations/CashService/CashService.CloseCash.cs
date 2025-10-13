using PDV.Application.DTOs.Cash;

namespace PDV.Application.Services.Implementations
{
    public partial class CashService
    {
        /// <summary>
        /// Fecha a sessão de caixa atual.
        /// </summary>
        /// <param name="dto">Objeto com os dados restantes para fechar o caixa.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="Exception">Erro ao fechar o caixa.</exception>
        public async Task CloseCash(CloseCashSessionDTO dto)
        {
            try
            {
                var session = await _repository.GetByIdAsync(dto.Id);

                if (session == null)
                    throw new Exception("Caixa não encontrado.");

                if (session.ClosedAt != null)
                    throw new Exception("Caixa já foi fechado.");

                session.ClosingAmount = await _repository.SumOfCashSession(dto.Id) + session.OpeningAmount;
                session.ClosedAt = DateTime.Now;

                await _repository.UpdateAsync(session);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fechar o caixa: " + ex.Message);
            }
        }
    }
}
