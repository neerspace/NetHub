using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Data.SqlServer.Extensions;
using Sieve.Services;

namespace NetHub.Sieve;

public class ArticleContributorSieve: ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper) {
        mapper.OnlyFilter<ArticleContributor>(ac => ac.Role);
        mapper.OnlyFilter<ArticleContributor>(ac => ac.UserId);
    }
}