using ChampionPortal.Domain.Entities;

namespace ChampionPortal.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Employee employee);
}
