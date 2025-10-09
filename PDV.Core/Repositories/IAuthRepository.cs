using PDV.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface responsável pelas operações de autenticação e gerenciamento de usuários.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Verifica se existe um usuário com o nome de usuário e senha fornecidos.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>Retorna <c>true</c> se o usuário existir; caso contrário, <c>false</c>.</returns>
        Task<bool> ThisUserExist(string username, string password);

        /// <summary>
        /// Obtém o papel (role) associado a um determinado nome de usuário.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <returns>Retorna o papel (role) do usuário.</returns>
        Task<string> getRoleByUser(string username);

        /// <summary>
        /// Recupera todos os usuários cadastrados de forma assíncrona.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="User"/>.</returns>
        Task<List<User>> GetAllUserAsync();
    }
}
