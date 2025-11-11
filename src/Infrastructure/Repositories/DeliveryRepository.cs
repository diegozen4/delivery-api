using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAvailableOrdersAsync()
    {
        // Busca pedidos que estén listos para recoger y no tengan un repartidor asignado.
        // Incluye la información del Negocio y del Cliente para usarla en el DTO.
        return await _context.Orders
            .Include(o => o.Commerce)
            .Include(o => o.User) // Cambiado de o.Client a o.User
                .ThenInclude(u => u.Addresses) // Cambiado de c.Addresses a u.Addresses
            .Where(o => o.Status == OrderStatus.ReadyForPickup && o.DeliveryUserId == null)
            .ToListAsync();
    }
}
