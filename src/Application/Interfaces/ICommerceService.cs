using Contracts.Commerces;

namespace Application.Interfaces;

public interface ICommerceService
{
    Task<IEnumerable<CommerceDto>> GetAllAsync();
    Task<CommerceDto?> GetByIdAsync(Guid id);
    Task<CommerceDto> CreateAsync(CreateCommerceRequest request);
    Task UpdateAsync(Guid id, UpdateCommerceRequest request);
    Task DeleteAsync(Guid id);
}