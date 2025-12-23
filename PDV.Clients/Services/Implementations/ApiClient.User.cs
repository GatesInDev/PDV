using PDV.Core.Entities;
using PDV.Application.DTOs.User;
using System.Net.Http.Json;

namespace PDV.Clients.Services.Implementations;

public partial class ApiClient
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/Auth/users") ?? new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<bool> CreateUserAsync(CreateUserDTO dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Auth", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Auth/{id}", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Auth/{id}");
        return response.IsSuccessStatusCode;
    }
}