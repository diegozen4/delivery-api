using AutoMapper;
using Contracts.Commerces;
using Contracts.Users; // Para AddressDto
using Domain.Entities;

namespace Application.Common.Mapping;

public class CommerceMappingProfile : Profile
{
    public CommerceMappingProfile()
    {
        CreateMap<Commerce, CommerceDto>()
            .ForMember(dest => dest.OwnerUserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : null))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)); // Mapeo directo de la entidad Address a AddressDto

        CreateMap<CreateCommerceRequest, Commerce>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)); // Mapeo de CreateAddressRequest a Address

        CreateMap<UpdateCommerceRequest, Commerce>()
            .ForMember(dest => dest.Address, opt => opt.Ignore()) // La dirección se actualiza por separado en el servicio
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignorar propiedades nulas en la actualización

        CreateMap<CreateAddressRequest, Address>();
        CreateMap<UpdateAddressRequest, Address>();
        CreateMap<Address, AddressDto>();
    }
}