using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Abstractions;
using NetHub.Api.Shared;

namespace NetHub.Admin.Endpoints.Permissions;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Permissions)]
[Authorize(Policy = Policies.HasReadRolesPermission)]
public sealed class PermissionListEndpoint : ResultEndpoint<string[]>
{
    [HttpGet]
    public override Task<string[]> HandleAsync(CancellationToken ct = default) =>
        Task.FromResult(PermissionsMetadata.Values);
}