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

    public DeliveryService(
        IDeliveryRepository deliveryRepository,
        IOrderRepository orderRepository,
        IMapper mapper,
        IValidator<CreateOfferRequest> createOfferRequestValidator)
    {
        _deliveryRepository = deliveryRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
        _createOfferRequestValidator = createOfferRequestValidator;
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
}
