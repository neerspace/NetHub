using NetHub.Admin.Extensions;
using NetHub.Data.SqlServer.Entities.Articles;
using Sieve.Services;

namespace NetHub.Admin.SieveConfigurations;

public class ArticleSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.AllowFilterAndSort<Article>(a => a.Id);
        mapper.AllowFilterAndSort<Article>(a => a.Name);
        mapper.AllowFilterAndSort<Article>(a => a.Created);
        mapper.AllowFilterAndSort<Article>(a => a.Updated);
        mapper.AllowFilterAndSort<Article>(a => a.Published);
        mapper.AllowFilterAndSort<Article>(a => a.Banned);
        mapper.AllowFilterAndSort<Article>(a => a.Rate);
    }
}