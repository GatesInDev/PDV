using PDV.Core.Entities; // Para acessar a entidade Category

namespace PDV.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> NameExistsAsync(string name);

        Task AddAsync(Category category);

        Task<Category> GetByIdAsync(int id);

        Task UpdateAsync(Category category);
    }
}

