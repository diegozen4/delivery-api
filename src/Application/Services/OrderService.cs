using Application.Interfaces;
using Contracts.Orders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Domain.Enums;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICommerceRepository _commerceRepository;
    private readonly IValidator<PublishOrderRequest> _publishOrderRequestValidator;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICommerceRepository commerceRepository,
        IValidator<PublishOrderRequest> publishOrderRequestValidator)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _commerceRepository = commerceRepository;
        _publishOrderRequestValidator = publishOrderRequestValidator;
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

    public async Task PublishOrderAsync(Guid orderId, PublishOrderRequest request, Guid ownerId)
    {
        // 1. Validate the request using FluentValidation
        await _publishOrderRequestValidator.ValidateAndThrowAsync(request);

        // 2. Retrieve the order
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        // 3. Verify ownership (order's commerce must belong to the ownerId)
        var commerce = await _commerceRepository.GetByIdAsync(order.CommerceId);
        if (commerce == null || commerce.UserId != ownerId)
        {
            throw new UnauthorizedAccessException($"Commerce with ID {order.CommerceId} not found or does not belong to user {ownerId}.");
        }

        // 4. Validate order status for publishing
        if (order.Status != OrderStatus.Confirmed && order.Status != OrderStatus.InPreparation)
        {
            throw new InvalidOperationException($"Order with ID {orderId} cannot be published. Current status is {order.Status}.");
        }

        // 5. Update order properties
        order.DispatchMode = request.DispatchMode;
        order.ProposedDeliveryFee = request.ProposedDeliveryFee;

        // 6. Set new status based on dispatch mode
        if (request.DispatchMode == DispatchMode.Market)
        {
            order.Status = OrderStatus.ReadyForPickup;
        }
        else if (request.DispatchMode == DispatchMode.Negotiation)
        {
            order.Status = OrderStatus.AwaitingBids;
        }

        // 7. Persist changes
        await _orderRepository.UpdateAsync(order);
    }
}
