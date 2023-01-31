// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using NeerCore.Data.EntityFramework.Abstractions;
// using NetHub.Data.SqlServer.Entities;
// using NetHub.Data.SqlServer.Entities.Identity;
//
// namespace NetHub.Data.SqlServer.Seeders;
//
// internal class AppUserSeeder : IDataSeeder
// {
// 	private static readonly PasswordHasher<AppUser> Hasher = new();
//
// 	public IEnumerable<AppUser> Data => new[]
// 	{
// 		new AppUser
// 		{
// 			Id = 19,
// 			UserName = "aspadmin",
// 			NormalizedUserName = "ASPADMIN",
// 			FirstName = "vlad",
// 			MiddleName = "tarasovich",
// 			LastName = "fit",
// 			Email = "aspadmin@asp.net",
// 			NormalizedEmail = "ASPADMIN@ASP.NET",
// 			EmailConfirmed = true,
// 			PasswordHash = Hasher.HashPassword(null!, "aspX1234"),
// 			SecurityStamp = "6904F123-FB6C-4886-A401-C69CD895E8D5",
// 		},
// 	};
//
// 	public void Seed(ModelBuilder builder)
// 	{
// 		builder.Entity<AppUser>(b =>
// 		{
// 			b.HasData(new
// 			{
// 				Id = 19L,
// 				UserName = "aspadmin",
// 				NormalizedUserName = "ASPADMIN",
// 				FirstName = "vlad",
// 				MiddleName = "tarasovich",
// 				LastName = "fit",
// 				Email = "aspadmin@asp.net",
// 				NormalizedEmail = "ASPADMIN@ASP.NET",
// 				EmailConfirmed = true,
// 				PasswordHash = Hasher.HashPassword(null!, "aspX1234"),
// 				SecurityStamp = "6904F123-FB6C-4886-A401-C69CD895E8D5",
// 				AccessFailedCount = 0,
// 				UsernameChanges_Count = (byte) 0
// 			});
// 			b.OwnsOne(u => u.UsernameChanges)
// 				.HasData(new
// 				{
// 					AppUserId = 19L,
// 					Count = (byte) 0
// 				});
// 		});
// 	}
// }