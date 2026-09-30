using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Commands.ActivityTypes;

public record ProposeActivityTypeCommand(
    string Name,
    string Description,
    int? SuggestedPoints) : IRequest<ActivityTypeDto>;

public record ReviewActivityTypeCommand(
    Guid Id,
    bool Approved,
    int? Points) : IRequest;
