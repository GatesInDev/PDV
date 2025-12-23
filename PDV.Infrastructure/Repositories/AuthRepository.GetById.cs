using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    public partial class AuthRepository
    {
        /// <summary>
        /// Recupera um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Objeto <see cref="User"/> se encontrado; caso contrário, nulo.</returns>
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? null!;
        }
    }
}