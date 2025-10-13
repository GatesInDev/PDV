using PDV.Application.DTOs.Category;

namespace PDV.Application.Services.Implementations
{
    public partial class CategoryService
    {
        /// <summary>
        /// Retorna uma categoria pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador da Categoria.</param>
        /// <returns>A Categoria pelo seu ID</returns>
        /// <exception cref="Exception">Erro ao encontrar a Categoria.</exception>
        public async Task<CategoryDetailsDTO> GetById(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);

                if (category == null)
                    throw new Exception("Categoria não encontrada.");

                var map = _mapper.Map<CategoryDetailsDTO>(category);

                if (map == null)
                    throw new Exception("Erro ao mapear a categoria.");

                if (!map.IsActive)
                    throw new Exception("Categoria inativa.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encontrar a categoria no banco de dados.", ex);
            }
        }
    }
}
