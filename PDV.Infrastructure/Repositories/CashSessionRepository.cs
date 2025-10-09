using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para gerenciar sessões de caixa.
    /// </summary>
    public class CashSessionRepository : ICashSessionRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de sessões de caixa.
        /// </summary>
        /// <param name="context">Acesso ao bando de dados.</param>
        public CashSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova sessão de caixa ao banco de dados.
        /// </summary>
        /// <param name="session">Objeto com os dados a serem adicionados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task AddAsync(CashSession session)
        {
            await _context.Set<CashSession>().AddAsync(session);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da sessão a ser encontrada.</param>
        /// <returns>Objeto com os dados da sessão buscada.</returns>
        public async Task<CashSession?> GetByIdAsync(Guid id)
        {
            return await _context.Set<CashSession>()
                                 .Include(c => c.Sales)
                                 .ThenInclude(c => c.SaleProducts)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Retorna a sessão de caixa que está atualmente aberta.
        /// </summary>
        /// <returns>A sessão que está atualmente aberta, ou seja, não tem fechamento.</returns>
        public async Task<CashSession?> GetOpenSessionAsync()
        {
            return await _context.Set<CashSession>()
                                 .FirstOrDefaultAsync(c => c.ClosedAt == null);
        }

        /// <summary>
        /// Atualiza os dados de uma sessão de caixa no banco de dados.
        /// </summary>
        /// <param name="session">Objeto com os dados a serem atualiados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task UpdateAsync(CashSession session)
        {
            _context.Set<CashSession>().Update(session);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Soma todas as vendas da sessão do caixa.
        /// </summary>
        /// <param name="id">Identificador do caixa.</param>
        /// <returns>Valor decimal do total.</returns>
        public async Task<decimal> SumOfCashSession(Guid id)
        {
            return await _context.Set<Sale>()
                                 .Where(s => s.CashSessionId == id)
                                 .SumAsync(s => s.TotalPrice);
        }
    }
}