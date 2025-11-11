using Application.Interfaces;
using AutoMapper;
using Contracts.Orders;
using Contracts.Deliveries; // Añadir esta línea
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
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
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IValidator<PublishOrderRequest> _publishOrderRequestValidator;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICommerceRepository commerceRepository,
        IDeliveryRepository deliveryRepository,
        IValidator<PublishOrderRequest> publishOrderRequestValidator,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _commerceRepository = commerceRepository;
        _deliveryRepository = deliveryRepository;
        _publishOrderRequestValidator = publishOrderRequestValidator;
        _mapper = mapper;
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

    public async Task<IEnumerable<DeliveryOfferDto>> GetOffersForOrderAsync(Guid orderId, Guid ownerId)
    {
        // 1. Retrieve the order
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        // 2. Verify ownership
        if (order.Commerce.UserId != ownerId)
        {
            throw new UnauthorizedAccessException($"User {ownerId} is not authorized to view offers for order {orderId}.");
        }

        // 3. Map and return the offers
        return _mapper.Map<IEnumerable<DeliveryOfferDto>>(order.DeliveryOffers);
    }

    public async Task AcceptOfferAsync(Guid offerId, Guid ownerId)
    {
        // 1. Retrieve the offer and its related order and other offers
        var offer = await _deliveryRepository.GetOfferByIdAsync(offerId);
        if (offer == null)
        {
            throw new ArgumentException($"Offer with ID {offerId} not found.");
        }

        var order = offer.Order;

        // 2. Verify ownership
        var commerce = await _commerceRepository.GetByIdAsync(order.CommerceId);
        if (commerce == null || commerce.UserId != ownerId)
        {
            throw new UnauthorizedAccessException($"User {ownerId} is not authorized to accept offers for order {order.Id}.");
        }

        // 3. Verify order status
        if (order.Status != OrderStatus.AwaitingBids)
        {
            throw new InvalidOperationException($"Order with ID {order.Id} is not awaiting bids.");
        }

        // 4. Update the order
        order.Status = OrderStatus.ReadyForPickup;
        order.DeliveryUserId = offer.DeliveryUserId;

        // 5. Update all offers for the order
        foreach (var o in order.DeliveryOffers)
        {
            o.Status = o.Id == offerId ? OfferStatus.Accepted : OfferStatus.Rejected;
        }

        // 6. Persist all changes
        await _orderRepository.UpdateAsync(order);
    }

    public async Task CancelOrderAsync(Guid orderId, Guid userId, string userRole)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        // Check if the order is already in a final state
        if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException($"Order with ID {orderId} is already in a final state ({order.Status}) and cannot be cancelled.");
        }

        bool canCancel = false;

        if (userRole == "Cliente")
        {
            if (order.ClientId != userId)
            {
                throw new UnauthorizedAccessException($"Client {userId} is not authorized to cancel order {orderId}.");
            }
            // Client can cancel if order is Pending or Confirmed
            if (order.Status == OrderStatus.Pending || order.Status == OrderStatus.Confirmed)
            {
                canCancel = true;
            }
        }
        else if (userRole == "Negocio")
        {
            var commerce = await _commerceRepository.GetByIdAsync(order.CommerceId);
            if (commerce == null || commerce.UserId != userId)
            {
                throw new UnauthorizedAccessException($"Business {userId} is not authorized to cancel order {orderId}.");
            }
            // Business can cancel if order is Pending, Confirmed, InPreparation, or ReadyForPickup
            if (order.Status == OrderStatus.Pending ||
                order.Status == OrderStatus.Confirmed ||
                order.Status == OrderStatus.InPreparation ||
                order.Status == OrderStatus.ReadyForPickup ||
                order.Status == OrderStatus.AwaitingBids)
            {
                canCancel = true;
            }
        }
        // Add Admin role cancellation logic if needed (Admin can cancel at any stage)

        if (!canCancel)
        {
            throw new InvalidOperationException($"Order with ID {orderId} cannot be cancelled by a {userRole} in its current state ({order.Status}).");
        }

        order.Status = OrderStatus.Cancelled;
        await _orderRepository.UpdateAsync(order);
    }

    public async Task<IEnumerable<OrderHistoryItemDto>> GetClientOrderHistoryAsync(Guid clientId)
    {
        var orders = await _orderRepository.GetOrdersByClientIdAsync(clientId);
        return _mapper.Map<IEnumerable<OrderHistoryItemDto>>(orders);
    }

    public async Task<IEnumerable<OrderHistoryItemDto>> GetCommerceOrderHistoryAsync(Guid commerceId)
    {
        var orders = await _orderRepository.GetOrdersByCommerceIdAsync(commerceId);
        return _mapper.Map<IEnumerable<OrderHistoryItemDto>>(orders);
    }

    public async Task<IEnumerable<OrderHistoryItemDto>> GetDeliveryUserOrderHistoryAsync(Guid deliveryUserId)
    {
        var orders = await _orderRepository.GetOrdersByDeliveryUserIdAsync(deliveryUserId);
        return _mapper.Map<IEnumerable<OrderHistoryItemDto>>(orders);
    }
}
