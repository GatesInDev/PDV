using PDV.Application.DTOs.Category;

namespace PDV.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoryDto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador de categoria.</returns>
        Task<int> CreateAsync(CreateCategoryDTO categoryDto);

        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">Identificados da categoria a ser atualizada.</param>
        /// <param name="categoryDto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Identificador de categoria.</returns>
        Task<int> UpdateAsync(int id, UpdateCategoryDTO categoryDto);

        /// <summary>
        /// Retorna uma categoria pelo ID.
        /// </summary>
        /// <param name="id">Identificador da categoria a ser retornada.</param>
        /// <returns>Objeto da categoria encontrada.</returns>
        Task<CategoryDetailsDTO> GetByIdAsync(int id);

        /// <summary>
        /// Retorna todas as categorias.
        /// </summary>
        /// <returns>Uma lista resumida de todas as categorias.</returns>
        Task<List<CategoryDTO>> GetAllAsync();
    }
}
