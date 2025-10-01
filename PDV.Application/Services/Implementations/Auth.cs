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
    public class Auth : IAuth
    {
        private readonly IAuthRepository _authRepository;

        public Auth(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        public User AuthenticateUser(LoginModel user)
        {

            if (_authRepository.ThisUserExist(user.Username, user.Password))
            {
                var role = _authRepository.getRoleByUser(user.Username) ?? throw new Exception("Cargo inválido.");

                return new User { Id = Guid.NewGuid(), 
                    Username = user.Username, 
                    Role = role 
                };
            }
            return null;
        }

        public string GenerateToken(string username, string role, string genKey)
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

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _authRepository.GetAllUserAsync() ?? throw new Exception();
        }
    }
}
