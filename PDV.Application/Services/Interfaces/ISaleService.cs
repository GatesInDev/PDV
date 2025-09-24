using PDV.Application.DTOs.Sales;

namespace PDV.Application.Services.Interfaces
{
    public interface ISaleService
    {
        public Task CreateSale(CreateSalesDTO dto);
    }
}
