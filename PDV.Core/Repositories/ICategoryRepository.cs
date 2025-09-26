using PDV.Core.Entities; // Para acessar a entidade Category

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface para o repositório de categorias.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Verifica se o nome da categoria já existe no banco de dados.
        /// </summary>
        /// <param name="name">Nome a ser verificado se existe.</param>
        /// <returns>True/False</returns>
        Task<bool> NameExistsAsync(string name);

        /// <summary>
        /// Adiciona uma nova categoria no banco de dados.
        /// </summary>
        /// <param name="category">Objeto com os dados a ser criados.</param>
        /// <returns>Sem retorno.</returns>
        Task AddAsync(Category category);

        /// <summary>
        /// Retorna uma categoria pelo ID do banco de dados.
        /// </summary>
        /// <param name="id">Identificador da categoria.</param>
        /// <returns>Objeto com os dados da categoria.</returns>
        Task<Category> GetByIdAsync(int id);

        /// <summary>
        /// Atualiza uma categoria existente no banco de dados.
        /// </summary>
        /// <param name="category">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        Task UpdateAsync(Category category);

        /// <summary>
        /// Retorna todas as categorias do banco de dados.
        /// </summary>
        /// <returns>Uma lista com os dados resumidos da categoria.</returns>
        Task<List<Category>> GetAllAsync();
    }
}

