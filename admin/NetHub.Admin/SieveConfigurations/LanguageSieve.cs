using NetHub.Admin.Extensions;
using NetHub.Data.SqlServer.Entities;
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