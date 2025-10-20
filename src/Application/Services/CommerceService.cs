using Application.Interfaces;
using Contracts.Commerces;
using Domain.Entities;

namespace Application.Services;

public class CommerceService : ICommerceService
{
    private readonly ICommerceRepository _commerceRepository;

    public CommerceService(ICommerceRepository commerceRepository)
    {
        _commerceRepository = commerceRepository;
    }

    public async Task<IEnumerable<CommerceDto>> GetAllAsync()
    {
        var commerces = await _commerceRepository.GetAllAsync();
        return commerces.Select(c => new CommerceDto
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address,
            UserId = c.UserId
        });
    }

    public async Task<CommerceDto?> GetByIdAsync(Guid id)
    {
        var commerce = await _commerceRepository.GetByIdAsync(id);
        if (commerce == null)
        {
            return null;
        }
        return new CommerceDto
        {
            Id = commerce.Id,
            Name = commerce.Name,
            Address = commerce.Address,
            UserId = commerce.UserId
        };
    }

    public async Task<CommerceDto> CreateAsync(CreateCommerceRequest request)
    {
        var commerce = new Commerce
        {
            Name = request.Name,
            Address = request.Address,
            UserId = request.UserId
        };

        var createdCommerce = await _commerceRepository.AddAsync(commerce);
        return new CommerceDto
        {
            Id = createdCommerce.Id,
            Name = createdCommerce.Name,
            Address = createdCommerce.Address,
            UserId = createdCommerce.UserId
        };
    }

    public async Task UpdateAsync(Guid id, UpdateCommerceRequest request)
    {
        var commerce = await _commerceRepository.GetByIdAsync(id);
        if (commerce != null)
        {
            commerce.Name = request.Name;
            commerce.Address = request.Address;
            await _commerceRepository.UpdateAsync(commerce);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await _commerceRepository.DeleteAsync(id);
    }
}