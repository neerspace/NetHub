using Microsoft.AspNetCore.Http;

namespace NetHub.Shared.Services;

public interface IResourceService
{
    Task<Guid> SaveResourceToDb(IFormFile file);
    Task DeleteResourceFromDb(Guid id);
}