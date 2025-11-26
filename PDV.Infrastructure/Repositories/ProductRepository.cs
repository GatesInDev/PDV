using Microsoft.EntityFrameworkCore; // Para ter acesso ao DbContext e DbSet
using PDV.Core.Entities; // Para ter acesso às entidades
using PDV.Core.Repositories; // Para ter acesso às interfaces de repositório
using PDV.Infrastructure.Data; // Para ter acesso ao AppDbContext

namespace PDV.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas a produtos.
    /// </summary>
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de produtos.
        /// </summary>
        /// <param name="context">Conexão com o banco de dados.</param>
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna um produto pelo ID do banco de dados.
        /// </summary>
        /// <param name="id">Identificador do produto.</param>
        /// <returns>Objeto com os dados do produto.</returns>
        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(s => s.Stock)
                .FirstOrDefaultAsync(p => p.Id == id) ?? null!;
        }

        /// <summary>
        /// Adiciona um novo produto no banco de dados.
        /// </summary>
        /// <param name="product">Objeto com os dados a serem adicionados</param>
        /// <returns>Sem retorno.</returns>
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifica se o SKU já existe no banco de dados.
        /// </summary>
        /// <param name="sku">SKU do produto.</param>
        /// <returns>True/False</returns>
        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _context.Products.AnyAsync(p => p.Sku == sku);
        }

        /// <summary>
        /// Atualiza um produto existente no banco de dados.
        /// </summary>
        /// <param name="product">Objeto com os dados a serem atualizados.</param>
        /// <returns></returns>
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna todos os produtos do banco de dados.
        /// </summary>
        /// <returns>Uma lista com todos os produtos.</returns>
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna todos os produtos de uma categoria específica do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>uma lista com todos os produtos daquela categoria.</returns>
        public async Task<List<Product>> GetByCategory(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Id == id)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByNameAsync(string name)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }
    }
}