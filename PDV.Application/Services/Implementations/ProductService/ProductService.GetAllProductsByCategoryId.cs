using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        /// <summary>
        /// Retorna todos os Produtos por Categoria.
        /// </summary>
        /// <param name="id">O Nome(String) da categoria (Sem Tratamento ainda)</param>
        /// <returns>Uma lista com todos os produtos pertencesteas aquela categoria.</returns>
        public async Task<List<ProductDTO>> GetAllProductsByCategoryId(int id)
        {
            try
            {
                var products = await _productRepository.GetByCategory(id);

                if (products == null || !products.Any())
                    throw new Exception("Nenhum produto encontrado.");

                var map = _mapper.Map<List<ProductDTO>>(products);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro a recuperar os produtos: " + ex.Message);
            }
        }
    }
}
