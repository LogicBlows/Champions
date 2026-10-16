using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Application.Queries.ActivityTypes;
using ChampionPortal.Domain.Enums;
using MediatR;

namespace ChampionPortal.Application.Queries.ActivityTypes;

public class GetApprovedActivityTypesQueryHandler
    : IRequestHandler<GetApprovedActivityTypesQuery, IReadOnlyList<ActivityTypeDto>>
{
    private readonly IActivityTypeRepository _activityTypeRepository;

    public GetApprovedActivityTypesQueryHandler(IActivityTypeRepository activityTypeRepository)
    {
        _activityTypeRepository = activityTypeRepository;
    }

    public async Task<IReadOnlyList<ActivityTypeDto>> Handle(
        GetApprovedActivityTypesQuery request,
        CancellationToken cancellationToken)
    {
        var types = await _activityTypeRepository.GetApprovedAsync();
        return types
            .Select(t => new ActivityTypeDto(t.Id, t.Name, t.BasePoints, t.Description, t.SortOrder))
            .ToList();
    }
}

public class GetPendingActivityTypesQueryHandler
    : IRequestHandler<GetPendingActivityTypesQuery, IReadOnlyList<PendingActivityTypeDto>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IActivityTypeRepository _activityTypeRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetPendingActivityTypesQueryHandler(
        ICurrentUserService currentUser,
        IActivityTypeRepository activityTypeRepository,
        IEmployeeRepository employeeRepository)
    {
        _currentUser = currentUser;
        _activityTypeRepository = activityTypeRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IReadOnlyList<PendingActivityTypeDto>> Handle(
        GetPendingActivityTypesQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsReviewer && !_currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only reviewers or admins can view pending activity types.");
        }

        var types = await _activityTypeRepository.GetPendingReviewAsync();
        var employees = await _employeeRepository.GetAllActiveAsync();
        var employeeNames = employees.ToDictionary(e => e.Id, e => e.Name);

        return types
            .Select(t => new PendingActivityTypeDto(
                t.Id,
                t.Name,
                t.Description,
                t.SuggestedPoints,
                t.ProposedByEmployeeId is Guid proposedBy && employeeNames.TryGetValue(proposedBy, out var name)
                    ? name
                    : null))
            .ToList();
    }
}
