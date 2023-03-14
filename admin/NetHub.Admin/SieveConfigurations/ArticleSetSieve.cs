using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Extensions;
using Sieve.Services;

namespace NetHub.Admin.SieveConfigurations;

public class ArticleSetSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.AllowFilterAndSort<ArticleSet>(a => a.Id);
        mapper.AllowFilterAndSort<ArticleSet>(a => a.Created);
        mapper.AllowFilterAndSort<ArticleSet>(a => a.Rate);
    }
}