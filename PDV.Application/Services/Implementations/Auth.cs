using PDV.Application.DTOs;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para autenticação.
    /// </summary>
    public class Auth : IAuth
    {
        private readonly IAuthRepository _authRepository;

        /// <summary>
        /// Construtor para a autenticação.
        /// </summary>
        /// <param name="authRepository">Repositório da autenticação.</param>
        public Auth(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Serviço para validar a autenticação.
        /// </summary>
        /// <param name="user">Objeto com usuario e senha.</param>
        /// <returns>Objeto com o id e o cargo deste usuario.</returns>
        /// <exception cref="Exception">Usuario sem cargo válido.</exception>
        public async Task<User> AuthenticateUser(LoginModel user)
        {

            if ( await _authRepository.ThisUserExist(user.Username, user.Password))
            {
                var role = await _authRepository.getRoleByUser(user.Username) ?? throw new Exception("Cargo inválido.");

                return new User { Id = Guid.NewGuid(), 
                    Username = user.Username, 
                    Role = role 
                };
            }
            return null;
        }

        /// <summary>
        /// Serviço para gerar o Token JWT.
        /// </summary>
        /// <param name="username">Username para qual será destinado o token.</param>
        /// <param name="role">Cargo deste usuario que o token representará.</param>
        /// <param name="genKey">Chave de codificação do sistema para a chave.</param>
        /// <returns>String com o token.</returns>
        public async Task<string> GenerateToken(string username, string role, string genKey)
        {
            var key = Encoding.UTF8.GetBytes(genKey);

            var roleClaim = role switch
            {
                "Administrador" => "Administrador",
                "Operador" => "Operador",
                "Gerente" => "Gerente",
                "Estoquista" => "Estoquista",
                _ => "Visitante"
            };

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, roleClaim)
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            var code = new JwtSecurityTokenHandler().WriteToken(token);

            return $"Bearer {code}";
        }

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
