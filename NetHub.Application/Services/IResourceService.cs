using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Services;

public interface IResourceService
{
	Task<Guid> SaveResourceToDb(IFormFile file);
	Task DeleteResourceFromDb(Guid id);
}