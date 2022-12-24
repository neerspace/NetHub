using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public class AppUserLogin : IdentityUserLogin<long>, IEntity { }