using ChampionPortal.Application.Commands.Activities;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Domain.Enums;
using MediatR;

namespace ChampionPortal.Application.Commands.Activities;

public class SubmitActivityCommandHandler : IRequestHandler<SubmitActivityCommand, MyActivityDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IActivityTypeRepository _activityTypeRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IActivityPointsCalculator _pointsCalculator;

    public SubmitActivityCommandHandler(
        ICurrentUserService currentUser,
        IActivityTypeRepository activityTypeRepository,
        IActivityLogRepository activityLogRepository,
        IActivityPointsCalculator pointsCalculator)
    {
        _currentUser = currentUser;
        _activityTypeRepository = activityTypeRepository;
        _activityLogRepository = activityLogRepository;
        _pointsCalculator = pointsCalculator;
    }

    public async Task<MyActivityDto> Handle(SubmitActivityCommand request, CancellationToken cancellationToken)
    {
        var employeeId = _currentUser.EmployeeId
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var activityType = await _activityTypeRepository.GetByIdAsync(request.ActivityTypeId)
            ?? throw new KeyNotFoundException("Activity type not found.");

        int? pointsAwarded = activityType.Status == ActivityTypeStatus.Approved
            ? _pointsCalculator.CalculatePoints(activityType)
            : null;

        var activityLog = new ActivityLog
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            ActivityTypeId = activityType.Id,
            Title = request.Title,
            Description = request.Description,
            PointsAwarded = pointsAwarded,
            Status = ActivityLogStatus.Pending,
            Source = ActivitySource.ManualSelfReport,
            SubmittedOn = DateTime.UtcNow
        };

        await _activityLogRepository.AddAsync(activityLog);

        return MapToDto(activityLog, activityType);
    }

    internal static MyActivityDto MapToDto(ActivityLog log, ActivityType activityType) =>
        new(
            log.Id,
            log.EmployeeId,
            log.Title,
            activityType.Name,
            log.Status.ToString(),
            log.PointsAwarded,
            activityType.Status != ActivityTypeStatus.Approved,
            log.SubmittedOn,
            log.Endorsements?.Count ?? 0);
}

public class EndorseActivityCommandHandler : IRequestHandler<EndorseActivityCommand>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IEndorsementRepository _endorsementRepository;

    public EndorseActivityCommandHandler(
        ICurrentUserService currentUser,
        IActivityLogRepository activityLogRepository,
        IEndorsementRepository endorsementRepository)
    {
        _currentUser = currentUser;
        _activityLogRepository = activityLogRepository;
        _endorsementRepository = endorsementRepository;
    }

    public async Task Handle(EndorseActivityCommand request, CancellationToken cancellationToken)
    {
        var employeeId = _currentUser.EmployeeId
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var activityLog = await _activityLogRepository.GetByIdAsync(request.ActivityLogId)
            ?? throw new KeyNotFoundException("Activity log not found.");

        if (activityLog.EmployeeId == employeeId)
        {
            throw new UnauthorizedAccessException("You cannot endorse your own activity.");
        }

        var endorsement = new Endorsement
        {
            Id = Guid.NewGuid(),
            ActivityLogId = activityLog.Id,
            EndorsedByEmployeeId = employeeId,
            EndorsedOn = DateTime.UtcNow,
            Comment = request.Comment
        };

        activityLog.Status = ActivityLogStatus.Endorsed;

        await _endorsementRepository.AddAsync(endorsement);
        await _activityLogRepository.UpdateAsync(activityLog);
    }
}
