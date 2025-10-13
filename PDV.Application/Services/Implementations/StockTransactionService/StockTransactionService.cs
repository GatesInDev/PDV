using AutoMapper;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Repositories;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações de transações de estoque.
    /// </summary>
    public partial class StockTransactionService : IStockTransactionService
    {

        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de transações de estoque.
        /// </summary>
        /// <param name="stockTransactionRepository">Repositório de Transações.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        /// <param name="productService">Serviço de Produto.</param>
        public StockTransactionService(IStockTransactionRepository stockTransactionRepository, IMapper mapper)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
        }
    }
}
