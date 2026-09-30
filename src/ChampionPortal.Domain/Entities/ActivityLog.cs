using ChampionPortal.Domain.Enums;

namespace ChampionPortal.Domain.Entities;

public class ActivityLog
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ActivityTypeId { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int? PointsAwarded { get; set; }
    public ActivityLogStatus Status { get; set; } = ActivityLogStatus.Pending;
    public ActivitySource Source { get; set; } = ActivitySource.ManualSelfReport;
    public DateTime SubmittedOn { get; set; }

    public Employee Employee { get; set; } = default!;
    public ActivityType ActivityType { get; set; } = default!;
    public ICollection<Endorsement> Endorsements { get; set; } = new List<Endorsement>();
}
