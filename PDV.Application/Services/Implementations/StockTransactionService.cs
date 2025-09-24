using AutoMapper;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Repositories;
using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public class StockTransactionService : IStockTransactionService
    {

        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IProductRepository _productService;
        private readonly IMapper _mapper;

        public StockTransactionService(IStockTransactionRepository stockTransactionRepository, IMapper mapper, IProductRepository productService)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<List<StockTransactionDTO>> GetAllStockTransaction()
        {

            var stock = await _stockTransactionRepository.GetAllStockTransaction();
            if (!stock.Any())
            {
                throw new Exception("Sem Estoque.");
            }
            return _mapper.Map<List<StockTransactionDTO>>(stock);
        }

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
