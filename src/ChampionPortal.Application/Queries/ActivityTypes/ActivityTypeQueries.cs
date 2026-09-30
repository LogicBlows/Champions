using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Queries.ActivityTypes;

public record GetApprovedActivityTypesQuery() : IRequest<IReadOnlyList<ActivityTypeDto>>;

public record GetPendingActivityTypesQuery() : IRequest<IReadOnlyList<PendingActivityTypeDto>>;
