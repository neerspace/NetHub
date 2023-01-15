using NeerCore.Data;

namespace NetHub.Admin.Infrastructure.Models.Languages;

public sealed class LanguageModel
{
    public required string Code { get; init; }
    public LocalizedString Name { get; init; }
}