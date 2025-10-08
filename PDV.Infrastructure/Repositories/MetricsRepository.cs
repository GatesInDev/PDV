using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        private readonly AppDbContext _context;

        public MetricsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetBestSellersAsync(int items)
        {
            return await _context.Products
                .OrderByDescending(p => p.SaledQuantity)
                .Take(items)
                .ToListAsync();
        }
    }
}
