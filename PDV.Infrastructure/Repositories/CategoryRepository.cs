using PDV.Core.Entities; // Para ter acesso às entidades
using PDV.Core.Repositories; // Para ter acesso às interfaces de repositório
using PDV.Infrastructure.Data; // Para ter acesso ao AppDbContext

using Microsoft.EntityFrameworkCore; // Para ter acesso ao DbContext e DbSet

namespace PDV.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de categorias.
        /// </summary>
        /// <param name="context">Conexão ao banco de dados.</param>
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Verifica se o nome da categoria já existe no banco de dados.
        /// </summary>
        /// <param name="name">Nome da categoria a ser verificado.</param>
        /// <returns>True/False</returns>
        public async Task<bool> NameExistsAsync(string name)
        {
            if (_context.Categories.Any(c => c.Name == name))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adiciona uma nova categoria no banco de dados.
        /// </summary>
        /// <param name="category">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna uma categoria pelo ID do banco de dados.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser retornada.</param>
        /// <returns>Objeto com os dados da categoria.</returns>
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Atualiza uma categoria existente no banco de dados.
        /// </summary>
        /// <param name="category">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna todas as categorias do banco de dados.
        /// </summary>
        /// <returns>Uma lista com todas as categorias resumidas.</returns>
        public async Task<List<Category>> GetAllAsync()
        {
           return await _context.Categories.ToListAsync();
        }
    }
}

