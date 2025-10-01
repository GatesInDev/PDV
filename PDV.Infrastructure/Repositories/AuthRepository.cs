using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;
using System.Xml.Serialization;

namespace PDV.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool ThisUserExist(string username, string password)
        {
            return _context.Users.Any(u => u.Username == username && u.Password == password);
        }

        public string getRoleByUser(string username)
        {
             return _context.Users.FirstOrDefault(u => u.Username == username)?.Role ?? string.Empty;
        }

        public Task<List<User>> GetAllUserAsync()
        {
            return _context.Users.ToListAsync();
        }
    }
}
