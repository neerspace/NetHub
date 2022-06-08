﻿using NetHub.Application.Features.Public.Articles.Localizations;

namespace NetHub.Application.Features.Public.Articles;

public class ArticleModel
{
	public long Id { get; set; }
	public string Name { get; set; } = default!;
	public long AuthorId { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }
	public ArticleLocalizationModel[]? Localizations { get; set; }
}