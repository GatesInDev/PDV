using PDV.Application.DTOs.Cash;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CashService
    {
        /// <summary>
        /// Abre uma nova sessão de caixa.
        /// </summary>
        /// <param name="dto">Objeto com os dados iniciais de abertura do caixa.</param>
        /// <returns>Identificador do caixa.</returns>
        /// <exception cref="Exception">Já existe um caixa aberto.</exception>
        public async Task<Guid> OpenCash(OpenCashSessionDTO dto)
        {
            try
            {

                if (await _repository.GetOpenSessionAsync() != null)
                    throw new Exception("Já existe um caixa aberto.");

                var session = new CashSession
                {
                    Id = Guid.NewGuid(),
                    OperatorName = _httpContextAcessor.HttpContext?.User?.Identity?.Name ?? throw new Exception("Operador Inválido."),
                    OpeningAmount = dto.OpeningAmount,
                    OpenedAt = DateTime.Now
                };

                if (session.OpeningAmount < 0)
                    throw new Exception("Valor de abertura inválido.");

                await _repository.AddAsync(session);
                return session.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o caixa: " + ex.Message);
            }
        }
    }
}
