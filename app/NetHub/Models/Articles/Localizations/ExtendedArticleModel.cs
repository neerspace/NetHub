using NetHub.Data.SqlServer.Enums;

namespace NetHub.Models.Articles.Localizations;

public sealed class ExtendedArticleModel
{
    public long? UserId { get; set; }
    public bool? IsSaved { get; set; }
    public DateTimeOffset? SavedDate { get; set; }
    public Vote? Vote { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Html { get; set; } = default!;
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Banned { get; set; }
    public int Views { get; set; } = 0;
    public long ArticleId { get; set; }
    public string LanguageCode { get; set; } = default!;
    public ContentStatus Status { get; set; }
    public long LocalizationId { get; set; }
    public long ContributorId { get; set; }
    public ArticleContributorRole ContributorRole { get; set; }
    public int Rate { get; set; }
}