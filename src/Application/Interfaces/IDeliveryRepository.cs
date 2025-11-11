using Domain.Entities;

namespace Application.Interfaces;

public interface IDeliveryRepository
{
    Task<IEnumerable<Order>> GetAvailableOrdersAsync();
}
