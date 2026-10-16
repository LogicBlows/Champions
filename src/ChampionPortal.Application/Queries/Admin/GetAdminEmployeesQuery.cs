using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Queries.Admin;

public record GetAdminEmployeesQuery(bool IncludeInactive = false)
    : IRequest<IReadOnlyList<AdminEmployeeListItemDto>>;
