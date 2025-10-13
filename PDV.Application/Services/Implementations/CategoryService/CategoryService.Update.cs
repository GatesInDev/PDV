using PDV.Application.DTOs.Category;

namespace PDV.Application.Services.Implementations
{
    public partial class CategoryService
    {
        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">Identificador da categoria a atualizar.</param>
        /// <param name="categoryDto">Dados para alterar na categoria.</param>
        /// <returns>Retorna o Id da categoria atualizada.</returns>
        /// <exception cref="Exception">Erro ao atualizar a categoria.</exception>
        public async Task<int> Update(int id, UpdateCategoryDTO categoryDto)
        {
            try
            {
                var existingCategory = await _repository.GetByIdAsync(id);

                if (existingCategory == null)
                    throw new Exception("Categoria não encontrada.");

                if (existingCategory.Name != categoryDto.Name && await _repository.NameExistsAsync(categoryDto.Name))
                    throw new Exception("Nome da categoria já existe.");

                _mapper.Map(categoryDto, existingCategory);

                if (existingCategory == null)
                    throw new Exception("Erro ao mapear a categoria.");

                existingCategory.UpdatedAt = DateTime.UtcNow;
                existingCategory.IsActive = true;

                await _repository.UpdateAsync(existingCategory);

                return existingCategory.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar a categoria no banco de dados.", ex);
            }
        }
    }
}
