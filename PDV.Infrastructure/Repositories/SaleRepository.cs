using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para operações relacionadas a vendas.
    /// </summary>
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
                                     .Include(cs => cs.CashSession)
                                     .Include(cn => cn.Customer)
                                     .FirstOrDefaultAsync(s => s.Id == id) ?? null!;
        }

        /// <summary>
        /// Retorna todas as vendas em um período.
        /// </summary>
        /// <param name="startDate">Data de inicio do filtro.</param>
        /// <param name="endDate">Data de fim do filtro.</param>
        /// <returns>Lista com as vendas do periodo.</returns>
        public async Task<List<Sale>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
        {
             return await _context.Sales
                                    .Include(s => s.SaleProducts)
                                        .ThenInclude(sp => sp.Product)
                                    .Include(cs => cs.CashSession)
                                    .Include(cn => cn.Customer)
                                    .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate && s.Customer.IsActive == true)
                                    .OrderByDescending(s => s.SaleDate)
                                    .ToListAsync();
        }
    }
}
