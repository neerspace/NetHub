using MediatR;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Enums;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.Update;

public class UpdateArticleHandler : AuthorizedHandler<UpdateArticleRequest>
{
	public UpdateArticleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<Unit> Handle(UpdateArticleRequest request)
	{
		var userId = UserProvider.GetUserId();

		var article = await Database.Set<Article>().FirstOr404Async(a => a.Id == request.Id);

		if (article.AuthorId != userId)
			throw new PermissionsException();

		if (request.AuthorId is not null)
			article.AuthorId =
				await Database.Set<Data.SqlServer.Entities.User>().FirstOrDefaultAsync(p => p.Id == request.AuthorId) is null
					? throw new NotFoundException("No user with such Id")
					: request.AuthorId.Value;

		if (request.Name is not null)
			article.Name = request.Name;

		article.Updated = DateTime.UtcNow;
		article.TranslatedArticleLink = request.TranslatedArticleLink;

		await Database.SaveChangesAsync();

		return Unit.Value;
	}
}