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
    public class CashService : ICashService
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

        /// <summary>
        /// Abre uma nova sessão de caixa.
        /// </summary>
        /// <param name="dto">Objeto com os dados iniciais de abertura do caixa.</param>
        /// <returns>Identificador do caixa.</returns>
        /// <exception cref="Exception">Já existe um caixa aberto.</exception>
        public async Task<Guid> OpenCash(OpenCashSessionDTO dto)
        {
            try
            {
                var existing = await _repository.GetOpenSessionAsync();

                if (existing != null)
                    throw new Exception("Já existe um caixa aberto.");

                var session = new CashSession
                {
                    Id = Guid.NewGuid(),
                    OperatorName = _httpContextAcessor.HttpContext?.User?.Identity?.Name ?? throw new Exception("Operador Inválido."),
                    OpeningAmount = dto.OpeningAmount,
                    OpenedAt = DateTime.Now
                };

                if (session.OpeningAmount < 0)
                    throw new Exception("Valor de abertura inválido.");

                    await _repository.AddAsync(session);
                    return session.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o caixa: " + ex.Message);
            }

        }

        /// <summary>
        /// Fecha a sessão de caixa atual.
        /// </summary>
        /// <param name="dto">Objeto com os dados restantes para fechar o caixa.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="Exception">Erro ao fechar o caixa.</exception>
        public async Task CloseCash(CloseCashSessionDTO dto)
        {
            try
            {
                var session = await _repository.GetByIdAsync(dto.Id);

                if (session == null)
                    throw new Exception("Caixa não encontrado.");

                if (session.ClosedAt != null)
                    throw new Exception("Caixa já foi fechado.");

                session.ClosingAmount = await _repository.SumOfCashSession(dto.Id) + session.OpeningAmount;
                session.ClosedAt = DateTime.Now;

                await _repository.UpdateAsync(session);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fechar o caixa: " + ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do caixa.</param>
        /// <returns>Objeto com os dados do caixa.</returns>
        public async Task<CashSession?> GetCashById(Guid id)
        {
            try
            {
                var session = await _repository.GetByIdAsync(id);

                if (session == null)
                    throw new Exception("Caixa não encontrado.");

                return session;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o caixa: " + ex.Message);
            }
        }
    }
}
