using AutoMapper;
using Contracts.Users;
using Domain.Entities;

namespace Application.Common.Mapping;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<CreateAddressRequest, Address>();
        CreateMap<UpdateAddressRequest, Address>();
    }
}
