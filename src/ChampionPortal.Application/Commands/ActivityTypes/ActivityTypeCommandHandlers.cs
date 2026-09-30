using ChampionPortal.Application.Commands.ActivityTypes;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Domain.Enums;
using MediatR;

namespace ChampionPortal.Application.Commands.ActivityTypes;

public class ProposeActivityTypeCommandHandler : IRequestHandler<ProposeActivityTypeCommand, ActivityTypeDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IActivityTypeRepository _activityTypeRepository;

    public ProposeActivityTypeCommandHandler(
        ICurrentUserService currentUser,
        IActivityTypeRepository activityTypeRepository)
    {
        _currentUser = currentUser;
        _activityTypeRepository = activityTypeRepository;
    }

    public async Task<ActivityTypeDto> Handle(ProposeActivityTypeCommand request, CancellationToken cancellationToken)
    {
        var employeeId = _currentUser.EmployeeId
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var activityType = new ActivityType
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            SuggestedPoints = request.SuggestedPoints,
            ProposedByEmployeeId = employeeId,
            Status = ActivityTypeStatus.PendingReview,
            IsActive = true,
            SortOrder = 999
        };

        await _activityTypeRepository.AddAsync(activityType);

        return new ActivityTypeDto(
            activityType.Id,
            activityType.Name,
            activityType.BasePoints,
            activityType.Description,
            activityType.SortOrder);
    }
}

public class ReviewActivityTypeCommandHandler : IRequestHandler<ReviewActivityTypeCommand>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IActivityTypeRepository _activityTypeRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IActivityPointsCalculator _pointsCalculator;

    public ReviewActivityTypeCommandHandler(
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

    public async Task Handle(ReviewActivityTypeCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsReviewer)
        {
            throw new UnauthorizedAccessException("Only reviewers can review activity types.");
        }

        var reviewerId = _currentUser.EmployeeId
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var activityType = await _activityTypeRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Activity type not found.");

        if (request.Approved)
        {
            if (request.Points is null or <= 0)
            {
                throw new ArgumentException("Points are required when approving an activity type.");
            }

            activityType.Approve(request.Points.Value, reviewerId);

            var logs = await _activityLogRepository.GetAllWithNullPointsForActivityTypeAsync(activityType.Id);
            foreach (var log in logs)
            {
                log.PointsAwarded = _pointsCalculator.CalculatePoints(activityType);
                await _activityLogRepository.UpdateAsync(log);
            }
        }
        else
        {
            activityType.Reject(reviewerId);
        }

        await _activityTypeRepository.UpdateAsync(activityType);
    }
}
