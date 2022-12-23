using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Seeders;

public class AppRoleClaimSeeder : IEntityDataSeeder<AppRoleClaim>
{
    public IEnumerable<AppRoleClaim> Data => new[]
    {
        new AppRoleClaim
        {
            Id = 1,
            RoleId = 2,
            ClaimType = Claims.Permission,
            ClaimValue = Permissions.Admin
        }
    };
}