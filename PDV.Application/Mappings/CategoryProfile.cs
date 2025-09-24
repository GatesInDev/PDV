using AutoMapper;
using PDV.Application.DTOs.Category;
using PDV.Core.Entities;

namespace PDV.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<UpdateCategoryDTO, Category>();

            CreateMap<Category, CategoryDetailsDTO>();

            CreateMap<Category, CategoryDTO>();
        }
    }
}
