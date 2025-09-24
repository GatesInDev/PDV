using PDV.Core.Repositories;
using PDV.Infrastructure.Data;
using PDV.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PDV.Infrastructure.Repositories
{
    public class StockTransactionRepository : IStockTransactionRepository
    {
        private readonly AppDbContext _context;

        public StockTransactionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<StockTransaction>> GetAllStockTransaction()
        {
            return await _context.StockTransactions.ToListAsync();
        }

        public async Task<StockTransaction> GetById(Guid id)
        {
            return await _context.StockTransactions.FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task CreateTransaction(StockTransaction stockTransaction)
        {
            await _context.StockTransactions.AddAsync(stockTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
