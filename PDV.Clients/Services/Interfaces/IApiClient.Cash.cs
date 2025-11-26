using PDV.Application.DTOs.Cash;
using PDV.Clients.Models.Cash;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task<List<CashSessionDTO>> GetCashSessionsAsync();
        Task<bool> OpenCashSessionAsync(OpenCashSessionDTO dto);
        Task<bool> CloseCashSessionAsync(CloseCashSessionDTO dto);
    }
}
