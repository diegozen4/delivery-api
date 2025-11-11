using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IDeliveryGroupRepository
{
    Task<IEnumerable<DeliveryGroup>> GetAllAsync();
    Task<DeliveryGroup?> GetByIdAsync(Guid id);
    Task<IEnumerable<DeliveryGroup>> GetDeliveryGroupsByCommerceIdAsync(Guid commerceId);
    Task<DeliveryGroup> AddAsync(DeliveryGroup deliveryGroup);
    Task UpdateAsync(DeliveryGroup deliveryGroup);
    Task DeleteAsync(Guid id);
}
