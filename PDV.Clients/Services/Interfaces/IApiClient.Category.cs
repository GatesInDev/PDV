using PDV.Application.DTOs.Category;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task CreateCategoryAsync(CreateCategoryDTO category);

        Task UpdateCategoryAsync(int id, UpdateCategoryDTO category);
        
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task DeleteCategoryAsync(int id);
    }
}
