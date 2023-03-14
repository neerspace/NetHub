using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;
using Sieve.Services;

namespace NetHub.Admin.SieveConfigurations;

public sealed class LanguageSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.AllowFilterAndSort<Language>(e => e.Code);
        mapper.AllowFilterAndSort<Language>(e => e.Name);
    }
}