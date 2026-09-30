using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Queries.Dashboard;

public record GetDashboardQuery(
    string? Search,
    DateTime? RangeStart,
    DateTime? RangeEnd) : IRequest<DashboardDto>;
