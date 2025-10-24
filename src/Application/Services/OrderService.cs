using Application.Interfaces;
using Contracts.Orders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICommerceRepository _commerceRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICommerceRepository commerceRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _commerceRepository = commerceRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, Guid clientId)
    {
        // Step 1: Validation
        var commerce = await _commerceRepository.GetByIdAsync(request.CommerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {request.CommerceId} not found.");
        }

        var productDictionary = new Dictionary<Guid, Product>();
        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {item.ProductId} not found.");
            }
            if (product.CommerceId != commerce.Id)
            {
                throw new ArgumentException($"Product with ID {item.ProductId} does not belong to commerce {commerce.Id}.");
            }
            productDictionary.Add(product.Id, product);
        }

        // Step 2: Create Domain Entities
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CommerceId = commerce.Id,
            ClientId = clientId,
            Status = Domain.Enums.OrderStatus.Pending,
            OrderDetails = new List<OrderDetail>()
        };

        decimal totalPrice = 0;

        foreach (var itemRequest in request.Items)
        {
            var product = productDictionary[itemRequest.ProductId];
            var orderDetail = new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = itemRequest.Quantity,
                Price = product.Price // Use price from DB, not from client
            };
            order.OrderDetails.Add(orderDetail);
            totalPrice += orderDetail.Price * orderDetail.Quantity;
        }

        order.TotalPrice = totalPrice;

        // Step 3: Persist
        await _orderRepository.AddAsync(order);

        // Step 4: Map to DTO
        var orderDto = new OrderDto(
            order.Id,
            order.CommerceId,
            order.ClientId,
            order.OrderDetails.Select(od => new OrderItemDto(
                od.Id,
                od.ProductId,
                productDictionary[od.ProductId].Name, // Get name from fetched products
                od.Quantity,
                od.Price
            )).ToList(),
            order.TotalPrice,
            order.Status.ToString(),
            order.CreatedAt
        );

        return orderDto;
    }
}
