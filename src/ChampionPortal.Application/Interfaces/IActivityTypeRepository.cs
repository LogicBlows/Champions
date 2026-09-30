using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IActivityTypeRepository
{
    Task<ActivityType?> GetByIdAsync(Guid id);
    Task<List<ActivityType>> GetApprovedAsync();
    Task<List<ActivityType>> GetPendingReviewAsync();
    Task AddAsync(ActivityType activityType);
    Task UpdateAsync(ActivityType activityType);
}
