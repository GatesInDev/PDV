using PDV.Application.DTOs.Stock;
using PDV.Core.Entities;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class StockService
    {
        /// <summary>
        /// Atualiza o estoque.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem atualizados.</param>
        /// <returns>Sem retorno.</returns>
        /// <exception cref="ArgumentNullException">Objeto invalido.</exception>
        /// <exception cref="Exception">Erro ao atualizar o DB.</exception>
        public async Task Update(UpdateStockDTO dto)
        {
            try
            {
                if (dto != null)
                {
                    var stock = _mapper.Map<Stock>(dto);
                    stock.LastUpdated = DateTime.UtcNow;
                    stock.MetricUnit = dto.MetricUnit;

                    await _repository.UpdateAsync(stock);
                }
                else
                {
                    throw new ArgumentNullException(nameof(dto));
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Dados inválidos fornecidos para atualizar o estoque.", ex);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao atualizar o estoque no banco de dados.", ex);
            }
        }
    }
}
