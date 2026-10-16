using ChampionPortal.Application.Interfaces;
using ChampionPortal.Domain.Entities;
using ChampionPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChampionPortal.Infrastructure.Repositories;

public class CompetencyRepository : ICompetencyRepository
{
    private readonly AppDbContext _context;

    public CompetencyRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Competency?> GetByIdAsync(Guid id) =>
        _context.Competencies.FirstOrDefaultAsync(c => c.ID == id);

    public Task<Competency?> GetByHeadEmployeeIdAsync(Guid employeeId) =>
        _context.Competencies.FirstOrDefaultAsync(c => c.HeadEmployeeId == employeeId);

    public Task<List<Competency>> GetAllAsync(bool includeInactive) =>
        _context.Competencies
            .Where(c => includeInactive || c.isActive)
            .OrderBy(c => c.SortOrder)
            .ToListAsync();

    public Task<bool> ExistsCodeAsync(string code, Guid? excludeCompetencyId = null) =>
        _context.Competencies.AnyAsync(c =>
            c.Code == code && (excludeCompetencyId == null || c.ID != excludeCompetencyId));

    public async Task AddAsync(Competency competency)
    {
        _context.Competencies.Add(competency);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Competency competency)
    {
        _context.Competencies.Update(competency);
        await _context.SaveChangesAsync();
    }
}
