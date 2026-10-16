using ChampionPortal.Application.Admin;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using MediatR;

namespace ChampionPortal.Application.Queries.Admin;

public class GetAdminEmployeesQueryHandler
    : IRequestHandler<GetAdminEmployeesQuery, IReadOnlyList<AdminEmployeeListItemDto>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAdminEmployeesQueryHandler(
        ICurrentUserService currentUser,
        IEmployeeRepository employeeRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
    }

    public async Task<IReadOnlyList<AdminEmployeeListItemDto>> Handle(
        GetAdminEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var employees = await _employeeRepository.GetAllForAdminAsync(request.IncludeInactive);
        return employees
            .Select(e => new AdminEmployeeListItemDto(
                e.Id,
                e.EmployeeCode,
                e.Name,
                e.Email,
                e.CompetencyId,
                e.Competency.Name,
                e.IsAdmin,
                e.IsReviewer,
                e.IsActive))
            .ToList();
    }
}

public record GetAdminEmployeeByIdQuery(Guid Id) : IRequest<AdminEmployeeDetailDto>;

public class GetAdminEmployeeByIdQueryHandler : IRequestHandler<GetAdminEmployeeByIdQuery, AdminEmployeeDetailDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAdminEmployeeByIdQueryHandler(
        ICurrentUserService currentUser,
        IEmployeeRepository employeeRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
    }

    public async Task<AdminEmployeeDetailDto> Handle(
        GetAdminEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var employee = await _employeeRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Employee not found.");

        return new AdminEmployeeDetailDto(
            employee.Id,
            employee.EmployeeCode,
            employee.Name,
            employee.Email,
            employee.CompetencyId,
            employee.Competency.Name,
            employee.IsAdmin,
            employee.IsReviewer,
            employee.IsActive);
    }
}
