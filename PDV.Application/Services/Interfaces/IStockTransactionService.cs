using PDV.Application.DTOs.StockTransaction;
using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    public interface IStockTransactionService
    {
        Task<List<StockTransactionDTO>> GetAllStockTransaction();
        Task<StockTransaction> GetStockTransactionById(Guid id);
        Task<Guid> CreateTransaction(CreateStockTransactionDTO stockTransaction);
    }
}
