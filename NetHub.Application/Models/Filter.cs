namespace NetHub.Application.Models;

public record Filter(
	string Filters,
	string Sorting,
	int Page = 1,
	int Limit = 10
)
{
	public int Skip => (Page - 1) * Limit;
	public int Take => Limit;
}