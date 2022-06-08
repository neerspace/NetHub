﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Articles.GetMany;

public class GetArticlesHandler : DbHandler<GetArticlesRequest, ArticleModel[]>
{
	public GetArticlesHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	public override async Task<ArticleModel[]> Handle(GetArticlesRequest request, CancellationToken cancel)
	{
		await Database.Set<Language>().FirstOr404Async(l => l.Code == request.Code, cancel);

		var articles = await Database.Set<Article>()
			.Include(a => a.Localizations)
			.Where(a =>
				a.Localizations != null &&
				a.Localizations.Count(l => l.LanguageCode == request.Code) == 1)
			.Skip((request.Page - 1) * request.PerPage)
			.Take(request.PerPage)
			.Select(a => new Article
			{
				Id = a.Id,
				Name = a.Name,
				Views = a.Views,
				Created = a.Created,
				Updated = a.Updated,
				Localizations = a.Localizations!.Where(l => l.LanguageCode == request.Code).ToList()
			})
			.ProjectToType<ArticleModel>()
			.ToArrayAsync(cancel);

		return articles;
	}
}