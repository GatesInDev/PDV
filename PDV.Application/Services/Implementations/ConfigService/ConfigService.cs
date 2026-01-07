using PDV.Application.Services.Interfaces;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations.ConfigService
{
    public partial class ConfigService : IConfigService
    {
        private readonly IConfigRepository _repository;

        public ConfigService(IConfigRepository repository)
        {
            _repository = repository;
        }

    }
}
