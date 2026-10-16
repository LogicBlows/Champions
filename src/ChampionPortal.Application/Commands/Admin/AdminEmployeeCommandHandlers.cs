using ChampionPortal.Application.Admin;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using MediatR;

namespace ChampionPortal.Application.Commands.Admin;

public class CreateAdminEmployeeCommandHandler
    : IRequestHandler<CreateAdminEmployeeCommand, AdminEmployeeDetailDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompetencyRepository _competencyRepository;

    public CreateAdminEmployeeCommandHandler(
        ICurrentUserService currentUser,
        IEmployeeRepository employeeRepository,
        ICompetencyRepository competencyRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
        _competencyRepository = competencyRepository;
    }

    public async Task<AdminEmployeeDetailDto> Handle(
        CreateAdminEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        if (await _employeeRepository.ExistsEmployeeCodeAsync(request.EmployeeCode))
        {
            throw new InvalidOperationException("Employee code already exists.");
        }

        if (await _employeeRepository.ExistsEmailAsync(request.Email))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        var competency = await _competencyRepository.GetByIdAsync(request.CompetencyId)
            ?? throw new KeyNotFoundException("Competency not found.");

        if (!competency.isActive)
        {
            throw new InvalidOperationException("Cannot assign an inactive competency.");
        }

        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeCode = request.EmployeeCode.Trim(),
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            CompetencyId = request.CompetencyId,
            IsReviewer = request.IsReviewer,
            IsAdmin = request.IsAdmin,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
        };

        await _employeeRepository.AddAsync(employee);

        employee = await _employeeRepository.GetByIdAsync(employee.Id)
            ?? throw new InvalidOperationException("Failed to load created employee.");

        return MapDetail(employee);
    }

    internal static AdminEmployeeDetailDto MapDetail(Employee employee) =>
        new(
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

public class UpdateAdminEmployeeCommandHandler
    : IRequestHandler<UpdateAdminEmployeeCommand, AdminEmployeeDetailDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompetencyRepository _competencyRepository;

    public UpdateAdminEmployeeCommandHandler(
        ICurrentUserService currentUser,
        IEmployeeRepository employeeRepository,
        ICompetencyRepository competencyRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
        _competencyRepository = competencyRepository;
    }

    public async Task<AdminEmployeeDetailDto> Handle(
        UpdateAdminEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var employee = await _employeeRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Employee not found.");

        if (await _employeeRepository.ExistsEmailAsync(request.Email, request.Id))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        var competency = await _competencyRepository.GetByIdAsync(request.CompetencyId)
            ?? throw new KeyNotFoundException("Competency not found.");

        if (!competency.isActive)
        {
            throw new InvalidOperationException("Cannot assign an inactive competency.");
        }

        if (employee.IsAdmin && !request.IsAdmin)
        {
            var adminCount = await _employeeRepository.CountActiveAdminsAsync();
            if (adminCount <= 1)
            {
                throw new InvalidOperationException("Cannot remove the last active admin.");
            }
        }

        if (_currentUser.EmployeeId == employee.Id && !request.IsActive)
        {
            throw new InvalidOperationException("You cannot deactivate your own account.");
        }

        if (_currentUser.EmployeeId == employee.Id && !request.IsAdmin)
        {
            throw new InvalidOperationException("You cannot remove your own admin role.");
        }

        var competencyChanged = employee.CompetencyId != request.CompetencyId;
        employee.Name = request.Name.Trim();
        employee.Email = request.Email.Trim();
        employee.CompetencyId = request.CompetencyId;
        employee.IsReviewer = request.IsReviewer;
        employee.IsAdmin = request.IsAdmin;
        employee.IsActive = request.IsActive;

        if (!request.IsActive || competencyChanged)
        {
            var headed = await _competencyRepository.GetByHeadEmployeeIdAsync(employee.Id);
            if (headed is not null)
            {
                headed.HeadEmployeeId = null;
                await _competencyRepository.UpdateAsync(headed);
            }
        }

        await _employeeRepository.UpdateAsync(employee);

        employee = await _employeeRepository.GetByIdAsync(employee.Id)
            ?? throw new InvalidOperationException("Failed to load updated employee.");

        return CreateAdminEmployeeCommandHandler.MapDetail(employee);
    }
}

public class DeactivateAdminEmployeeCommandHandler : IRequestHandler<DeactivateAdminEmployeeCommand>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompetencyRepository _competencyRepository;

    public DeactivateAdminEmployeeCommandHandler(
        ICurrentUserService currentUser,
        IEmployeeRepository employeeRepository,
        ICompetencyRepository competencyRepository)
    {
        _currentUser = currentUser;
        _employeeRepository = employeeRepository;
        _competencyRepository = competencyRepository;
    }

    public async Task Handle(DeactivateAdminEmployeeCommand request, CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var employee = await _employeeRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Employee not found.");

        if (_currentUser.EmployeeId == employee.Id)
        {
            throw new InvalidOperationException("You cannot deactivate your own account.");
        }

        if (employee.IsAdmin)
        {
            var adminCount = await _employeeRepository.CountActiveAdminsAsync();
            if (adminCount <= 1)
            {
                throw new InvalidOperationException("Cannot deactivate the last active admin.");
            }
        }

        employee.IsActive = false;
        employee.IsAdmin = false;

        var headed = await _competencyRepository.GetByHeadEmployeeIdAsync(employee.Id);
        if (headed is not null)
        {
            headed.HeadEmployeeId = null;
            await _competencyRepository.UpdateAsync(headed);
        }

        await _employeeRepository.UpdateAsync(employee);
    }
}
