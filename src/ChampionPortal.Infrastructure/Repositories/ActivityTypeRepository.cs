using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Domain.Enums;
using ChampionPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChampionPortal.Infrastructure.Repositories;

public class ActivityTypeRepository : IActivityTypeRepository
{
    private readonly AppDbContext _context;

    public ActivityTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<ActivityType?> GetByIdAsync(Guid id) =>
        _context.ActivityTypes.FirstOrDefaultAsync(a => a.Id == id);

    public Task<List<ActivityType>> GetApprovedAsync() =>
        _context.ActivityTypes
            .Where(a => a.Status == ActivityTypeStatus.Approved && a.IsActive)
            .OrderBy(a => a.SortOrder)
            .ToListAsync();

    public Task<List<ActivityType>> GetPendingReviewAsync() =>
        _context.ActivityTypes
            .Where(a => a.Status == ActivityTypeStatus.PendingReview && a.IsActive)
            .OrderBy(a => a.Name)
            .ToListAsync();

    public async Task AddAsync(ActivityType activityType)
    {
        _context.ActivityTypes.Add(activityType);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ActivityType activityType)
    {
        _context.ActivityTypes.Update(activityType);
        await _context.SaveChangesAsync();
    }
}
