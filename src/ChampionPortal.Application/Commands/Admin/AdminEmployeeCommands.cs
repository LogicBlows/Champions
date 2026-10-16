using ChampionPortal.Application.DTOs;
using MediatR;

namespace ChampionPortal.Application.Commands.Admin;

public record CreateAdminEmployeeCommand(
    string EmployeeCode,
    string Name,
    string Email,
    Guid CompetencyId,
    bool IsReviewer,
    bool IsAdmin) : IRequest<AdminEmployeeDetailDto>;

public record UpdateAdminEmployeeCommand(
    Guid Id,
    string Name,
    string Email,
    Guid CompetencyId,
    bool IsReviewer,
    bool IsAdmin,
    bool IsActive) : IRequest<AdminEmployeeDetailDto>;

public record DeactivateAdminEmployeeCommand(Guid Id) : IRequest;
