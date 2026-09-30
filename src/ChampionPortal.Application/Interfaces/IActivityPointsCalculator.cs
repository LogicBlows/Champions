using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IActivityPointsCalculator
{
    int CalculatePoints(ActivityType activityType);
}
