namespace ChampionPortal.Application.DTOs;

public record LoginRequestDto(string EmployeeCode);

public record LoginResponseDto(
    string Token,
    Guid EmployeeId,
    string Name,
    bool IsReviewer);
