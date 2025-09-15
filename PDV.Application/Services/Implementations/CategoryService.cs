using PDV.Core.Entities; // Para ter acesso a entidade Category
using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório
using PDV.Application.DTOs.Category; // Para ter acesso as DTOs de Category
using PDV.Application.Services.Interfaces;  // Para ter acesso a interface ICategoryService

using AutoMapper; // Para ter acesso ao AutoMapper

namespace PDV.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDetailsDTO> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            return _mapper.Map<CategoryDetailsDTO>(category);
        }

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

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}

