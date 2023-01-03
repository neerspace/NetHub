using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppUser : IdentityUser<long>, IEntity
{
    public override long Id { get; set; }
    public override string UserName { get; set; } = default!;
    public UsernameChange UsernameChanges { get; set; } = new();
    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public override string NormalizedUserName { get; set; } = default!;
    public override string Email { get; set; } = default!;
    public override string NormalizedEmail { get; set; } = default!;
    public override bool EmailConfirmed { get; set; }
    public string? Description { get; set; }
    public string? ProfilePhotoUrl { get; set; }

    public DateTime Registered { get; set; } = DateTime.UtcNow;


    public ICollection<AppUserRole>? UserRoles { get; init; }
    public ICollection<AppUserClaim>? UserClaims { get; init; }
    public ICollection<AppToken>? RefreshTokens { get; init; }
    public ICollection<Article>? Articles { get; set; }
    public ICollection<SavedArticle>? SavedArticles { get; set; }

    #region Photo

    public Guid? PhotoId { get; set; }
    public Resource? Photo { get; set; }

    #endregion
}