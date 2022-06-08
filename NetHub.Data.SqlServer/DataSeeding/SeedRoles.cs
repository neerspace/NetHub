using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.DataSeeding;

public static partial class SeedExtensions
{
	public static void SeedRoles(this ModelBuilder builder)
	{
		builder.Entity<AppRole>().HasData(Roles);
		builder.Entity<IdentityRoleClaim<long>>().HasData(RoleClaims);
	}

	private static readonly AppRole[] Roles =
	{
		new()
		{
			Id = 1,
			Name = "user",
			NormalizedName = "USER",
		},
		new()
		{
			Id = 2,
			Name = "admin",
			NormalizedName = "ADMIN"
		}
	};

	private static readonly IdentityRoleClaim<long>[] RoleClaims =
	{
		new()
		{
			Id = 1,
			RoleId = 2,
			ClaimType = Claims.Permission,
			ClaimValue = Permissions.Admin
		}
	};
}