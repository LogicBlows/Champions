using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChampionPortal.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Employee?> GetByIdAsync(Guid id) =>
        _context.Employees
            .Include(e => e.Competency)
            .FirstOrDefaultAsync(e => e.Id == id);

    public Task<Employee?> GetByEmployeeCodeAsync(string employeeCode) =>
        _context.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);

    public Task<List<Employee>> GetAllActiveAsync() =>
        _context.Employees.Where(e => e.IsActive).OrderBy(e => e.Name).ToListAsync();

    public Task<List<Employee>> GetAllForAdminAsync(bool includeInactive) =>
        _context.Employees
            .Include(e => e.Competency)
            .Where(e => includeInactive || e.IsActive)
            .OrderBy(e => e.EmployeeCode)
            .ToListAsync();

    public Task<bool> ExistsEmployeeCodeAsync(string employeeCode, Guid? excludeEmployeeId = null) =>
        _context.Employees.AnyAsync(e =>
            e.EmployeeCode == employeeCode && (excludeEmployeeId == null || e.Id != excludeEmployeeId));

    public Task<bool> ExistsEmailAsync(string email, Guid? excludeEmployeeId = null) =>
        _context.Employees.AnyAsync(e =>
            e.Email == email && (excludeEmployeeId == null || e.Id != excludeEmployeeId));

    public Task<int> CountActiveAdminsAsync() =>
        _context.Employees.CountAsync(e => e.IsActive && e.IsAdmin);

    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
}
