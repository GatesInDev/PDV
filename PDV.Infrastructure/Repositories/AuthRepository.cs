using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório responsável pela autenticação e gerenciamento de usuários.
    /// </summary>
    public partial class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Verifica se o usuário existe com base no nome de usuário e senha.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <param name="password">Senha em texto plano (deve ser comparada com hash).</param>
        /// <returns>Verdadeiro se o usuário existir; caso contrário, falso.</returns>
        public async Task<bool> ThisUserExist(string username, string password)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username && u.Password == password);
        }

        /// <summary>
        /// Obtém o papel (role) associado ao nome de usuário.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <returns>Papel do usuário ou string vazia se não encontrado.</returns>
        public async Task<string> getRoleByUser(string username)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);

            return user?.Role ?? string.Empty;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        public Task<List<User>> GetAllUserAsync()
        {
            return _context.Users.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">Objeto User com os dados a serem criados.</param>
        public async Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="user">Objeto User com os dados atualizados.</param>
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deleta um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se um usuário com o username específico já existe.
        /// </summary>
        /// <param name="username">Nome de usuário.</param>
        /// <returns>Verdadeiro se o usuário existir; caso contrário, falso.</returns>
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
