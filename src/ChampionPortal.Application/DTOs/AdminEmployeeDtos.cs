namespace ChampionPortal.Application.DTOs;

public record AdminEmployeeListItemDto(
    Guid Id,
    string EmployeeCode,
    string Name,
    string Email,
    Guid CompetencyId,
    string CompetencyName,
    bool IsAdmin,
    bool IsReviewer,
    bool IsActive);

public record AdminEmployeeDetailDto(
    Guid Id,
    string EmployeeCode,
    string Name,
    string Email,
    Guid CompetencyId,
    string CompetencyName,
    bool IsAdmin,
    bool IsReviewer,
    bool IsActive);

public record CreateAdminEmployeeRequestDto(
    string EmployeeCode,
    string Name,
    string Email,
    Guid CompetencyId,
    bool IsReviewer,
    bool IsAdmin);

public record UpdateAdminEmployeeRequestDto(
    string Name,
    string Email,
    Guid CompetencyId,
    bool IsReviewer,
    bool IsAdmin,
    bool IsActive);
