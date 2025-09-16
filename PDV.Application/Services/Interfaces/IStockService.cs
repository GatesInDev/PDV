using PDV.Application.DTOs.Stock;

namespace PDV.Application.Services.Interfaces
{
    public interface IStockService
    {
        Task<StockDTO> GetStockByProductId(Guid productId);

        Task UpdateStock(UpdateStockDTO dto);

        Task CreateAsync(CreateStockDTO dto);

        Task<bool> StockExistsAsync(Guid productId);
    }
}
