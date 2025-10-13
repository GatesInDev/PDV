using Microsoft.IdentityModel.Tokens;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para autenticação.
    /// </summary>
    public partial class Auth : IAuth
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
    }
}
