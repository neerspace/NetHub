using NetHub.Data.SqlServer.Enums;

namespace NetHub.Models.Articles.Localizations;

public sealed class ArticleContributorModel
{
    public ArticleContributorRole Role { get; set; }
    public required string UserName { get; set; }
}