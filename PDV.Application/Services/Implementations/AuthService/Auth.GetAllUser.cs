using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {

        /// <summary>
        /// Retorna todos os usuarios do sistema. Somente Administrador.
        /// </summary>
        /// <returns>Lista com todos os usuarios.</returns>
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _authRepository.GetAllUserAsync() ?? throw new Exception();
        }
    }
}
