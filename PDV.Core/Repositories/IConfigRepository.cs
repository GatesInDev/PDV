using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IConfigRepository
    {
        Task<Config> GetConfigAsync();
        Task SaveConfigAsync(Config data);
    }
}
