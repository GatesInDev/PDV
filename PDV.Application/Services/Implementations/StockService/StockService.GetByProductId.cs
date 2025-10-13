using PDV.Application.DTOs.Stock;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class StockService
    {
        /// <summary>
        /// Retorna o estoque pelo ID do produto.
        /// </summary>
        /// <param name="productId">Identificador do produto cujo estoque será encontrado.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StockDTO> GetByProductId(Guid productId)
        {
            try
            {
                var stock = await _repository.GetByProductIdAsync(productId);

                if (stock == null)
                {
                    throw new StockNotFoundException(productId);
                }

                return _mapper.Map<StockDTO>(stock);
            }
            catch (StockNotFoundException)
            {
                throw new StockNotFoundException(productId);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao obter o estoque do banco de dados.", ex);
            }
        }
    }
}
