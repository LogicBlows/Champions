using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Application.Queries.Dashboard;
using MediatR;

namespace ChampionPortal.Application.Queries.Dashboard;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetDashboardQueryHandler(
        IActivityLogRepository activityLogRepository,
        IEmployeeRepository employeeRepository)
    {
        _activityLogRepository = activityLogRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var logs = await _activityLogRepository.GetForDashboardAsync(
            request.Search,
            request.RangeStart,
            request.RangeEnd);

        var activeEmployees = await _employeeRepository.GetAllActiveAsync();

        var grouped = logs
            .GroupBy(l => l.EmployeeId)
            .Select(g => new
            {
                EmployeeId = g.Key,
                Name = g.First().Employee.Name,
                TotalPoints = g.Sum(l => l.PointsAwarded ?? 0),
                ActivityCount = g.Count(),
                LastActivity = g.Max(l => l.SubmittedOn)
            })
            .OrderByDescending(x => x.TotalPoints)
            .ThenBy(x => x.Name)
            .ToList();

        var leaderboard = grouped
            .Select((entry, index) => new DashboardLeaderboardEntryDto(
                index + 1,
                entry.EmployeeId,
                entry.Name,
                entry.TotalPoints,
                entry.ActivityCount,
                entry.LastActivity))
            .ToList();

        var topCategory = logs
            .GroupBy(l => l.ActivityType.Name)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault() ?? "—";

        var summary = new DashboardSummaryDto(
            logs.Count,
            logs.Sum(l => l.PointsAwarded ?? 0),
            grouped.Count,
            activeEmployees.Count,
            topCategory);

        return new DashboardDto(summary, leaderboard);
    }
}
