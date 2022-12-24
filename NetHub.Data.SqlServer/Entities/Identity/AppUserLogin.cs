using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

[Table($"{nameof(AppUserLogin)}s")]
public sealed class AppUserLogin : IdentityUserLogin<long>, IEntity { }