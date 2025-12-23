using PDV.Application.DTOs;
using PDV.Application.DTOs.Auth;
using PDV.Application.DTOs.User;
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
        /// <param name="key"></param>
        /// <returns>Objeto com o id e o cargo deste usuario.</returns>
        Task<LoginRespondeDTO> AuthenticateUser(LoginModel user, string key);

        /// <summary>
        /// Serviço para gerar o Token JWT.
        /// </summary>
        /// <param name="username">Username para qual será destinado o token.</param>
        /// <param name="role">Cargo deste usuario que o token representará.</param>
        /// <param name="genKey">Chave de codificação do sistema para a chave.</param>
        /// <returns>String com o token.</returns>
        string GenerateToken(string username, string role, string genKey);

        /// <summary>
        /// Retorna todos os usuarios do sistema. Somente Administrador.
        /// </summary>
        /// <returns>Lista com todos os usuarios.</returns>
        Task<List<User>> GetAllUserAsync();

        /// <summary>
        /// Retorna um usuário pelo seu ID. Somente Administrador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Objeto com os dados do usuário.</returns>
        Task<UserDetailsDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Cria um novo usuário. Somente Administrador.
        /// </summary>
        /// <param name="createUserDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do usuário criado.</returns>
        Task<Guid> CreateAsync(CreateUserDTO createUserDto);

        /// <summary>
        /// Atualiza um usuário existente. Somente Administrador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="updateUserDto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task UpdateAsync(Guid id, UpdateUserDTO updateUserDto);

        /// <summary>
        /// Deleta um usuário. Somente Administrador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task DeleteAsync(Guid id);
    }
}
