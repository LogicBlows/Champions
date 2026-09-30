namespace ChampionPortal.Application.DTOs;

public record SubmitActivityRequestDto(
    Guid ActivityTypeId,
    string Title,
    string? Description);

public record EndorseActivityRequestDto(string? Comment);

public record MyActivityDto(
    Guid Id,
    Guid EmployeeId,
    string Title,
    string CategoryName,
    string Status,
    int? PointsAwarded,
    bool IsPendingCategoryReview,
    DateTime SubmittedOn,
    int EndorsementCount);

public record MyActivitiesResponseDto(
    int TotalPoints,
    IReadOnlyList<MyActivityDto> Activities);

public record EmployeeActivitiesResponseDto(
    Guid EmployeeId,
    string EmployeeName,
    int TotalPoints,
    IReadOnlyList<MyActivityDto> Activities);
