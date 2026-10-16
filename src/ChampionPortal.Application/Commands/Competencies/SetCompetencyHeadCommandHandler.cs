using ChampionPortal.Application.Interfaces;
using MediatR;

namespace ChampionPortal.Application.Commands.Competencies;

public class SetCompetencyHeadCommandHandler : IRequestHandler<SetCompetencyHeadCommand>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public SetCompetencyHeadCommandHandler(
        ICurrentUserService currentUser,
        ICompetencyRepository competencyRepository,
        IEmployeeRepository employeeRepository)
    {
        _currentUser = currentUser;
        _competencyRepository = competencyRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task Handle(SetCompetencyHeadCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only admins can assign competency heads.");
        }

        var competency = await _competencyRepository.GetByIdAsync(request.CompetencyId)
            ?? throw new KeyNotFoundException("Competency not found.");

        if (!competency.isActive)
        {
            throw new InvalidOperationException("Cannot assign a head to an inactive competency.");
        }

        if (request.EmployeeId is null)
        {
            competency.HeadEmployeeId = null;
            await _competencyRepository.UpdateAsync(competency);
            return;
        }

        var employeeId = request.EmployeeId.Value;
        var employee = await _employeeRepository.GetByIdAsync(employeeId)
            ?? throw new KeyNotFoundException("Employee not found.");

        if (!employee.IsActive)
        {
            throw new InvalidOperationException("Cannot assign an inactive employee as competency head.");
        }

        if (employee.CompetencyId != competency.ID)
        {
            throw new InvalidOperationException("Competency head must belong to the same competency.");
        }

        var priorCompetencyForEmployee = await _competencyRepository.GetByHeadEmployeeIdAsync(employeeId);
        if (priorCompetencyForEmployee is not null && priorCompetencyForEmployee.ID != competency.ID)
        {
            priorCompetencyForEmployee.HeadEmployeeId = null;
            await _competencyRepository.UpdateAsync(priorCompetencyForEmployee);
        }

        competency.HeadEmployeeId = employeeId;
        employee.IsReviewer = true;

        await _competencyRepository.UpdateAsync(competency);
        await _employeeRepository.UpdateAsync(employee);
    }
}
