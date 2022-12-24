using Mapster;
using Microsoft.EntityFrameworkCore;
using NetHub.Application.Features.Public.Users.Dto;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities.Identity;

namespace NetHub.Application.Features.Public.Search.Users;

internal sealed class SearchUsersHandler : DbHandler<SearchUsersRequest, PrivateUserDto[]>
{
    public SearchUsersHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override Task<PrivateUserDto[]> Handle(SearchUsersRequest request, CancellationToken ct)
    {
        var result = Database.Set<AppUser>()
            .Where(u => u.NormalizedUserName.Contains(request.Username.ToUpper()))
            .ProjectToType<PrivateUserDto>()
            .ToArrayAsync(ct);

        return result;
    }
}