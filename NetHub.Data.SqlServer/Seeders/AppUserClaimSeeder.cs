using NeerCore.Data.EntityFramework.Abstractions;
using NetHub.Core.Constants;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Data.SqlServer.Seeders;

internal class AppUserClaimSeeder : IEntityDataSeeder<AppUserClaim>
{
    public IEnumerable<AppUserClaim> Data => new[]
    {
        new AppUserClaim
        {
            Id = 1,
            UserId = 19,
            ClaimType = Claims.Permissions,
            ClaimValue = Permissions.Master
        }
    };
}