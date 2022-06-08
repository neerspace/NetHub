using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.DataSeeding;

public static partial class SeedExtensions
{
	public static void SeedDefaultUser(this ModelBuilder builder)
	{
		builder.Entity<UserProfile>().HasData(Users);
		builder.Entity<IdentityUserClaim<long>>().HasData(UserClaims);
	}

	private static readonly PasswordHasher<UserProfile?> Hasher = new();

	private static readonly UserProfile[] Users =
	{
		new()
		{
			Id = 19,
			UserName = "aspadmin",
			NormalizedUserName = "ASPADMIN",
			FirstName = "vlad",
			MiddleName = "tarasovich",
			LastName = "fit",
			Email = "aspadmin@asp.net",
			NormalizedEmail = "ASPADMIN@ASP.NET",
			EmailConfirmed = true,
			PasswordHash = Hasher.HashPassword(null, "aspX1234"),
			SecurityStamp = Guid.NewGuid().ToString()
		},
	};


	private static readonly IdentityUserClaim<long>[] UserClaims =
	{
		new()
		{
			Id = 1,
			UserId = 19,
			ClaimType = Claims.Permission,
			ClaimValue = Permissions.Master
		}
	};
}