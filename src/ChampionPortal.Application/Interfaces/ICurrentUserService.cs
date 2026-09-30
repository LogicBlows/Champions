namespace ChampionPortal.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? EmployeeId { get; }
    bool IsReviewer { get; }
}
