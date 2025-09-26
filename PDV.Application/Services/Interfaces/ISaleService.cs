using PDV.Application.DTOs.Sales;

namespace PDV.Application.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de vendas.
    /// </summary>
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

        /// <summary>
        /// Retorna todas as vendas em um período.
        /// </summary>
        /// <param name="startDate">Data de inicio do filtro.</param>
        /// <param name="endDate">Data de fim do filtro.</param>
        /// <returns>uma lista com objetos de vendas dentro do periodo informado.</returns>
        public Task<List<SaleDetailsDTO>> GetSalesByPeriod(DateTime startDate, DateTime endDate);
    }
}
