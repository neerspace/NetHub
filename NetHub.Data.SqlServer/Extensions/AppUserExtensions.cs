using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Extensions;

public static class AppUserExtensions
{
    public static async Task<AppUser?> GetByLoginAsync(this IQueryable<AppUser> dbUsers, string login, CancellationToken ct = default)
    {
        login = login.ToUpperInvariant();
        return await dbUsers
            .Where(e => e.NormalizedUserName == login || e.NormalizedEmail == login)
            .FirstOrDefaultAsync(ct);
    }
}