using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Queries.Activities;

public record GetMyActivitiesQuery() : IRequest<MyActivitiesResponseDto>;
