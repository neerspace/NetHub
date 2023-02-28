using NetHub.Admin.Extensions;
using NetHub.Data.SqlServer.Entities.Articles;
using Sieve.Services;

namespace NetHub.Admin.SieveConfigurations;

public class ArticleSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.AllowFilterAndSort<Article>(a => a.Created);
    }
}