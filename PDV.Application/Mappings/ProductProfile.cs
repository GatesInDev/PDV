using AutoMapper;
using PDV.Application.DTOs.Product;
using PDV.Core.Entities;

namespace PDV.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                            opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Product, ProductDetailsDTO>()
                .ForMember(dest => dest.CategoryName,
                            opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateProductDTO, Product>()
                .ForMember(dest => dest.CategoryId, 
                            opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.MetricUnit, 
                            opt => opt.MapFrom(src => src.MetricUnit));

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId))
                .ForPath(dest => dest.Stock.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));
        }
    }
}