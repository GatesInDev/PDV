using AutoMapper;
using PDV.Application.DTOs.Sales;
using PDV.Core.Entities;
using PDV.Application.DTOs.Product;
using PDV.Application.DTOs.Customer;

namespace PDV.Application.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDetailsDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.SaleProducts))
            .ForMember(dest => dest.CashOperator, opt => opt.MapFrom(src => src.CashSession.OperatorName))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));

            CreateMap<SaleProduct, SaleProductDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.PriceAtSaleTime, opt => opt.MapFrom(src => src.PriceAtSaleTime))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CreateSalesDTO, Sale>()
            .ForMember(dest => dest.SaleProducts, opt => opt.Ignore()) 
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SaleDate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

            CreateMap<Sale, CustomersAndSalesDTO>();
        }
    }
}
