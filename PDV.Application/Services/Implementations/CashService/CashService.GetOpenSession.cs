using Microsoft.AspNetCore.Http;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CashService
    {
        /// <summary>
        /// Obtém a sessão de caixa aberta do operador atual.
        /// </summary>
        /// <returns>Sessão aberta do operador, ou null se não houver.</returns>
        public async Task<CashSession?> GetOpenSessionAsync()
        {
            if (_httpContextAcessor?.HttpContext?.User?.Identity?.Name == null)
            {
                return null;
            }

            var operatorName = _httpContextAcessor.HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(operatorName))
                throw new Exception("Operador inválido.");

            return await _repository.GetOpenSessionByOperatorAsync(operatorName);
        }
    }
}
