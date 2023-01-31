using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public sealed class AppUserLogin : IdentityUserLogin<long>, IEntity { }