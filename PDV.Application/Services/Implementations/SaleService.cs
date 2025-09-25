using PDV.Application.DTOs.Sales;
using PDV.Application.DTOs.Stock;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;
using AutoMapper;

namespace PDV.Application.Services.Implementations
{
    public class SaleService : ISaleService
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
        public SaleService(ICashSessionRepository cashSessionRepository, IMapper mapper, ISaleRepository saleRepository, IProductRepository productRepository, IStockTransactionService stockTransactionService, IStockService stockService)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _stockTransactionService = stockTransactionService;
            _stockService = stockService;
            _mapper = mapper;
            _cashSessionRepository = cashSessionRepository;
        }

        /// <summary>
        /// Cria uma nova venda.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Não retorna nada.</returns>
        /// <exception cref="Exception">Produto não encontrado para criar a venda.</exception>
        public async Task CreateSale(CreateSalesDTO dto)
        {
            var openCash = await _cashSessionRepository.GetOpenSessionAsync();
            if (openCash == null)
            {
                throw new Exception("Não há caixa aberto para registrar a venda.");
            }

            var sale = _mapper.Map<Sale>(dto);
            sale.Id = Guid.NewGuid();
            sale.SaleDate = DateTime.Now;
            sale.SaleProducts = new List<SaleProduct>();

            sale.CashSessionId = openCash.Id;   

            decimal totalPrice = 0;

            foreach (var item in dto.Products)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Produto com ID {item.ProductId} não encontrado.");
                }

                // Cria SaleProduct com preço atual do produto
                var saleProduct = new SaleProduct
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    PriceAtSaleTime = product.Price
                };

                sale.SaleProducts.Add(saleProduct);

                // Soma parcial do total da venda
                totalPrice += product.Price * item.Quantity;

                // Cria transação de estoque
                var stockTransactionDto = new CreateStockTransactionDTO
                {
                    ProductId = product.Id,
                    QuantityChanged = -item.Quantity,
                    Type = "Sale",
                    Reason = $"Venda realizada em {sale.SaleDate}, Operador: {openCash.OperatorName}"
                };
                await _stockTransactionService.CreateTransaction(stockTransactionDto);

                // Atualiza o estoque
                var stock = await _stockService.GetStockByProductId(product.Id);

                var stockUpdate = new UpdateStockDTO
                {
                    ProductId = product.Id,
                    Quantity = stock.Quantity - item.Quantity,
                    MetricUnit = stock.MetricUnit
                };
                await _stockService.UpdateStock(stockUpdate);
            }

            sale.TotalPrice = totalPrice;

            await _saleRepository.AddAsync(sale);
        }

        /// <summary>
        /// Retorna uma venda pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Objeto com os dados da venda.</returns>
        /// <exception cref="Exception">Venda não encontrada.</exception>
        public async Task<SaleDetailsDTO> GetSaleById(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale == null)
            {
                throw new Exception($"Venda com ID {id} não encontrada.");
            }

            var dto = _mapper.Map<SaleDetailsDTO>(sale);

            return dto;
        }

        public async Task<List<SaleDetailsDTO>> GetSalesByPeriod(DateTime startDate, DateTime endDate)
        {
            var list = await _saleRepository.GetByPeriodAsync(startDate, endDate);

            return _mapper.Map<List<SaleDetailsDTO>>(list);
        }
    }
}
