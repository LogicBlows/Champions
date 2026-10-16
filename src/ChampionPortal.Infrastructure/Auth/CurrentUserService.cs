using System.Security.Claims;
using ChampionPortal.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChampionPortal.Infrastructure.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? EmployeeId
    {
        get
        {
            var sub = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? _httpContextAccessor.HttpContext?.User.FindFirstValue("sub");

            return Guid.TryParse(sub, out var id) ? id : null;
        }
    }

    public bool IsReviewer
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("is_reviewer");
            return bool.TryParse(value, out var isReviewer) && isReviewer;
        }
    }

    public bool IsAdmin
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("is_admin");
            return bool.TryParse(value, out var isAdmin) && isAdmin;
        }
    }
}
