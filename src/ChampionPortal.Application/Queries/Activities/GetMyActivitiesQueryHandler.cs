using ChampionPortal.Application.Commands.Activities;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Application.Queries.Activities;
using MediatR;

namespace ChampionPortal.Application.Queries.Activities;

public class GetMyActivitiesQueryHandler : IRequestHandler<GetMyActivitiesQuery, MyActivitiesResponseDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IActivityLogRepository _activityLogRepository;

    public GetMyActivitiesQueryHandler(
        ICurrentUserService currentUser,
        IActivityLogRepository activityLogRepository)
    {
        _currentUser = currentUser;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<MyActivitiesResponseDto> Handle(
        GetMyActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var employeeId = _currentUser.EmployeeId
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var logs = await _activityLogRepository.GetByEmployeeIdAsync(employeeId);
        var activities = logs
            .OrderByDescending(l => l.SubmittedOn)
            .Select(l => SubmitActivityCommandHandler.MapToDto(l, l.ActivityType))
            .ToList();

        var totalPoints = activities.Sum(a => a.PointsAwarded ?? 0);
        return new MyActivitiesResponseDto(totalPoints, activities);
    }
}
