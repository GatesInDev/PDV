using PDV.Application.DTOs.Sales;
using PDV.Application.DTOs.Stock;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockTransactionService _stockTransactionService;
        private readonly IStockService _stockService;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IStockTransactionService stockTransactionService, IStockService stockService)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _stockTransactionService = stockTransactionService;
            _stockService = stockService;
        }

        public async Task CreateSale(CreateSalesDTO dto)
        {
            var sale = new Sales
            {
                Id = Guid.NewGuid(),
                SaleDate = DateTime.Now,
                PaymentMethod = dto.PaymentMethod,
                CashOperator = dto.CashOperator,
                Products = new List<Product>()
            };

            foreach (var item in dto.Products)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Produto com ID {item.ProductId} não encontrado.");
                }

                sale.Products.Add(new Product                                              
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    SaleQuantity = item.Quantity
                });

                var stockTransactionDto = new CreateStockTransactionDTO
                {
                    ProductId = product.Id,
                    QuantityChanged = -item.Quantity, 
                    Type = "Sale",
                    Reason = $"Venda realizada em {sale.SaleDate}, Operador: {sale.CashOperator}"
                };

                await _stockTransactionService.CreateTransaction(stockTransactionDto);

                var actualProduct = _stockService.GetStockByProductId(item.ProductId);

                var stockUpdate = new UpdateStockDTO
                {
                    ProductId = item.ProductId,
                    Quantity = actualProduct.Result.Quantity - item.Quantity,
                    MetricUnit = actualProduct.Result.MetricUnit
                };

                await _stockService.UpdateStock(stockUpdate);
            }

            sale.TotalPrice = sale.Products.Sum(p => p.Price * p.SaleQuantity);

            await _saleRepository.AddAsync(sale);
        }
    }
}
