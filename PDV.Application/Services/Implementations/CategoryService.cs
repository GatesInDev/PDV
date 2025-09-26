using PDV.Core.Entities; // Para ter acesso a entidade Category
using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório
using PDV.Application.DTOs.Category; // Para ter acesso as DTOs de Category
using PDV.Application.Services.Interfaces;  // Para ter acesso a interface ICategoryService

using AutoMapper; // Para ter acesso ao AutoMapper

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações relacionadas a categorias.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de categoria.
        /// </summary>
        /// <param name="categoryRepository">Repositório de categoria.</param>
        /// <param name="mapper">Acesso ao AutoMapper</param>
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma categoria pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador da Categoria.</param>
        /// <returns>A Categoria pelo seu ID</returns>
        /// <exception cref="Exception">Erro ao encontrar a Categoria.</exception>
        public async Task<CategoryDetailsDTO> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            return _mapper.Map<CategoryDetailsDTO>(category);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoryDto">Objeto com os dados da categoria a criar.</param>
        /// <returns>Identificador da Categoria.</returns>
        /// <exception cref="Exception">Erro ao criar a categoria.</exception>
        public async Task<int> CreateAsync(CreateCategoryDTO categoryDto)
        {
            if (await _repository.NameExistsAsync(categoryDto.Name))
            {
                throw new Exception("Nome da categoria já existe.");
            }

            var category = _mapper.Map<Category>(categoryDto);

            category.CreatedAt = DateTime.UtcNow;
            category.IsActive = true;

            try
            {
                await _repository.AddAsync(category);
                return category.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a categoria no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">Identificador da categoria a atualizar.</param>
        /// <param name="categoryDto">Dados para alterar na categoria.</param>
        /// <returns>Retorna o Id da categoria atualizada.</returns>
        /// <exception cref="Exception">Erro ao atualizar a categoria.</exception>
        public async Task<int> UpdateAsync(int id, UpdateCategoryDTO categoryDto)
        {
            var existingCategory = await _repository. GetByIdAsync(id);
            if ( existingCategory == null )
            {
                throw new Exception("Categoria não encontrada.");
            }
            if ( existingCategory.Name != categoryDto.Name && await _repository.NameExistsAsync(categoryDto.Name) )
            {
                throw new Exception("Nome da categoria já existe.");
            }

            _mapper.Map(categoryDto, existingCategory);

            existingCategory.UpdatedAt = DateTime.UtcNow;
            existingCategory.IsActive = true;

            try
            {
                await _repository.UpdateAsync(existingCategory);
                return existingCategory.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar a categoria no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Retorna todas as categorias.
        /// </summary>
        /// <returns>Uma lista com todas as categorias.</returns>
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}

