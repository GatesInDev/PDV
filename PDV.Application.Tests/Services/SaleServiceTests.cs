using AutoMapper;
using Moq;
using PDV.Application.DTOs.Sales;
using PDV.Application.Services.Implementations;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Exceptions;
using PDV.Core.Repositories;

namespace PDV.Application.Tests.Services
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepoMock = new();
        private readonly Mock<IProductRepository> _productRepoMock = new();
        private readonly Mock<IStockTransactionService> _stockTransactionServiceMock = new();
        private readonly Mock<IStockService> _stockServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ICashSessionRepository> _cashSessionRepoMock = new();
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _service = new SaleService(
                _cashSessionRepoMock.Object,
                _mapperMock.Object,
                _saleRepoMock.Object,
                _productRepoMock.Object,
                _stockTransactionServiceMock.Object,
                _stockServiceMock.Object
            );
        }

        [Fact]
        public async Task GetById_ShouldReturnSale_WhenExists()
        {
            var id = Guid.NewGuid();
            var sale = new Sale { Id = id };
            var saleDto = new SaleDetailsDTO { Id = id };

            _saleRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(sale);
            _mapperMock.Setup(m => m.Map<SaleDetailsDTO>(sale)).Returns(saleDto);

            var result = await _service.GetById(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _saleRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Sale)null);

            await Assert.ThrowsAsync<SaleNotFoundException>(() => _service.GetById(id));
        }

        [Fact]
        public async Task GetByPeriod_ShouldThrow_WhenStartDateGreaterThanEndDate()
        {
            var start = DateTime.UtcNow;
            var end = start.AddDays(-1);

            await Assert.ThrowsAsync<InvalidSalePeriodException>(() => _service.GetByPeriod(start, end));
        }

        [Fact]
        public async Task GetByPeriod_ShouldThrow_WhenNoSales()
        {
            var start = DateTime.UtcNow;
            var end = start.AddDays(1);
            _saleRepoMock.Setup(r => r.GetByPeriodAsync(start, end)).ReturnsAsync(new List<Sale>());

            await Assert.ThrowsAsync<NoSalesInPeriodException>(() => _service.GetByPeriod(start, end));
        }

        [Fact]
        public async Task GetByPeriod_ShouldReturnSales_WhenExists()
        {
            var start = DateTime.UtcNow;
            var end = start.AddDays(1);
            var sales = new List<Sale> { new Sale() };
            var salesDto = new List<SaleDetailsDTO> { new SaleDetailsDTO() };

            _saleRepoMock.Setup(r => r.GetByPeriodAsync(start, end)).ReturnsAsync(sales);
            _mapperMock.Setup(m => m.Map<List<SaleDetailsDTO>>(sales)).Returns(salesDto);

            var result = await _service.GetByPeriod(start, end);

            Assert.Single(result);
        }
    }
}