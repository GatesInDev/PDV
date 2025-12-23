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

        /// <summary>
        /// Recupera um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Objeto <see cref="User"/> se encontrado; caso contrário, nulo.</returns>
        Task<User> GetByIdAsync(Guid id);

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">Objeto <see cref="User"/> com os dados a serem criados.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task CreateAsync(User user);

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="user">Objeto <see cref="User"/> com os dados atualizados.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// Deleta um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Verifica se um usuário com o username específico já existe.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <returns>Retorna <c>true</c> se o usuário existir; caso contrário, <c>false</c>.</returns>
        Task<bool> UsernameExistsAsync(string username);
    }
}
