using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class DeliveryCandidateRepository : IDeliveryCandidateRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryCandidateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(DeliveryCandidate candidate)
    {
        await _context.DeliveryCandidates.AddAsync(candidate);
        await _context.SaveChangesAsync();
    }

    public async Task<DeliveryCandidate?> GetByIdAsync(Guid id)
    {
        return await _context.DeliveryCandidates
            .Include(dc => dc.User)
            .FirstOrDefaultAsync(dc => dc.Id == id);
    }

    public async Task<DeliveryCandidate?> GetByUserIdAsync(Guid userId)
    {
        return await _context.DeliveryCandidates
            .Include(dc => dc.User)
            .FirstOrDefaultAsync(dc => dc.UserId == userId);
    }

    public async Task<IEnumerable<DeliveryCandidate>> GetAllPendingAsync()
    {
        return await _context.DeliveryCandidates
            .Include(dc => dc.User)
            .Where(dc => dc.Status == ApplicationStatus.Pending)
            .ToListAsync();
    }

    public async Task UpdateAsync(DeliveryCandidate candidate)
    {
        _context.DeliveryCandidates.Update(candidate);
        await _context.SaveChangesAsync();
    }
}
