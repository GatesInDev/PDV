using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PDV.Application.Services.Implementations
{
    public partial class Auth
    {
        /// <summary>
        /// Serviço para gerar o Token JWT.
        /// </summary>
        /// <param name="username">Username para qual será destinado o token.</param>
        /// <param name="role">Cargo deste usuario que o token representará.</param>
        /// <param name="genKey">Chave de codificação do sistema para a chave.</param>
        /// <returns>String com o token.</returns>
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
