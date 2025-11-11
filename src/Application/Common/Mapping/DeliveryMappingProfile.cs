using AutoMapper;
using Contracts.Deliveries;
using Contracts.Orders; // Añadir esta línea
using Domain.Entities;
using Domain.Enums;
using System.Linq;

namespace Application.Common.Mapping;

public class DeliveryMappingProfile : Profile
{
    public DeliveryMappingProfile()
    {
        CreateMap<Order, AvailableOrderDto>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src.Commerce.Name))
            .ForMember(dest => dest.CommerceAddress, opt => opt.MapFrom(src => src.Commerce.Address)) // Asumiendo que Commerce tiene una propiedad Address
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.ClientAddress, opt => opt.MapFrom(src => 
                // Busca la dirección por defecto del usuario o toma la primera que encuentre.
                src.User.Addresses.FirstOrDefault(a => a.IsDefault) != null 
                ? $"{src.User.Addresses.FirstOrDefault(a => a.IsDefault).Street}, {src.User.Addresses.FirstOrDefault(a => a.IsDefault).City}"
                : (src.User.Addresses.Any() 
                    ? $"{src.User.Addresses.First().Street}, {src.User.Addresses.First().City}" 
                    : "Dirección no especificada")
            ));

        CreateMap<Order, NegotiableOrderDto>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src.Commerce.Name))
            .ForMember(dest => dest.CommerceAddress, opt => opt.MapFrom(src => src.Commerce.Address))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.ProposedDeliveryFee, opt => opt.MapFrom(src => src.ProposedDeliveryFee))
            .ForMember(dest => dest.ClientAddress, opt => opt.MapFrom(src =>
                src.User.Addresses.FirstOrDefault(a => a.IsDefault) != null
                ? $"{src.User.Addresses.FirstOrDefault(a => a.IsDefault).Street}, {src.User.Addresses.FirstOrDefault(a => a.IsDefault).City}"
                : (src.User.Addresses.Any()
                    ? $"{src.User.Addresses.First().Street}, {src.User.Addresses.First().City}"
                    : "Dirección no especificada")
            ));

        CreateMap<DeliveryOffer, DeliveryOfferDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Order, OrderHistoryItemDto>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CommerceName, opt => opt.MapFrom(src => src.Commerce.Name))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.DeliveryUserName, opt => opt.MapFrom(src => src.DeliveryUser != null ? $"{src.DeliveryUser.FirstName} {src.DeliveryUser.LastName}" : null))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.DispatchMode, opt => opt.MapFrom(src => src.DispatchMode))
            .ForMember(dest => dest.ProposedDeliveryFee, opt => opt.MapFrom(src => src.ProposedDeliveryFee));
    }
}
