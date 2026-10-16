using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface ICompetencyRepository
{
    Task<Competency?> GetByIdAsync(Guid id);
    Task<Competency?> GetByHeadEmployeeIdAsync(Guid employeeId);
    Task<List<Competency>> GetAllAsync(bool includeInactive);
    Task<bool> ExistsCodeAsync(string code, Guid? excludeCompetencyId = null);
    Task AddAsync(Competency competency);
    Task UpdateAsync(Competency competency);
}
