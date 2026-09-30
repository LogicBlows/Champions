using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByEmployeeCodeAsync(string employeeCode);
    Task<List<Employee>> GetAllActiveAsync();
}
