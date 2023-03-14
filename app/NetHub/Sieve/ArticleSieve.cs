using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Extensions;
using Sieve.Services;

namespace NetHub.Sieve;

public class ArticleSieve: ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper) {
        mapper.AllowFilterAndSort<Article>(a => a.Id);
        mapper.AllowFilterAndSort<Article>(a => a.ArticleSetId);
        mapper.AllowFilterAndSort<Article>(a => a.LanguageCode);

        mapper.OnlySort<Article>(a => a.Views);
        mapper.OnlySort<Article>(a => a.Created);
        mapper.OnlySort<Article>(a => a.Updated);
        mapper.OnlySort<Article>(a => a.Published);
        mapper.OnlySort<Article>(a => a.Banned);

        mapper.OnlyFilter<Article>(a => a.Status);
        mapper.OnlyFilter<Article>(a => a.Contributors);
    }
}