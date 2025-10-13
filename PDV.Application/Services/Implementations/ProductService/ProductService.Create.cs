using PDV.Application.DTOs.Product;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class ProductService
    {
        /// <summary>
        /// Cria um novo Produto.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Guid> Create(CreateProductDTO dto)
        {
            try
            {
                if (await _productRepository.SkuExistsAsync(dto.Sku))
                    throw new Exception("SKU já existe.");

                var product = _mapper.Map<Product>(dto);
                product.Id = Guid.NewGuid();
                product.CreatedAt = DateTime.UtcNow;
                product.IsActive = true;
                product.SaledQuantity = 0;

                await _productRepository.AddAsync(product);

                var stock = new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = dto.Quantity,
                    MetricUnit = product.MetricUnit,
                    LastUpdated = DateTime.UtcNow
                };

                await _stockRepository.CreateAsync(stock);

                return product.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o produto ou o estoque. " + ex.Message, ex);
            }
        }
    }
}
