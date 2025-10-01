using PDV.Core.Entities; // Para ter acesso a entidade Product
using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório
using PDV.Application.DTOs.Product; // Para ter acesso as DTOs de Product
using PDV.Application.DTOs.Stock; // Para ter acesso as DTOs de Stock
using PDV.Application.Services.Interfaces;  // Para ter acesso a interface IProductService e IStockService

using AutoMapper; // Para ter acesso ao AutoMapper

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações relacionadas a produtos.
    /// </summary>
    public class ProductService : IProductService
        {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de produtos.
        /// </summary>
        /// <param name="productRepository">Repositorio de produtos.</param>
        /// <param name="mapper">Adição do AutoMapper.</param>
        /// <param name="stockService">Serviço do Estoque.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        /// <summary>
        /// Retorna um Produto pelo ID.
        /// </summary>
        /// <param name="id">Identificador unico do Produto.</param>
        /// <returns>O Objeto do produto especifico completo.</returns>
        /// <exception cref="Exception">Não foi possivel encontrar o produto.</exception>
        public async Task<ProductDetailsDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                        throw new Exception("Produto não encontrado.");

                var map = _mapper.Map<ProductDetailsDTO>(product);

                if (map == null)
                    throw new Exception("Erro ao mapear o produto.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o produto: " + ex.Message);
            }   
        }

        /// <summary>
        /// Cria um novo Produto.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Guid> CreateAsync(CreateProductDTO dto)
        {
            try
            {
                if (await _productRepository.SkuExistsAsync(dto.Sku))
                        throw new Exception("SKU já existe.");

                var product = _mapper.Map<Product>(dto);

                if (product == null)
                    throw new Exception("Erro ao mapear o produto.");
                
                product.Id = Guid.NewGuid();
                product.CreatedAt = DateTime.UtcNow;
                product.IsActive = true;


                await _productRepository.AddAsync(product);

                var stockDto = new CreateStockDTO
                {
                    MetricUnit = product.MetricUnit,
                    ProductId = product.Id,
                    Quantity = dto.Quantity,
                };

                if (stockDto == null)
                    throw new Exception("Erro ao mapear o estoque.");

                await _stockService.CreateAsync(stockDto);

                return product.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o produto ou o estoque.", ex);
            }
        }

        /// <summary>
        /// Atualiza um Produto existente.
        /// </summary>
        /// <param name="id">Identificador do produto a ser atualizado.</param>
        /// <param name="dto">Objeto com os dados para a atualização</param>
        /// <returns>Identificar do produto Atualizado.</returns>
        /// <exception cref="Exception">Erro ao atualizar o produto.</exception>
        public async Task<Guid> UpdateAsync(Guid id, UpdateProductDTO dto)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);

                if (existingProduct == null)
                    throw new Exception("Produto não encontrado.");

                if (!existingProduct.IsActive)
                    throw new Exception("Produto está desativado e não pode ser atualizado.");

                if (existingProduct.Sku != dto.Sku && await _productRepository.SkuExistsAsync(dto.Sku))
                    throw new Exception("SKU já existe.");

                _mapper.Map(dto, existingProduct);
                existingProduct.UpdatedAt = DateTime.UtcNow;

                if (existingProduct == null)
                    throw new Exception("Erro ao mapear o produto.");

                await _productRepository.UpdateAsync(existingProduct);

                return existingProduct.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto no banco de dados: " + ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os Produtos.
        /// </summary>
        /// <returns>Uma lista com todos os produtos.</returns>
        public async Task<List<ProductDTO>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();

                if (products == null || !products.Any())
                    throw new Exception("Nenhum produto encontrado.");

                var map = _mapper.Map<List<ProductDTO>>(products);

                if (map == null)
                    throw new Exception("Erro ao mapear os produtos.");

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar os produtos: " + ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os Produtos por Categoria.
        /// </summary>
        /// <param name="category">O Nome(String) da categoria (Sem Tratamento ainda)</param>
        /// <returns>Uma lista com todos os produtos pertencesteas aquela categoria.</returns>
        public async Task<List<ProductDTO>> GetByCategoryAsync(string category)
        {
            try
            {
                var products = await _productRepository.GetByCategory(category);

                if (products == null || !products.Any())
                    throw new Exception("Nenhum produto encontrado.");

                var map = _mapper.Map<List<ProductDTO>>(products);

                if (map == null)
                    throw new Exception("Erro ao mapear os produtos.");

                return map;
            }
            catch(Exception ex)
            {
                throw new Exception("Erro a recuperar os produtos: " + ex.Message);
            }
        }

        /// <summary>
        /// Desativa um Produto.
        /// </summary>
        /// <param name="id">Identificar do produto a ser desabilitado.</param>
        /// <returns>Valor booleano, True = Success Operation/False = Fail Operation.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DisableProductAsync(Guid id)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);

                if (existingProduct == null)
                    throw new Exception("Produto não encontrado.");

                if (existingProduct.IsActive == false)
                    throw new Exception("Produto já desabilitado.");

                existingProduct.IsActive = false;
                existingProduct.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(existingProduct);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desativar o produto no banco de dados: " + ex.Message);
            }
        }
    }
}
