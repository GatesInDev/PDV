using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface ICashSessionRepository
    {
        Task AddAsync(CashSession session);
        Task<CashSession?> GetByIdAsync(Guid id);
        Task<CashSession?> GetOpenSessionAsync();
        Task UpdateAsync(CashSession session);
    }
}
