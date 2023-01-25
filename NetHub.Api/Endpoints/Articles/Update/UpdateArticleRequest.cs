using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Update;

public sealed record UpdateArticleRequest(
    [property: JsonIgnore] long Id,
    string? Name,
    long? AuthorId,
    string? OriginalArticleLink
) : IRequest;