using PDV.Application.DTOs.Category;

namespace PDV.Application.Services.Implementations
{
    public partial class CategoryService
    {
        /// <summary>
        /// Retorna todas as categorias.
        /// </summary>
        /// <returns>Uma lista com todas as categorias.</returns>
        /// <exception cref="Exception">Erro ao recuperar as categorias do banco de dados.</exception>
        public async Task<List<CategoryDTO>> GetAll()
        {
            try
            {
                var categories = await _repository.GetAllAsync();

                if (categories == null || categories.Count == 0)
                    throw new Exception("Nenhuma categoria encontrada.");

                var map = _mapper.Map<List<CategoryDTO>>(categories);

                if (map == null)
                    throw new Exception("Erro ao mapear as categorias.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar as categorias do banco de dados.", ex);
            }
        }
    }
}
