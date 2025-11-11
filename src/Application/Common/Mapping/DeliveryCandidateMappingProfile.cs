using AutoMapper;
using Contracts.Users;
using Domain.Entities;

namespace Application.Common.Mapping;

public class DeliveryCandidateMappingProfile : Profile
{
    public DeliveryCandidateMappingProfile()
    {
        CreateMap<DeliveryCandidate, DeliveryCandidateDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
    }
}
