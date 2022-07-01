using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Update;

public record UpdateArticleRequest([property: JsonIgnore] long Id, string? Name, long? AuthorId,
	string? TranslatedArticleLink) : IRequest;