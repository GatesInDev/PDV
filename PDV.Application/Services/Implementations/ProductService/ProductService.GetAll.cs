using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        /// <summary>
        /// Retorna todos os Produtos.
        /// </summary>
        /// <returns>Uma lista com todos os produtos.</returns>
        public async Task<List<ProductDTO>> GetAll()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();

                if (products == null || !products.Any())
                    throw new Exception("Nenhum produto encontrado.");

                var map = _mapper.Map<List<ProductDTO>>(products);

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar os produtos: " + ex.Message);
            }
        }
    }
}
