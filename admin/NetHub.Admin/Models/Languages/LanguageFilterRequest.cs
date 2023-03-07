using NetHub.Shared.Models;

namespace NetHub.Admin.Models.Languages;

public sealed record LanguageFilterRequest : FilterRequest
{
    public override string? Filters { get; set; }
    public override string Sorts { get; set; } = "code";
}