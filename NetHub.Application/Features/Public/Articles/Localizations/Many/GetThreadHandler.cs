using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;
using NetHub.Application.Interfaces;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Application.Features.Public.Articles.Localizations.Many;

public class GetThreadHandler : DbHandler<GetThreadRequest, ExtendedArticleModel[]>
{
	private readonly IUserProvider _userProvider;

	public GetThreadHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
		_userProvider = serviceProvider.GetRequiredService<IUserProvider>();
	}

	public override async Task<ExtendedArticleModel[]> Handle(GetThreadRequest request,
		CancellationToken cancel)
	{
		var userId = _userProvider.TryGetUserId();

		if (userId is not null)
		{
			return await Database.Set<ExtendedUserArticle>()
				.Where(al => al.LanguageCode == request.LanguageCode && al.UserId == userId)
				.OrderByDescending(al => al.Created)
				.Paginate(request.Page, request.PerPage)
				.ProjectToType<ExtendedArticleModel>()
				.ToArrayAsync(cancel);
		}

		return await Database.Set<ArticleLocalization>().Where(al => al.LanguageCode == request.LanguageCode)
			.Include(al => al.Article)
			.OrderByDescending(al => al.Created)
			.Paginate(request.Page, request.PerPage)
			.ProjectToType<ExtendedArticleModel>()
			.ToArrayAsync(cancel);
	}
}