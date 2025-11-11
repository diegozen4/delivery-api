using Domain.Entities;

namespace Application.Interfaces;

public interface IDeliveryRepository
{
    Task<IEnumerable<Order>> GetAvailableOrdersAsync();
    Task<IEnumerable<Order>> GetNegotiableOrdersAsync();
    Task AddOfferAsync(DeliveryOffer offer);
    Task<DeliveryOffer?> GetOfferByIdAsync(Guid offerId);
}
