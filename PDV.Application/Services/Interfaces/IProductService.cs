using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de produtos.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retorna um produto pelo ID.
        /// </summary>
        /// <param name="id">Identificador do produto a ser encontrado.</param>
        /// <returns>Um objeto com os dados detalhados do produto.</returns>
        Task<ProductDetailsDTO> GetById(Guid id);

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador do produto criado.</returns>
        Task<Guid> Create(CreateProductDTO dto);

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">Identificador do produto a ser atualizado.</param>
        /// <param name="dto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Identificador do produto atualizado.</returns>
        Task<Guid> Update(Guid id, UpdateProductDTO dto);

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        /// <returns>uma lista resumida com todos os produtos.</returns>
        Task<List<ProductDTO>> GetAll();

        /// <summary>
        /// Retorna todos os produtos de uma categoria específica.
        /// </summary>
        /// <param name="id">Categoria que deseja fazer o filtro.</param>
        /// <returns>Uma lista resumida com todos os produtos pertencentes aquela categoria.</returns>
        Task<List<ProductDTO>> GetAllProductsByCategoryId(int id);

        /// <summary>
        /// Desabilita um produto.
        /// </summary>
        /// <param name="id">Identificador do produto a ser dasabilitado.</param>
        /// <returns>True/False</returns>
        Task<bool> Delete(Guid id);

        Task<List<ProductDTO>> GetProductsByNameAsync(string name);
    }
}
