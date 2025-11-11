using Application.Interfaces;
using Application.Interfaces;
using Contracts.DeliveryGroups;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class DeliveryGroupService : IDeliveryGroupService
{
    private readonly IDeliveryGroupRepository _deliveryGroupRepository;
    private readonly ICommerceRepository _commerceRepository;
    private readonly UserManager<User> _userManager;
    private readonly IDeliveryUserRepository _deliveryUserRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateDeliveryGroupRequest> _createDeliveryGroupRequestValidator;
    private readonly IValidator<UpdateDeliveryGroupRequest> _updateDeliveryGroupRequestValidator;
    private readonly IValidator<AssignDeliveryUserToGroupRequest> _assignDeliveryUserToGroupRequestValidator;

    public DeliveryGroupService(
        IDeliveryGroupRepository deliveryGroupRepository,
        ICommerceRepository commerceRepository,
        UserManager<User> userManager,
        IDeliveryUserRepository deliveryUserRepository,
        IMapper mapper,
        IValidator<CreateDeliveryGroupRequest> createDeliveryGroupRequestValidator,
        IValidator<UpdateDeliveryGroupRequest> updateDeliveryGroupRequestValidator,
        IValidator<AssignDeliveryUserToGroupRequest> assignDeliveryUserToGroupRequestValidator)
    {
        _deliveryGroupRepository = deliveryGroupRepository;
        _commerceRepository = commerceRepository;
        _userManager = userManager;
        _deliveryUserRepository = deliveryUserRepository;
        _mapper = mapper;
        _createDeliveryGroupRequestValidator = createDeliveryGroupRequestValidator;
        _updateDeliveryGroupRequestValidator = updateDeliveryGroupRequestValidator;
        _assignDeliveryUserToGroupRequestValidator = assignDeliveryUserToGroupRequestValidator;
    }

    public async Task<IEnumerable<DeliveryGroupDto>> GetDeliveryGroupsByCommerceIdAsync(Guid commerceId)
    {
        var deliveryGroups = await _deliveryGroupRepository.GetDeliveryGroupsByCommerceIdAsync(commerceId);
        return _mapper.Map<IEnumerable<DeliveryGroupDto>>(deliveryGroups);
    }

    public async Task<DeliveryGroupDto> GetDeliveryGroupByIdAsync(Guid commerceId, Guid groupId)
    {
        var deliveryGroup = await _deliveryGroupRepository.GetByIdAsync(groupId);
        if (deliveryGroup == null || deliveryGroup.CommerceId != commerceId)
        {
            throw new ArgumentException($"Delivery group with ID {groupId} not found in commerce {commerceId}.");
        }
        return _mapper.Map<DeliveryGroupDto>(deliveryGroup);
    }

    public async Task<DeliveryGroupDto> CreateDeliveryGroupAsync(Guid commerceId, CreateDeliveryGroupRequest request)
    {
        await _createDeliveryGroupRequestValidator.ValidateAndThrowAsync(request);

        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }

        var deliveryGroup = _mapper.Map<DeliveryGroup>(request);
        deliveryGroup.Id = Guid.NewGuid();
        deliveryGroup.CommerceId = commerceId;

        var createdDeliveryGroup = await _deliveryGroupRepository.AddAsync(deliveryGroup);
        return _mapper.Map<DeliveryGroupDto>(createdDeliveryGroup);
    }

    public async Task UpdateDeliveryGroupAsync(Guid commerceId, Guid groupId, UpdateDeliveryGroupRequest request)
    {
        await _updateDeliveryGroupRequestValidator.ValidateAndThrowAsync(request);

        var deliveryGroup = await _deliveryGroupRepository.GetByIdAsync(groupId);
        if (deliveryGroup == null || deliveryGroup.CommerceId != commerceId)
        {
            throw new ArgumentException($"Delivery group with ID {groupId} not found in commerce {commerceId}.");
        }

        _mapper.Map(request, deliveryGroup);
        await _deliveryGroupRepository.UpdateAsync(deliveryGroup);
    }

    public async Task DeleteDeliveryGroupAsync(Guid commerceId, Guid groupId)
    {
        var deliveryGroup = await _deliveryGroupRepository.GetByIdAsync(groupId);
        if (deliveryGroup == null || deliveryGroup.CommerceId != commerceId)
        {
            throw new ArgumentException($"Delivery group with ID {groupId} not found in commerce {commerceId}.");
        }

        await _deliveryGroupRepository.DeleteAsync(groupId);
    }

    public async Task AssignDeliveryUserToGroupAsync(Guid commerceId, Guid groupId, AssignDeliveryUserToGroupRequest request)
    {
        await _assignDeliveryUserToGroupRequestValidator.ValidateAndThrowAsync(request);

        var deliveryGroup = await _deliveryGroupRepository.GetByIdAsync(groupId);
        if (deliveryGroup == null || deliveryGroup.CommerceId != commerceId)
        {
            throw new ArgumentException($"Delivery group with ID {groupId} not found in commerce {commerceId}.");
        }

        var deliveryUser = await _deliveryUserRepository.GetByIdAsync(request.DeliveryUserId);
        if (deliveryUser == null)
        {
            throw new ArgumentException($"Delivery user with ID {request.DeliveryUserId} not found.");
        }

        // Check if the delivery user is already in the group
        if (deliveryGroup.DeliveryGroupUsers.Any(dgu => dgu.DeliveryUserId == request.DeliveryUserId))
        {
            throw new InvalidOperationException($"Delivery user with ID {request.DeliveryUserId} is already in group {groupId}.");
        }

        deliveryGroup.DeliveryGroupUsers.Add(new DeliveryGroupUser
        {
            DeliveryGroupId = groupId,
            DeliveryUserId = request.DeliveryUserId
        });

        await _deliveryGroupRepository.UpdateAsync(deliveryGroup);
    }

    public async Task RemoveDeliveryUserFromGroupAsync(Guid commerceId, Guid groupId, Guid deliveryUserId)
    {
        var deliveryGroup = await _deliveryGroupRepository.GetByIdAsync(groupId);
        if (deliveryGroup == null || deliveryGroup.CommerceId != commerceId)
        {
            throw new ArgumentException($"Delivery group with ID {groupId} not found in commerce {commerceId}.");
        }

        var deliveryGroupUser = deliveryGroup.DeliveryGroupUsers.FirstOrDefault(dgu => dgu.DeliveryUserId == deliveryUserId);
        if (deliveryGroupUser == null)
        {
            throw new ArgumentException($"Delivery user with ID {deliveryUserId} not found in group {groupId}.");
        }

        deliveryGroup.DeliveryGroupUsers.Remove(deliveryGroupUser);
        await _deliveryGroupRepository.UpdateAsync(deliveryGroup);
    }
}
