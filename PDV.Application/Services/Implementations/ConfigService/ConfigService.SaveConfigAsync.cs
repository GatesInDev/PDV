using PDV.Core.Entities;

namespace PDV.Application.Services.Implementations.ConfigService
{
    public partial class ConfigService
    {
        public async Task SaveConfigAsync(Config data)
        {
            _repository.SaveConfigAsync(data);
        }
    }
}
