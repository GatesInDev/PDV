using AutoMapper;
using PDV.Application.DTOs.Stock;
using PDV.Application.Services.Interfaces;
using PDV.Core.Entities;
using PDV.Core.Repositories;

namespace PDV.Application.Services.Implementations
{
    public class StockService : IStockService
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

        /// <summary>
        /// Verifica se o estoque existe para o produto.
        /// </summary>
        /// <param name="productId">Produto cujo estoque será validado.</param>
        /// <returns>True/False</returns>
        public Task<bool> StockExistsAsync(Guid productId)
        {
            return _repository.StockExistsAsync(productId);
        }

        /// <summary>
        /// Cria um novo estoque.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="Exception">Estoque já existe ou houve erro ao criar o estoque.</exception>
        public async Task CreateAsync(CreateStockDTO dto)
        {
            if (await StockExistsAsync(dto.ProductId))
            {
                throw new Exception("Estoque já existe para este produto.");
            }

            var stock = _mapper.Map<Stock>(dto);
            stock.Id = Guid.NewGuid();
            stock.LastUpdated = DateTime.UtcNow;

            try
            {
                await _repository.CreateAsync(stock);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o estoque no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Atualiza o estoque.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="ArgumentNullException">Objeto invalido.</exception>
        /// <exception cref="Exception">Erro ao atualizar o DB.</exception>
        public async Task UpdateStock(UpdateStockDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var stock = _mapper.Map<Stock>(dto);
            stock.LastUpdated = DateTime.UtcNow;
            stock.MetricUnit = dto.MetricUnit;

            try
            {
                await _repository.UpdateAsync(stock);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o estoque no banco de dados.", ex);
            }
        }

        /// <summary>
        /// Retorna o estoque pelo ID do produto.
        /// </summary>
        /// <param name="productId">Identificador do produto cujo estoque será encontrado.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StockDTO> GetStockByProductId(Guid productId)
        {
            var stock = await _repository.GetByProductIdAsync(productId);
            if (stock == null)
            {
                throw new Exception("Estoque não encontrado.");
            }
            return _mapper.Map<StockDTO>(stock);
        }
    }
}
