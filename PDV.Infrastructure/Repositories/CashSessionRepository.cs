using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    public class CashSessionRepository : ICashSessionRepository
    {
        private readonly AppDbContext _context;

        public CashSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CashSession session)
        {
            await _context.Set<CashSession>().AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task<CashSession?> GetByIdAsync(Guid id)
        {
            return await _context.Set<CashSession>()
                                 .Include(c => c.Sales)
                                 .ThenInclude(sp => sp.SaleProducts)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CashSession?> GetOpenSessionAsync()
        {
            return await _context.Set<CashSession>()
                                 .FirstOrDefaultAsync(c => c.ClosedAt == null);
        }

        public async Task UpdateAsync(CashSession session)
        {
            _context.Set<CashSession>().Update(session);
            await _context.SaveChangesAsync();
        }
    }
}