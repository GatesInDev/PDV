using PDV.Application.DTOs.Sales;
using PDV.Application.DTOs.Stock;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using AutoMapper;

namespace PDV.Application.Services.Implementations
{
    public partial class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockTransactionService _stockTransactionService;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly ICashSessionRepository _cashSessionRepository;

        /// <summary>
        /// Construtor do serviço de vendas.
        /// </summary>
        /// <param name="saleRepository">Repositório de vendas.</param>
        /// <param name="productRepository">Repositório de produtos.</param>
        /// <param name="stockTransactionService">Serviço de Transações.</param>
        /// <param name="stockService">Serviço de estoque.</param>
        /// <param name="cashSessionRepository">Repositório de sessão do Caixa.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        public SaleService(ICashSessionRepository cashSessionRepository, IMapper mapper, ISaleRepository saleRepository, IProductRepository productRepository, IStockTransactionService stockTransactionService, IStockService stockService)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _stockTransactionService = stockTransactionService;
            _stockService = stockService;
            _mapper = mapper;
            _cashSessionRepository = cashSessionRepository;
        }
    }
}
