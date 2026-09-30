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
        _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

    public Task<Employee?> GetByEmployeeCodeAsync(string employeeCode) =>
        _context.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);

    public Task<List<Employee>> GetAllActiveAsync() =>
        _context.Employees.Where(e => e.IsActive).OrderBy(e => e.Name).ToListAsync();
}
