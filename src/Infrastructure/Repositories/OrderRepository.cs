using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Commerce)
            .Include(o => o.User) // Client
            .Include(o => o.DeliveryUser) // Delivery person
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(Guid clientId)
    {
        return await _context.Orders
            .Include(o => o.Commerce)
            .Include(o => o.User) // Cliente
            .Include(o => o.DeliveryUser) // Repartidor
            .Where(o => o.ClientId == clientId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByCommerceIdAsync(Guid commerceId)
    {
        return await _context.Orders
            .Include(o => o.Commerce)
            .Include(o => o.User) // Cliente
            .Include(o => o.DeliveryUser) // Repartidor
            .Where(o => o.CommerceId == commerceId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByDeliveryUserIdAsync(Guid deliveryUserId)
    {
        return await _context.Orders
            .Include(o => o.Commerce)
            .Include(o => o.User) // Cliente
            .Include(o => o.DeliveryUser) // Repartidor
            .Where(o => o.DeliveryUserId == deliveryUserId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}
