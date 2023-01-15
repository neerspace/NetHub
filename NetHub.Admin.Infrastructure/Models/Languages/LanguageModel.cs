using Microsoft.Extensions.Localization;

namespace NetHub.Admin.Infrastructure.Models.Languages;

public sealed class LanguageModel
{
    public required string Code { get; init; }
    public LocalizedString Name { get; init; }
}