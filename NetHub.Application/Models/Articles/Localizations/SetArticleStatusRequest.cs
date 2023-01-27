using MediatR;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed record SetArticleStatusRequest(
    long Id,
    string LanguageCode,
    ArticleStatusRequest Status
) : IRequest;

public enum ArticleStatusRequest
{
    Publish,
    UnPublish
}