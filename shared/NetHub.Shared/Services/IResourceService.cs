using Microsoft.AspNetCore.Http;

namespace NetHub.Shared.Services;

public interface IResourceService
{
    Task<Guid> UploadAsync(IFormFile file, CancellationToken ct = default);

    Task DeleteAsync(Guid id, CancellationToken ct = default);
}