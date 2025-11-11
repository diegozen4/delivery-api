using AutoMapper;
using Contracts.Categories;
using Domain.Entities;

namespace Application.Common.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src.Commerce != null ? src.Commerce.Name : null));

        CreateMap<CreateCategoryRequest, Category>();

        CreateMap<UpdateCategoryRequest, Category>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignorar propiedades nulas en la actualización
    }
}
