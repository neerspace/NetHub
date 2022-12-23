using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace NetHub.Data.SqlServer.Entities.Identity;

public class AppRoleClaim : IdentityRoleClaim<long>, IEntity { }