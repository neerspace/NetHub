using Microsoft.AspNetCore.Http;
using NeerCore.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Services;

namespace NetHub.Services;

[Service]
internal sealed class ResourceService : IResourceService
{
    private readonly ISqlServerDatabase _database;

    public ResourceService(ISqlServerDatabase database)
    {
        _database = database;
    }


    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken ct = default)
    {
        if (file is null || file.Length == 0)
            throw new ApiException("File is corrupted");

        var fileEntity = new Resource
        {
            Filename = file.FileName,
            Bytes = await GetFileBytesAsync(file),
            Mimetype = file.ContentType
        };

        var createdEntity = await _database.Set<Resource>().AddAsync(fileEntity, ct);
        await _database.SaveChangesAsync(ct);

        return createdEntity.Entity.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        _database.Set<Resource>().Remove(new() { Id = id });
        await _database.SaveChangesAsync(ct);
    }


    private async Task<byte[]> GetFileBytesAsync(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var fileBytes = stream.ToArray();
        return fileBytes;
    }
}