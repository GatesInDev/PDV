using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations.ConfigService
{
    public partial class ConfigService
    {
        public async Task<Config> GetConfigAsync()
        {
            var data = await _repository.GetConfigAsync();
            return data;
        }
    }
}
