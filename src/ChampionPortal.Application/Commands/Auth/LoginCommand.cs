using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Commands.Auth;

public record LoginCommand(string EmployeeCode) : IRequest<LoginResponseDto>;
