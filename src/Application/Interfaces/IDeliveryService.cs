using Contracts.Deliveries;

namespace Application.Interfaces;

public interface IDeliveryService
{
    Task<IEnumerable<AvailableOrderDto>> GetAvailableOrdersAsync();
}
