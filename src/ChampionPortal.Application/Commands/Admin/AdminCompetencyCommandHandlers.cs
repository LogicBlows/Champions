using ChampionPortal.Application.Admin;
using ChampionPortal.Application.DTOs;
using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using MediatR;

namespace ChampionPortal.Application.Commands.Admin;

public class CreateAdminCompetencyCommandHandler
    : IRequestHandler<CreateAdminCompetencyCommand, AdminCompetencyListItemDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ICompetencyRepository _competencyRepository;

    public CreateAdminCompetencyCommandHandler(
        ICurrentUserService currentUser,
        ICompetencyRepository competencyRepository)
    {
        _currentUser = currentUser;
        _competencyRepository = competencyRepository;
    }

    public async Task<AdminCompetencyListItemDto> Handle(
        CreateAdminCompetencyCommand request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var code = request.Code.Trim().ToLowerInvariant();
        if (await _competencyRepository.ExistsCodeAsync(code))
        {
            throw new InvalidOperationException("Competency code already exists.");
        }

        var competency = new Competency
        {
            ID = Guid.NewGuid(),
            Code = code,
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            SortOrder = request.SortOrder,
            isActive = true,
        };

        await _competencyRepository.AddAsync(competency);

        return new AdminCompetencyListItemDto(
            competency.ID,
            competency.Code,
            competency.Name,
            competency.Description,
            competency.SortOrder,
            competency.isActive,
            null,
            null);
    }
}

public class UpdateAdminCompetencyCommandHandler
    : IRequestHandler<UpdateAdminCompetencyCommand, AdminCompetencyListItemDto>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateAdminCompetencyCommandHandler(
        ICurrentUserService currentUser,
        ICompetencyRepository competencyRepository,
        IEmployeeRepository employeeRepository)
    {
        _currentUser = currentUser;
        _competencyRepository = competencyRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<AdminCompetencyListItemDto> Handle(
        UpdateAdminCompetencyCommand request,
        CancellationToken cancellationToken)
    {
        AdminAuthorization.EnsureAdmin(_currentUser);

        var competency = await _competencyRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException("Competency not found.");

        competency.Name = request.Name.Trim();
        competency.Description = request.Description.Trim();
        competency.SortOrder = request.SortOrder;
        competency.isActive = request.IsActive;

        if (!request.IsActive)
        {
            competency.HeadEmployeeId = null;
        }

        await _competencyRepository.UpdateAsync(competency);

        string? headName = null;
        if (competency.HeadEmployeeId is Guid headId)
        {
            var head = await _employeeRepository.GetByIdAsync(headId);
            headName = head?.Name;
        }

        return new AdminCompetencyListItemDto(
            competency.ID,
            competency.Code,
            competency.Name,
            competency.Description,
            competency.SortOrder,
            competency.isActive,
            competency.HeadEmployeeId,
            headName);
    }
}
