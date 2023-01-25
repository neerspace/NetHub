using NetHub.Data.SqlServer.Enums;

namespace NetHub.Application.Features.Public.Articles.Localizations;

public sealed class ArticleContributorModel
{
    public ArticleContributorRole Role { get; set; }
    public string UserName { get; set; }
}