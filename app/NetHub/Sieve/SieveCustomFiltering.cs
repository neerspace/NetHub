using NeerCore.DependencyInjection;
using NetHub.Data.SqlServer.Entities.Articles;
using Sieve.Services;

namespace NetHub.Sieve;

[Service(Lifetime = Lifetime.Scoped)]
internal sealed class SieveCustomFiltering : ISieveCustomFilterMethods
{
    public IQueryable<Article> InContributors(IQueryable<Article> source, string op, string[] values)
    {
        var username = values[0];

        var result = source.Where(p =>
            p.Contributors.Any(c => string.Equals(c.User.UserName, username, StringComparison.InvariantCultureIgnoreCase)));
        return result;
    }
}