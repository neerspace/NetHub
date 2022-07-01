using MediatR;

namespace NetHub.Application.Features.Public.Articles.Localizations.Status.Publish;

public record SetArticleStatusRequest(long Id, string LanguageCode, ArticleStatusRequest Status) : IRequest;

public enum ArticleStatusRequest
{
	Publish,
	UnPublish
}