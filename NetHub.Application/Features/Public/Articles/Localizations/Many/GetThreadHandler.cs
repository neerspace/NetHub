using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using NetHub.Application.Extensions;
using NetHub.Application.Features.Public.Articles.Localizations.GetSaving.All;
using NetHub.Application.Interfaces;
using NetHub.Application.Models;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Entities.Views;

namespace NetHub.Application.Features.Public.Articles.Localizations.Many;

public class GetThreadHandler : DbHandler<GetThreadRequest, ExtendedArticleModel[]>
{
	private readonly IUserProvider _userProvider;
	private readonly IFilterService _filterService;

	public GetThreadHandler(IServiceProvider serviceProvider, IFilterService filterService) : base(serviceProvider)
	{
		_filterService = filterService;
		_userProvider = serviceProvider.GetRequiredService<IUserProvider>();
	}

	public override async Task<ExtendedArticleModel[]> Handle(GetThreadRequest request,
		CancellationToken cancel)
	{
		var userId = _userProvider.TryGetUserId();

		var result = userId != null
			? GetExtendedArticles(request, cancel, userId.Value)
			: GetSimpleArticles(request, cancel);

		return await result;
	}

	private async Task<ExtendedArticleModel[]> GetSimpleArticles(FilterRequest request, CancellationToken cancel)
	{
		if (request.Filters != null && request.Filters.Contains("contributorRole"))
			request.Filters = request.Filters.Replace("contributorRole==Author", "");

		if (request.Filters != null && request.Filters.Contains("contributorId"))
			request.Filters = request.Filters.Replace("contributorId", "InContributors");

		var result = await _filterService
			.FilterAsync<ArticleLocalization, ExtendedArticleModel>(request, cancel, al => al.Contributors);

		return result;
	}

	private async Task<ExtendedArticleModel[]> GetExtendedArticles(FilterRequest request, CancellationToken cancel,
		long userId)
	{
		request.Filters += $",userId=={userId}";
		var result =
			await _filterService.FilterAsync<ExtendedUserArticle, ExtendedArticleModel>(request, ct: cancel);
		return result;
	}
}