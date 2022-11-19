using FluentValidation;

namespace NetHub.Application.Models;

public record FilterRequest
{
	public string? Filters { get; set; }
	public string Sorts { get; init; } = "id";
	public int Page { get; init; } = 1;
	public int PageSize { get; init; } = 10;
}

public sealed class FilterRequestValidator : AbstractValidator<FilterRequest>
{
	public FilterRequestValidator()
	{
		RuleFor(o => o.Filters).MaximumLength(300);
		RuleFor(o => o.Sorts).MaximumLength(150);
		RuleFor(o => o.Page).GreaterThan(0).LessThanOrEqualTo(1_000_000);
		RuleFor(o => o.PageSize).GreaterThan(0).LessThanOrEqualTo(1_000);
	}
}

public sealed record Filtered<TModel>(
	int Total,
	IEnumerable<TModel> Data
);