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
        /// <param name="context">Acesso ao banco de dados.</param>
        public CashSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova sessão de caixa ao banco de dados.
        /// </summary>
        public async Task AddAsync(CashSession session)
        {
            await _context.Set<CashSession>().AddAsync(session);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna uma sessão de caixa pelo seu Id.
        /// </summary>
        public async Task<CashSession?> GetByIdAsync(Guid id)
        {
            return await _context.Set<CashSession>()
                                 .Include(c => c.Sales)
                                 .ThenInclude(c => c.SaleProducts)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Retorna a sessão de caixa que está atualmente aberta (sem ClosedAt).
        /// ⚠️ DEPRECATED: Use GetOpenSessionByOperatorAsync em vez disso.
        /// </summary>
        public async Task<CashSession?> GetOpenSessionAsync()
        {
            return await _context.Set<CashSession>()
                                 .FirstOrDefaultAsync(c => c.ClosedAt == null);
        }

        /// <summary>
        /// Retorna a sessão aberta de um operador específico.
        /// </summary>
        public async Task<CashSession?> GetOpenSessionByOperatorAsync(string operatorName)
        {
            return await _context.Set<CashSession>()
                                 .FirstOrDefaultAsync(c => c.ClosedAt == null && c.OperatorName == operatorName);
        }

        /// <summary>
        /// Atualiza os dados de uma sessão de caixa no banco de dados.
        /// </summary>
        public async Task UpdateAsync(CashSession session)
        {
            _context.Set<CashSession>().Update(session);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Soma todas as vendas da sessão do caixa.
        /// </summary>
        public async Task<decimal> SumOfCashSession(Guid id)
        {
            return await _context.Set<Sale>()
                                 .Where(s => s.CashSessionId == id)
                                 .SumAsync(s => s.TotalPrice);
        }
    }
}