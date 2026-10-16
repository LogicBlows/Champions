using ChampionPortal.Application.Interfaces;
using ChampionPortal.Infrastructure.Auth;
using ChampionPortal.Infrastructure.Repositories;
using ChampionPortal.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChampionPortal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICompetencyRepository, CompetencyRepository>();
        services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
        services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
        services.AddScoped<IEndorsementRepository, EndorsementRepository>();
        services.AddScoped<IActivityPointsCalculator, ActivityPointsCalculator>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
