using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Infrastructure.Services;

public class ActivityPointsCalculator : IActivityPointsCalculator
{
    public int CalculatePoints(ActivityType activityType) =>
        activityType.BasePoints ?? 0;
}
