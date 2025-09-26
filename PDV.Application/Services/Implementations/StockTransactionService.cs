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
    public class StockTransactionService : IStockTransactionService
    {

        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IProductRepository _productService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do serviço de transações de estoque.
        /// </summary>
        /// <param name="stockTransactionRepository">Repositório de Transações.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        /// <param name="productService">Serviço de Produto.</param>
        public StockTransactionService(IStockTransactionRepository stockTransactionRepository, IMapper mapper, IProductRepository productService)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// Retorna todas as transações de estoque.
        /// </summary>
        /// <returns>Uma lista com o estoque resumido.</returns>
        /// <exception cref="Exception">Não existem estoques.</exception>
        public async Task<List<StockTransactionDTO>> GetAllStockTransaction()
        {

            var stock = await _stockTransactionRepository.GetAllStockTransaction();
            if (!stock.Any())
            {
                throw new Exception("Sem Estoque.");
            }
            return _mapper.Map<List<StockTransactionDTO>>(stock);
        }

        /// <summary>
        /// Retorna uma transação de estoque pelo ID.
        /// </summary>
        /// <param name="id">Identificador da Transação.</param>
        /// <returns>Objeto com os dados da transação especifica.</returns>
        /// <exception cref="Exception">Transação não encontrada.</exception>
        public async Task<StockTransaction> GetStockTransactionById(Guid id)
        {
            var stock = await _stockTransactionRepository.GetById(id);

            if (stock == null)
            {
                throw new Exception("Transação não encontrada.");
            }

            var product = await _productService.GetByIdAsync(stock.ProductId);


            return _mapper.Map<StockTransaction>(stock);
        }

        /// <summary>
        /// Cria uma nova transação de estoque.
        /// </summary>
        /// <param name="stock">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="ArgumentNullException">Objeto invalido.</exception>
        /// <exception cref="Exception">Erro genêrico.</exception>
        public async Task<Guid> CreateTransaction(CreateStockTransactionDTO stock)
        {
            if (stock == null)
            {
                throw new ArgumentNullException(nameof(stock));
            }
            var stockTransaction = _mapper.Map<StockTransaction>(stock);
            stockTransaction.Id = Guid.NewGuid();
            stockTransaction.LastUpdated = DateTime.UtcNow;
            try
            {
                await _stockTransactionRepository.CreateTransaction(stockTransaction);
                return stockTransaction.Id;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
