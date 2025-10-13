using PDV.Application.DTOs.Sales;
using PDV.Application.DTOs.Stock;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public partial class SaleService
    {
        /// <summary>
        /// Cria uma nova venda.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Identificador da venda.</returns>
        /// <exception cref="Exception">Produto não encontrado para criar a venda.</exception>
        public async Task<Guid> Create(CreateSalesDTO dto)
        {
            try
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
                        throw new Exception($"Produto com ID {item.ProductId} não encontrado.");

                    if (product.Stock.Quantity < item.Quantity)
                        throw new Exception($"Estoque insuficiente, items restantes: {product.Stock.Quantity}");

                    var saleProduct = new SaleProduct
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        PriceAtSaleTime = product.Price
                    };

                    sale.SaleProducts.Add(saleProduct);

                    totalPrice += product.Price * item.Quantity;

                    if (totalPrice < 0)
                        throw new Exception("O valor do produto ou sua quantidade não deve ser negativo.");

                    var stockTransactionDto = new CreateStockTransactionDTO
                    {
                        ProductId = product.Id,
                        QuantityChanged = -item.Quantity,
                        Type = "Sale",
                        Reason = $"Venda realizada em {sale.SaleDate}, Operador: {openCash.OperatorName}"
                    };

                    await _stockTransactionService.Create(stockTransactionDto);

                    var stock = await _stockService.GetByProductId(product.Id);

                    if (stock == null)
                        throw new Exception("Erro ao requisitar estoque.");

                    var stockUpdate = new UpdateStockDTO
                    {
                        ProductId = product.Id,
                        Quantity = stock.Quantity - item.Quantity,
                        MetricUnit = stock.MetricUnit
                    };

                    await _stockService.Update(stockUpdate);

                    var productUpdate = _mapper.Map<Product>(product);
                    productUpdate.SaledQuantity += item.Quantity;

                    await _productRepository.UpdateAsync(productUpdate);
                }

                sale.TotalPrice = totalPrice;

                await _saleRepository.AddAsync(sale);

                return sale.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a venda: " + ex.Message);
            }
        }
    }
}
