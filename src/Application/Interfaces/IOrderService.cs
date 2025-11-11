using Contracts.Orders;
using Contracts.Deliveries; // Añadir esta línea
using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, Guid clientId);
    Task PublishOrderAsync(Guid orderId, PublishOrderRequest request, Guid ownerId);
    Task<IEnumerable<DeliveryOfferDto>> GetOffersForOrderAsync(Guid orderId, Guid ownerId);
    Task AcceptOfferAsync(Guid offerId, Guid ownerId);
}
