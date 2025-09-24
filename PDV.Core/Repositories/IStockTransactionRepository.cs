using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IStockTransactionRepository
    {
        Task<List<StockTransaction>> GetAllStockTransaction();
                                                                
        Task<StockTransaction> GetById(Guid id);

        Task CreateTransaction(StockTransaction stockTransaction);
    }
}
