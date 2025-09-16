using PDV.Core.Entities; // Para ter acesso a entidade Product
using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório
using PDV.Application.DTOs.Product; // Para ter acesso as DTOs de Product
using PDV.Application.DTOs.Stock; // Para ter acesso as DTOs de Stock
using PDV.Application.Services.Interfaces;  // Para ter acesso a interface IProductService e IStockService

using AutoMapper; // Para ter acesso ao AutoMapper

namespace PDV.Application.Services.Implementations
{
    public class ProductService : IProductService
        {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }
        public async Task<ProductDetailsDTO> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new Exception("Produto não encontrado.");
            }

            return _mapper.Map<ProductDetailsDTO>(product);
        }

        public async Task<Guid> CreateAsync(CreateProductDTO dto)
        {
            if (await _productRepository.SkuExistsAsync(dto.Sku))
            {
                throw new Exception("SKU já existe.");
            }

            var product = _mapper.Map<Product>(dto);
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;
            product.IsActive = true;

            try
            {
                await _productRepository.AddAsync(product);

                var stockDto = new CreateStockDTO
                {
                    MetricUnit = product.MetricUnit,
                    ProductId = product.Id,
                    Quantity = dto.Quantity,
                };

                await _stockService.CreateAsync(stockDto);

                return product.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o produto ou o estoque.", ex);
            }
        }

        public async Task<Guid> UpdateAsync(Guid id, UpdateProductDTO dto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                throw new Exception("Produto não encontrado.");
            }

            if (existingProduct.Sku != dto.Sku && await _productRepository.SkuExistsAsync(dto.Sku))
            {
                throw new Exception("SKU já existe.");
            }

            _mapper.Map(dto, existingProduct);
            existingProduct.UpdatedAt = DateTime.UtcNow;
            existingProduct.IsActive = true;

            try
            {
                await _productRepository.UpdateAsync(existingProduct);
                return existingProduct.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto no banco de dados.", ex);
            }
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<List<ProductDTO>> GetByCategoryAsync(string category)
        {
            var products = await _productRepository.GetByCategory(category);
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
