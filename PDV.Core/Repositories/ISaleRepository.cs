using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface ISaleRepository
    {
        public Task AddAsync(Sales sale);
    }
}
