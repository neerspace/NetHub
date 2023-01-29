namespace NetHub.Application.Models.Articles.Localizations;

public sealed record SetArticleStatusRequest(
    long Id,
    string LanguageCode,
    ArticleStatusRequest Status
);

public enum ArticleStatusRequest
{
    Publish,
    UnPublish
}