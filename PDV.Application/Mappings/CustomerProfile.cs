using AutoMapper;
using PDV.Core.Entities;
using PDV.Application.DTOs.Customer;

namespace PDV.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDTO, Customer>();

            CreateMap<Customer, CustomerDTO>();
        }
    }
}
