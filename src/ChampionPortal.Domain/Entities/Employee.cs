namespace ChampionPortal.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string EmployeeCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Competency { get; set; } = default!;
    public bool IsReviewer { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; }

    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
}
