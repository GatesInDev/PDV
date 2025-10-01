using PDV.Application.DTOs;
using PDV.Core.Entities;
using System.Data;

namespace PDV.Application.Services.Interfaces
{
    public interface IAuth
    {
        User AuthenticateUser(LoginModel user);

        string GenerateToken(string username, string role, string genKey);

        Task<List<User>> GetAllUserAsync();
    }
}
