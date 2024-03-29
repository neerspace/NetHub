using NetHub.Data.SqlServer.Enums;

namespace NetHub.Shared.Models.Articles;

public sealed class ArticleContributorModel
{
    public ArticleContributorRole Role { get; set; }
    public required string UserName { get; set; }
    public string? ProfilePhotoUrl { get; set; }
}