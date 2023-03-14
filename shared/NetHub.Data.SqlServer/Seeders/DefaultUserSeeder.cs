using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Seeders;

public sealed class DefaultUserSeeder : IDataSeeder
{
    private static readonly PasswordHasher<AppUser> Hasher = new();

    public void Seed(ModelBuilder builder)
    {
        builder.Entity<AppUser>(b =>
        {
            b.HasData(new
            {
                Id = 1L,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Admin",
                Email = "admin@nethub.com.ua",
                NormalizedEmail = "ADMIN@NETHUB.COM.UA",
                EmailConfirmed = true,
                PasswordHash = Hasher.HashPassword(null!, "Admin1234"),
                SecurityStamp = "745B8028-E31C-4548-A813-941EC4C9D33B",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UsernameChanges_Count = (byte)0
            });
            b.OwnsOne(u => u.UsernameChanges)
                .HasData(new
                {
                    AppUserId = 1L,
                    Count = (byte)0
                });
        });

        builder.Entity<AppUserClaim>(b =>
        {
            b.HasData(new AppUserClaim
            {
                Id = 1,
                UserId = 1L,
                ClaimType = Claims.Permissions,
                ClaimValue = "*"
            });
        });
    }
}