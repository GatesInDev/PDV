using AutoMapper;
using PDV.Application.DTOs.Cash;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public class CashService : ICashService
    {
        private readonly ICashSessionRepository _repository;
        private readonly IMapper _mapper;

        public CashService(ICashSessionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> OpenCash(OpenCashSessionDTO dto)
        {
            // Não permitir abrir outro se já existe aberto
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

        public async Task<CashSession?> GetCashById(Guid id)
        {

            return await _repository.GetByIdAsync(id);
        }
    }
}
