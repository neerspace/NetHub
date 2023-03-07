using NeerCore.Localization;

namespace NetHub.Admin.Models.Languages;

public sealed class LanguageModel
{
    public required string Code { get; init; }
    public LocalizedString Name { get; init; }
    public string? FlagUrl { get; set; }
}