using PDV.Core.Repositories;
using PDV.Infrastructure.Data;
using PDV.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PDV.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetByProductIdAsync(Guid productId)
        {
            return await _context.Stocks
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ProductId == productId) ?? null!;
        }

        public async Task CreateAsync(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> StockExistsAsync(Guid productId)
        {
              return await _context.Stocks.AnyAsync(s => s.ProductId == productId);
        }
    }
}
