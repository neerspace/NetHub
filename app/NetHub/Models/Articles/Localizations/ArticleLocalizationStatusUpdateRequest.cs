namespace NetHub.Models.Articles.Localizations;

public sealed record ArticleLocalizationStatusUpdateRequest(
    long Id,
    string LanguageCode,
    ArticleStatusActions Status
);

public enum ArticleStatusActions
{
    Publish,
    UnPublish
}