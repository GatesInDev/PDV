using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de vendas.
        /// </summary>
        /// <param name="context">Conexão com o banco de dados.</param>
        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova venda.
        /// </summary>
        /// <param name="sale">Objeto com os dados da venda.</param>
        /// <returns>Sem retorno.</returns>
        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna uma venda pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Entidade com os dados da venda.</returns>
        public async Task<Sale> GetByIdAsync(Guid id)
        {
            return await _context.Sales
         .Include(s => s.SaleProducts)
             .ThenInclude(sp => sp.Product)
         .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Sale>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
             return await _context.Sales
                .Include(s => s.SaleProducts)
                    .ThenInclude(sp => sp.Product)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }
    }
}
