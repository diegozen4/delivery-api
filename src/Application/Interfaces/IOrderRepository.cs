using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task UpdateAsync(Order order);
    Task<IEnumerable<Order>> GetOrdersByClientIdAsync(Guid clientId);
    Task<IEnumerable<Order>> GetOrdersByCommerceIdAsync(Guid commerceId);
    Task<IEnumerable<Order>> GetOrdersByDeliveryUserIdAsync(Guid deliveryUserId);
}
