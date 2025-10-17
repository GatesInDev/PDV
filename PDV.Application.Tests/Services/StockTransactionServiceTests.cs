using AutoMapper;
using Moq;
using PDV.Application.DTOs.StockTransaction;
using PDV.Application.Services.Implementations;
using PDV.Core.Entities;
using PDV.Core.Exceptions;
using PDV.Core.Repositories;

namespace PDV.Application.Tests.Services
{
    public class StockTransactionServiceTests
    {
        private readonly Mock<IStockTransactionRepository> _repoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly StockTransactionService _service;

        public StockTransactionServiceTests()
        {
            _service = new StockTransactionService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnTransactions_WhenExists()
        {
            var transactions = new List<StockTransaction> { new StockTransaction() };
            var dtos = new List<StockTransactionDTO> { new StockTransactionDTO() };

            _repoMock.Setup(r => r.GetAllStockTransaction()).ReturnsAsync(transactions);
            _mapperMock.Setup(m => m.Map<List<StockTransactionDTO>>(transactions)).Returns(dtos);

            var result = await _service.GetAll();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetAll_ShouldThrow_WhenNoTransactions()
        {
            _repoMock.Setup(r => r.GetAllStockTransaction()).ReturnsAsync(new List<StockTransaction>());

            await Assert.ThrowsAsync<NoStockTransactionsException>(() => _service.GetAll());
        }

        [Fact]
        public async Task GetById_ShouldReturnTransaction_WhenExists()
        {
            var id = Guid.NewGuid();
            var transaction = new StockTransaction { Id = id };
            var dto = new StockTransaction();

            _repoMock.Setup(r => r.GetById(id)).ReturnsAsync(transaction);
            _mapperMock.Setup(m => m.Map<StockTransaction>(transaction)).Returns(dto);

            var result = await _service.GetById(id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetById(id)).ReturnsAsync((StockTransaction)null);

            await Assert.ThrowsAsync<StockTransactionNotFoundException>(() => _service.GetById(id));
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenDtoIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
        }

        [Fact]
        public async Task Create_ShouldCallRepository_WhenValid()
        {
            var dto = new CreateStockTransactionDTO { ProductId = Guid.NewGuid(), QuantityChanged = 1, Type = "Entrada" };
            _mapperMock.Setup(m => m.Map<StockTransaction>(dto)).Returns(new StockTransaction());

            await _service.Create(dto);

            _repoMock.Verify(r => r.CreateTransaction(It.IsAny<StockTransaction>()), Times.Once);
        }
    }
}