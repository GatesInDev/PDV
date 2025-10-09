using PDV.Application.DTOs;
using PDV.Core.Entities;
using System.Data;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de autenticação.
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// Serviço para validar a autenticação.
        /// </summary>
        /// <param name="user">Objeto com usuario e senha.</param>
        /// <returns>Objeto com o id e o cargo deste usuario.</returns>
        Task<User> AuthenticateUser(LoginModel user);

        /// <summary>
        /// Serviço para gerar o Token JWT.
        /// </summary>
        /// <param name="username">Username para qual será destinado o token.</param>
        /// <param name="role">Cargo deste usuario que o token representará.</param>
        /// <param name="genKey">Chave de codificação do sistema para a chave.</param>
        /// <returns>String com o token.</returns>
        Task<string> GenerateToken(string username, string role, string genKey);

        /// <summary>
        /// Retorna todos os usuarios do sistema. Somente Administrador.
        /// </summary>
        /// <returns>Lista com todos os usuarios.</returns>
        Task<List<User>> GetAllUserAsync();
    }
}
