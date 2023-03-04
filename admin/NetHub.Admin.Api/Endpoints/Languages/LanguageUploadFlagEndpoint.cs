using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Admin.Models.Languages;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Api.Abstractions;
using NetHub.Shared.Api.Constants;
using NetHub.Shared.Api.Swagger;
using NetHub.Shared.Services;

namespace NetHub.Admin.Api.Endpoints.Languages;

[ApiVersion(Versions.V1)]
[Tags(TagNames.Languages)]
[Authorize(Policy = Policies.HasManageResourcesPermission)]
public class LanguageUploadFlagEndpoint : ActionEndpoint<LanguageFlagUploadRequest>
{
    private readonly IResourceService _resourceService;
    private readonly ISqlServerDatabase _database;

    public LanguageUploadFlagEndpoint(IResourceService resourceService, ISqlServerDatabase database)
    {
        _resourceService = resourceService;
        _database = database;
    }


    [HttpPost("languages/{code:alpha:length(2)}/upload-flag"), ClientSide(ActionName = "uploadFlag")]
    [Consumes("multipart/form-data")]
    public override async Task HandleAsync([FromForm] LanguageFlagUploadRequest request, CancellationToken ct)
    {
        var languagesDb = _database.Set<Language>();
        var language = await languagesDb.AsNoTracking()
            .Where(l => l.Code == request.Code)
            .FirstOr404Async(ct);

        var prevFlagId = language.FlagId;

        // Upload new flag
        language.FlagId = await _resourceService.UploadAsync(request.File, ct);
        languagesDb.Entry(language).Property(l => l.FlagId).IsModified = true;

        if (prevFlagId.HasValue)
            await _resourceService.DeleteAsync(prevFlagId.Value, ct);

        await _database.SaveChangesAsync(ct);
    }
}