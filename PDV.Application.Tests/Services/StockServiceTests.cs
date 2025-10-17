using AutoMapper;
using Moq;
using PDV.Application.DTOs.Stock;
using PDV.Application.Services.Implementations;
using PDV.Core.Entities;
using PDV.Core.Exceptions;
using PDV.Core.Repositories;

namespace PDV.Application.Tests.Services
{
    public class StockServiceTests
    {
        private readonly Mock<IStockRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly StockService _service;

        public StockServiceTests()
        {
            _repoMock = new Mock<IStockRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new StockService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenStockExists()
        {
            // Arrange
            var dto = new CreateStockDTO { ProductId = Guid.NewGuid(), Quantity = 10, MetricUnit = "Un" };
            _repoMock.Setup(r => r.StockExistsAsync(dto.ProductId)).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<StockAlreadyExistsException>(() => _service.Create(dto));
        }

        [Fact]
        public async Task Create_ShouldCallRepository_WhenStockDoesNotExist()
        {
            // Arrange
            var dto = new CreateStockDTO { ProductId = Guid.NewGuid(), Quantity = 10, MetricUnit = "Un" };
            _repoMock.Setup(r => r.StockExistsAsync(dto.ProductId)).ReturnsAsync(false);
            _mapperMock.Setup(m => m.Map<Stock>(dto)).Returns(new Stock());

            // Act
            await _service.Create(dto);

            // Assert
            _repoMock.Verify(r => r.CreateAsync(It.IsAny<Stock>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenDtoIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));
        }

        [Fact]
        public async Task Update_ShouldCallRepository_WhenValid()
        {
            // Arrange
            var dto = new UpdateStockDTO { ProductId = Guid.NewGuid(), Quantity = 5, MetricUnit = "Un" };
            _mapperMock.Setup(m => m.Map<Stock>(dto)).Returns(new Stock());

            // Act
            await _service.Update(dto);

            // Assert
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Stock>()), Times.Once);
        }

        [Fact]
        public async Task GetByProductId_ShouldReturnStockDTO_WhenStockExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var stock = new Stock { ProductId = productId };
            var stockDto = new StockDTO { ProductId = productId };

            _repoMock.Setup(r => r.GetByProductIdAsync(productId)).ReturnsAsync(stock);
            _mapperMock.Setup(m => m.Map<StockDTO>(stock)).Returns(stockDto);

            // Act
            var result = await _service.GetByProductId(productId);

            // Assert
            Assert.Equal(productId, result.ProductId);
        }

        [Fact]
        public async Task GetByProductId_ShouldThrow_WhenStockNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByProductIdAsync(productId)).ReturnsAsync((Stock)null);

            // Act & Assert
            await Assert.ThrowsAsync<StockNotFoundException>(() => _service.GetByProductId(productId));
        }
    }
}