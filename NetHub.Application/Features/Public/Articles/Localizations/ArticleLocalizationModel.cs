using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations;

public record ArticleLocalizationModel
{
	public long ArticleId { get; set; }
	public string LanguageCode { get; set; } = default!;
	public ArticleContributorModel[] Contributors { get; set; } = default!;
	public string Title { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Html { get; set; } = default!;
	public ContentStatus Status { get; set; }
	public int Views { get; set; }
	public int Rate { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }
}