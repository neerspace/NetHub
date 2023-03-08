using Microsoft.AspNetCore.Mvc;
using NeerCore.Extensions;
using NetHub.Admin.Models.Metadata;
using NetHub.Data.SqlServer.Enums;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Admin.Api.Endpoints.Metadata;

[Tags(TagNames.Metadata)]
[ApiVersion(Shared.Api.Constants.Versions.V1)]
public class GetContentStatusEnumEndpoint : ResultEndpoint<EnumMetadataModel[]>
{
    [HttpGet("metadata/enums/content-statuses"), ClientSide(ActionName = "getContentStatuses")]
    public override Task<EnumMetadataModel[]> HandleAsync(CancellationToken ct)
    {
        var displayNames = Enum.GetValues<ContentStatus>()
            .Select(s => new EnumMetadataModel
            {
                Key = s.ToString(),
                Title = s.GetDisplayName()
            })
            .ToArray();
        return Task.FromResult(displayNames);
    }
}