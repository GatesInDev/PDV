using Microsoft.EntityFrameworkCore; // Para ter acesso ao DbContext e DbSet
using PDV.Core.Entities; // Para ter acesso às entidades
using PDV.Core.Repositories; // Para ter acesso às interfaces de repositório
using PDV.Infrastructure.Data; // Para ter acesso ao AppDbContext

namespace PDV.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id) ?? null!;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _context.Products.AnyAsync(p => p.Sku == sku);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByCategory(string category)
        {
            return await _context.Products.Include(p => p.Category).Where(p => p.Category.Name == category).ToListAsync();
        }
    }
}