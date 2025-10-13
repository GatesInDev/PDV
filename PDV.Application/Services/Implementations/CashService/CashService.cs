using AutoMapper;
using Microsoft.AspNetCore.Http;
using PDV.Application.DTOs.Cash;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações relacionadas ao caixa.
    /// </summary>
    public partial class CashService : ICashService
    {
        private readonly ICashSessionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAcessor;

        /// <summary>
        /// Construtor do serviço de caixa.
        /// </summary>
        /// <param name="repository">Repositório da sessão de caixa.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        /// <param name="contextAccessor">Acesso aos dados do Jwt Bearer.</param>
        public CashService(ICashSessionRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAcessor = contextAccessor;
        }
    }
}
