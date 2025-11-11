using AutoMapper;
using Contracts.Users;
using Domain.Entities;

namespace Application.Common.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserProfileDto>();
        CreateMap<User, UserListItemDto>();
        CreateMap<User, UserDetailDto>();
    }
}