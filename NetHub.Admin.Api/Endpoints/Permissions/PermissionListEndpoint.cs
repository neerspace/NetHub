using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Mapping.Extensions;
using NetHub.Admin.Infrastructure.Models.Permissions;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;

namespace NetHub.Admin.Api.Endpoints.Permissions;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Permissions)]
[Authorize(Policy = Policies.HasReadRolesPermission)]
public sealed class PermissionListEndpoint : ResultEndpoint<PermissionModel[]>
{
    private static PermissionModel[]? s_cached;

    [HttpGet("permissions")]
    public override Task<PermissionModel[]> HandleAsync(CancellationToken ct = default) =>
        Task.FromResult(s_cached ??= GetPermissionsTree());

    private static PermissionModel[] GetPermissionsTree()
    {
        var all = PermissionsMetadata.AllPermissions.AdaptAll<PermissionModel>().ToArray();
        var modifyPermissions = all.Where(p => p.Key.EndsWith('+')).ToArray();
        all = all.Where(p => !p.Key.EndsWith('+')).Select(p =>
                p with { ManageKey = modifyPermissions.FirstOrDefault(k => k.Key == p.Key + '+')?.Key })
            .ToArray();

        // * / mt
        var root = all.Where(p => !p.Key.Contains('.')).ToArray();

        foreach (var rootPermission in root)
        {
            string rootPrefix = rootPermission.Key + '.';

            // mt.usr / mt.rol
            rootPermission.Children = all.Where(p => p.Key.StartsWith(rootPrefix) && p.Key.Count(k => k == '.') == 1).ToArray();

            foreach (var childPermission in rootPermission.Children)
            {
                string childPrefix = rootPrefix + childPermission.Key.Split('.')[1] + '.';

                childPermission.Children = all.Where(p => p.Key.StartsWith(childPrefix) && p.Key.Count(k => k == '.') == 2).ToArray();

                foreach (var subPermission in childPermission.Children)
                {
                    string subPrefix = rootPrefix + string.Join('.', childPermission.Key.Split('.')[1..2]) + '.';

                    // mt.usr.pem
                    subPermission.Children = all.Where(p => p.Key.StartsWith(subPrefix) && p.Key.Count(k => k == '.') == 3).ToArray();

                    if (subPermission.Children.Length == 0)
                        subPermission.Children = null;
                }

                if (childPermission.Children.Length == 0)
                    childPermission.Children = null;
            }

            if (rootPermission.Children.Length == 0)
                rootPermission.Children = null;
        }

        return root;
    }
}