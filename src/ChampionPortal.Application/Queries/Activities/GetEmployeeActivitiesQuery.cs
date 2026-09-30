using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Queries.Activities;

public record GetEmployeeActivitiesQuery(Guid EmployeeId) : IRequest<EmployeeActivitiesResponseDto>;
