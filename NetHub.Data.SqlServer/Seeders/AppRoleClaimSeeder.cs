using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Seeders;

internal class AppRoleClaimSeeder : IEntityDataSeeder<AppRoleClaim>
{
    public IEnumerable<AppRoleClaim> Data => new[]
    {
        new AppRoleClaim
        {
            Id = 3,
            RoleId = 2,
            ClaimType = Claims.Permissions,
            ClaimValue = "*"
        }
    };
}