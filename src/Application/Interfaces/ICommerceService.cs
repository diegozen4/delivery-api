using Contracts.Commerces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICommerceService
{
    Task<IEnumerable<CommerceDto>> GetAllCommercesAsync();
    Task<CommerceDto> GetCommerceByIdAsync(Guid commerceId);
    Task<CommerceDto> CreateCommerceAsync(CreateCommerceRequest request);
    Task UpdateCommerceAsync(Guid commerceId, UpdateCommerceRequest request);
    Task DeleteCommerceAsync(Guid commerceId);
    Task AssignCommerceOwnerAsync(Guid commerceId, AssignCommerceOwnerRequest request);
}