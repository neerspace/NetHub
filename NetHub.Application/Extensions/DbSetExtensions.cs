namespace NetHub.Application.Extensions;

public static class DbSetExtensions
{
	public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int take) where T : class
	{
		query = query
			.Skip((page - 1) * take)
			.Take(take);
		return query;
	}
}