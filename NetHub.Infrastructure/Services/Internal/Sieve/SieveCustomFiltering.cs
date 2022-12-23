using NetHub.Data.SqlServer.Entities.ArticleEntities;
using Sieve.Services;
using static System.Int32;

namespace NetHub.Infrastructure.Services.Internal.Sieve;

public class SieveCustomFiltering : ISieveCustomFilterMethods
{
	public IQueryable<ArticleLocalization> InContributors(IQueryable<ArticleLocalization> source, string op,
		string[] values)
	{
		var parseResult = TryParse(values[0], out var contributorId);
		if (!parseResult)
			return source;

		var result = source.Where(p => p.Contributors.Any(c => c.UserId == contributorId));
		return result;
	}
}