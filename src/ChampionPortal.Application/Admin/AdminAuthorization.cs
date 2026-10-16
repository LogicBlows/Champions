using ChampionPortal.Application.Interfaces;

namespace ChampionPortal.Application.Admin;

internal static class AdminAuthorization
{
    public static void EnsureAdmin(ICurrentUserService currentUser)
    {
        if (!currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only admins can perform this action.");
        }
    }
}
