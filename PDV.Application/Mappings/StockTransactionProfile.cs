using AutoMapper;
using PDV.Application.DTOs.StockTransaction;
using PDV.Core.Entities;

namespace PDV.Application.Mappings
{
    public class StockTransactionProfile : Profile
    {
        public StockTransactionProfile()
        {
            CreateMap<CreateStockTransactionDTO, StockTransaction>();

            CreateMap<StockTransaction, StockTransactionDTO>();
        }
    }
}
