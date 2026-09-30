using ChampionPortal.Application.Commands.Activities;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using MediatR;

namespace ChampionPortal.Application.Queries.Activities;

public class GetEmployeeActivitiesQueryHandler
    : IRequestHandler<GetEmployeeActivitiesQuery, EmployeeActivitiesResponseDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public GetEmployeeActivitiesQueryHandler(
        IEmployeeRepository employeeRepository,
        IActivityLogRepository activityLogRepository)
    {
        _employeeRepository = employeeRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<EmployeeActivitiesResponseDto> Handle(
        GetEmployeeActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId)
            ?? throw new KeyNotFoundException("Employee not found.");

        var logs = await _activityLogRepository.GetByEmployeeIdAsync(request.EmployeeId);
        var activities = logs
            .Select(l => SubmitActivityCommandHandler.MapToDto(l, l.ActivityType))
            .ToList();

        var totalPoints = activities.Sum(a => a.PointsAwarded ?? 0);
        return new EmployeeActivitiesResponseDto(
            employee.Id,
            employee.Name,
            totalPoints,
            activities);
    }
}
