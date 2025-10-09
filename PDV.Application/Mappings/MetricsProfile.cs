using AutoMapper;
using PDV.Application.DTOs.Metrics;
using PDV.Core.Entities;

namespace PDV.Application.Mappings
{
    public class MetricsProfile : Profile
    {
        public MetricsProfile()
        {
            CreateMap<Product, GetBestSellersMetricsDTO>()
                .ForMember(dest => dest.CategoryName,
                            opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Stock, GetBelowStockDTO>()
                .ForMember(dest => dest.ProductName, 
                            opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<GetBelowStockDTO, Stock>();
        }
    }
}
