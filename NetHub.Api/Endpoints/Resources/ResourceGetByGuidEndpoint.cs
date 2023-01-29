using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Shared.Api;
using NetHub.Shared.Api.Abstractions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Api.Endpoints.Resources;

[Tags(TagNames.Resources)]
[ApiVersion(Versions.V1)]
public class ResourceGetByGuidEndpoint : Endpoint<Guid, FileResult>
{
    private readonly ISqlServerDatabase _database;
    public ResourceGetByGuidEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("resources/{id:guid}")]
    public override async Task<FileResult> HandleAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var resource = await _database.Set<Resource>().FirstOr404Async(r => r.Id == id, ct);
        return File(resource.Bytes, resource.Mimetype);
    }
}