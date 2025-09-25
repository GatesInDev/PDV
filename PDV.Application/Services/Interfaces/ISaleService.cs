using PDV.Application.DTOs.Sales;

namespace PDV.Application.Services.Interfaces
{
    public interface ISaleService
    {
        /// <summary>
        /// Cria uma nova venda.
        /// </summary>
        /// <param name="dto">Objeto com os dados a serem criados.</param>
        /// <returns>Sem retorno.</returns>
        public Task CreateSale(CreateSalesDTO dto);

        /// <summary>
        /// Retorna uma venda pelo seu Id.
        /// </summary>
        /// <param name="id">Identificador da venda.</param>
        /// <returns>Objeto com os dados da venda.</returns>
        public Task<SaleDetailsDTO> GetSaleById(Guid id);

        public Task<List<SaleDetailsDTO>> GetSalesByPeriod(DateTime startDate, DateTime endDate);
    }
}
