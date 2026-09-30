namespace ChampionPortal.Domain.Entities;

public class Endorsement
{
    public Guid Id { get; set; }
    public Guid ActivityLogId { get; set; }
    public Guid EndorsedByEmployeeId { get; set; }
    public DateTime EndorsedOn { get; set; }
    public string? Comment { get; set; }

    public ActivityLog ActivityLog { get; set; } = default!;
}
