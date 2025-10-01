using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IAuthRepository
    {
        bool ThisUserExist(string username, string password);

        string getRoleByUser(string username);

        Task<List<User>> GetAllUserAsync();
    }
}
