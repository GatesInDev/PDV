using AutoMapper; // Para ter acesso ao AutoMapper
using PDV.Application.DTOs.Product; // Para ter acesso as DTOs de Product
using PDV.Application.DTOs.Stock; // Para ter acesso as DTOs de Stock
using PDV.Application.Services.Interfaces;  // Para ter acesso a interface IProductService e IStockService
using PDV.Core.Entities; // Para ter acesso a entidade Product
using PDV.Core.Repositories; // Para ter acesso as interfaces de repositório

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações relacionadas a produtos.
    /// </summary>
    public partial class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de produtos.
        /// </summary>
        /// <param name="productRepository">Repositorio de produtos.</param>
        /// <param name="mapper">Adição do AutoMapper.</param>
        /// <param name="stockService">Serviço do Estoque.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockRepository = stockRepository;
        }
    }
}
