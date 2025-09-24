using AutoMapper;
using PDV.Application.DTOs.Stock;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly IMapper _mapper;
        private readonly IStockRepository _repository;

        public StockService(IStockRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> StockExistsAsync(Guid productId)
        {
            return _repository.StockExistsAsync(productId);
        }

        public async Task CreateAsync(CreateStockDTO dto)
        {
            if (await StockExistsAsync(dto.ProductId))
            {
                throw new Exception("Estoque já existe para este produto.");
            }
            var stock = _mapper.Map<Stock>(dto);
            stock.Id = Guid.NewGuid();
            stock.LastUpdated = DateTime.UtcNow;
            try
            {
                await _repository.CreateAsync(stock);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o estoque no banco de dados.", ex);
            }
        }

        public async Task UpdateStock(UpdateStockDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }


            var stock = _mapper.Map<Stock>(dto);
            stock.LastUpdated = DateTime.UtcNow;
            stock.MetricUnit = dto.MetricUnit;

            try
            {
                await _repository.UpdateAsync(stock);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o estoque no banco de dados.", ex);
            }
        }

        public async Task<StockDTO> GetStockByProductId(Guid productId)
        {
            var stock = await _repository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                throw new Exception("Estoque não encontrado.");
            }
            return _mapper.Map<StockDTO>(stock);
        }
    }
}
