using PDV.Core.Entities;
using PDV.Application.DTOs.User;

namespace PDV.Clients.Services.Interfaces;

public partial interface IApiClient
{
    Task<List<User>> GetAllUsersAsync();
    Task<bool> CreateUserAsync(CreateUserDTO dto);
    Task<bool> UpdateUserAsync(Guid id, UpdateUserDTO dto);
    Task<bool> DeleteUserAsync(Guid id);
}