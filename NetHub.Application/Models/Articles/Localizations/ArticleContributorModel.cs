using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Models.Articles.Localizations;

public sealed class ArticleContributorModel
{
    public ArticleContributorRole Role { get; set; }
    public string UserName { get; set; }
}