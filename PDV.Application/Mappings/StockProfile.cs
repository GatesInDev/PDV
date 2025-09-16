using AutoMapper;
using PDV.Application.DTOs.Stock;
using PDV.Core.Entities;

public class StockProfile : Profile
{
    public StockProfile()
    {

        CreateMap<CreateStockDTO, Stock>();

        CreateMap<Stock, StockDTO>()
        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
        .ForMember(dest => dest.MetricUnit, opt => opt.MapFrom(src => src.Product.MetricUnit));


    }
}
