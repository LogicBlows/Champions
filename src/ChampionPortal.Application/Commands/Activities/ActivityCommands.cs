using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Commands.Activities;

public record SubmitActivityCommand(
    Guid ActivityTypeId,
    string Title,
    string? Description) : IRequest<MyActivityDto>;

public record EndorseActivityCommand(
    Guid ActivityLogId,
    string? Comment) : IRequest;
