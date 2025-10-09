using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using PDV.Infrastructure.Data;

namespace PDV.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório responsável por fornecer métricas e indicadores operacionais do sistema.
    /// </summary>
    public class MetricsRepository : IMetricsRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="MetricsRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public MetricsRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna os produtos mais vendidos, ordenados pela quantidade de vendas.
        /// </summary>
        /// <param name="items">Número máximo de produtos a retornar.</param>
        /// <returns>Lista dos produtos mais vendidos.</returns>
        public async Task<List<Product>> GetBestSellersAsync(int items)
        {
            return await _context.Products
                .OrderByDescending(p => p.SaledQuantity)
                .Take(items)
                .Include(p => p.Category)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna os produtos cujo estoque está abaixo ou igual ao valor especificado.
        /// </summary>
        /// <param name="stockQuantity">Quantidade mínima de estoque para comparação.</param>
        /// <returns>Lista de estoques abaixo do limite definido.</returns>
        public async Task<List<Stock>> GetBelowStockAsync(int stockQuantity)
        {
            return await _context.Stocks
                .Where(s => s.Quantity <= stockQuantity)
                .Include(s => s.Product)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém o número total de vendas realizadas em uma data específica.
        /// </summary>
        /// <param name="date">Data para análise das vendas.</param>
        /// <returns>Total de vendas realizadas no dia.</returns>
        public async Task<int> GetDailySales(DateTime date)
        {
            var list = _context.Sales
                .Where(s => s.SaleDate.Date == date.Date)
                .Count();

            return list;
        }

        /// <summary>
        /// Calcula o faturamento total obtido em uma data específica.
        /// </summary>
        /// <param name="date">Data para análise de faturamento.</param>
        /// <returns>Valor total de receita gerada no dia.</returns>
        public async Task<decimal> GetDailyIncome(DateTime date)
        {
            return await _context.Sales
                .Where(w => w.SaleDate.Date == date.Date)
                .Select(s => s.TotalPrice)
                .SumAsync();
        }
    }
}
