using NetHub.Application.Models;

namespace NetHub.Admin.Infrastructure.Models.Languages;

public record LanguageFilterRequest : FilterRequest
{
    public override string Sorts { get; set; } = "code";
}