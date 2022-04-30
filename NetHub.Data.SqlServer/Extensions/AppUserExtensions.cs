using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Extensions;

public static class AppUserExtensions
{
	public static async Task<AppUser?> GetByLoginAsync(this IQueryable<AppUser> queryable, string login, CancellationToken cancel = default)
	{
		login = login.ToUpperInvariant();
		return await queryable
				.Where(e => e.NormalizedUserName == login || e.NormalizedEmail == login)
				.IncludeMany("Claims", "Roles.Role.Claims")
				.FirstOrDefaultAsync(cancel);
	}
}