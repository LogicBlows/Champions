using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IActivityLogRepository
{
    Task<ActivityLog?> GetByIdAsync(Guid id);
    Task AddAsync(ActivityLog activityLog);
    Task UpdateAsync(ActivityLog activityLog);
    Task<List<ActivityLog>> GetByEmployeeIdAsync(Guid employeeId);
    Task<List<ActivityLog>> GetAllWithNullPointsForActivityTypeAsync(Guid activityTypeId);
    Task<List<ActivityLog>> GetForDashboardAsync(string? search, DateTime? rangeStart, DateTime? rangeEnd);
}
