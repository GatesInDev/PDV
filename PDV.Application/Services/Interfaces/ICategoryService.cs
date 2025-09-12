using PDV.Application.DTOs.Category;

namespace PDV.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<int> CreateAsync(CreateCategoryDTO categoryDto);

        Task<int> UpdateAsync(int id, UpdateCategoryDTO categoryDto);

        //Task<bool> DeleteAsync(int id);

        Task<CategoryDetailsDTO> GetByIdAsync(int id);

        //Task<IEnumerable<CategoryDTO>> GetAllAsync();

        //Task<bool> ToggleActiveStatusAsync(int id);
    }
}
