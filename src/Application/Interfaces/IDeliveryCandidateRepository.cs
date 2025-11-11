using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IDeliveryCandidateRepository
{
    Task AddAsync(DeliveryCandidate candidate);
    Task<DeliveryCandidate?> GetByIdAsync(Guid id);
    Task<DeliveryCandidate?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<DeliveryCandidate>> GetAllPendingAsync();
    Task UpdateAsync(DeliveryCandidate candidate);
}
