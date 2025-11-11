using Application.Interfaces;
using AutoMapper;
using Contracts.Deliveries;
using Domain.Entities;

namespace Application.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IMapper _mapper;

    public DeliveryService(IDeliveryRepository deliveryRepository, IMapper mapper)
    {
        _deliveryRepository = deliveryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AvailableOrderDto>> GetAvailableOrdersAsync()
    {
        var availableOrders = await _deliveryRepository.GetAvailableOrdersAsync();
        
        // Mapear las entidades Order a AvailableOrderDto
        // La lógica de mapeo específica se definirá en un MappingProfile de AutoMapper
        return _mapper.Map<IEnumerable<AvailableOrderDto>>(availableOrders);
    }
}
