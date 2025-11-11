using AutoMapper;
using Contracts.Products;
using Domain.Entities;

namespace Application.Common.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src.Commerce != null ? src.Commerce.Name : null))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

        CreateMap<CreateProductRequest, Product>();

        CreateMap<UpdateProductRequest, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignorar propiedades nulas en la actualización
    }
}
