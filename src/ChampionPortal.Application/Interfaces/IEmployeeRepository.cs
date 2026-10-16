using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByEmployeeCodeAsync(string employeeCode);
    Task<List<Employee>> GetAllActiveAsync();
    Task<List<Employee>> GetAllForAdminAsync(bool includeInactive);
    Task<bool> ExistsEmployeeCodeAsync(string employeeCode, Guid? excludeEmployeeId = null);
    Task<bool> ExistsEmailAsync(string email, Guid? excludeEmployeeId = null);
    Task<int> CountActiveAdminsAsync();
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
}
