using PDV.Clients.Models;
using System.Net.Http.Json;

namespace PDV.Clients.Services.Implementations
{
    public partial class ApiClient
    {
        public async Task<ConfigModel> GetConfigAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ConfigModel>("/api/Config");
                return response ?? new ConfigModel();
            }
            catch (Exception e)
            {
                return new ConfigModel();
            }
        }

        public async Task SaveConfigAsync(ConfigModel data)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Config", data);
            response.EnsureSuccessStatusCode();
        }
    }
}
