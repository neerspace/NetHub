using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Interfaces;

public interface IResourceService
{
	Task<Guid> SaveResourceToDb(IFormFile file);
	Task DeleteResourceFromDb(Guid id);
}