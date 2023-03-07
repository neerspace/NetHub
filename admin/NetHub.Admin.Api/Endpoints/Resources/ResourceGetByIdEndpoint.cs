using Microsoft.AspNetCore.Mvc;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Swagger;

namespace NetHub.Admin.Api.Endpoints.Resources;

[Tags(TagNames.Resources)]
[ApiVersion(Shared.Api.Constants.Versions.V1)]
public class ResourceGetByIdEndpoint : Endpoint<Guid, FileResult>
{
    private readonly ISqlServerDatabase _database;

    public ResourceGetByIdEndpoint(ISqlServerDatabase database) => _database = database;


    [HttpGet("resources/{id:guid}"), ClientSide(ActionName = "getById")]
    public override async Task<FileResult> HandleAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var resource = await _database.Set<Resource>().FirstOr404Async(r => r.Id == id, ct);
        return File(resource.Bytes, resource.Mimetype);
    }
}