using PDV.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _httpClient.GetFromJsonAsync<List<CategoryDTO>>("api/Category");
                return categories ?? new List<CategoryDTO>();
            }
            catch (Exception ex)
            {
                return new List<CategoryDTO>();
            }
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO category)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Category", category);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDTO category)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Category/{id}", category);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Category/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
