using Application.Interfaces;
using AutoMapper;
using Contracts.Deliveries;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOfferRequest> _createOfferRequestValidator;
    private readonly IValidator<UpdateDeliveryStatusRequest> _updateDeliveryStatusRequestValidator;

    public DeliveryService(
        IDeliveryRepository deliveryRepository,
        IOrderRepository orderRepository,
        IMapper mapper,
        IValidator<CreateOfferRequest> createOfferRequestValidator,
        IValidator<UpdateDeliveryStatusRequest> updateDeliveryStatusRequestValidator)
    {
        _deliveryRepository = deliveryRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
        _createOfferRequestValidator = createOfferRequestValidator;
        _updateDeliveryStatusRequestValidator = updateDeliveryStatusRequestValidator;
    }

    public async Task<IEnumerable<AvailableOrderDto>> GetAvailableOrdersAsync()
    {
        var availableOrders = await _deliveryRepository.GetAvailableOrdersAsync();
        return _mapper.Map<IEnumerable<AvailableOrderDto>>(availableOrders);
    }

    public async Task<IEnumerable<NegotiableOrderDto>> GetNegotiableOrdersAsync()
    {
        var negotiableOrders = await _deliveryRepository.GetNegotiableOrdersAsync();
        return _mapper.Map<IEnumerable<NegotiableOrderDto>>(negotiableOrders);
    }

    public async Task CreateOfferAsync(Guid orderId, CreateOfferRequest request, Guid deliveryUserId)
    {
        // 1. Validate the request using FluentValidation
        await _createOfferRequestValidator.ValidateAndThrowAsync(request);

        // 2. Retrieve the order
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        // 3. Verify order status and dispatch mode
        if (order.Status != OrderStatus.AwaitingBids || order.DispatchMode != DispatchMode.Negotiation)
        {
            throw new InvalidOperationException($"Order with ID {orderId} is not available for negotiation.");
        }

        // 4. Check if delivery user has already made an offer for this order
        if (order.DeliveryOffers.Any(o => o.DeliveryUserId == deliveryUserId))
        {
            throw new InvalidOperationException($"Delivery user {deliveryUserId} has already made an offer for order {orderId}.");
        }

        // 5. Create DeliveryOffer entity
        var offer = new DeliveryOffer
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            DeliveryUserId = deliveryUserId,
            OfferAmount = request.OfferAmount,
            Status = OfferStatus.Pending
        };

        // 6. Persist the offer
        await _deliveryRepository.AddOfferAsync(offer);
    }

    public async Task AcceptMarketOrderAsync(Guid orderId, Guid deliveryUserId)
    {
        // 1. Retrieve the order
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        // 2. Verify order status and dispatch mode
        if (order.Status != OrderStatus.ReadyForPickup || order.DispatchMode != DispatchMode.Market)
        {
            throw new InvalidOperationException($"Order with ID {orderId} is not available for market acceptance.");
        }

        // 3. Verify if the order has already been accepted
        if (order.DeliveryUserId != null)
        {
            throw new InvalidOperationException($"Order with ID {orderId} has already been accepted by another delivery user.");
        }

        // 4. Assign the delivery user and update status
        order.DeliveryUserId = deliveryUserId;
        order.Status = OrderStatus.InTransit;

        // 5. Persist changes
        await _orderRepository.UpdateAsync(order);
    }

    public async Task UpdateDeliveryStatusAsync(Guid orderId, UpdateDeliveryStatusRequest request, Guid deliveryUserId)
    {
        // 1. Validate the request using FluentValidation
        await _updateDeliveryStatusRequestValidator.ValidateAndThrowAsync(request);

        // 2. Retrieve the order
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        // 3. Verify ownership (order must be assigned to this delivery user)
        if (order.DeliveryUserId != deliveryUserId)
        {
            throw new UnauthorizedAccessException($"Order with ID {orderId} is not assigned to delivery user {deliveryUserId}.");
        }

        // 4. Validate status transition
        // This is a simplified example. A more robust solution would use a state machine.
        if (request.NewStatus == OrderStatus.Pending ||
            request.NewStatus == OrderStatus.Confirmed ||
            request.NewStatus == OrderStatus.InPreparation || // Corregido de EnPreparacion
            request.NewStatus == OrderStatus.AwaitingBids ||
            request.NewStatus == OrderStatus.ReadyForPickup)
        {
            throw new InvalidOperationException($"Invalid status transition. Cannot set order {orderId} to {request.NewStatus}.");
        }

        // Prevent going backwards in the delivery process
        if (order.Status == OrderStatus.Delivered && request.NewStatus != OrderStatus.Delivered)
        {
            throw new InvalidOperationException($"Order {orderId} is already delivered and cannot be changed to {request.NewStatus}.");
        }
        if (order.Status == OrderStatus.InTransit && request.NewStatus == OrderStatus.ReadyForPickup)
        {
            throw new InvalidOperationException($"Order {orderId} is already in transit and cannot be set to ReadyForPickup.");
        }
        // Add more specific transition rules as needed

        // 5. Update order status
        order.Status = request.NewStatus;

        // 6. Persist changes
        await _orderRepository.UpdateAsync(order);
    }
}

