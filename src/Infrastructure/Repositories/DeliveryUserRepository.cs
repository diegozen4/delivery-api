using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class DeliveryUserRepository : IDeliveryUserRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeliveryUser>> GetAllAsync()
    {
        return await _context.DeliveryUsers
            .Include(du => du.User)
            .ToListAsync();
    }

    public async Task<DeliveryUser?> GetByIdAsync(Guid id)
    {
        return await _context.DeliveryUsers
            .Include(du => du.User)
            .FirstOrDefaultAsync(du => du.Id == id);
    }

    public async Task<DeliveryUser> AddAsync(DeliveryUser deliveryUser)
    {
        _context.DeliveryUsers.Add(deliveryUser);
        await _context.SaveChangesAsync();
        return deliveryUser;
    }

    public async Task UpdateAsync(DeliveryUser deliveryUser)
    {
        _context.Entry(deliveryUser).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var deliveryUser = await _context.DeliveryUsers.FindAsync(id);
        if (deliveryUser != null)
        {
            _context.DeliveryUsers.Remove(deliveryUser);
            await _context.SaveChangesAsync();
        }
    }
}
