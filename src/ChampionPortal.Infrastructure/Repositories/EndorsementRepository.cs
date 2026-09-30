using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChampionPortal.Infrastructure.Repositories;

public class EndorsementRepository : IEndorsementRepository
{
    private readonly AppDbContext _context;

    public EndorsementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Endorsement endorsement)
    {
        _context.Endorsements.Add(endorsement);
        await _context.SaveChangesAsync();
    }

    public Task<List<Endorsement>> GetByActivityLogIdAsync(Guid activityLogId) =>
        _context.Endorsements
            .Where(e => e.ActivityLogId == activityLogId)
            .OrderByDescending(e => e.EndorsedOn)
            .ToListAsync();
}
