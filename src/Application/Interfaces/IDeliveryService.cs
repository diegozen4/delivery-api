using Contracts.Deliveries;

namespace Application.Interfaces;

public interface IDeliveryService
{
    Task<IEnumerable<AvailableOrderDto>> GetAvailableOrdersAsync();
    Task<IEnumerable<NegotiableOrderDto>> GetNegotiableOrdersAsync();
    Task CreateOfferAsync(Guid orderId, CreateOfferRequest request, Guid deliveryUserId);
}
