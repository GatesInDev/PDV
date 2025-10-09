using AutoMapper;
using PDV.Application.DTOs.Stock;
using PDV.Core.Entities;

namespace PDV.Application.Mappings
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<CreateStockDTO, Stock>();

            CreateMap<Stock, StockDTO>()
                .ForMember(dest => dest.ProductName, 
                            opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.MetricUnit, 
                            opt => opt.MapFrom(src => src.Product.MetricUnit));

            CreateMap<UpdateStockDTO, Stock>();
        }
    }
}