using AutoMapper;
using PDV.Application.DTOs.Product;
using PDV.Core.Entities;

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
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

        CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

    }
}
