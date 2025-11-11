using Contracts.DeliveryGroups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IDeliveryGroupService
{
    Task<IEnumerable<DeliveryGroupDto>> GetDeliveryGroupsByCommerceIdAsync(Guid commerceId);
    Task<DeliveryGroupDto> GetDeliveryGroupByIdAsync(Guid commerceId, Guid groupId);
    Task<DeliveryGroupDto> CreateDeliveryGroupAsync(Guid commerceId, CreateDeliveryGroupRequest request);
    Task UpdateDeliveryGroupAsync(Guid commerceId, Guid groupId, UpdateDeliveryGroupRequest request);
    Task DeleteDeliveryGroupAsync(Guid commerceId, Guid groupId);
    Task AssignDeliveryUserToGroupAsync(Guid commerceId, Guid groupId, AssignDeliveryUserToGroupRequest request);
    Task RemoveDeliveryUserFromGroupAsync(Guid commerceId, Guid groupId, Guid deliveryUserId);
}
