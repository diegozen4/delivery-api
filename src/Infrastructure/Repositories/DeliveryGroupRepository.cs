using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class DeliveryGroupRepository : IDeliveryGroupRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryGroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeliveryGroup>> GetAllAsync()
    {
        return await _context.DeliveryGroups
            .Include(dg => dg.Commerce)
            .Include(dg => dg.DeliveryGroupUsers)
                .ThenInclude(dgu => dgu.DeliveryUser)
                    .ThenInclude(du => du.User)
            .ToListAsync();
    }

    public async Task<DeliveryGroup?> GetByIdAsync(Guid id)
    {
        return await _context.DeliveryGroups
            .Include(dg => dg.Commerce)
            .Include(dg => dg.DeliveryGroupUsers)
                .ThenInclude(dgu => dgu.DeliveryUser)
                    .ThenInclude(du => du.User)
            .FirstOrDefaultAsync(dg => dg.Id == id);
    }

    public async Task<IEnumerable<DeliveryGroup>> GetDeliveryGroupsByCommerceIdAsync(Guid commerceId)
    {
        return await _context.DeliveryGroups
            .Where(dg => dg.CommerceId == commerceId)
            .Include(dg => dg.Commerce)
            .Include(dg => dg.DeliveryGroupUsers)
                .ThenInclude(dgu => dgu.DeliveryUser)
                    .ThenInclude(du => du.User)
            .ToListAsync();
    }

    public async Task<DeliveryGroup> AddAsync(DeliveryGroup deliveryGroup)
    {
        _context.DeliveryGroups.Add(deliveryGroup);
        await _context.SaveChangesAsync();
        return deliveryGroup;
    }

    public async Task UpdateAsync(DeliveryGroup deliveryGroup)
    {
        _context.Entry(deliveryGroup).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var deliveryGroup = await _context.DeliveryGroups.FindAsync(id);
        if (deliveryGroup != null)
        {
            _context.DeliveryGroups.Remove(deliveryGroup);
            await _context.SaveChangesAsync();
        }
    }
}
