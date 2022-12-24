using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Seeders;

internal class AppRoleSeeder : IEntityDataSeeder<AppRole>
{
    public IEnumerable<AppRole> Data => new[]
    {
        new AppRole
        {
            Id = 1,
            Name = "user",
            NormalizedName = "USER",
            ConcurrencyStamp = "1F141CDE-0000-1111-2222-3333444417A1"
        },
        new AppRole
        {
            Id = 2,
            Name = "admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = "2F141CDE-0000-1111-2222-33334444ABE2"
        }
    };
}