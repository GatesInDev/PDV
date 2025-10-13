using PDV.Application.DTOs.Sales;
using PDV.Core.Exceptions;

namespace PDV.Application.Services.Implementations
{
    public partial class SaleService
    {
        /// <summary>
        /// Retorna uma venda pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Objeto com os dados da venda.</returns>
        /// <exception cref="Exception">Venda não encontrada.</exception>
        public async Task<SaleDetailsDTO> GetById(Guid id)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(id);

                if (sale == null)
                {
                    throw new SaleNotFoundException(id);
                }

                return _mapper.Map<SaleDetailsDTO>(sale);
            }
            catch (SaleNotFoundException)
            {
                throw new SaleNotFoundException(id);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao obter a venda do banco de dados.", ex);
            }
        }
    }
}
