using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.One;

public class GetArticleHandler : DbHandler<GetArticleRequest, (ArticleModel, Guid[]?)>
{
	private readonly IServiceProvider _serviceProvider;

	public GetArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	protected override async Task<(ArticleModel, Guid[]?)> Handle(GetArticleRequest request)
	{
		//TODO: TEST IT!!!!!!!!!!!!!!!
		
		var article = await Database.Set<Article>()
			.Include(a => a.Tags)!.ThenInclude(at => at.Tag)
			.Include(a => a.Images)
			.FirstOr404Async(a => a.Id == request.Id);

		var model = article.Adapt<ArticleModel>();
		var imageIds = article.Images?.Select(i => i.ResourceId).ToArray();

		return (model, imageIds);
	}
}