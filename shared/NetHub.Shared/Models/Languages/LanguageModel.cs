using NeerCore.Localization;

namespace NetHub.Shared.Models.Languages;

public sealed class LanguageModel
{
    public required string Code { get; init; }
    public LocalizedString Name { get; init; }
    public string? FlagUrl { get; set; }
}