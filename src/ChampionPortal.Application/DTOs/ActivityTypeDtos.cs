namespace ChampionPortal.Application.DTOs;

public record ActivityTypeDto(
    Guid Id,
    string Name,
    int? BasePoints,
    string Description,
    int SortOrder);

public record PendingActivityTypeDto(
    Guid Id,
    string Name,
    string Description,
    int? SuggestedPoints,
    string? ProposedByName);

public record ProposeActivityTypeRequestDto(
    string Name,
    string Description,
    int? SuggestedPoints);

public record ReviewActivityTypeRequestDto(
    bool Approved,
    int? Points);
