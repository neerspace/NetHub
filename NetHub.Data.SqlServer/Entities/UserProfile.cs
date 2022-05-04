using Microsoft.AspNetCore.Identity;
using NetHub.Core.Abstractions.Entities;
using NetHub.Data.SqlServer.Entities.ArticleEntities;

namespace NetHub.Data.SqlServer.Entities;

public class UserProfile : IEntity
{
    public long Id { get; set; }
    
    public string UserId { get; set; }
    public string UserName { get; set; } = default!;
    public string NormalizedUserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NormalizedEmail { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }

    public DateTime Registered { get; set; } = DateTime.UtcNow;


    public virtual ICollection<Article>? Articles { get; set; }
    public virtual ICollection<ArticleLocalization>? Localizations { get; set; }
}