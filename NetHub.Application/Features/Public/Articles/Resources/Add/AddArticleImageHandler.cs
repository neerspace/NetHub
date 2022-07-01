using NetHub.Application.Services;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Resources.Add;

public class AddArticleImageHandler : AuthorizedHandler<AddArticleImageRequest, Guid>
{
	private readonly IResourceService _resourceService;

	public AddArticleImageHandler(IServiceProvider serviceProvider, IResourceService resourceService) : base(
		serviceProvider)
	{
		_resourceService = resourceService;
	}

	protected override async Task<Guid> Handle(AddArticleImageRequest request)
	{
		var userId = UserProvider.GetUserId();

		var article = await Database.Set<Article>().FirstOr404Async();

		if (article.AuthorId != userId)
			throw new PermissionsException();

		var resourceId = await _resourceService.SaveResourceToDb(request.File);

		await Database.Set<ArticleResource>().AddAsync(new()
		{
			ArticleId = request.ArticleId,
			ResourceId = resourceId
		});

		await Database.SaveChangesAsync();

		return resourceId;
	}
}