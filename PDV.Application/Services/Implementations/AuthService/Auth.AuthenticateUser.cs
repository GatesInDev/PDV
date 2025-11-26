using PDV.Application.DTOs;
using PDV.Application.DTOs.Auth;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {
        /// <summary>
        /// Serviço para validar a autenticação.
        /// </summary>
        /// <param name="user">Objeto com usuario e senha.</param>
        /// <param name="key"></param>
        /// <returns>Objeto com o id e o cargo deste usuario.</returns>
        /// <exception cref="Exception">Usuario sem cargo válido.</exception>
        public async Task<LoginRespondeDTO> AuthenticateUser(LoginModel user, string key)
        {
            if (await _authRepository.ThisUserExist(user.Username, user.Password))
            {
                var role = await _authRepository.getRoleByUser(user.Username) ?? throw new Exception("Cargo inválido.");

                var data = new User
                {
                    Id = Guid.NewGuid(),
                    Username = user.Username,
                    Role = role
                };

                if (data != null)
                {
                    var tokenString = GenerateToken(data.Username, data.Role, key);

                    var json = new LoginRespondeDTO()
                    {
                        Token = tokenString,
                        Username = user.Username,
                        Role = role
                    };

                    return json;
                }
            }

            throw new Exception("Erro de autenticação.");
        }
    }
}
