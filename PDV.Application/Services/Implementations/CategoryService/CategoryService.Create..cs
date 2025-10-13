using PDV.Application.DTOs.Category;
using PDV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Application.Services.Implementations
{
    public partial class CategoryService
    {
        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoryDto">Objeto com os dados da categoria a criar.</param>
        /// <returns>Identificador da Categoria.</returns>
        /// <exception cref="Exception">Erro ao criar a categoria.</exception>
        public async Task<int> Create(CreateCategoryDTO categoryDto)
        {
            try
            {
                if (await _repository.NameExistsAsync(categoryDto.Name))
                    throw new Exception("Nome da categoria já existe.");

                var category = _mapper.Map<Category>(categoryDto);

                category.CreatedAt = DateTime.UtcNow;
                category.UpdatedAt = DateTime.UtcNow;
                category.IsActive = true;

                await _repository.AddAsync(category);

                return category.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a categoria no banco de dados." + ex.Message);
            }
        }
    }
}
