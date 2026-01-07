using PDV.Clients.Models;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        Task<ConfigModel> GetConfigAsync();
        Task SaveConfigAsync(ConfigModel data);
    }
}
