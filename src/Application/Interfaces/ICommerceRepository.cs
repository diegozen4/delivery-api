using Domain.Entities;

namespace Application.Interfaces;

public interface ICommerceRepository
{
    Task<IEnumerable<Commerce>> GetAllAsync();
    Task<Commerce?> GetByIdAsync(Guid id);
    Task<Commerce> AddAsync(Commerce commerce);
    Task UpdateAsync(Commerce commerce);
    Task DeleteAsync(Guid id);
}