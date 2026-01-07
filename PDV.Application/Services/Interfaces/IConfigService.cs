using PDV.Core.Entities;

namespace PDV.Application.Services.Interfaces
{
    public interface IConfigService
    {
        Task<Config> GetConfigAsync();
        Task SaveConfigAsync(Config data);
    }
}
