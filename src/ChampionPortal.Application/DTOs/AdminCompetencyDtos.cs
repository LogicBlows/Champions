namespace ChampionPortal.Application.DTOs;

public record AdminCompetencyListItemDto(
    Guid Id,
    string Code,
    string Name,
    string Description,
    int SortOrder,
    bool IsActive,
    Guid? HeadEmployeeId,
    string? HeadEmployeeName);

public record CreateAdminCompetencyRequestDto(
    string Code,
    string Name,
    string Description,
    int SortOrder);

public record UpdateAdminCompetencyRequestDto(
    string Name,
    string Description,
    int SortOrder,
    bool IsActive);
