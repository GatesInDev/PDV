using AutoMapper;
using PDV.Application.DTOs.Stock;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    /// <summary>
    /// Serviço para operações relacionadas ao estoque.
    /// </summary>
    public partial class StockService : IStockService
    {
        private readonly IMapper _mapper;
        private readonly IStockRepository _repository;

        /// <summary>
        /// Construtor do serviço de estoque.
        /// </summary>
        /// <param name="repository">Repositório de estoque.</param>
        /// <param name="mapper">DI do AutoMapper</param>
        public StockService(IStockRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
