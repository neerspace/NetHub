using System.ComponentModel.DataAnnotations;
using MediatR;

namespace NetHub.Application.Features.Public.Articles.GetMany;

public record GetArticlesRequest([Required] string Code, int Page = 1, int PerPage = 20) : IRequest<ArticleModel[]>;