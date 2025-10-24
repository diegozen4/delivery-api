using Contracts.Orders;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, Guid clientId);
}
