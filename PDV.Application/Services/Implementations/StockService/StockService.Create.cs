using PDV.Application.DTOs.Stock;
using PDV.Core.Entities;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class StockService
    {
        /// <summary>
        /// Cria um novo estoque.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="Exception">Estoque já existe ou houve erro ao criar o estoque.</exception>
        public async Task Create(CreateStockDTO dto)
        {
            try
            {
                if (await StockExists(dto.ProductId))
                {
                    throw new StockAlreadyExistsException(dto.ProductId);
                }

                var stock = _mapper.Map<Stock>(dto);
                stock.Id = Guid.NewGuid();
                stock.LastUpdated = DateTime.UtcNow;

                await _repository.CreateAsync(stock);
            }
            catch (StockAlreadyExistsException)
            {
                throw new StockAlreadyExistsException(dto.ProductId);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao criar o estoque no banco de dados.", ex);
            }
        }
    }
}
