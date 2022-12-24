using MediatR;
using NetHub.Application.Models.Mezha;

namespace NetHub.Application.Features.Public.News;

public sealed class GetMezhaNewsRequest : PostsFilter, IRequest<PostModel[]> { }