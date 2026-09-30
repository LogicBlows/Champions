namespace ChampionPortal.Application.DTOs;

public record DashboardSummaryDto(
    int TotalActivities,
    int TotalPoints,
    int ParticipatingEmployees,
    int TotalActiveEmployees,
    string TopCategory);

public record DashboardLeaderboardEntryDto(
    int Rank,
    Guid EmployeeId,
    string Name,
    int TotalPoints,
    int ActivityCount,
    DateTime? LastActivity);

public record DashboardDto(
    DashboardSummaryDto Summary,
    IReadOnlyList<DashboardLeaderboardEntryDto> Leaderboard);
