using PDV.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDV.Clients.Services.Interfaces
{
    public partial interface IApiClient
    {
        /// <summary>
        /// Busca o relatório de vendas filtrado por um intervalo de datas.
        /// </summary>
        /// <param name="startDate">Data inicial da busca.</param>
        /// <param name="endDate">Data final da busca.</param>
        /// <returns>Uma lista de vendas simplificada (SaleResultDto).</returns>
        Task<IEnumerable<SaleResultDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}