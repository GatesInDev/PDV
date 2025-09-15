using PDV.Core.Entities; // Para ter acesso às entidades
using PDV.Core.Repositories; // Para ter acesso às interfaces de repositório
using PDV.Infrastructure.Data; // Para ter acesso ao AppDbContext

using Microsoft.EntityFrameworkCore; // Para ter acesso ao DbContext e DbSet

namespace PDV.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            if (_context.Categories.Any(c => c.Name == name))
            {
                return true;
            }

            return false;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
           return await _context.Categories.ToListAsync();
        }
    }
}

