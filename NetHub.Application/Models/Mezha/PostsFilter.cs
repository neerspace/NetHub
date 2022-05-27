namespace NetHub.Application.Models.Mezha;

public class PostsFilter
{
	public int? Page { get; init; }
	public int? PerPage { get; init; }
	public string? Search { get; init; }
}