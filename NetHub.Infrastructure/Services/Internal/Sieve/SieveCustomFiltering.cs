using NeerCore.DependencyInjection;
using NetHub.Data.SqlServer.Entities.Articles;
using Sieve.Services;
using static System.Int32;

namespace NetHub.Infrastructure.Services.Internal.Sieve;

[Service(Lifetime = Lifetime.Scoped)]
internal sealed class SieveCustomFiltering : ISieveCustomFilterMethods
{
    // TODO: why this method in unused? mb remove it?
    public IQueryable<ArticleLocalization> InContributors(IQueryable<ArticleLocalization> source, string op, string[] values)
    {
        var parseResult = TryParse(values[0], out var contributorId);
        if (!parseResult)
            return source;

        var result = source.Where(p => p.Contributors.Any(c => c.UserId == contributorId));
        return result;
    }
}