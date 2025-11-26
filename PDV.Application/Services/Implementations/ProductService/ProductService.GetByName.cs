using PDV.Application.DTOs.Product;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        public async Task<List<ProductDTO>> GetProductsByNameAsync(string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
