using PDV.Application.DTOs.Cash;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CashService
    {
        /// <summary>
        /// Abre uma nova sessão de caixa para o operador atual.
        /// Permite múltiplas sessões abertas para operadores diferentes.
        /// Cada operador pode ter apenas UMA sessão aberta por vez.
        /// </summary>
        /// <param name="dto">Objeto com os dados iniciais de abertura do caixa.</param>
        /// <returns>Identificador do caixa aberto.</returns>
        /// <exception cref="Exception">Operador inválido ou já possui caixa aberto.</exception>
        public async Task<Guid> OpenCash(OpenCashSessionDTO dto)
        {
            try
            {
                var operatorName = _httpContextAcessor.HttpContext?.User?.Identity?.Name 
                    ?? throw new Exception("Operador inválido.");

                if (dto.OpeningAmount < 0)
                    throw new Exception("Valor de abertura inválido.");

                var operatorOpenSession = await _repository.GetOpenSessionByOperatorAsync(operatorName);

                if (operatorOpenSession != null)
                    throw new Exception($"Você já possui um caixa aberto desde {operatorOpenSession.OpenedAt:dd/MM/yyyy HH:mm}.");

                var session = new CashSession
                {
                    Id = Guid.NewGuid(),
                    OperatorName = operatorName,
                    OpeningAmount = dto.OpeningAmount,
                    OpenedAt = DateTime.UtcNow
                };

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
