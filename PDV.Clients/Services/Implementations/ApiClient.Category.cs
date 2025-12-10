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
    }
}
