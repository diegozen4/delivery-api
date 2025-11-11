using AutoMapper;
using Contracts.DeliveryGroups;
using Contracts.Users; // For UserListItemDto
using Domain.Entities;
using System.Linq;

namespace Application.Common.Mapping;

public class DeliveryGroupMappingProfile : Profile
{
    public DeliveryGroupMappingProfile()
    {
        CreateMap<DeliveryGroup, DeliveryGroupDto>()
            .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src.Commerce != null ? src.Commerce.Name : null))
            .ForMember(dest => dest.DeliveryUsers, opt => opt.MapFrom(src => src.DeliveryGroupUsers.Select(dgu => dgu.DeliveryUser.User)));

        CreateMap<CreateDeliveryGroupRequest, DeliveryGroup>();

        CreateMap<UpdateDeliveryGroupRequest, DeliveryGroup>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignorar propiedades nulas en la actualización
        
        CreateMap<User, UserListItemDto>(); // Needed for mapping DeliveryUser.User to UserListItemDto
    }
}
