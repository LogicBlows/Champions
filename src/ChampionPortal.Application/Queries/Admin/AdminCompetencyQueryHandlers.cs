using ChampionPortal.Application.Admin;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using MediatR;

namespace ChampionPortal.Application.Queries.Admin;

public record GetAdminCompetenciesQuery(bool IncludeInactive = false)
    : IRequest<IReadOnlyList<AdminCompetencyListItemDto>>;

public class GetAdminCompetenciesQueryHandler
    : IRequestHandler<GetAdminCompetenciesQuery, IReadOnlyList<AdminCompetencyListItemDto>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAdminCompetenciesQueryHandler(
        ICurrentUserService currentUser,
        ICompetencyRepository competencyRepository,
        IEmployeeRepository employeeRepository)
    {
        _currentUser = currentUser;
        _competencyRepository = competencyRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IReadOnlyList<AdminCompetencyListItemDto>> Handle(
        GetAdminCompetenciesQuery request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var competencies = await _competencyRepository.GetAllAsync(request.IncludeInactive);
        var employees = await _employeeRepository.GetAllForAdminAsync(includeInactive: true);
        var names = employees.ToDictionary(e => e.Id, e => e.Name);

        return competencies
            .Select(c => new AdminCompetencyListItemDto(
                c.ID,
                c.Code,
                c.Name,
                c.Description,
                c.SortOrder,
                c.isActive,
                c.HeadEmployeeId,
                c.HeadEmployeeId is Guid headId && names.TryGetValue(headId, out var headName)
                    ? headName
                    : null))
            .ToList();
    }
}
