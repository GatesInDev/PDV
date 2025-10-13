using PDV.Core.Entities; // Para acessar a entidade Product

namespace PDV.Core.Repositories
{
    /// <summary>
    /// Interface para o repositório de produtos.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retorna um produto pelo ID do banco de dados.
        /// </summary>
        /// <param name="id">Identificador do produto.</param>
        /// <returns>Um objeto do produto especificado.</returns>
        Task<Product> GetByIdAsync(Guid id);

        /// <summary>
        /// Adiciona um novo produto no banco de dados.
        /// </summary>
        /// <param name="product">Objeto com os dados a serem adicionados.</param>
        /// <returns>Sem retorno.</returns>
        Task AddAsync(Product product);

        /// <summary>
        /// Verifica se o SKU já existe no banco de dados.
        /// </summary>
        /// <param name="sku">SKU do produto.</param>
        /// <returns>True/False</returns>
        Task<bool> SkuExistsAsync(string sku);

        /// <summary>
        /// Atualiza um produto existente no banco de dados.
        /// </summary>
        /// <param name="product">Objeto com os dados do produto a serem atualizados.</param>
        /// <returns></returns>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Retorna todos os produtos do banco de dados.
        /// </summary>
        /// <returns>Uma lista com todos os produtos resumidos.</returns>
        Task<List<Product>> GetAllAsync();

        /// <summary>
        /// Retorna todos os produtos de uma categoria específica do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Uma lista com os produtos pertencentes aquela categoria.</returns>
        Task<List<Product>> GetByCategory(int id);
    }
}