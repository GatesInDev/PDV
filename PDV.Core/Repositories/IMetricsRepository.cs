using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface responsável por fornecer métricas e indicadores do sistema.
    /// </summary>
    public interface IMetricsRepository
    {
        /// <summary>
        /// Obtém os produtos mais vendidos com base na quantidade especificada.
        /// </summary>
        /// <param name="items">Número de produtos a retornar.</param>
        /// <returns>Lista dos produtos mais vendidos.</returns>
        Task<List<Product>> GetBestSellersAsync(int items);

        /// <summary>
        /// Retorna os produtos cujo estoque está abaixo de um determinado limite.
        /// </summary>
        /// <param name="stockQuantity">Quantidade mínima de estoque para comparação.</param>
        /// <returns>Lista de produtos com estoque abaixo do limite.</returns>
        Task<List<Stock>> GetBelowStockAsync(int stockQuantity);

        /// <summary>
        /// Obtém o número total de vendas realizadas em uma data específica.
        /// </summary>
        /// <param name="date">Data para análise das vendas.</param>
        /// <returns>Total de vendas realizadas no dia.</returns>
        Task<int> GetDailySales(DateTime date);

        /// <summary>
        /// Calcula o faturamento total obtido em uma data específica.
        /// </summary>
        /// <param name="date">Data para análise de faturamento.</param>
        /// <returns>Valor total de receita gerada no dia.</returns>
        Task<decimal> GetDailyIncome(DateTime date);
    }
}
