using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Commands.Admin;

public record CreateAdminCompetencyCommand(
    string Code,
    string Name,
    string Description,
    int SortOrder) : IRequest<AdminCompetencyListItemDto>;

public record UpdateAdminCompetencyCommand(
    Guid Id,
    string Name,
    string Description,
    int SortOrder,
    bool IsActive) : IRequest<AdminCompetencyListItemDto>;
