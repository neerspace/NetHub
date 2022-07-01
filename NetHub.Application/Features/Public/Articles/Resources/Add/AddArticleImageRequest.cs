using MediatR;
using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Features.Public.Articles.Resources.Add;

public record AddArticleImageRequest(IFormFile File, long ArticleId) : IRequest<Guid>;
