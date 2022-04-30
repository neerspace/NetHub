using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.DataSeeding;

public static partial class SeedExtensions
{
	public static void SeedDefaultUser(this ModelBuilder builder)
	{
		builder.Entity<AppUser>().HasData(Users);
	}

	private static readonly PasswordHasher<AppUser?> hasher = new();

	private static readonly AppUser[] Users =
	{
		new()
		{
			Id = 1,
			UserName = "aspadmin",
			NormalizedUserName = "ASPADMIN",
			Email = "aspadmin@asp.net",
			NormalizedEmail = "ASPADMIN@ASP.NET",
			EmailConfirmed = true,
			PasswordHash = hasher.HashPassword(null, "aspX1234"),
			SecurityStamp = Guid.NewGuid().ToString()
		},
	};
}