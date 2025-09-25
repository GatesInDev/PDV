using PDV.Application.DTOs.Cash;
using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    public interface ICashService
    {
        Task<Guid> OpenCash(OpenCashSessionDTO dto);
        Task CloseCash(CloseCashSessionDTO dto);
        Task<CashSession?> GetCashById(Guid id);
    }
}