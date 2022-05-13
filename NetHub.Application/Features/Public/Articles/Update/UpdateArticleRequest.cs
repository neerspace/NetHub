using System.Text.Json.Serialization;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.Update;

public record UpdateArticleRequest([property: JsonIgnore] long Id, string? AuthorName, long? AuthorId) : IRequest;