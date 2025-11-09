using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommerceRepository : ICommerceRepository
{
    private readonly ApplicationDbContext _context;

    public CommerceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Commerce>> GetAllAsync()
    {
        return await _context.Commerces.ToListAsync();
    }

    public async Task<Commerce?> GetByIdAsync(Guid id)
    {
        return await _context.Commerces.FindAsync(id);
    }

    public async Task<Commerce> AddAsync(Commerce commerce)
    {
        _context.Commerces.Add(commerce);
        await _context.SaveChangesAsync();
        return commerce;
    }

    public async Task UpdateAsync(Commerce commerce)
    {
        _context.Entry(commerce).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var commerce = await _context.Commerces.FindAsync(id);
        if (commerce != null)
        {
            _context.Commerces.Remove(commerce);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsUserOwnerAsync(Guid commerceId, Guid userId)
    {
        return await _context.Commerces.AnyAsync(c => c.Id == commerceId && c.UserId == userId);
    }
}