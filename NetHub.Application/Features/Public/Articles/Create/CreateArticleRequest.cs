using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NetHub.Application.Features.Public.Articles.Create;

public record CreateArticleRequest(string Name, string[] Tags) : IRequest<ArticleModel>;