using Microsoft.AspNetCore.Identity;
using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Data.SqlServer.Seeders;

internal class AppUserSeeder : IEntityDataSeeder<User>
{
    public IEnumerable<User> Data => new[]
    {
        new User
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

    private static readonly PasswordHasher<User?> Hasher = new();
}