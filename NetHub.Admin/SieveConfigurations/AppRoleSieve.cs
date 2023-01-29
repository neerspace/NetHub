using NetHub.Admin.Extensions;
using NetHub.Data.SqlServer.Entities.Identity;
using Sieve.Services;

namespace NetHub.Admin.SieveConfigurations;

public sealed class AppRoleSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.AllowFilterAndSort<AppRole>(e => e.Id);
        mapper.AllowFilterAndSort<AppRole>(e => e.NormalizedName, nameof(AppRole.Name));
    }
}