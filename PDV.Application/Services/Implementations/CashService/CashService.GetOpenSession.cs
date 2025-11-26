using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations
{
    public partial class CashService
    {
        public Task<CashSession?> GetOpenSessionAsync()
        {
            return _repository.GetOpenSessionAsync();
        }
    }
}
