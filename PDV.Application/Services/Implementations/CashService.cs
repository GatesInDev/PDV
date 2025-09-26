using AutoMapper;
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

        /// <summary>
        /// Construtor do serviço de caixa.
        /// </summary>
        /// <param name="repository">Repositório da sessão de caixa.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        public CashService(ICashSessionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Abre uma nova sessão de caixa.
        /// </summary>
        /// <param name="dto">Objeto com os dados iniciais de abertura do caixa.</param>
        /// <returns>Identificador do caixa.</returns>
        /// <exception cref="Exception">Já existe um caixa aberto.</exception>
        public async Task<Guid> OpenCash(OpenCashSessionDTO dto)
        {
            var existing = await _repository.GetOpenSessionAsync();
            if (existing != null)
                throw new Exception("Já existe um caixa aberto.");

            var session = new CashSession
            {
                Id = Guid.NewGuid(),
                OperatorName = dto.OperatorName,
                OpeningAmount = dto.OpeningAmount,
                OpenedAt = DateTime.Now
            };

            await _repository.AddAsync(session);
            return session.Id;
        }

        /// <summary>
        /// Fecha a sessão de caixa atual.
        /// </summary>
        /// <param name="dto">Objeto com os dados restantes para fechar o caixa.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="Exception">Erro ao fechar o caixa.</exception>
        public async Task CloseCash(CloseCashSessionDTO dto)
        {
            var session = await _repository.GetByIdAsync(dto.Id);
            if (session == null)
                throw new Exception("Caixa não encontrado.");
            if (session.ClosedAt != null)
                throw new Exception("Caixa já foi fechado.");

            session.ClosingAmount = dto.ClosingAmount;
            session.ClosedAt = DateTime.Now;

            await _repository.UpdateAsync(session);
        }

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador do caixa.</param>
        /// <returns>Objeto com os dados do caixa.</returns>
        public async Task<CashSession?> GetCashById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
