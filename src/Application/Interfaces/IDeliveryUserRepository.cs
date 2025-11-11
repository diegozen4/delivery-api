using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IDeliveryUserRepository
{
    Task<IEnumerable<DeliveryUser>> GetAllAsync();
    Task<DeliveryUser?> GetByIdAsync(Guid id);
    Task<DeliveryUser> AddAsync(DeliveryUser deliveryUser);
    Task UpdateAsync(DeliveryUser deliveryUser);
    Task DeleteAsync(Guid id);
}
