using ChampionPortal.Domain.Enums;

namespace ChampionPortal.Domain.Entities;

public class ActivityType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int? BasePoints { get; set; }
    public string Description { get; set; } = default!;
    public ActivityTypeStatus Status { get; set; } = ActivityTypeStatus.PendingReview;
    public Guid? ProposedByEmployeeId { get; set; }
    public int? SuggestedPoints { get; set; }
    public Guid? ReviewedByEmployeeId { get; set; }
    public DateTime? ReviewedOn { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }

    public void Approve(int points, Guid reviewedBy)
    {
        BasePoints = points;
        Status = ActivityTypeStatus.Approved;
        ReviewedByEmployeeId = reviewedBy;
        ReviewedOn = DateTime.UtcNow;
    }

    public void Reject(Guid reviewedBy)
    {
        Status = ActivityTypeStatus.Rejected;
        ReviewedByEmployeeId = reviewedBy;
        ReviewedOn = DateTime.UtcNow;
    }
}
