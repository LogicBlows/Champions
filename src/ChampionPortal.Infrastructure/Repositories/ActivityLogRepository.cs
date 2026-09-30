using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChampionPortal.Infrastructure.Repositories;

public class ActivityLogRepository : IActivityLogRepository
{
    private readonly AppDbContext _context;

    public ActivityLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<ActivityLog?> GetByIdAsync(Guid id) =>
        _context.ActivityLogs
            .Include(l => l.ActivityType)
            .Include(l => l.Employee)
            .Include(l => l.Endorsements)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task AddAsync(ActivityLog activityLog)
    {
        _context.ActivityLogs.Add(activityLog);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ActivityLog activityLog)
    {
        _context.ActivityLogs.Update(activityLog);
        await _context.SaveChangesAsync();
    }

    public Task<List<ActivityLog>> GetByEmployeeIdAsync(Guid employeeId) =>
        _context.ActivityLogs
            .Include(l => l.ActivityType)
            .Include(l => l.Endorsements)
            .Where(l => l.EmployeeId == employeeId)
            .OrderByDescending(l => l.SubmittedOn)
            .ToListAsync();

    public Task<List<ActivityLog>> GetAllWithNullPointsForActivityTypeAsync(Guid activityTypeId) =>
        _context.ActivityLogs
            .Where(l => l.ActivityTypeId == activityTypeId && l.PointsAwarded == null)
            .ToListAsync();

    public Task<List<ActivityLog>> GetForDashboardAsync(
        string? search,
        DateTime? rangeStart,
        DateTime? rangeEnd)
    {
        var query = _context.ActivityLogs
            .Include(l => l.Employee)
            .Include(l => l.ActivityType)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(l =>
                l.Employee.Name.Contains(term) ||
                l.Title.Contains(term) ||
                l.ActivityType.Name.Contains(term));
        }

        if (rangeStart.HasValue)
        {
            query = query.Where(l => l.SubmittedOn >= rangeStart.Value);
        }

        if (rangeEnd.HasValue)
        {
            query = query.Where(l => l.SubmittedOn <= rangeEnd.Value);
        }

        return query.OrderByDescending(l => l.SubmittedOn).ToListAsync();
    }
}
