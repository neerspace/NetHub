using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Data.SqlServer.Entities;

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
        },
        new AppRole
        {
            Id = 2,
            Name = "admin",
            NormalizedName = "ADMIN"
        }
    };
}