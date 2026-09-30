using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IEndorsementRepository
{
    Task AddAsync(Endorsement endorsement);
    Task<List<Endorsement>> GetByActivityLogIdAsync(Guid activityLogId);
}
