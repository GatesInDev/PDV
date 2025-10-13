using AutoMapper;
using PDV.Application.DTOs.Metrics;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para as métricas.
    /// </summary>
    public partial class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository _metricsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor para o servico de métricas.
        /// </summary>
        /// <param name="metricsrepository">Repositório das métricas.</param>
        /// <param name="mapper">DI do AutoMapper.</param>
        public MetricsService(IMetricsRepository metricsrepository, IMapper mapper)
        {
            _metricsRepository = metricsrepository;
            _mapper = mapper;
        }
    }
}
