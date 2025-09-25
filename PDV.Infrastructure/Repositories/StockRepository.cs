using PDV.Core.Repositories;
using PDV.Infrastructure.Data;
using PDV.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PDV.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de estoque.
        /// </summary>
        /// <param name="context">Conexão com o banco de dados.</param>
        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna o estoque pelo ID do produto do banco de dados.
        /// </summary>
        /// <param name="productId">Identificador do produto.</param>
        /// <returns>O estoque do produto.</returns>
        public async Task<Stock> GetByProductIdAsync(Guid productId)
        {
            return await _context.Stocks
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ProductId == productId) ?? null!;
        }

        /// <summary>
        /// Cria um novo estoque no banco de dados.
        /// </summary>
        /// <param name="stock">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task CreateAsync(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza o estoque no banco de dados.
        /// </summary>
        /// <param name="stock">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        public Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifica se o estoque existe para um determinado produto no banco de dados.
        /// </summary>
        /// <param name="productId">Identificador do produto.</param>
        /// <returns>True/False</returns>
        public async Task<bool> StockExistsAsync(Guid productId)
        {
              return await _context.Stocks.AnyAsync(s => s.ProductId == productId);
        }
    }
}
