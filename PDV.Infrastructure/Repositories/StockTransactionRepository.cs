using PDV.Core.Repositories;
using PDV.Infrastructure.Data;
using PDV.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PDV.Infrastructure.Repositories
{
    public class StockTransactionRepository : IStockTransactionRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do repositório de transações de estoque.
        /// </summary>
        /// <param name="context">Conexão ao banco de dados.</param>
        public StockTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as transações de estoque do banco de dados.
        /// </summary>
        /// <returns>Lista com todas as transações.</returns>
        public async Task<List<StockTransaction>> GetAllStockTransaction()
        {
            return await _context.StockTransactions.ToListAsync();
        }

        /// <summary>
        /// Retorna uma transação de estoque pelo ID do banco de dados.
        /// </summary>
        /// <param name="id">Identificador da transação.</param>
        /// <returns>Objeto com os dados da transação.</returns>
        public async Task<StockTransaction> GetById(Guid id)
        {
            return await _context.StockTransactions.FirstOrDefaultAsync(st => st.Id == id);
        }

        /// <summary>
        /// Cria uma nova transação de estoque no banco de dados.
        /// </summary>
        /// <param name="stockTransaction">Objetos com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        public async Task CreateTransaction(StockTransaction stockTransaction)
        {
            await _context.StockTransactions.AddAsync(stockTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
