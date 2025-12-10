using PDV.Application.DTOs;
using PDV.Application.DTOs.Sales;
using PDV.Core.Entities;
using PDV.Core.Exceptions;
using PDV.Domain;

namespace PDV.Application.Services.Implementations
{
    public partial class SaleService
    {
        /// <summary>
        /// Retorna as vendas em um período.
        /// </summary>
        /// <param name="startDate">Data de inicio do filtro.</param>
        /// <param name="endDate">Data de fim do filtro.</param>
        /// <returns>Uma lista com todas as vendas do periodo.</returns>
        public async Task<List<SaleResultDto>> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    throw new InvalidSalePeriodException(startDate, endDate);
                }

                var list = await _saleRepository.GetByPeriodAsync(startDate, endDate);

                if (!list.Any())
                {
                    return new List<SaleResultDto>();
                }

                var resultDtos = list.Select(sale => MapToDto(sale)).ToList();
                return resultDtos;//_mapper.Map<List<SaleDetailsDTO>>(list);
            }
            catch (InvalidSalePeriodException)
            {
                throw new InvalidSalePeriodException(startDate, endDate);
            }
            catch (NoSalesInPeriodException)
            {
                throw new NoSalesInPeriodException(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("Erro ao requisitar por periodo.", ex);
            }
        }

        private SaleResultDto MapToDto(Sale sale)
        {
            var dto = new SaleResultDto
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalPrice,
                Status = "Concluído",
                CustomerName = sale.Customer != null ? sale.Customer.Name : "Consumidor Final"
            };

            return dto;
        }
    }
}
